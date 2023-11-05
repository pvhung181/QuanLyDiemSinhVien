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
        private readonly string SELECT_STATEMENT = $"select sv.MaSv [Mã sinh viên], TenSv [Họ và tên], sv.MaLop [Mã lớp], " +
                $" TenMon [Tên Môn], HocKy [Học Kỳ], LanThi [Lần thi], Diem [Điểm] " +
                $" from SinhVien sv join Diem d on sv.MaSv = d.MaSv " +
                $" join MonHoc mh on mh.MaMon = d.MaMon ";

        public DiemRepository()
        {
            db = new ConnectionDatabase();
        }

        public DataTable getDiemByLop(string lop, string mon, string hk, int lan)
        {
            string sql = SELECT_STATEMENT + 
                         $" where sv.MaLop = '{lop}' and HocKy = {hk} and d.MaMon = '{mon}' and lanthi = {lan}";
            DataTable dt = db.readData(sql);
            return dt;
        }

        public DataTable getDiemById(string id)
        {
            string sql = SELECT_STATEMENT + $" where d.Masv = '{id}'";
            DataTable dataTable = db.readData(sql);
            return dataTable;
        }

        public DataTable getDiemByMLMHLT(string malop, string monhoc, string lan)
        {
            string sql = $"select sv.MaSv [Mã sinh viên], TenSv [Họ và tên], HocKy [Học Kỳ], Diem [Điểm] " +
                $" from SinhVien sv join Diem d on sv.MaSv = d.MaSv " +
                $" where d.Malop = '{malop}' and MaMon = '{monhoc}' and LanThi = {lan}";
            DataTable dt = db.readData(sql);
            return dt;
        }

        public bool updateDiem(string sql)
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
