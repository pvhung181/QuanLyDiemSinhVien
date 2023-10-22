using QuanLyDiemSinhVien.Databases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDiemSinhVien.Repository
{
    public class LopRepository
    {
        ConnectionDatabase db;

        public LopRepository()
        {
            db = new ConnectionDatabase();
        }

        public DataTable getAllMaLopByMaKhoa(string maKhoa)
        {
            string sql = $"select MaLop from Lop where MaKhoa = {maKhoa}";
            DataTable maLops = db.readData(sql);
            return maLops;
        }
    }
}
