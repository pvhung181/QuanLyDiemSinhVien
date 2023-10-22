using QuanLyDiemSinhVien.Databases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDiemSinhVien.Repository
{
    public class ChuyenNganhRepository
    {
        ConnectionDatabase db;

        public ChuyenNganhRepository()
        {
            db = new ConnectionDatabase();
        }

        public DataTable getMCLByMaKhoa(string maKhoa)
        {
            string sql = $"select MaCN from KhoaCN where MaKhoa = {maKhoa}";
            DataTable chuyenNganh = db.readData(sql);
            return chuyenNganh;
        }
    }
}
