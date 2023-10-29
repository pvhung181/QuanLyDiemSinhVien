using QuanLyDiemSinhVien.Databases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDiemSinhVien.Repository
{
    public class DiemRepository
    {
        ConnectionDatabase db;

        public DiemRepository()
        {
            db = new ConnectionDatabase();
        }

        public DataTable getDiemByLop(string lop, string mon, string hk, int lan)
        {
            string sql = $"select sv.MaSv [Mã sinh viên], TenSv [Họ và tên], sv.MaLop [Mã lớp]," +
                $" TenMon [Tên Môn], HocKy [Học Kỳ], LanThi [Lần thi], Diem [Điểm]" +
                $" from SinhVien sv join Diem d on sv.MaSv = d.MaSv" +
                $" join MonHoc mh on mh.MaMon = d.MaMon" +
                $" where sv.MaLop = '{lop}' and HocKy = {hk} and d.MaMon = '{mon}' and lanthi = {lan}";
            DataTable dt = db.readData(sql);
            return dt;
        }

        public bool updateMultiDiem(string sql)
        {
            try
            {
                db.persistData(sql);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
