using QuanLyDiemSinhVien.Databases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDiemSinhVien.Repository
{
    public class TKBRepository
    {
        ConnectionDatabase db;

        public TKBRepository()
        {
            db = new ConnectionDatabase();
        }

        public DataTable getTenMonHoc(string sql)
        {
            DataTable dataTable = db.readData(sql);
            return dataTable;
        }
    }
}
