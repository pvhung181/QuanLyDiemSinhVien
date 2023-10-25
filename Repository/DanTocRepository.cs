using QuanLyDiemSinhVien.Databases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDiemSinhVien.Repository
{
    internal class DanTocRepository
    {
        ConnectionDatabase db;

        public DanTocRepository()
        {
            db = new ConnectionDatabase();
        }

        public DataTable getMaDanToc()
        {
            string sql = "select DanToc from DanToc";
            DataTable dataTable = db.readData(sql);
            return dataTable;
        }

        public int getMaDTByDT(string dt)
        {
            string sql = $"select MaDanToc from DanToc where DanToc = N'{dt}'";
            DataTable dataTable = db.readData(sql);
            return Convert.ToInt32(dataTable.Rows[0][0].ToString());
        }
    }
}
