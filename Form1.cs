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
		SinhVienRepository repository;
		public Form1()
		{
			InitializeComponent();
			repository = new SinhVienRepository();
			dataGridView1.DataSource = repository.getAllStudents();
		}

    }
}
