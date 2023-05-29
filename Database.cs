using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using ExcelDataReader;
using SQLite;
using MySql.Data.MySqlClient;
using System.Diagnostics;
 

namespace taClassLibrary
{
     
    public class aiMyData
    {
        [Table("Data_Table")]
        public class Data_Table
        {
            [PrimaryKey, AutoIncrement, Column("id")]
            public int Id { get; set; }

            [Column("F1")]
            public string F1 { get; set; }

            [Column("F2")]
            public string F2 { get; set; }

            [Column("F3")]
            public string F3 { get; set; }

            [Column("F4")]
            public string F4 { get; set; }

            [Column("F5")]
            public string F5 { get; set; }

            [Column("F6")]
            public string F6 { get; set; }

            [Column("F7")]
            public string F7 { get; set; }

            [Column("F8")]
            public string F8 { get; set; }
        }

        // Cài đặt thư viện Dapper
         
        
    }

    public class aiDataBase
    {
        

        [Table("pcb_result")]
        public class PcbResult
        {
            [PrimaryKey, AutoIncrement, Column("id")]
            public int Id { get; set; }

            [Column("sn")]
            public string SN { get; set; }

            [Column("status")]
            public string Status { get; set; }

            [Column("start_time")]
            public string StartTime { get; set; }

            [Column("end_time")]
            public string EndTime { get; set; }

            [Column("pcbCodeTop")]
            public string PcbCodeTop { get; set; }

            [Column("pcbCodeBot")]
            public string PcbCodeBot { get; set; }

            [Column("pcbCodeMylar")]
            public string PcbCodeMylar { get; set; }

            [Column("packBoxStatus")]
            public string PackBoxStatus { get; set; }
        }

        public class PcbResultDAO
        {
            private SQLiteConnection _connection;

            public PcbResultDAO()
            {
                string sPath = "aiData.db";
                //if (!File.Exists(sPath))
                //{
                _connection = new SQLiteConnection(sPath);
                _connection.CreateTable<PcbResult>();
                //}
            } 

            // Thêm bản ghi mới vào bảng pcb_result
            public void AddPcbResult(PcbResult pcbResult)
            {
                _connection.Insert(pcbResult);
            }

            // Lấy danh sách các bản ghi trong bảng pcb_result
            public List<PcbResult> GetAllPcbResults()
            {
                return _connection.Table<PcbResult>().ToList();
            }

            // Lấy thông tin bản ghi theo Id
            public PcbResult GetPcbResultById(int id)
            {
                return _connection.Table<PcbResult>().FirstOrDefault(p => p.Id == id);
            }

            // Sửa thông tin bản ghi
            public void UpdatePcbResult(PcbResult pcbResult)
            {
                _connection.Update(pcbResult);
            }

            // Xoá bản ghi
            public void DeletePcbResult(PcbResult pcbResult)
            {
                _connection.Delete(pcbResult);
            }
        }

        public PcbResult newData = new PcbResult();
        public void fnAdd(PcbResult newPcbResult=null)
        {
            PcbResultDAO dao = new PcbResultDAO();
            if (newPcbResult == null)
            {
                PcbResult newPcbResult1 = new PcbResult
                {
                    SN = "ABC123",
                    Status = "PASS",
                    StartTime = DateTime.Now.ToString(),
                    EndTime = DateTime.Now.ToString(),
                    PcbCodeTop = "ABC123",
                    PcbCodeBot = "DEF456",
                    PcbCodeMylar = "GHI789",
                    PackBoxStatus = "PACKED"
                };
                newPcbResult = newPcbResult1;
            }

            dao.AddPcbResult(newPcbResult);
        }

        public void fnRead()
        {
            PcbResultDAO dao = new PcbResultDAO();

            List<PcbResult> allPcbResults = dao.GetAllPcbResults();

            foreach (PcbResult pcbResult in allPcbResults)
            {
                Console.WriteLine($"Id: {pcbResult.Id}, SN: {pcbResult.SN}, Status: {pcbResult.Status}, Start Time: {pcbResult.StartTime}, End Time: {pcbResult.EndTime}, PCB Code Top: {pcbResult.PcbCodeTop}, PCB Code Bot: {pcbResult.PcbCodeBot}, PCB Code Mylar: {pcbResult.PcbCodeMylar}, Pack Box Status: {pcbResult.PackBoxStatus}");
            }

        }

    
        // OK nhưng chưa dùng:
        //public class DBinfo
        //{
        //    [PrimaryKey, AutoIncrement]
        //    public int id { get; set; }
        //    public string Col1 { get; set; }
        //    public string Col2 { get; set; }
        //    public DBinfo() { }

        //    public DBinfo(string C1, string C2)
        //    {
        //        Col1 = C1;
        //        Col2 = C2;
        //    }
        //}
        //public class aiDataBase
        //{   
        //    public string strDBPath="";
        //    public void fnCreateDB_ifnotExists(string spath = "./aiData.bd3")
        //    {
        //        strDBPath = spath;
        //        if (!File.Exists(spath))
        //        {
        //            var db = new SQLiteConnection(spath);
        //            db.CreateTable<DBinfo>();
        //            db.Close();
        //        }
        //    }
        //    public void fnInsert()
        //    {
        //        Random rnd = new Random();            
        //        DBinfo temp = new DBinfo($"TACT: {rnd.Next(1, 13)}", $"{rnd.Next(33, 1000)} Không có gì đâu nhá 2");
        //        var db = new SQLiteConnection(strDBPath);
        //        db.Insert(temp);
        //        db.Close();
        //    }
        //}

    }
    public class MySQLAccess
    {
        /* Cài đặt: dùng Nuget cài: MySql.Data
         * 
         * Cách dùng:
         * > cách sử dụng class này như thế nào?

        1 / 1

        Để sử dụng class `MySQLAccess` để truy cập và đọc dữ liệu từ MySQL trong ứng dụng C#, bạn cần thực hiện các bước sau:

        1.  Tạo một đối tượng `MySQLAccess` mới:

        java

        ```java
        MySQLAccess dbAccess = new MySQLAccess();
        ```

        2.  Gọi hàm `Select()` để lấy dữ liệu từ MySQL:

        java

        ```java
        DataTable dataTable = dbAccess.Select();
        ```

        3.  Sử dụng đối tượng `dataTable` để xử lý và hiển thị dữ liệu trong ứng dụng của bạn.

        Ví dụ, nếu bạn muốn hiển thị dữ liệu trong một `DataGridView`, bạn có thể sử dụng mã sau:

        `dataGridView.DataSource = dataTable;`

        Nếu bạn muốn truy cập các giá trị trong `dataTable`, bạn có thể sử dụng vòng lặp để lấy từng dòng và từng cột trong `dataTable`, ví dụ:

        scss

        ```scss
        foreach (DataRow row in dataTable.Rows)
        {
            string id = row["id"].ToString();
            string sn = row["sn"].ToString();
            string status = row["status"].ToString();
            // Lấy các giá trị khác tương tự
            // ...
        }
        ```

        Chú ý rằng, khi sử dụng đối tượng `MySQLAccess`, bạn cần chắc chắn rằng đã thêm thư viện `MySql.Data.MySqlClient` vào dự án C# của mình. Bạn có thể thêm thư viện này bằng cách vào phần References trong Solution Explorer, chọn Add Reference và chọn MySql.Data trong danh sách các thư viện có sẵn.

        Các câu lệnh SQL liên quan:
        Tạo bảng:
        CREATE TABLE pcb_result (
            id INT PRIMARY KEY,
            sn VARCHAR(50) NOT NULL,
            status VARCHAR(20) NOT NULL,
            start_time DATETIME NOT NULL,
            end_time DATETIME,
            pcbCodeTop VARCHAR(50),
            pcbCodeBot VARCHAR(50),
            pcbCodeMylar VARCHAR(50),
            packBoxStatus VARCHAR(20)
        );

        Thêm Dữ liệu:
        INSERT INTO final2.pcb_result 
        (id,   sn,                status, start_time,    end_time,       pcbCodeTop,    pcbCodeBot,        pcbCodeMylar,  packBoxStatus) 
        VALUES 
        (10685,'N11002314000SKFG','PASS','2023-4-4 9:49','2023-4-4 9:49','1423F284CD4E,N11002314000SKFG,REV006DEV000,BCM957504-N1100GD06','','1423F284CD4E 1423F284CD5E,N11002314000SKFG,REV006DEV000,BCM957504-N1100GD06',NULL),
        (10686,'N11002314000R3FG','PASS','2023-4-4 9:49','2023-4-4 9:49','1423F284E776,N11002314000R3FG,REV006DEV000,BCM957504-N1100GD06','','1423F284E776 1423F284E786,N11002314000R3FG,REV006DEV000,BCM957504-N1100GD06',NULL),
        (10687,'N11002314000R5FG','PASS','2023-4-4 9:49','2023-4-4 9:49','1423F2856E9A,N11002314000R5FG,REV006DEV000,BCM957504-N1100GD06','','1423F2856E9A 1423F2856EAA,N11002314000R5FG,REV006DEV000,BCM957504-N1100GD06',NULL),
        (10688,'N11002314001R2FG','PASS','2023-4-4 9:50','2023-4-4 9:50','1423F285C864,N11002314001R2FG,REV006DEV000,BCM957504-N1100GD06','','1423F285C864 1423F285C874,N11002314001R2FG,REV006DEV000,BCM957504-N1100GD06',NULL),
        (10689,'N11002314000RNFG','PASS','2023-4-4 9:50','2023-4-4 9:50','1423F2859414,N11002314000RNFG,REV006DEV000,BCM957504-N1100GD06','','1423F2859414 1423F2859424,N11002314000RNFG,REV006DEV000,BCM957504-N1100GD06',NULL),
        (10690,'N11002314002ZXFG','PASS','2023-4-4 9:51','2023-4-4 9:51','1423F285DC02,N11002314002ZXFG,REV006DEV000,BCM957504-N1100GD06','','1423F285DC02 1423F285DC12,N11002314002ZXFG,REV006DEV000,BCM957504-N1100GD06',NULL),
        (10691,'N11002314000R9FG','PASS','2023-4-4 9:51','2023-4-4 9:51','1423F2851A04,N11002314000R9FG,REV006DEV000,BCM957504-N1100GD06','','1423F2851A04 1423F2851A14,N11002314000R9FG,REV006DEV000,BCM957504-N1100GD06',NULL),
        (10692,'N110023140013GFG','PASS','2023-4-4 9:51','2023-4-4 9:51','1423F28544FA,N110023140013GFG,REV006DEV000,BCM957504-N1100GD06','','1423F28544FA 1423F285450A,N110023140013GFG,REV006DEV000,BCM957504-N1100GD06',NULL),
        (10693,'N11002314000S7FG','PASS','2023-4-4 9:51','2023-4-4 9:51','1423F2852FF4,N11002314000S7FG,REV006DEV000,BCM957504-N1100GD06','','1423F2852FF4 1423F2853004,N11002314000S7FG,REV006DEV000,BCM957504-N1100GD06',NULL),
        (10694,'N110023140014XFG','PASS','2023-4-4 9:52','2023-4-4 9:52','1423F2855C76,N110023140014XFG,REV006DEV000,BCM957504-N1100GD06','','1423F2855C76 1423F2855C86,N110023140014XFG,REV006DEV000,BCM957504-N1100GD06',NULL),
        (10695,'N11002314000RPFG','PASS','2023-4-4 9:52','2023-4-4 9:52','1423F284E0EC,N11002314000RPFG,REV006DEV000,BCM957504-N1100GD06','','1423F284E0EC 1423F284E0FC,N11002314000RPFG,REV006DEV000,BCM957504-N1100GD06',NULL),
        (10696,'N11002314000RQFG','PASS','2023-4-4 9:53','2023-4-4 9:53','1423F2850B9A,N11002314000RQFG,REV006DEV000,BCM957504-N1100GD06','','1423F2850B9A 1423F2850BAA,N11002314000RQFG,REV006DEV000,BCM957504-N1100GD06',NULL),
        (10697,'N11002314000RFFG','PASS','2023-4-4 9:53','2023-4-4 9:53','1423F2849370,N11002314000RFFG,REV006DEV000,BCM957504-N1100GD06','','1423F2849370 1423F2849380,N11002314000RFFG,REV006DEV000,BCM957504-N1100GD06',NULL),
        (10698,'N110023140016FFG','PASS','2023-4-4 9:53','2023-4-4 9:53','1423F2855F7C,N110023140016FFG,REV006DEV000,BCM957504-N1100GD06','','1423F2855F7C 1423F2855F8C,N110023140016FFG,REV006DEV000,BCM957504-N1100GD06',NULL),
        (10699,'N11002314000SAFG','PASS','2023-4-4 9:53','2023-4-4 9:53','1423F2851A16,N11002314000SAFG,REV006DEV000,BCM957504-N1100GD06','','1423F2851A16 1423F2851A26,N11002314000SAFG,REV006DEV000,BCM957504-N1100GD06',NULL),
        (10700,'N11002314000QBFG','PASS','2023-4-4 9:54','2023-4-4 9:54','1423F28527F0,N11002314000QBFG,REV006DEV000,BCM957504-N1100GD06','','1423F28527F0 1423F2852800,N11002314000QBFG,REV006DEV000,BCM957504-N1100GD06',NULL),
        (10701,'N11002314000VMFG','PASS','2023-4-4 9:54','2023-4-4 9:54','1423F284DF4E,N11002314000VMFG,REV006DEV000,BCM957504-N1100GD06','','1423F284DF4E 1423F284DF5E,N11002314000VMFG,REV006DEV000,BCM957504-N1100GD06',NULL),
        (10702,'N11002314000PVFG','PASS','2023-4-4 9:55','2023-4-4 9:55','1423F284E79A,N11002314000PVFG,REV006DEV000,BCM957504-N1100GD06','','1423F284E79A 1423F284E7AA,N11002314000PVFG,REV006DEV000,BCM957504-N1100GD06',NULL),
        (10703,'N11002314000PSFG','PASS','2023-4-4 9:55','2023-4-4 9:55','1423F284E33E,N11002314000PSFG,REV006DEV000,BCM957504-N1100GD06','','1423F284E33E 1423F284E34E,N11002314000PSFG,REV006DEV000,BCM957504-N1100GD06',NULL),
        (10704,'N11002314000P1FG','PASS','2023-4-4 9:55','2023-4-4 9:55','1423F283A55C,N11002314000P1FG,REV006DEV000,BCM957504-N1100GD06','','1423F283A55C 1423F283A56C,N11002314000P1FG,REV006DEV000,BCM957504-N1100GD06',NULL),
        (10705,'N11002314000P6FG','PASS','2023-4-4 9:55','2023-4-4 9:55','1423F285676E,N11002314000P6FG,REV006DEV000,BCM957504-N1100GD06','','1423F285676E 1423F285677E,N11002314000P6FG,REV006DEV000,BCM957504-N1100GD06',NULL),
        (10706,'N11002314000Q6FG','PASS','2023-4-4 9:56','2023-4-4 9:57','1423F284760C,N11002314000Q6FG,REV006DEV000,BCM957504-N1100GD06','','1423F284760C 1423F284761C,N11002314000Q6FG,REV006DEV000,BCM957504-N1100GD06',NULL),
        (10707,'N11002314000SCFG','FAIL','2023-4-4 9:56','2023-4-4 9:56','1423F2852D7E,N11002314000SCFG,NOT FOUND,BCM957504-N1100GD06','','1423F2852D7E 1423F2852D8E,N11002314000SCFG,REV006DEV000,BCM957504-N1100GD06',NULL),
        (10708,'N11002314000Q2FG','PASS','2023-4-4 9:57','2023-4-4 9:57','1423F283A66A,N11002314000Q2FG,REV006DEV000,BCM957504-N1100GD06','','1423F283A66A 1423F283A67A,N11002314000Q2FG,REV006DEV000,BCM957504-N1100GD06',NULL),
        (10709,'N11002314000PQFG','PASS','2023-4-4 9:57','2023-4-4 9:57','1423F284DFBA,N11002314000PQFG,REV006DEV000,BCM957504-N1100GD06','','1423F284DFBA 1423F284DFCA,N11002314000PQFG,REV006DEV000,BCM957504-N1100GD06',NULL),
        (10710,'N11002314000PRFG','FAIL','2023-4-4 9:58','2023-4-4 9:58','1423F284DF84,N11002314000PRFG,REV006DEV000,BCM957504-N1100GD06','','1423F284DF84 1423F284DF94,N11002314000PRFG,REV006DEV000,BCM957504-N1100GD06',NULL),
        (10711,'N11002314000QNFG','PASS','2023-4-4 9:58','2023-4-4 9:58','1423F2855928,N11002314000QNFG,REV006DEV000,BCM957504-N1100GD06','','1423F2855928 1423F2855938,N11002314000QNFG,REV006DEV000,BCM957504-N1100GD06',NULL);



        INSERT INTO `pcb_result` (`id`, `sn`, `status`, `start_time`, `end_time`, `pcbCodeTop`, `pcbCodeBot`, `pcbCodeMylar`, `packBoxStatus`) VALUES 
        ('1', 'a123123asd', 'fail', current_timestamp(), '2023-04-05 09:43:09', NULL, NULL, NULL, NULL);

         * */
        private MySqlConnection connection;
        private string server;
        private int port;
        private string database;
        private string uid;
        private string password;

        // Constructor
        public MySQLAccess()
        {
            Initialize();
        }

        // Initialize values
        private void Initialize()
        {
            //server = "10.220.97.73";
            server = "127.0.0.1";
            port = 3306;
            database = "final2";
            uid = "packbox";
            password = "Foxconn168!!";
            uid = "root";
            string connectionString;
            //https://www.connectionstrings.com/mysql/
            connectionString = $"Server={server};Port={port};Database={database};UID={uid};"; // ;Uid={};Pwd={};";
            connection = new MySqlConnection(connectionString);
            Debug.WriteLine("Đã mở DB");
        }

        // Open Connection to database
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                // Handle exception
                return false;
            }
        }

        // Close connection to database
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                // Handle exception
                return false;
            }
        }

        // Select statement
        public DataTable Select(string QueryVal = "", string GetCol = "pcbCodeMylar", string QueryColl = "macMylar")
        {
            //string query = "SELECT id,sn,status,start_time,end_time,pcbCodeTop,pcbCodeBot,pcbCodeMylar,packBoxStatus FROM final2.pcb_result;";
            string query = $"SELECT `{GetCol}` FROM final2.pcb_result where `{QueryColl}`=\"{QueryVal}\";";
            Debug.WriteLine(query);
            // Create a DataTable to hold the results
            DataTable dataTable = new DataTable();

            // Open connection
            if (this.OpenConnection() == true)
            {
                // Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);

                // Create a data adapter to fill the DataTable
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd);

                // Fill the DataTable
                dataAdapter.Fill(dataTable);

                // Close Data Adapter
                dataAdapter.Dispose();

                // Close Connection
                this.CloseConnection();
            }

            // Return the DataTable
            return dataTable;
        }
        
    }

}