using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;

namespace QLDSV_TC.Forms
{
    public partial class frmDKMH : DevExpress.XtraEditors.XtraForm
    {
        public frmDKMH()
        {
            InitializeComponent();
        }

        private void loadDSMH(String nienKhoa, String hocKy)
        {
            
            String strLenh = "EXEC SP_DKMH '" + nienKhoa + "', '" + hocKy + "'";
            DataTable db = new DataTable();
            db = Program.ExecSqlDataTable(strLenh);

            BindingSource bsDSMH = new BindingSource();
            bsDSMH.DataSource = db;
            gridDSMH.DataSource = bsDSMH;
            gridDSMH.Refresh();
        }

        private void gridDKMH_Load(object sender, EventArgs e)
        {
            BindingSource bsDSMH = new BindingSource();
            gridDKMH.DataSource = bsDSMH;
            gridDKMH.Refresh();
        }


        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (Program.ketNoi_SV() == 0) return;
            String nienKhoa = txtNienKhoa.Text.Trim();
            String hocKy = numHK.Value.ToString();
            this.loadDSMH(nienKhoa, hocKy);
        }

        private void btnDK_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gridView1.SelectedRowsCount; i++)
            {
                String maLT = gridView1.GetRowCellValue(gridView1.GetSelectedRows()[i], "MALTC").ToString();
                String maMH = gridView1.GetRowCellValue(gridView1.GetSelectedRows()[i], "MAMH").ToString();
                String tenMH = gridView1.GetRowCellValue(gridView1.GetSelectedRows()[i], "TENMH").ToString();
                String nhom = gridView1.GetRowCellValue(gridView1.GetSelectedRows()[i], "NHOM").ToString();
                String GV = gridView1.GetRowCellValue(gridView1.GetSelectedRows()[i], "HOTEN").ToString();

                gridView2.AddNewRow();
                gridView2.SetRowCellValue(GridControl.NewItemRowHandle, "MALTC", maLT);
                gridView2.SetRowCellValue(GridControl.NewItemRowHandle, "MAMH", maMH);
                gridView2.SetRowCellValue(GridControl.NewItemRowHandle, "TENMH", tenMH);
                gridView2.SetRowCellValue(GridControl.NewItemRowHandle, "NHOM", nhom);
                gridView2.SetRowCellValue(GridControl.NewItemRowHandle, "HOTEN", GV);
            }
        }

        
    }
}