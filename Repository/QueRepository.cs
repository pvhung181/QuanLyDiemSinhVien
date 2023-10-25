using QuanLyDiemSinhVien.Databases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDiemSinhVien.Repository
{
    public class QueRepository
    {
        ConnectionDatabase db;

        public QueRepository()
        {
            db = new ConnectionDatabase();
        }

        public DataTable getMaQue()
        {
            string sql = "select TenQue from Que";
            DataTable dataTable = db.readData(sql);
            return dataTable;
        }
    }
}
