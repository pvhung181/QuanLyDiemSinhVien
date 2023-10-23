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

namespace QuanLyDiemSinhVien.Forms
{
	public partial class Menu : Form
	{
		SinhVienRepository svRepo;
		public Menu()
		{
			InitializeComponent();
			svRepo = new SinhVienRepository();
			dataGridView1.DataSource = svRepo.getAllStudents();
		}
    }
}
