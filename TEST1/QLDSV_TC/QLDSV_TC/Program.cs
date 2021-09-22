using DevExpress.Skins;
using DevExpress.UserSkins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using QLDSV_TC.Forms;
using System.Data.SqlClient;
using System.Data;

namespace QLDSV_TC
{
    static class Program
    {
        //khoi tao cac ket noi
        public static SqlConnection conn = new SqlConnection();
        public static String connstr;
        public static String connstr_publisher = "Data Source=DESKTOP-Q5JKCKU;Initial Catalog=QLDSV;Integrated Security = True";

        //khoi tao cac bien khi dang nhap
        public static SqlDataReader myReader;
        public static String serverName = "";
        public static String userName = "";
        public static String login = "";
        public static String pass = "";
        public static String loaiTK = "";

        public static String database = "QLDSV";
        public static String remoteLogin = "HTKN";
        public static String remotePass = "123";
        public static String loginDN = "";
        public static String passDN = "";
        public static String group = "";
        public static String hoTen = "";
        public static int khoa = 0;


        //khoi tao danh sach phan manh
        public static BindingSource bds_DSPM = new BindingSource();//giu bds_ds phan manh dang nhap

        //khoi tao frm chinh va Login
        public static frmMain frmChinh;
        public static frmLogin frmDangNhap;

        //ket noi csdl doi voi dang nhap la giang vien
        public static int ketNoi()
        {
            if (conn != null && Program.conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            try
            {
                connstr = "Data Source=" + Program.serverName + ";Initial Catalog=" + Program.database +
                    ";User ID=" + Program.login + ";password=" + Program.pass;
                Program.conn.ConnectionString = Program.connstr;
                Program.conn.Open();
                return 1;
            }
            catch (Exception e)
            {
                MessageBox.Show("Lỗi kết nối csdl gốc.\nBạn xem lại tên đăng nhập và password.\n" + e.Message);
                return 0;
            }
        }

        //ket noi csdl doi voi dang nhap la sinh vien
        public static int ketNoi_SV()
        {
            if (conn != null && Program.conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            try
            {
                connstr = "Data Source=" + Program.serverName + ";Initial Catalog=" + Program.database +
                    ";User ID=SINHVIEN; password = 123";
                Program.conn.ConnectionString = Program.connstr;
                Program.conn.Open();
                return 1;
            }
            catch (Exception e)
            {
                MessageBox.Show("Lỗi kết nối csdl gốc.\nBạn xem lại tên đăng nhập và password.\n" + e.Message);
                return 0;
            }
        }

        //thuc thi cau lenh tra ve reader (du lieu chi co the xem), tai ve nhanh hon do chi xem
        public static SqlDataReader ExecSqlDataReader(String strLenh)
        {
            SqlDataReader myReader;
            SqlCommand sqlcmd = new SqlCommand(strLenh, Program.conn);

            //xác định kiểu lệnh cho sqlcmd là kiểu text.
            sqlcmd.CommandType = CommandType.Text;
            sqlcmd.CommandTimeout = 600;
            if (Program.conn.State == ConnectionState.Closed) Program.conn.Open();
            try
            {
                myReader = sqlcmd.ExecuteReader();
                return myReader;
            }
            catch (SqlException ex)
            {
                Program.conn.Close();
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        //thuc thi cau lenh, tra ve la datatable co the them, sua, xoa, ..=> tai ve cham hon
        public static DataTable ExecSqlDataTable(String cmd)
        {
            DataTable dt = new DataTable();
            if (Program.conn.State == ConnectionState.Closed) Program.conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd, conn);
            da.Fill(dt);
            conn.Close();
            return dt;
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            frmDangNhap = new frmLogin();
            Application.Run(frmDangNhap);
        }
    }
}
