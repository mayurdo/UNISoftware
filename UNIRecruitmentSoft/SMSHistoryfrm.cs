using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UNIRecruitmentSoft
{
    public partial class SMSHistoryfrm : Form
    {
        private readonly ReportForm<SMSHistory> _reportForm;
        private UniEnums.VendorType _vendorType;

        public SMSHistoryfrm()
        {
            InitializeComponent();

            _reportForm = new ReportForm<SMSHistory>(bindingNavigator1, dataGridView1,
                                                      bindingNavigatorPositionItem.TextBox, lblReportStatus);
        }


        #region user define functions

        private IQueryable<SMSHistory> GetFiterQuery()
        {

            return _reportForm.UniDb.SMSHistories
                              .Where(x => x.MobileNo.Contains(txtMobileNo.Text)
                                          && (!cbSearchByDate.Checked
                                              || (x.Date >= dtpDateFrom.Value.Date
                                                  && x.Date <= dtpDateTo.Value.Date)));

        }

        #endregion


        private void SMSHistoryfrm_Load(object sender, EventArgs e)
        {
            _reportForm.BindGridViewWithFilter(GetFiterQuery());
            lblHeader.Width = this.Width;
            panel1.Width = this.Width - 40;
            dataGridView1.Width = panel1.Width;
            buttonReset.Location = new Point(buttonReset.Location.X, lblHeader.Height + 10);
        }

        #region change Page No

        private void bindingNavigatorMoveFirstItem_Click(object sender, EventArgs e)
        {
            _reportForm.MoveToFirstPage();
        }

        private void bindingNavigatorMovePreviousItem_Click(object sender, EventArgs e)
        {
            _reportForm.MoveToPriviousPage();
        }

        private void bindingNavigatorMoveNextItem_Click(object sender, EventArgs e)
        {
            _reportForm.MoveToNextPage();
        }

        private void bindingNavigatorMoveLastItem_Click(object sender, EventArgs e)
        {
            _reportForm.MoveToLastPage();
        }

        private void bindingNavigatorPositionItem_Click(object sender, EventArgs e)
        {
            _reportForm.ChangePage();
        }
        #endregion

        #region Export to Excel

        private void toolStripButtonExcel_Click(object sender, EventArgs e)
        {
            _reportForm.ExportToExcel("SMS History", GetExcelColumnList());
        }

        private List<string> GetExcelColumnList()
        {
            return new List<string>() { "SrNo", "Date", "MobileNo", "SMSText" };
        }

        #endregion

        private void txtMobileNo_TextChanged(object sender, EventArgs e)
        {
            _reportForm.BindGridViewWithFilter(GetFiterQuery());
        }

        private void txtMobileNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            CommanFuction.AcceptOnlyNumber(e);
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
