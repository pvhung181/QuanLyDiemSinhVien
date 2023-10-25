using QuanLyDiemSinhVien.Databases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDiemSinhVien.Repository
{
    public class HeDTRepository
    {
        ConnectionDatabase db;

        public HeDTRepository()
        {
            db = new ConnectionDatabase();
        }

        public DataTable getAllMaHDT()
        {
            string sql = "select MaHeDT from HeDaoTao";
            DataTable dataTable = db.readData(sql);
            return dataTable;
        }
    }
}
