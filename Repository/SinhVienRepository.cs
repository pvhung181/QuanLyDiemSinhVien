using QuanLyDiemSinhVien.Databases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDiemSinhVien.Repository
{
    public class SinhVienRepository
    {
        ConnectionDatabase db;
        

        public SinhVienRepository()
        {
            db = new ConnectionDatabase();
        }

        public DataTable getAllStudents()
        {
            string sql = "select * from SinhVien";
            DataTable students = db.
        }
    }
}
