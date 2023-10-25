using QuanLyDiemSinhVien.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
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
		}

        private void Form1_Load(object sender, EventArgs e)
        {
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

        private void makhoa_SelectedValueChanged(object sender, EventArgs e)
        {
            string item = makhoa.GetItemText(makhoa.SelectedItem);
            malop.SelectedIndex = -1;
            machuyennganh.SelectedIndex = -1;
            loadValuesOfCombox(malop, lopRepository.getAllMaLopByMaKhoa(item));
            loadValuesOfCombox(machuyennganh, chuyenNganhRepository.getMCNByMaKhoa(item));
        }

        private void malop_SelectedValueChanged(object sender, EventArgs e)
        {
            string item = malop.GetItemText(malop.SelectedItem);
            DataTable dt = svRepository.getStudentsByMaLop(item);
            dataGridView1.DataSource = dt;
        }

        public bool isValid()
        {
            if(msv.Text.Trim() == "" || hoten.Text.Trim() == "" || makhoa.SelectedIndex == -1 || malop.SelectedIndex == -1 ||
               machuyennganh.SelectedIndex == -1 || que.SelectedIndex == -1 || dantoc.SelectedIndex == -1 || gioitinh.SelectedIndex == -1 || 
               machucvu.SelectedIndex == -1 || mahdt.SelectedIndex == -1 || !ngaysinh.MaskCompleted)
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin", "Nhập thiếu thông tin", MessageBoxButtons.OK);
                return false;
            }

            if(msv.Text.Length > 10)
            {
                MessageBox.Show("", "Nhập thông tin không hợp lệ", MessageBoxButtons.OK);
                return false;
            }

            if(hoten.Text.Length > 50)
            {
                MessageBox.Show("", "Nhập thông tin không hợp lệ", MessageBoxButtons.OK);
                return false;
            }

            DateTime? dt = splitDateOfBirth();
            if(dt != null)
            {
                if(DateTime.Compare((DateTime)dt, DateTime.Now) > 0)
                {
                    MessageBox.Show("Ngày sinh không được lớn hơn thời gian hiện tại", "Nhập ngày sinh không hợp lê", MessageBoxButtons.OK);
                    return false;
                }
                    
                if(DateTime.Now.Year - dt.Value.Year < 18)
                {
                    MessageBox.Show("Tuổi của sinh viên không được nhỏ hơn 18", "Nhập ngày sinh không hợp lê", MessageBoxButtons.OK);
                    return false;
                }
                
            }

            return true;
        }

        public DateTime? splitDateOfBirth()
        {
            string[] s = ngaysinh.Text.Split('/');
            DateTime dt;
            try
            {
               dt = new DateTime(Convert.ToInt32(s[2]), Convert.ToInt32(s[1]), Convert.ToInt32(s[0]));    
               return dt;
            }
            catch
            {
                MessageBox.Show("", "Nhập ngày sinh không hợp lê", MessageBoxButtons.OK);
                return null;
            }
            
        }
      

        private void button2_Click(object sender, EventArgs e)
        {

            if (!isValid()) return;
        }
    }
}
