using QuanLyDiemSinhVien.Databases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDiemSinhVien.Repository
{
    public class KhoaRepository
    {
        ConnectionDatabase db;

        public KhoaRepository()
        {
            db = new ConnectionDatabase();
        }

        public DataTable getAllMaKhoa()
        {
            string sql = "select MaKhoa from Khoa";
            DataTable dataTable = db.readData(sql);
            return dataTable;
        }
    }
}
