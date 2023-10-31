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
            cb.Items.Clear();
            foreach (DataRow row in table.Rows)
            {
                cb.Items.Add(row.ItemArray[0]);
            }
        }

        private void malop_SelectedValueChanged(object sender, EventArgs e)
        {
            if ((hocky.Text != null && hocky.SelectedIndex != -1) || (hocky.Text != "" && hocky.SelectedIndex != -1))
            {
                string sql = $"select TenMon from MonHoc mh" +
                    $" inner join TKB on mh.MaMon = tkb.MaMon" +
                    $" where MaLop = '{malop.Text}' and HocKy = {hocky.Text}";
                loadValuesOfCombox(mamon, tKBRepository.getTenMonHoc(sql));
            }
        }

        private void hocky_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((malop.Text != null && malop.SelectedIndex != -1) || (malop.Text != "" && malop.SelectedIndex != -1))
            {
                string sql = $"select TenMon from MonHoc mh" +
                   $" inner join TKB on mh.MaMon = tkb.MaMon" +
                   $" where MaLop = '{malop.Text}' and HocKy = {hocky.Text}";
                loadValuesOfCombox(mamon, tKBRepository.getTenMonHoc(sql));
            }
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

            //  MessageBox.Show(e.RowIndex + " " + e.ColumnIndex, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            try
            {
                double tem = Convert.ToDouble(danhsachdiem.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                if (tem > 10 || tem < 0)
                {
                    MessageBox.Show("Điểm không hợp lệ vui lòng nhập lại !", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    danhsachdiem.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;
                    return;
                }
            }
            catch
            {
                MessageBox.Show("Điểm không hợp lệ vui lòng nhập lại !", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                danhsachdiem.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;
                return;
            }
            string key = danhsachdiem.Rows[e.RowIndex].Cells[0].Value.ToString();
            string value = danhsachdiem.Rows[e.RowIndex].Cells[6].Value.ToString();
            if(editedCells.ContainsKey(key)) editedCells[key] = value;
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
            foreach(KeyValuePair<string, string> entry in editedCells)
            {
                sql += $"update Diem set Diem = {entry.Value} where Masv = '{entry.Key}'  ";
            }

            bool res = diemRepository.updateMultiDiem(sql);
            if(res)
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
    }
}
