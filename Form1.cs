using QuanLyDiemSinhVien.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyDiemSinhVien
{
    public partial class Form1 : Form
    {
        //repositories
        SinhVienRepository svRepository;
        KhoaRepository khoaRepository;
        LopRepository lopRepository;
        ChuyenNganhRepository chuyenNganhRepository;
        ChucVuRepository chucVuRepository;
        HeDTRepository heDTRepository;
        QueRepository queRepository;
        DanTocRepository danTocRepository;

        public Form1()
        {
            InitializeComponent();
            svRepository = new SinhVienRepository();
            khoaRepository = new KhoaRepository();
            lopRepository = new LopRepository();
            chuyenNganhRepository = new ChuyenNganhRepository();
            chucVuRepository = new ChucVuRepository();
            heDTRepository = new HeDTRepository();
            queRepository = new QueRepository();
            danTocRepository = new DanTocRepository();

            // initial datasource gridview
            dataGridView1.DataSource = svRepository.getAllStudents();

            this.Size = new Size(900, 730);
            loadDefaultValueOfAllCombobox();
        }

        //form
        private void button1_Click(object sender, EventArgs e)
        {
            string que = tkque.Text;
            string khoa = tkkhoa.Text;
            string cn = tkchuyennghanh.Text;
            string sql = loadSqlForSearch(que, khoa, cn);
            DataTable dt = svRepository.getStudentsWithWhereClause(sql);
            dataGridView1.DataSource = dt;

        }

        private void lammoitk_Click(object sender, EventArgs e)
        {
            tkque.SelectedIndex = -1;
            tkkhoa.SelectedIndex = -1;
            tkchuyennghanh.SelectedIndex = -1;
            reloadDataGridView();
        }

        private void tkkhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            string item = tkkhoa.Text;
            tkchuyennghanh.SelectedIndex = -1;
            loadValuesOfCombox(tkchuyennghanh, chuyenNganhRepository.getMCNByMaKhoa(item));
        }

        private void makhoa_TextChanged(object sender, EventArgs e)
        {
            string item = makhoa.Text;
            if (item == null || item == "")
            {
                reloadDataGridView();
                return;
            }
        }

        private void makhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            string item = makhoa.Text;
            malop.SelectedIndex = -1;
            machuyennganh.SelectedIndex = -1;
            loadValuesOfCombox(malop, lopRepository.getAllMaLopByMaKhoa(item));
            loadValuesOfCombox(machuyennganh, chuyenNganhRepository.getMCNByMaKhoa(item));
        }

        private void malop_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string item = malop.Text;
            DataTable dt = svRepository.getStudentsByMaLop(item);
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = dt;
            dataGridView1.Update();
            dataGridView1.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!isValid()) return;
            if (isExists()) return;
            string sql = loadUserForInsert();
            if (svRepository.persistStudent(sql))
            {
                MessageBox.Show("Lưu sinh viên thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                reloadDataGridView();
                clearInput();
            }
            else
            {
                MessageBox.Show("Lưu sinh viên thất bại !", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (msv.Text == null || msv.Text == "" || msv.Text.Length > 10)
            {
                MessageBox.Show("Mã sinh viên không hợp lệ !", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa ? Dữ liệu sẽ không thể khôi phục lại",
                "Cảnh Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                bool result = svRepository.deleteStudent(msv.Text);
                if (result)
                {
                    MessageBox.Show("Xóa sinh viên thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clearInput();
                }
                else MessageBox.Show("Xóa sinh viên thất bại !", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!isValid()) return;
            string sql = loadUserForUpdate();
            bool result = svRepository.persistStudent(sql);
            if (result)
            {
                MessageBox.Show("Cập nhật sinh viên thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                reloadDataGridView();
            }
            else MessageBox.Show("Cập nhật sinh viên thất bại !", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.Update();
            DataGridViewRow row = dataGridView1.CurrentRow;
            msv.Text = row.Cells[0].Value?.ToString();
            hoten.Text = row.Cells[1].Value?.ToString();
            makhoa.Text = row.Cells[2].Value?.ToString();
            malop.Text = row.Cells[3].Value?.ToString();
            string[] s = row.Cells[4].Value?.ToString().Split('/');
            ngaysinh.Value = new DateTime(Convert.ToInt32(s[2].Substring(0, 4)), Convert.ToInt32(s[0]), Convert.ToInt32(s[1]));
            gioitinh.Text = row.Cells[5].Value?.ToString();
            que.Text = row.Cells[6].Value?.ToString();
            dantoc.Text = row.Cells[7].Value?.ToString();
            machuyennganh.Text = row.Cells[8].Value?.ToString();
            mahdt.Text = row.Cells[9].Value?.ToString();
            machucvu.Text = row.Cells[10].Value?.ToString();
        }

        private void lammoi_Click(object sender, EventArgs e)
        {
            clearInput();
        }
        //utility function

        public void loadDefaultValueOfAllCombobox()
        {
            loadValuesOfCombox(makhoa, khoaRepository.getAllMaKhoa());
            loadValuesOfCombox(tkkhoa, khoaRepository.getAllMaKhoa());
            loadValuesOfCombox(malop, lopRepository.getAllMaLop());
            loadValuesOfCombox(machuyennganh, chuyenNganhRepository.getAllMCN());
            loadValuesOfCombox(tkchuyennghanh, chuyenNganhRepository.getAllMCN());
            loadValuesOfCombox(machucvu, chucVuRepository.getAllMCV());
            loadValuesOfCombox(mahdt, heDTRepository.getAllMaHDT());
            loadValuesOfCombox(que, queRepository.getMaQue());
            loadValuesOfCombox(tkque, queRepository.getMaQue());
            loadValuesOfCombox(dantoc, danTocRepository.getMaDanToc());
        }

        public void loadValuesOfCombox(ComboBox cb, DataTable table)
        {
            cb.Items.Clear();
            foreach (DataRow row in table.Rows)
            {
                cb.Items.Add(row.ItemArray[0]);
            }
        }

        public bool isValid()
        {
            if (msv.Text.Trim() == "" || hoten.Text.Trim() == "" || makhoa.SelectedIndex == -1 || malop.SelectedIndex == -1 ||
               machuyennganh.SelectedIndex == -1 || que.SelectedIndex == -1 || dantoc.SelectedIndex == -1 || gioitinh.SelectedIndex == -1 ||
               machucvu.SelectedIndex == -1 || mahdt.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin", "Nhập thiếu thông tin", MessageBoxButtons.OK);
                return false;
            }

            if (msv.Text.Length > 10)
            {
                MessageBox.Show("Mã sinh viên quá dài", "Nhập thông tin không hợp lệ", MessageBoxButtons.OK);
                return false;
            }

            if (hoten.Text.Length > 50)
            {
                MessageBox.Show("Họ tên quá dài vui lòng kiểm tra lại", "Nhập thông tin không hợp lệ", MessageBoxButtons.OK);
                return false;
            }

            DateTime dt = convertDate();
            if (dt != null)
            {
                if (DateTime.Compare(dt, DateTime.Now) > 0)
                {
                    MessageBox.Show("Ngày sinh không được lớn hơn thời gian hiện tại", "Nhập ngày sinh không hợp lê", MessageBoxButtons.OK);
                    return false;
                }

                if (DateTime.Now.Year - dt.Year < 18)
                {
                    MessageBox.Show("Tuổi của sinh viên không được nhỏ hơn 18", "Nhập ngày sinh không hợp lê", MessageBoxButtons.OK);
                    return false;
                }

            }

            return true;
        }

        public bool isExists()
        {
            if (svRepository.countStudentById(msv.Text) != 0)
            {
                MessageBox.Show("Đã tồn tại mã sinh viên vui lòng nhập lại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
            return false;
        }

        public void clearInput()
        {
            msv.Text = string.Empty;
            hoten.Text = string.Empty;
            makhoa.SelectedIndex = -1;
            malop.SelectedIndex = -1;
            machuyennganh.SelectedIndex = -1;
            machucvu.SelectedIndex = -1;
            que.SelectedIndex = -1;
            gioitinh.SelectedIndex = -1;
            mahdt.SelectedIndex = -1;
            dantoc.SelectedIndex = -1;
            ngaysinh.Text = string.Empty;
        }

        public string loadUserForInsert()
        {
            string maQue = queRepository.getMaQueByQue(que.Text);
            int maDanToc = danTocRepository.getMaDTByDT(dantoc.Text);
            DateTime dt = convertDate();
            string sql = $"insert into SinhVien values " +
                $"('{msv.Text}', N'{hoten.Text}', '{makhoa.Text}', '{malop.Text}', " +
                $"'{dt.ToString("yyyy/MM/dd")}', N'{gioitinh.Text}', '{maQue}', {maDanToc}, " +
                $"'{machuyennganh.Text}', '{mahdt.Text}','{machucvu.Text}')";

            return sql;
        }

        public string loadUserForUpdate()
        {
            string maQue = queRepository.getMaQueByQue(que.Text);
            int maDanToc = danTocRepository.getMaDTByDT(dantoc.Text);
            DateTime dt = convertDate();
            string sql = $"update SinhVien " +
                $"set Tensv = N'{hoten.Text}', Makhoa = '{makhoa.Text}', Malop = '{malop.Text}', " +
                $"NgaySinh = '{dt.ToString("yyyy/MM/dd")}', GioiTinh = N'{gioitinh.Text}', " +
                $"MaQue = '{maQue}', MaDanToc = {maDanToc}, MaCN = '{machuyennganh.Text}', " +
                $"MaHDT = '{mahdt.Text}', MaChucVu = '{machucvu.Text}' " +
                $"where MaSv = '{msv.Text}'";
            return sql;
        }

        public void reloadDataGridView()
        {
            DataTable dt = svRepository.getAllStudents();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = dt;
            dataGridView1.Update();
            dataGridView1.Refresh();

        }

        public DateTime convertDate()
        {
            DateTime dt = new DateTime(ngaysinh.Value.Year, ngaysinh.Value.Month, ngaysinh.Value.Day);
            return dt;
        }

        public string loadSqlForSearch(string que, string khoa, string cn)
        {
            string whereClause = $"";
            if(que != null && que != "")
            {
                whereClause += $" tenque = '{que}' ";
                if (khoa != null && khoa != "") whereClause += $" and sv.MaKhoa = '{khoa}' ";
                if(cn != null && cn != "") whereClause += $" and sv.MaCN = '{cn}' ";
            }
            else if (khoa != null && khoa != "")
            {
                whereClause += $" sv.MaKhoa = '{khoa}' ";
                if (cn != null && cn != "") whereClause += $" and sv.MaCN = '{cn}' ";
            }
            else if(cn != null && khoa != "")
            {
                whereClause += $" sv.MaCN = '{cn}' ";
            }
            if(whereClause != "") whereClause = " where " + whereClause;
            string sql = SinhVienRepository.GET_ALL + whereClause;
            return sql;
        }

    }
}
