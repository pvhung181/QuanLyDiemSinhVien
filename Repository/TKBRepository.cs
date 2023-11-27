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

        public DataTable getTenMonHoc(string malop, string hk)
        {
            string sql = $"select TenMon from MonHoc mh" +
                        $" inner join TKB on mh.MaMon = tkb.MaMon" +
                        $" where MaLop = '{malop}' and HocKy = {hk}";
            DataTable dataTable = db.readData(sql);
            return dataTable;
        }

        public DataTable getTenMonHoc(string malop)
        {
            string sql = $"select TenMon from MonHoc mh" +
                        $" inner join TKB on mh.MaMon = tkb.MaMon" +
                        $" where MaLop = '{malop}'";
            DataTable dataTable = db.readData(sql);
            return dataTable;
        }

        public List<string> getHKsByMaLop(string maLop)
        {
            List<string> list = new List<string>();
            string sql = $"select HocKy from TKB where malop = '{maLop}' group by hocky";
            DataTable dt = db.readData(sql);
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(dr[0].ToString());
            }
            return list;
        }

        public List<string> getLopByHK(string hk)
        {
            List<string> list = new List<string>();
            string sql = $"select MaLop from TKB where hocky = '{hk}' group by malop";
            DataTable dt = db.readData(sql);
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(dr[0].ToString());
            }
            return list;
        }

        public DataTable getTKBByMaLop(string malop)
        {
            string sql = $" select tkb.MaMon, TenMon, HocKy, ThuHoc, CaHoc, TenPhong" +
                        $" from tkb join MonHoc mh on tkb.MaMon = mh.MaMon " +
                        $" join PhongHoc ph on tkb.MaPhong = ph.MaPhong" +
                        $" where Malop = '{malop}'";
            DataTable dt = db.readData(sql);
            return dt;
        }

		public DataTable getTKBByHK(string hocky)
		{
			string sql = $" select tkb.MaLop, tkb.MaMon, TenMon, HocKy, ThuHoc, CaHoc, TenPhong" +
						$" from tkb join MonHoc mh on tkb.MaMon = mh.MaMon " +
						$" join PhongHoc ph on tkb.MaPhong = ph.MaPhong" +
						$" where hocky = '{hocky}'";
			DataTable dt = db.readData(sql);
			return dt;
		}

		public DataTable getTKBByMaLopAndHk(string malop, string hk)
        {
            string sql = $" select tkb.MaMon, TenMon, HocKy, ThuHoc, CaHoc, TenPhong" +
                        $" from tkb join MonHoc mh on tkb.MaMon = mh.MaMon " +
                        $" join PhongHoc ph on tkb.MaPhong = ph.MaPhong" +
                        $" where Malop = '{malop}' and hocky = '{hk}'";
            DataTable dt = db.readData(sql);
            return dt;
        }

        public bool isExists(string malop, string hk)
        {
            string sql = $"select count(*) from tkb where malop = '{malop}' and hocky = '{hk}'";
            DataTable dt = db.readData(sql);
            return Convert.ToInt32(dt.Rows[0][0]) > 0;
        }
    }
}
