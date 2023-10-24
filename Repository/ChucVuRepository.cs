using QuanLyDiemSinhVien.Databases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDiemSinhVien.Repository
{
    public class ChucVuRepository
    {
        ConnectionDatabase db;

        public ChucVuRepository()
        {
            db = new ConnectionDatabase();
        }

        public DataTable getAllMCV()
        {
            string sql = "select MaChucVu from ChucVu";
            DataTable dataTable = db.readData(sql);
            return dataTable;
        }
    }
}
