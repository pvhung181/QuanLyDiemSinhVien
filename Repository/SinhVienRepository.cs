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
        public readonly static string GET_ALL = "select masv [Mã Sinh Viên], tensv [Họ Và Tên], makhoa [Mã Khoa], malop [Mã Lớp]," +
                "ngaysinh [Ngày Sinh],gioitinh [Giới Tính], tenque [Quê], dantoc [Dân Tộc]," +
                " macn [Mã Chuyên Ngành], mahdt [Mã HĐT], machucvu [Mã Chức Vụ] " +
                "from SinhVien sv join Que q on  sv.maque = q.maque " +
                "join DanToc dt on sv.madantoc = dt.madantoc ";

        public SinhVienRepository()
        {
            db = new ConnectionDatabase();
        }

        public DataTable getAllStudents()
        {
            DataTable students = db.readData(GET_ALL);
            return students;
        }

        public string getStudentById(string msv)
        {
            string sql = $"select TenSV from SinhVien where MaSv = '{msv}'";
            DataTable dt = db.readData(sql);
            string res = dt.Rows[0][0].ToString();
            return res;
        }

        public DataTable getStudentsByMaLop(string maLop)
        {
            string sql = $"{GET_ALL} where MaLop = '{maLop}'";
            DataTable students = db.readData(sql);
            return students;
        }

        public DataTable getStudentsWithWhereClause(string sql)
        {
            DataTable students = db.readData(sql);
            return students;
        }


        public DataTable getStudentsWithWhereClause2(string que, string khoa, string cn)
        {
            string whereClause = $"";
            if (que != null && que != "")
            {
                whereClause += $" tenque = N'{que}' ";
                if (khoa != null && khoa != "") whereClause += $" and sv.MaKhoa = '{khoa}' ";
                if (cn != null && cn != "") whereClause += $" and sv.MaCN = '{cn}' ";
            }
            else if (khoa != null && khoa != "")
            {
                whereClause += $" sv.MaKhoa = '{khoa}' ";
                if (cn != null && cn != "") whereClause += $" and sv.MaCN = '{cn}' ";
            }
            else if (cn != null && khoa != "")
            {
                whereClause += $" sv.MaCN = '{cn}' ";
            }
            if (whereClause != "") whereClause = " where " + whereClause;
            string sql = GET_ALL + whereClause;
            DataTable students = db.readData(sql);
            return students;
        }

        public bool isExists(string id)
        {
            string sql = $"select count(*) from sinhvien where Masv = '{id}'";
            int count = Convert.ToInt32(db.readData(sql).Rows[0][0]);
            if(count == 1) return true;
            return false;
        }

        public bool persistStudent(string sql)
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

        public bool deleteStudent(string msv)
        {
            string sql = $"delete from sinhvien where Masv = '{msv}'";
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
