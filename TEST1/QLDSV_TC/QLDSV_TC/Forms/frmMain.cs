using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QLDSV_TC.Forms;

namespace QLDSV_TC
{
    public partial class frmMain : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        //Tao bien kiem tra form thoat lun hay dang xuat
        public Boolean checkExit = true;

        public frmMain()
        {
            InitializeComponent();
        }

        //Kiem tra 1 form co ton tai
        private void openForm(Type ftype)
        {
            foreach (Form f in this.MdiChildren)
                if (f.GetType() == ftype)
                {
                    f.Activate();
                    return;
                }
            Form form = (Form)Activator.CreateInstance(ftype);
            form.MdiParent = this;
            form.Show();
        }

        private 
        

        void btnLogout_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.checkExit = false;
            Program.frmChinh.Dispose();
            Program.frmDangNhap.Visible = true;
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(this.checkExit == true)
            {
                Program.frmChinh.Close();
            }
        }

        private void btnLop_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.openForm(typeof(frmLop));
        }
    }
}
