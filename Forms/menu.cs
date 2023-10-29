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
		public Menu()
		{
			InitializeComponent();
            this.Size = new Size(900, 730);
            tabControl1.Size = new Size(900, 730);
            addform(tabPage1, new Form1());
            addform(tabPage2, new Quanlydiem());
        }
        public void addform(TabPage tp, Form f)
        {

            f.TopLevel = false;
            //no border if needed
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
    }
}
