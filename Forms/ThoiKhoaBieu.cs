using QuanLyDiemSinhVien.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace QuanLyDiemSinhVien.Forms
{
	public partial class ThoiKhoaBieu : Form
	{
		TKBRepository tKBRepository;
		LopRepository lopRepository;
		public ThoiKhoaBieu()
		{
			InitializeComponent();
			tKBRepository = new TKBRepository();
			lopRepository = new LopRepository();
		}

		private void ThoiKhoaBieu_Load(object sender, EventArgs e)
		{
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

		private void button2_Click(object sender, EventArgs e)
		{
			if (malop.SelectedIndex == -1 && hocky.SelectedIndex == -1)
			{
				MessageBox.Show("Vui lòng chọn mã lớp và học kỳ để in thời khóa biểu", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if (malop.SelectedIndex == -1 && hocky.SelectedIndex != -1)
			{
				if ((MessageBox.Show("Thời khóa biểu sẽ in tất cả các lớp với học kỳ " + hocky.Text + ". Bạn có muốn in không ?",
					"Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information)) == DialogResult.Cancel) { return; }
				exportTKBByHK();
			}
			else if (malop.SelectedIndex != -1 && hocky.SelectedIndex == -1)
			{
				if ((MessageBox.Show("Thời khóa biểu sẽ in tất cả học kỳ của lớp " + lopRepository.getTenLopByMaLop(malop.Text) + ". Bạn có muốn in không ?",
					"Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information)) == DialogResult.Cancel) { return; }
				exportTKBByMaLop();
			}
			else
			{
				if (MessageBox.Show("Thời khóa biểu sẽ in học kỳ " + hocky.Text + " của lớp " + lopRepository.getTenLopByMaLop(malop.Text) + ". Bạn có muốn in không ?",
					"Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel)
				{ return; }
				exportTKBByMLAndHK();
			}
		}


		public void exportTKBByHK()
		{
			DataTable dt = null;
			Excel.Application oXL = new Excel.Application();
			oXL.Visible = false;
			Excel.Workbook oWB = oXL.Workbooks.Add(Type.Missing);
			List<Excel.Worksheet> list = new List<Excel.Worksheet>();
			list.Add(oWB.ActiveSheet as Excel.Worksheet);
			List<string> lops = tKBRepository.getLopByHK(hocky.Text);
			for (int item = 0; item < lops.Count; item++)
			{
				if (item != 0)
				{
					list.Add(oWB.Sheets.Add(Type.Missing, Type.Missing, 1, Type.Missing) as Excel.Worksheet);
				}
				list[item].Name = lops[item];
				list[item].Range["A1"].Value = "Thời khóa biểu lớp " + lopRepository.getTenLopByMaLop(lops[item]) + " - Học kỳ " + hocky.Text;
				list[item].Range["A1"].Font.Color = Color.Red;
				list[item].Range["A1"].Font.Size = 18;
				list[item].Range["A1:H1"].Merge();

				list[item].Range["A4"].Value = "STT";
				list[item].Range["B4"].Value = "Mã Môn Học";
				list[item].Range["C4"].Value = "Tên Môn Học";
				list[item].Range["D4"].Value = "Học Kỳ";
				list[item].Range["E4"].Value = "Thứ";
				list[item].Range["F4"].Value = "Ca";
				list[item].Range["G4"].Value = "Phòng";

				list[item].Range["B:B"].ColumnWidth = 12.5;
				list[item].Range["C:C"].ColumnWidth = 18.5;


				list[item].Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;

				int rowStart = 5;
				dt = tKBRepository.getTKBByMaLopAndHk(lops[item], hocky.Text);

				for (int i = 0; i < dt.Rows.Count; i++)
				{
					list[item].Range["A" + (rowStart + i)].Value = i + 1;
					list[item].Range["B" + (rowStart + i)].Value = dt.Rows[i][0].ToString();
					list[item].Range["C" + (rowStart + i)].Value = dt.Rows[i][1].ToString();
					list[item].Range["D" + (rowStart + i)].Value = dt.Rows[i][2].ToString();
					list[item].Range["E" + (rowStart + i)].Value = dt.Rows[i][3].ToString();
					list[item].Range["F" + (rowStart + i)].Value = dt.Rows[i][4].ToString();
					list[item].Range["G" + (rowStart + i)].Value = dt.Rows[i][5].ToString();

				}

			}

			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.Filter = "Excel Document(*.xls)|*.xls |Word Document(*.doc)| *.doc | All files(*.*) | *.* ";
			saveFileDialog.FilterIndex = 1;
			saveFileDialog.AddExtension = true;
			saveFileDialog.DefaultExt = ".xls";
			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				oWB.SaveAs(saveFileDialog.FileName);
			}
			oWB.Close(Type.Missing, Type.Missing, Type.Missing);
			oXL.UserControl = true;
			oXL.Quit();
		}

		public void exportTKBByMaLop()
		{
			DataTable dt = null;
			Excel.Application oXL = new Excel.Application();
			oXL.Visible = false;
			Excel.Workbook oWB = oXL.Workbooks.Add(Type.Missing);
			List<Excel.Worksheet> list = new List<Excel.Worksheet>();
			list.Add(oWB.ActiveSheet as Excel.Worksheet);
			List<string> hks = tKBRepository.getHKsByMaLop(malop.Text);
			for (int item = 0; item < hks.Count; item++)
			{
				if (item != 0)
				{
					list.Add(oWB.Sheets.Add(Type.Missing, Type.Missing, 1, Type.Missing) as Excel.Worksheet);
				}
				//list[item].Name = "Học kỳ " + hks[item];
				list[item].Range["A1"].Value = "Thời khóa biểu lớp " + lopRepository.getTenLopByMaLop(malop.Text) + " - Học kỳ " + hks[item];
				list[item].Range["A1"].Font.Color = Color.Red;
				list[item].Range["A1"].Font.Size = 18;
				list[item].Range["A1:H1"].Merge();

				list[item].Range["A4"].Value = "STT";
				list[item].Range["B4"].Value = "Mã Môn Học";
				list[item].Range["C4"].Value = "Tên Môn Học";
				list[item].Range["D4"].Value = "Học Kỳ";
				list[item].Range["E4"].Value = "Thứ";
				list[item].Range["F4"].Value = "Ca";
				list[item].Range["G4"].Value = "Phòng";

				list[item].Range["B:B"].ColumnWidth = 12.5;
				list[item].Range["C:C"].ColumnWidth = 18.5;


				list[item].Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;

				int rowStart = 5;
				dt = tKBRepository.getTKBByMaLopAndHk(malop.Text, hks[item]);

				for (int i = 0; i < dt.Rows.Count; i++)
				{
					list[item].Range["A" + (rowStart + i)].Value = i + 1;
					list[item].Range["B" + (rowStart + i)].Value = dt.Rows[i][0].ToString();
					list[item].Range["C" + (rowStart + i)].Value = dt.Rows[i][1].ToString();
					list[item].Range["D" + (rowStart + i)].Value = dt.Rows[i][2].ToString();
					list[item].Range["E" + (rowStart + i)].Value = dt.Rows[i][3].ToString();
					list[item].Range["F" + (rowStart + i)].Value = dt.Rows[i][4].ToString();
					list[item].Range["G" + (rowStart + i)].Value = dt.Rows[i][5].ToString();

				}

			}

			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.Filter = "Excel Document(*.xls)|*.xls |Word Document(*.doc)| *.doc | All files(*.*) | *.* ";
			saveFileDialog.FilterIndex = 1;
			saveFileDialog.AddExtension = true;
			saveFileDialog.DefaultExt = ".xls";
			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				oWB.SaveAs(saveFileDialog.FileName);
			}
			oWB.Close(Type.Missing, Type.Missing, Type.Missing);
			oXL.UserControl = true;
			oXL.Quit();
		}

		public void exportTKBByMLAndHK()
		{
			DataTable dt = null;
			Excel.Application exApp = new Excel.Application();
			exApp.Visible = false;
			Excel.Workbook exWorkbook = exApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
			Excel.Worksheet exSheet = exWorkbook.Worksheets[1];

			if (!tKBRepository.isExists(malop.Text, hocky.Text))
			{
				MessageBox.Show("Lớp không có thời khóa biểu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}

			exSheet.Range["A1"].Value = "Thời khóa biểu lớp " + lopRepository.getTenLopByMaLop(malop.Text) + " - Học kỳ " + hocky.Text;
			exSheet.Range["A1"].Font.Color = Color.Red;
			exSheet.Range["A1"].Font.Size = 18;
			exSheet.Range["A1:H1"].Merge();

			exSheet.Range["A4"].Value = "STT";
			exSheet.Range["B4"].Value = "Mã Môn Học";
			exSheet.Range["C4"].Value = "Tên Môn Học";
			exSheet.Range["D4"].Value = "Học Kỳ";
			exSheet.Range["E4"].Value = "Thứ";
			exSheet.Range["F4"].Value = "Ca";
			exSheet.Range["G4"].Value = "Phòng";

			exSheet.Range["B:B"].ColumnWidth = 12.5;
			exSheet.Range["C:C"].ColumnWidth = 18.5;


			exSheet.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;

			int rowStart = 5;
			dt = tKBRepository.getTKBByMaLopAndHk(malop.Text, hocky.Text);

			for (int i = 0; i < dt.Rows.Count; i++)
			{
				exSheet.Range["A" + (rowStart + i)].Value = i + 1;
				exSheet.Range["B" + (rowStart + i)].Value = dt.Rows[i][0].ToString();
				exSheet.Range["C" + (rowStart + i)].Value = dt.Rows[i][1].ToString();
				exSheet.Range["D" + (rowStart + i)].Value = dt.Rows[i][2].ToString();
				exSheet.Range["E" + (rowStart + i)].Value = dt.Rows[i][3].ToString();
				exSheet.Range["F" + (rowStart + i)].Value = dt.Rows[i][4].ToString();
				exSheet.Range["G" + (rowStart + i)].Value = dt.Rows[i][5].ToString();

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

		private void button1_Click(object sender, EventArgs e)
		{
			if (malop.SelectedIndex == -1 && hocky.SelectedIndex == -1)
			{
				MessageBox.Show("Hãy nhập đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}
			if(malop.SelectedIndex != -1 && hocky.SelectedIndex == -1)
			{
				dataGridView1.DataSource = tKBRepository.getTKBByMaLop(malop.Text);
			}
			else if (malop.SelectedIndex == -1 && hocky.SelectedIndex != -1)
			{
				dataGridView1.DataSource = tKBRepository.getTKBByHK(hocky.Text);
			}
			else
			{
				dataGridView1.DataSource = tKBRepository.getTKBByMaLopAndHk(malop.Text, hocky.Text);
			}
		}

		private void button3_Click(object sender, EventArgs e)
		{
			malop.SelectedIndex = -1;
			hocky.SelectedIndex = -1;
		}
	}
}
