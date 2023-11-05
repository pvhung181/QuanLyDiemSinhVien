using QuanLyDiemSinhVien.Databases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyDiemSinhVien.Repository
{
    public class LopRepository 
    {
        ConnectionDatabase db;

        public LopRepository()
        {
            db = new ConnectionDatabase();
        }

        public DataTable getAllMaLop()
        {
            string sql = "select MaLop from Lop";
            DataTable dataTable = db.readData(sql);
            return dataTable;
        }

        public DataTable getAllMaLopByMaKhoa(string maKhoa)
        {
            string sql = $"select MaLop from Lop where MaKhoa = '{maKhoa}'";
            DataTable maLops = db.readData(sql);
            return maLops;
        }

        public string getTenLopByMaLop(string malop)
        {
            string sql = $"select TenLop from lop where malop = '{malop}'";
            DataTable dt = db.readData(sql);
            if(dt.Rows.Count == 0)
            {
                MessageBox.Show("Khong ton tai ma lop", "thong bao");
                return null;
            }
            return dt.Rows[0][0].ToString();
        }
    }
}
