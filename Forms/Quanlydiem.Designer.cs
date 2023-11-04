namespace QuanLyDiemSinhVien.Forms
{
	partial class Quanlydiem
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lanthi = new System.Windows.Forms.ComboBox();
            this.mamon = new System.Windows.Forms.ComboBox();
            this.hocky = new System.Windows.Forms.ComboBox();
            this.malop = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.danhsachdiem = new System.Windows.Forms.DataGridView();
            this.updatebtn = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.danhsachdiem)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(44, 79);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 37);
            this.label1.TabIndex = 16;
            this.label1.Text = "Mã lớp";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(44, 176);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 37);
            this.label2.TabIndex = 17;
            this.label2.Text = "Học kỳ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(739, 79);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(142, 37);
            this.label3.TabIndex = 20;
            this.label3.Text = "Mã môn ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(739, 176);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 37);
            this.label4.TabIndex = 21;
            this.label4.Text = "Lần thi";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lanthi);
            this.groupBox1.Controls.Add(this.mamon);
            this.groupBox1.Controls.Add(this.hocky);
            this.groupBox1.Controls.Add(this.malop);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(29, 23);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(1268, 304);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Nhập điểm";
            // 
            // lanthi
            // 
            this.lanthi.FormattingEnabled = true;
            this.lanthi.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.lanthi.Location = new System.Drawing.Point(907, 170);
            this.lanthi.Name = "lanthi";
            this.lanthi.Size = new System.Drawing.Size(98, 45);
            this.lanthi.TabIndex = 25;
            // 
            // mamon
            // 
            this.mamon.FormattingEnabled = true;
            this.mamon.Location = new System.Drawing.Point(907, 79);
            this.mamon.Name = "mamon";
            this.mamon.Size = new System.Drawing.Size(343, 45);
            this.mamon.TabIndex = 24;
            // 
            // hocky
            // 
            this.hocky.FormattingEnabled = true;
            this.hocky.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8"});
            this.hocky.Location = new System.Drawing.Point(226, 179);
            this.hocky.Name = "hocky";
            this.hocky.Size = new System.Drawing.Size(92, 45);
            this.hocky.TabIndex = 23;
            this.hocky.SelectedIndexChanged += new System.EventHandler(this.hocky_SelectedIndexChanged);
            // 
            // malop
            // 
            this.malop.FormattingEnabled = true;
            this.malop.Location = new System.Drawing.Point(226, 79);
            this.malop.Name = "malop";
            this.malop.Size = new System.Drawing.Size(371, 45);
            this.malop.TabIndex = 22;
            this.malop.SelectedValueChanged += new System.EventHandler(this.malop_SelectedValueChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(38, 338);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(229, 57);
            this.button1.TabIndex = 0;
            this.button1.Text = "Tìm Kiếm";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.danhsachdiem);
            this.groupBox2.Location = new System.Drawing.Point(29, 334);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1268, 846);
            this.groupBox2.TabIndex = 25;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Danh sách điểm";
            // 
            // danhsachdiem
            // 
            this.danhsachdiem.AllowUserToAddRows = false;
            this.danhsachdiem.AllowUserToDeleteRows = false;
            this.danhsachdiem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.danhsachdiem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.danhsachdiem.Location = new System.Drawing.Point(3, 27);
            this.danhsachdiem.Name = "danhsachdiem";
            this.danhsachdiem.RowHeadersWidth = 82;
            this.danhsachdiem.RowTemplate.Height = 33;
            this.danhsachdiem.Size = new System.Drawing.Size(1262, 816);
            this.danhsachdiem.TabIndex = 0;
            this.danhsachdiem.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.danhsachdiem_CellEndEdit);
            // 
            // updatebtn
            // 
            this.updatebtn.Location = new System.Drawing.Point(38, 488);
            this.updatebtn.Name = "updatebtn";
            this.updatebtn.Size = new System.Drawing.Size(229, 58);
            this.updatebtn.TabIndex = 26;
            this.updatebtn.Text = "Cập Nhật";
            this.updatebtn.UseVisualStyleBackColor = true;
            this.updatebtn.Click += new System.EventHandler(this.updatebtn_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button5);
            this.groupBox3.Controls.Add(this.button4);
            this.groupBox3.Controls.Add(this.button3);
            this.groupBox3.Controls.Add(this.button2);
            this.groupBox3.Controls.Add(this.updatebtn);
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Location = new System.Drawing.Point(1325, 42);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(309, 1138);
            this.groupBox3.TabIndex = 26;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Thao Tác";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(38, 957);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(229, 66);
            this.button5.TabIndex = 30;
            this.button5.Text = "button5";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(38, 789);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(229, 60);
            this.button4.TabIndex = 29;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(38, 635);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(229, 70);
            this.button3.TabIndex = 28;
            this.button3.Text = "Xuất Excel";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(38, 179);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(229, 60);
            this.button2.TabIndex = 27;
            this.button2.Text = "Làm Mới";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Quanlydiem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1662, 1209);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Quanlydiem";
            this.Text = "Quanlydiem";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.danhsachdiem)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView danhsachdiem;
        private System.Windows.Forms.ComboBox lanthi;
        private System.Windows.Forms.ComboBox mamon;
        private System.Windows.Forms.ComboBox hocky;
        private System.Windows.Forms.ComboBox malop;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button updatebtn;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button5;
	}
}