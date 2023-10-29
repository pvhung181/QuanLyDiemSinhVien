using QuanLyDiemSinhVien.Databases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDiemSinhVien.Repository
{
    public class MonHocRepository
    {
        ConnectionDatabase db;

        public MonHocRepository()
        {
            db = new ConnectionDatabase();
        }

        public DataTable getMonHoc()
        {
            string sql = "select TenMon from MonHoc";
            DataTable dataTable = db.readData(sql);
            return dataTable;
        }

        public string getMaMonByTen(string ten)
        {
            string sql = $"select MaMon from MonHoc where TenMon = N'{ten}'";
            DataTable dataTable = db.readData(sql);
            return dataTable.Rows[0][0].ToString();
        }
    }
}
