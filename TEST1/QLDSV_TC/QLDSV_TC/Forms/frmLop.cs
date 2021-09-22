using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLDSV_TC.Forms
{
    public partial class frmLop : Form
    {
        public frmLop()
        {
            InitializeComponent();
        }

        private void lOPBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bdsLop.EndEdit();
            this.tableAdapterManager.UpdateAll(this.DS1);

        }

        private void loadInitializeData()
        {
            DS1.EnforceConstraints = false;
            this.LOPTableAdapter.Connection.ConnectionString = Program.connstr;
            this.SINHVIENTableAdapter.Connection.ConnectionString = Program.connstr;
            this.LOPTableAdapter.Fill(this.DS1.LOP);
            this.SINHVIENTableAdapter.Fill(this.DS1.SINHVIEN);
        }

        private void frmLop_Load(object sender, EventArgs e)
        {
            //load form
            loadInitializeData();

            Program.bds_DSPM.Filter = "KHOA LIKE 'KHOA%'";
            Utils.getKhoaToComBo(cmbKhoa, Program.bds_DSPM);
        }

        private void cmbKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            Utils.changeCombobox(this.cmbKhoa);

            // kết nối database với dữ liệu ở đoạn code trên và fill dữ liệu, nếu như có lỗi thì
            // thoát.
            if (Program.ketNoi() == 0)
            {
                MessageBox.Show("Lỗi kết nối về chi nhánh mới", "", MessageBoxButtons.OK);
            }
            else
            {
                loadInitializeData();
                /*this.txtMaKhoa.EditValue = Utils.GetMaKhoa();*/
            }
        }
    }
}
