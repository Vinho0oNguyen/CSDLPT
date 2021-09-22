using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLDSV_TC.Forms
{
    public partial class frmLogin : DevExpress.XtraEditors.XtraForm
    {
        public SqlConnection connstr_publisher = new SqlConnection();

        //lay danh sach phan manh
        private void layDSPM(String cmd)
        {
            DataTable dt = new DataTable();
            if (connstr_publisher.State == ConnectionState.Closed) connstr_publisher.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd, connstr_publisher);
            da.Fill(dt);
            connstr_publisher.Close();
            Program.bds_DSPM.DataSource = dt;
            cmbKhoa.DataSource = Program.bds_DSPM;
            cmbKhoa.DisplayMember = "KHOA";
            cmbKhoa.ValueMember = "TENSERVER";
        }

        public frmLogin()
        {
            InitializeComponent();
        }

        //ham ket noi ve csdl goc
        private int ketNoi_CSDLGoc()
        {
            if(connstr_publisher != null && connstr_publisher.State == ConnectionState.Open)
            {
                connstr_publisher.Close();
            }
            try
            {
                connstr_publisher.ConnectionString = Program.connstr_publisher;
                connstr_publisher.Open();
                return 1;
            }
            catch (Exception e)
            {
                MessageBox.Show("Lỗi kết nối csdl gốc.\nBạn xem lại tên server của Publisher, và tên CSDL trong chuỗi kết nối.\n" + e.Message);
                return 0;
            }
        }

        //load form dang nhap ban dau
        private void frmLogin_Load(object sender, EventArgs e)
        {
            if (ketNoi_CSDLGoc() == 0) return;
            layDSPM("SELECT * FROM Get_Subscribes");
            cmbKhoa.SelectedIndex = 1; cmbKhoa.SelectedIndex = 0;
        
        }

        private void cmbKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Program.serverName = cmbKhoa.SelectedValue.ToString();
            }
            catch(Exception) { }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if(txtLogin.Text.Trim() == "" || txtPass.Text.Trim() == "")
            {
                MessageBox.Show("Tài khoảng hoặc mật khẩu không được để trống.", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            Program.login = txtLogin.Text;
            Program.pass = txtPass.Text;
            String strLenh = "";
            if (radGV.Checked)
            {
                if (Program.ketNoi() == 0) return;
                strLenh = "EXEC SP_DANGNHAP_GV '" + Program.login + "'";
            }
            else if (radSV.Checked)
            {
                if (Program.ketNoi_SV() == 0) return;
                strLenh = "EXEC SP_DANGNHAP_SV 'SINHVIEN','" + Program.login + "','" + Program.pass + "'";

            }

            Program.khoa = cmbKhoa.SelectedIndex;
            Program.loginDN = Program.login;
            Program.passDN = Program.pass;

            //thuc thi lenh reader tra ve thong tin gv dang nhap gom` 1 dong` 3 cot
            Program.myReader = Program.ExecSqlDataReader(strLenh);
            if (Program.myReader == null) return;
            Program.myReader.Read();

            //neu vao` catch thi` co nghia la` dang nhap bang sv mà sv chua co tai khoang
            try
            {
                Program.userName = Program.myReader.GetString(0);//gan du lieu username
            }
            catch (Exception)
            {
                MessageBox.Show("Nhập sai thông tin sinh viên.\nXem lại tên đăng nhập và password.", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            

            if (Convert.IsDBNull(Program.userName))
            {
                MessageBox.Show("Login name không có quyền truy cập dữ liệu.\nXem lại tài khoảng và password.", "Thông báo", MessageBoxButtons.OK);
                return;
            }

            Program.hoTen = Program.myReader.GetString(1);
            Program.group = Program.myReader.GetString(2);
            Program.conn.Close();

            Program.frmChinh = new frmMain();
            Program.frmChinh.MSV.Text = "Mã số: " + Program.userName;
            Program.frmChinh.HOTEN.Text = "Tên : " + Program.hoTen.ToUpper();
            Program.frmChinh.NHOM.Text = "Nhóm: " + Program.group;
            /*Program.frmChinh.Show();*/
            frmDKMH test = new frmDKMH();
            test.Show();
            Program.frmDangNhap.Hide();
        }

        private void checkHT_CheckedChanged(object sender, EventArgs e)
        {
            txtPass.UseSystemPasswordChar = (checkHT.Checked) ? false : true;
        }

        private void frmDangNhap_VisibleChanged(object sender, EventArgs e)
        {
            Program.bds_DSPM.RemoveFilter();
            cmbKhoa_SelectedIndexChanged(sender, e);
        }

    }
}