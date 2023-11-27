using QuanLyDiemSinhVien.Forms;
using QuanLyDiemSinhVien.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

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
        DiemRepository diemRepository;
        bool run = true;
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
            diemRepository = new DiemRepository();

            // initial datasource gridview
            dataGridView1.DataSource = svRepository.getAllStudents();

            loadDefaultValueOfAllCombobox();

            this.Size = new Size(880, 700);
            tabControl1.Size = new Size(880, 715);
            addform(tabPage2, new Quanlydiem());
			addform(tabPage3, new ThoiKhoaBieu());

			setRefreshBtn(button6);
            setRefreshBtn(button7);
            setRefreshBtn(button8);
		}

        //form
        private void button1_Click_1(object sender, EventArgs e)
        {
            string que = tkque.Text;
            string khoa = tkkhoa.Text;
            string cn = tkchuyennghanh.Text;
            DataTable dt = svRepository.getStudentsWith(que, khoa, cn);
            dataGridView1.DataSource = dt;
        }


        private void lammoitk_Click_1(object sender, EventArgs e)
        {
            tkque.SelectedIndex = -1;
            tkkhoa.SelectedIndex = -1;
            tkchuyennghanh.SelectedIndex = -1;
            loadValuesOfCombox(tkchuyennghanh, chuyenNganhRepository.getAllMCN());
            reloadDataGridView();
        }


        private void tkkhoa_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string item = tkkhoa.Text;
            tkchuyennghanh.SelectedIndex = -1;
			if (item == "") return;
			loadValuesOfCombox(tkchuyennghanh, chuyenNganhRepository.getMCNByMaKhoa(item));
        }

        private void makhoa_TextChanged_1(object sender, EventArgs e)
        {
            string item = makhoa.Text;
            if (item == null || item == "")
            {
                reloadDataGridView();
                return;
            }
        }

        private void makhoa_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string item = makhoa.Text;
            malop.SelectedIndex = -1;
            machuyennganh.SelectedIndex = -1;
            loadValuesOfCombox(malop, lopRepository.getAllMaLopByMaKhoa(item));
            loadValuesOfCombox(machuyennganh, chuyenNganhRepository.getMCNByMaKhoa(item));
        }


		private void malop_SelectedIndexChanged(object sender, EventArgs e)
		{
            if (!run) return;
			string item = malop.Text;
			DataTable dt = svRepository.getStudentsByMaLop(item);
			dataGridView1.DataSource = null;
			dataGridView1.DataSource = dt;
			dataGridView1.Update();
			dataGridView1.Refresh();
		}

		private void button2_Click_1(object sender, EventArgs e)
        {
            if (!isValid()) return;
            if (isExists()) return;
            string sql = loadUserForInsert();
            if (svRepository.persistStudent(sql))
            {
                MessageBox.Show("Lưu sinh viên thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                reloadDataGridView();
				run = false;
				clearInput();
                run = true;
            }
            else
            {
                MessageBox.Show("Lưu sinh viên thất bại !", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
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

        private void button4_Click_1(object sender, EventArgs e)
        {
            if (!isValid()) return;
            if (MessageBox.Show("Bạn có muốn sửa không ?", "Thông báo", 
                MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel) return;
            string sql = loadUserForUpdate();
            bool result = svRepository.persistStudent(sql);
            if (result)
            {
                MessageBox.Show("Cập nhật sinh viên thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                reloadDataGridView();
            }
            else MessageBox.Show("Cập nhật sinh viên thất bại !", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // ......
        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            run = false;
            dataGridView1.Update();
            DataGridViewRow row = dataGridView1.CurrentRow;
			makhoa.Text = row.Cells[2].Value?.ToString();
			malop.Text = row.Cells[3].Value?.ToString();
			msv.Text = row.Cells[0].Value?.ToString();
            hoten.Text = row.Cells[1].Value?.ToString();
            string[] s = row.Cells[4].Value?.ToString().Split('/');
            ngaysinh.Value = new DateTime(Convert.ToInt32(s[2].Substring(0, 4)), Convert.ToInt32(s[0]), Convert.ToInt32(s[1]));
            gioitinh.Text = row.Cells[5].Value?.ToString();
            que.Text = row.Cells[6].Value?.ToString();
            dantoc.Text = row.Cells[7].Value?.ToString();
            machuyennganh.Text = row.Cells[8].Value?.ToString();
            mahdt.Text = row.Cells[9].Value?.ToString();
            machucvu.Text = row.Cells[10].Value?.ToString();
            run = true;
        }

        private void lammoi_Click_1(object sender, EventArgs e)
        {
            run = false;
            clearInput();
            run = true;
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
            if (svRepository.isExists(msv.Text))
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

        //function to add form
        public void addform(TabPage tp, Form f)
        {

            f.TopLevel = false;

            f.FormBorderStyle = FormBorderStyle.None;
            f.AutoScaleMode = AutoScaleMode.Dpi;

            if (!tp.Controls.Contains(f))
            {
                tp.Controls.Add(f);
                f.Dock = DockStyle.Fill;
                f.Show();
                Refresh();
            }
            Refresh();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(msv.Text == "")
            {
                MessageBox.Show("Vui lòng nhập mã sinh viên hoặc chọn sinh viên trong bảng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if(!svRepository.isExists(msv.Text))
            {
                MessageBox.Show("Sinh viên không tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                msv.Focus();
                return;
            }

           if(diemRepository.getDiemById(msv.Text).Rows.Count == 0)
            {
                MessageBox.Show("Sinh viên không có bảng điểm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Excel.Application exApp = new Excel.Application();
            exApp.Visible = false;
            Excel.Workbook exWorkbook = exApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
            Excel.Worksheet exSheet = exWorkbook.Worksheets[1];
            exSheet.EnableSelection = Excel.XlEnableSelection.xlNoSelection;
            Excel.Range exRange = exSheet.Cells[1, 1];
            DataTable bd = diemRepository.getDiemById(msv.Text);

            exSheet.Range["A1"].Value = "Bảng điểm của sinh viên " + svRepository.getStudentById(msv.Text) + " - " + msv.Text.ToString();

            exSheet.Range["A4"].Value = "STT";
            exSheet.Range["B4"].Value = "Mã Sinh Viên";
            exSheet.Range["C4"].Value = "Họ Và Tên";
            exSheet.Range["D4"].Value = "Lớp";
            exSheet.Range["E4"].Value = "Môn Học";
            exSheet.Range["F4"].Value = "Học Kỳ";
            exSheet.Range["G4"].Value = "Lần Thi";
            exSheet.Range["H4"].Value = "Điểm";

            exSheet.Range["B:B"].ColumnWidth = 12;
            exSheet.Range["C:C"].ColumnWidth = 18;
            exSheet.Range["E:E"].ColumnWidth = 15;

            exSheet.Range["A1"].Font.Color = Color.Red;
            exSheet.Range["A1"].Font.Size = 18;

   
            exSheet.Range["A1:F1"].Merge();

            exSheet.Range["A1:F1"].MergeCells = true;
            exSheet.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
            int rowStart = 5;

            for(int i = 0; i < bd.Rows.Count; i++)
            {
                exSheet.Range["A" + (rowStart + i)].Value = i + 1;
                exSheet.Range["B" + (rowStart + i)].Value = bd.Rows[i][0].ToString();
                exSheet.Range["C" + (rowStart + i)].Value = bd.Rows[i][1].ToString();
                exSheet.Range["D" + (rowStart + i)].Value = bd.Rows[i][2].ToString();
                exSheet.Range["E" + (rowStart + i)].Value = bd.Rows[i][3].ToString();
                exSheet.Range["F" + (rowStart + i)].Value = bd.Rows[i][4].ToString();
                exSheet.Range["G" + (rowStart + i)].Value = bd.Rows[i][5].ToString();
                exSheet.Range["H" + (rowStart + i)].Value = bd.Rows[i][6].ToString();
            }

            exWorkbook.Activate();
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel Document(*.xls)|*.xls |Word Document(*.doc)| *.doc | All files(*.*) | *.* ";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.AddExtension = true;
            saveFileDialog.DefaultExt = ".xls";
            if(saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                exWorkbook.SaveAs(saveFileDialog.FileName);
            }
            Marshal.ReleaseComObject(exWorkbook);
            exApp.Quit();
        }

        public void setRefreshBtn(Button bt, int size = 12, int with = 30, int height = 30)
        {
			bt.Font = new Font("Wingdings 3", size, FontStyle.Bold);
			bt.Text = Char.ConvertFromUtf32(80); // or 80
			bt.Width = with;
			bt.Height = height;
		}

		private void button6_Click(object sender, EventArgs e)
		{
            tkque.SelectedIndex = -1;
		}

		private void button7_Click(object sender, EventArgs e)
		{
            tkkhoa.SelectedIndex = -1;
            loadValuesOfCombox(tkchuyennghanh, chuyenNganhRepository.getAllMCN());
		}

		private void button8_Click(object sender, EventArgs e)
		{
            tkchuyennghanh.SelectedIndex = -1;
		}
	}
}
