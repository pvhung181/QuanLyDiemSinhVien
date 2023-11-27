using QuanLyDiemSinhVien.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace QuanLyDiemSinhVien.Forms
{
    public partial class Quanlydiem : Form
    {
        LopRepository lopRepository;
        TKBRepository tKBRepository;
        MonHocRepository monHocRepository;
        DiemRepository diemRepository;

        Dictionary<string, string> editedCells = new Dictionary<string, string>();

        public Quanlydiem()
        {
            InitializeComponent();
            lopRepository = new LopRepository();
            tKBRepository = new TKBRepository();
            monHocRepository = new MonHocRepository();
            diemRepository = new DiemRepository();

            this.Size = new Size(960, 595);
            loadValuesOfCombox(malop, lopRepository.getAllMaLop());
        }

        public void loadValuesOfCombox(ComboBox cb, DataTable table)
        {
            cb.SelectedIndex = -1;
            cb.Items.Clear();
            foreach (DataRow row in table.Rows)
            {
                cb.Items.Add(row.ItemArray[0]);
            }
        }

        private void malop_SelectedValueChanged(object sender, EventArgs e)
        {
            if (hocky.SelectedIndex == -1) loadValuesOfCombox(mamon, tKBRepository.getTenMonHoc(malop.Text));
            else loadValuesOfCombox(mamon, tKBRepository.getTenMonHoc(malop.Text, hocky.Text));
        }

        private void hocky_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((malop.Text != null && malop.SelectedIndex != -1) || (malop.Text != "" && malop.SelectedIndex != -1))
                loadValuesOfCombox(mamon, tKBRepository.getTenMonHoc(malop.Text, hocky.Text));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!isValid()) return;
            string mm = monHocRepository.getMaMonByTen(mamon.Text);
            DataTable dt = diemRepository.getDiemByLop(malop.Text, mm, hocky.Text, Convert.ToInt32(lanthi.Text));
            danhsachdiem.DataSource = dt;
            for (int i = 0; i < dt.Columns.Count - 1; i++)
            {
                danhsachdiem.Columns[i].ReadOnly = true;
            }

        }

        public bool isValid()
        {
            if ((malop.Text != "" && malop.SelectedIndex == -1) ||
                (mamon.Text != "" && mamon.SelectedIndex == -1) ||
                (hocky.Text != "" && hocky.SelectedIndex == -1) ||
                (lanthi.Text != "" && lanthi.SelectedIndex == -1))
            {
                MessageBox.Show("Thông tin nhập không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (malop.SelectedIndex == -1 || mamon.SelectedIndex == -1 ||
               hocky.SelectedIndex == -1 || lanthi.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }

        private void danhsachdiem_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            
            try
            {
                double tem = Convert.ToDouble(danhsachdiem.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                if (tem > 10 || tem < 0)
                {
                    MessageBox.Show("Điểm không hợp lệ vui lòng nhập lại !", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    danhsachdiem.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;
                    danhsachdiem.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0;

					return;
                }
            }
            catch
            {
                MessageBox.Show("Điểm không hợp lệ vui lòng nhập lại !", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                danhsachdiem.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;
				danhsachdiem.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0;
				return;
            }
            string key = danhsachdiem.Rows[e.RowIndex].Cells[0].Value.ToString();
            string value = danhsachdiem.Rows[e.RowIndex].Cells[6].Value.ToString();
            if (editedCells.ContainsKey(key)) editedCells[key] = value;
            else editedCells.Add(key, value);
        }

		private void updatebtn_Click(object sender, EventArgs e)
        {
            if (editedCells.Count == 0)
            {
                MessageBox.Show("Không có gì để cập nhật", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string sql = $"";
            foreach (KeyValuePair<string, string> entry in editedCells)
            {
                sql += $"update Diem set Diem = {entry.Value} where Masv = '{entry.Key}'  ";
            }

            bool res = diemRepository.updateDiem(sql);
            if (res)
            {
                MessageBox.Show("Cập nhật điểm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                danhsachdiem.Refresh();
            }
            else
            {
                MessageBox.Show("Cập nhật điểm thất bại", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            malop.SelectedIndex = -1;
            mamon.Text = null;
            hocky.SelectedIndex = -1;
            lanthi.SelectedIndex = -1;
            danhsachdiem.DataSource = null;
            danhsachdiem.Refresh();
        }



        private void button3_Click(object sender, EventArgs e)
        {
            if(malop.SelectedIndex == -1 || mamon.SelectedIndex == -1 || lanthi.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin lớp, môn, lần thi để xuất excel", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string tl = lopRepository.getTenLopByMaLop(malop.Text);
            if ((MessageBox.Show("Xuất bảng điểm của lớp " + tl + " môn " + mamon.Text + " lần thi thứ " + lanthi.Text +
                " Bạn có muốn xuất không ?",
                "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel))
            { return; }
            Excel.Application exApp = new Excel.Application();
            exApp.Visible = false;
            Excel.Workbook exWorkbook = exApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
            Excel.Worksheet exSheet = exWorkbook.Worksheets[1];
            exSheet.EnableSelection = Excel.XlEnableSelection.xlNoSelection;
            Excel.Range exRange = exSheet.Cells[1, 1];
            string mm = monHocRepository.getMaMonByTen(mamon.Text);

            DataTable bd = diemRepository.getDiemByMLMHLT(malop.Text, mm, lanthi.Text);

            exSheet.Range["A1"].Value = "Bảng điểm của lớp " + tl
                                        + " - " + mamon.Text + " lần thi thứ " + lanthi.Text;

            exSheet.Range["A4"].Value = "STT";
            exSheet.Range["B4"].Value = "Mã Sinh Viên";
            exSheet.Range["C4"].Value = "Họ Và Tên";
            exSheet.Range["D4"].Value = "Học Kỳ";
            exSheet.Range["E4"].Value = "Điểm";

            exSheet.Range["B:B"].ColumnWidth = 12;
            exSheet.Range["C:C"].ColumnWidth = 18;
            exSheet.Range["E:E"].ColumnWidth = 15;

            exSheet.Range["A1"].Font.Color = Color.Red;
            exSheet.Range["A1"].Font.Size = 18;


            exSheet.Range["A1:I1"].Merge();

            exSheet.Range["A1:I1"].MergeCells = true;
            exSheet.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
            int rowStart = 5;

            for (int i = 0; i < bd.Rows.Count; i++)
            {
                exSheet.Range["A" + (rowStart + i)].Value = i + 1;
                exSheet.Range["B" + (rowStart + i)].Value = bd.Rows[i][0].ToString();
                exSheet.Range["C" + (rowStart + i)].Value = bd.Rows[i][1].ToString();
                exSheet.Range["D" + (rowStart + i)].Value = bd.Rows[i][2].ToString();
                exSheet.Range["E" + (rowStart + i)].Value = bd.Rows[i][3].ToString();
            }

            exWorkbook.Activate();
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel Document(*.xls)|*.xls |Word Document(*.doc)| *.doc | All files(*.*) | *.* ";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.AddExtension = true;
            saveFileDialog.DefaultExt = ".xls";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                exWorkbook.SaveAs(saveFileDialog.FileName);
            }
            Marshal.ReleaseComObject(exWorkbook);
            exApp.Quit();

        }
  
	}
}
