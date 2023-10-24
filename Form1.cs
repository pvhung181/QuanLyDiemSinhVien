using QuanLyDiemSinhVien.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyDiemSinhVien
{
	public partial class Form1 : Form
	{
		SinhVienRepository svRepository;
		KhoaRepository khoaRepository;
		LopRepository lopRepository;
		ChuyenNganhRepository chuyenNganhRepository;
        ChucVuRepository chucVuRepository;

		public Form1()
		{
			InitializeComponent();
			svRepository = new SinhVienRepository();
			khoaRepository = new KhoaRepository();
			lopRepository = new LopRepository();
			chuyenNganhRepository = new ChuyenNganhRepository();
            chucVuRepository = new ChucVuRepository();
            dataGridView1.DataSource = svRepository.getAllStudents();
		}

        private void Form1_Load(object sender, EventArgs e)
        {
			this.Size = new Size(830, 730);
			loadDefaultValueOfAllCombobox();
        }

		public void loadDefaultValueOfAllCombobox()
		{
            DataTable dataTable = khoaRepository.getAllMaKhoa();
            foreach (DataRow row in dataTable.Rows)
            {
                makhoa.Items.Add(row.ItemArray[0]);
            }
            dataTable = lopRepository.getAllMaLop();
            foreach (DataRow row in dataTable.Rows)
            {
                malop.Items.Add(row.ItemArray[0]);
            }
            dataTable = chuyenNganhRepository.getAllMCN();
            foreach (DataRow row in dataTable.Rows)
            {
                machuyennganh.Items.Add(row.ItemArray[0]);
            }

            dataTable = chucVuRepository.getAllMCV();
            foreach (DataRow row in dataTable.Rows)
            {
                machucvu.Items.Add(row.ItemArray[0]);
            }
        }

        private void makhoa_SelectedValueChanged(object sender, EventArgs e)
        {
            string item = makhoa.GetItemText(makhoa.SelectedItem);
            DataTable table = lopRepository.getAllMaLopByMaKhoa(item);
            malop.Items.Clear();
            foreach (DataRow row in table.Rows)
            {
                malop.Items.Add(row.ItemArray[0]);
            }
        }
    }
}
