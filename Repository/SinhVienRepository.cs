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
            string sql = "select masv [Mã Sinh Viên], tensv [Họ Và Tên], makhoa [Mã Khoa], malop [Mã Lớp]," +
                "ngaysinh [Ngày Sinh],gioitinh [Giới Tính], tenque [Quê], dantoc [Dân Tộc]," +
                " macn [Mã Chuyên Ngành], mahdt [Mã HĐT], machucvu [Mã Chức Vụ] " +
                "from SinhVien sv join Que q on  sv.maque = q.maque " +
                "join DanToc dt on sv.madantoc = dt.madantoc";
            DataTable students = db.readData(sql);
            return students;
        }

        public DataTable getStudentsByMaLop(string maLop)
        {
            string sql = $"select * from SinhVien where MaLop = '{maLop}'";
            DataTable students = db.readData(sql);
            return students;
        }
    }
}
