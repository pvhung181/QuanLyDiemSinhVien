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

        public void loadDefaultValueOfAllCombobox()
        {
            loadValuesOfCombox(makhoa, khoaRepository.getAllMaKhoa());
            loadValuesOfCombox(malop, lopRepository.getAllMaLop());
            loadValuesOfCombox(machuyennganh, chuyenNganhRepository.getAllMCN());
            loadValuesOfCombox(machucvu, chucVuRepository.getAllMCV());
            loadValuesOfCombox(mahdt, heDTRepository.getAllMaHDT());
            loadValuesOfCombox(que, queRepository.getMaQue());
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
                MessageBox.Show("", "Nhập thông tin không hợp lệ", MessageBoxButtons.OK);
                return false;
            }

            if (hoten.Text.Length > 50)
            {
                MessageBox.Show("", "Nhập thông tin không hợp lệ", MessageBoxButtons.OK);
                return false;
            }

            DateTime dt = new DateTime(ngaysinh.Value.Year, ngaysinh.Value.Month, ngaysinh.Value.Day);
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


        private void button2_Click(object sender, EventArgs e)
        {
            if (!isValid()) return;
            string sql = loadUserInfor();
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

        public string loadUserInfor()
        {
            string maQue = queRepository.getMaQueByQue(que.Text);
            int maDanToc = danTocRepository.getMaDTByDT(dantoc.Text);
            string sql = $"insert into SinhVien values " +
                $"('{msv.Text}', N'{hoten.Text}', '{makhoa.Text}', '{malop.Text}', " +
                $"'{ngaysinh.Value.ToString()}', N'{gioitinh.Text}', '{maQue}', {maDanToc}, " +
                $"'{machuyennganh.Text}', '{mahdt.Text}','{machucvu.Text}')";

            return sql;
        }


        private void button5_Click(object sender, EventArgs e)
        {
            DataTable dt = svRepository.getAllStudents();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = dt;
            dataGridView1.Update();
            dataGridView1.Refresh();
        }

        public void reloadDataGridView()
        {
            DataTable dt = svRepository.getAllStudents();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = dt;
            dataGridView1.Update();
            dataGridView1.Refresh();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dataGridView1.CurrentRow;
            
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
            if (s != null)
            {
                ngaysinh.Value = new DateTime(Convert.ToInt32(s[2].Substring(0, 4)), Convert.ToInt32(s[0]), Convert.ToInt32(s[1]));

            }
            gioitinh.Text = row.Cells[5].Value?.ToString();
            que.Text = row.Cells[6].Value?.ToString();
            dantoc.Text = row.Cells[7].Value?.ToString();
            machuyennganh.Text = row.Cells[8].Value?.ToString();
            mahdt.Text = row.Cells[9].Value?.ToString();
            machucvu.Text = row.Cells[10].Value?.ToString();
        }

        private void makhoa_Enter(object sender, EventArgs e)
        {
            string item = makhoa.Text;
            if(item != null) { return; }
            malop.SelectedIndex = -1;
            machuyennganh.SelectedIndex = -1;
            loadValuesOfCombox(malop, lopRepository.getAllMaLopByMaKhoa(item));
            loadValuesOfCombox(machuyennganh, chuyenNganhRepository.getMCNByMaKhoa(item));
        }

        private void makhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            string item = makhoa.Text;
            //if (item != null) { return; }
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
    }
}
