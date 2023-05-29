## HD code
khai báo 1 class C# để thao tác với SQLite, các thao tác bao gồm: tạo DB nếu chưa tồn tại, tạo table nếu chưa tồn tại, các hàm thêm, sửa, xoá, đọc tất cả table, đọc 1 row theo diều kiện

Dưới đây là một ví dụ về cách khai báo một lớp trong C# để thao tác với SQLite. Lớp này sẽ có các chức năng để tạo cơ sở dữ liệu (nếu chưa tồn tại), tạo bảng (nếu chưa tồn tại), thêm, sửa, xoá dữ liệu và đọc tất cả các hàng từ bảng, cũng như đọc một hàng dựa trên một điều kiện. Để sử dụng SQLite trong C#, bạn cần cài đặt gói NuGet "System.Data.SQLite".

```csharp
using System;
using System.Data;
using System.Data.SQLite;

public class SQLiteManager
{
    private SQLiteConnection connection;

    public SQLiteManager(string databasePath)
    {
        connection = new SQLiteConnection($"Data Source={databasePath}");
    }

    public void CreateDatabase()
    {
        connection.Open();
        connection.Close();
    }

    public void CreateTable(string tableName, string[] columnNames, string[] columnTypes)
    {
        if (columnNames.Length != columnTypes.Length)
        {
            throw new ArgumentException("Column names and types must have the same length.");
        }

        connection.Open();

        using (SQLiteCommand command = connection.CreateCommand())
        {
            string query = $"CREATE TABLE IF NOT EXISTS {tableName} (";

            for (int i = 0; i < columnNames.Length; i++)
            {
                query += $"{columnNames[i]} {columnTypes[i]}";

                if (i < columnNames.Length - 1)
                {
                    query += ", ";
                }
            }

            query += ")";

            command.CommandText = query;
            command.ExecuteNonQuery();
        }

        connection.Close();
    }

    public void InsertRow(string tableName, string[] columnNames, object[] values)
    {
        if (columnNames.Length != values.Length)
        {
            throw new ArgumentException("Column names and values must have the same length.");
        }

        connection.Open();

        using (SQLiteCommand command = connection.CreateCommand())
        {
            string query = $"INSERT INTO {tableName} (";

            for (int i = 0; i < columnNames.Length; i++)
            {
                query += columnNames[i];

                if (i < columnNames.Length - 1)
                {
                    query += ", ";
                }
            }

            query += ") VALUES (";

            for (int i = 0; i < values.Length; i++)
            {
                query += $"@value{i}";

                if (i < values.Length - 1)
                {
                    query += ", ";
                }

                command.Parameters.AddWithValue($"@value{i}", values[i]);
            }

            query += ")";

            command.CommandText = query;
            command.ExecuteNonQuery();
        }

        connection.Close();
    }

    public void UpdateRow(string tableName, string[] columnNames, object[] values, string condition)
    {
        if (columnNames.Length != values.Length)
        {
            throw new ArgumentException("Column names and values must have the same length.");
        }

        connection.Open();

        using (SQLiteCommand command = connection.CreateCommand())
        {
            string query = $"UPDATE {tableName} SET ";

            for (int i = 0; i < columnNames.Length; i++)
            {
                query += $"{columnNames[i]} = @value{i}";

                if (i < columnNames.Length - 1)
                {
                    query += ", ";
                }

                command.Parameters.AddWithValue($"@value{i}", values[i]);
            }

            query += $" WHERE {condition}";

            command.CommandText =

 query;
            command.ExecuteNonQuery();
        }

        connection.Close();
    }

    public void DeleteRow(string tableName, string condition)
    {
        connection.Open();

        using (SQLiteCommand command = connection.CreateCommand())
        {
            string query = $"DELETE FROM {tableName} WHERE {condition}";

            command.CommandText = query;
            command.ExecuteNonQuery();
        }

        connection.Close();
    }

    public DataTable ReadAllRows(string tableName)
    {
        connection.Open();

        using (SQLiteCommand command = connection.CreateCommand())
        {
            string query = $"SELECT * FROM {tableName}";

            command.CommandText = query;

            SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            connection.Close();

            return dataTable;
        }
    }

    public DataTable ReadRowsByCondition(string tableName, string condition)
    {
        connection.Open();

        using (SQLiteCommand command = connection.CreateCommand())
        {
            string query = $"SELECT * FROM {tableName} WHERE {condition}";

            command.CommandText = query;

            SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            connection.Close();

            return dataTable;
        }
    }
}
```

Để sử dụng lớp `SQLiteManager`, bạn có thể thực hiện các bước sau:

1. Tạo một đối tượng `SQLiteManager` và chỉ định đường dẫn tới cơ sở dữ liệu SQLite:
```csharp
SQLiteManager sqliteManager = new SQLiteManager("path/to/database.db");
```

2. Tạo cơ sở dữ liệu (nếu chưa tồn tại):
```csharp
sqliteManager.CreateDatabase();
```

3. Tạo bảng (nếu chưa tồn tại) bằng cách chỉ định tên bảng, tên cột và kiểu dữ liệu tương ứng:
```csharp
string[] columnNames = { "ID", "Name", "Age" };
string[] columnTypes = { "INTEGER PRIMARY KEY", "TEXT", "INTEGER" };
sqliteManager.CreateTable("TableName", columnNames, columnTypes);
```

4. Thực hiện các thao tác thêm, sửa, xoá và đọc dữ liệu:

- Thêm một hàng mới:
```csharp
string[] columnNames = { "Name", "Age" };
object[] values = { "John Doe", 25 };
sqliteManager.InsertRow("TableName", columnNames, values);
```

- Sửa một hàng dựa trên một điều kiện:
```csharp
string[] columnNames = { "Name", "Age" };
object[] values = { "Jane Smith", 30 };
string condition = "ID = 1";
sqliteManager.UpdateRow("TableName", columnNames, values, condition);
```

- Xoá một hàng dựa trên một điều kiện:
```csharp
string condition = "Age < 20";
sqliteManager.DeleteRow("TableName", condition);
```

- Đọc tất cả các hàng từ bảng:
```csharp
DataTable allRows = sqliteManager.ReadAllRows("TableName");
foreach (DataRow row in allRows.Rows)
{
    // Xử lý từng hàng dữ liệu
}
```

- Đọc một hàng dựa trên một điều kiện:
```csharp
string condition = "Name = 'John Doe'";
DataTable rowsByCondition = sqliteManager.ReadRowsByCondition("TableName", condition);
foreach (DataRow row in rows

ByCondition.Rows)
{
    // Xử lý từng hàng dữ liệu
}
```

Lưu ý rằng ví dụ trên chỉ là một ví dụ đơn giản để giúp bạn khai báo một lớp để thao tác với SQLite trong C#. Tùy thuộc vào yêu cầu cụ thể của bạn, bạn có thể mở rộng chức năng của lớp này và tối ưu hóa mã để phù hợp với nhu cầu của mình.
