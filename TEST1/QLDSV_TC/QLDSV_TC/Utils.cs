using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLDSV_TC
{
    class Utils
    {   //load khoa vao combobox
        public static void getKhoaToComBo(ComboBox combo, Object data)
        {
            combo.DataSource = data;
            combo.DisplayMember = "KHOA";
            combo.ValueMember = "TENSERVER";

            // lệnh này quan trọng... phải bỏ vào. ==> để cho combo box chạy đúng.
            combo.SelectedIndex = 1;

            // nếu login vào là khoa cntt, thì combox sẽ hiện khoa cntt
            // nếu login vào là khoa vt, thì combox sẽ hiện khoa vt
            combo.SelectedIndex = Program.khoa;
        }



        // hỗ trợ trong combobox chọn chi nhánh
        public static void changeCombobox(ComboBox cmb)
        {
            // bắt lỗi khi giá trị của selectedvalue = "sysem.data.datarowview"  ==> lỗi hay gặp của combobox winform
            if (cmb.SelectedValue.ToString() == "System.Data.DataRowView")
                return;

            // gán server đã chọn vào biến toàn cục.
            Program.serverName = cmb.SelectedValue.ToString();
            Console.WriteLine("Servername : " + Program.serverName);

            // đoạn code hỗ trợ chuyển chi nhánh
            // ở chi nhánh A qua B thì dùng RemoteLogin,
            if (cmb.SelectedIndex != Program.khoa)
            {
                Program.login = Program.remoteLogin;
                Program.pass = Program.remotePass;
            }
            else
            { // ở B về lại A dùng login ban đầu
                Program.login = Program.loginDN;
                Program.pass = Program.passDN;
            }
        }
    }
}
