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
    }
}
