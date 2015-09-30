using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UNIRecruitmentSoft
{
    public partial class VisitDetailsFrm : Form
    {
        private readonly ReportForm<VisitDetail> _reportForm;

        public VisitDetailsFrm()
        {
            InitializeComponent();

            _reportForm = new ReportForm<VisitDetail>(bindingNavigator1, dataGridView1,
                                                      bindingNavigatorPositionItem.TextBox, lblReportStatus);
        }

        private void VisitDetailsFrm_Load(object sender, EventArgs e)
        {
            var query = _reportForm.UniDb.VisitDetails.Where(x => x.CompanyName.Contains(txtCompanyName.Text)
                                                                   && x.ContactPerson.Contains(txtContactPerson.Text)
                                                                   && x.VisitType.Contains(txtVisitType.Text)
                                                                   && x.ExecutiveName.Contains(txtExcecutiveName.Text)
                                                                   && (!cbSearchByDate.Checked ||
                                                                       (x.VisitDate.Date >= dtpDateFrom.Value.Date
                                                                        && x.VisitDate.Date <= dtpDateTo.Value.Date))
                                                                   && !x.IsDeleted).OrderByDescending(x => x.SrNo);
            _reportForm.BindGridViewWithFilter(query);
            lblHeader.Width = this.Width;
            panel1.Width = this.Width - 40;
            dataGridView1.Width = panel1.Width;
            var column = dataGridView1.Columns["CommentRemark"];
            column.Width = 200;
        }

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

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            _reportForm.AddNewRecord<VisitDetailsFrm>();
        }

        private void bindingNavigatorEditItem_Click(object sender, EventArgs e)
        {
            _reportForm.EditRecord<VisitDetailsFrm>();
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            _reportForm.DeleteRecord();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            _reportForm.ShowRecyclePage();
        }

        private void txtCompanyName_TextChanged(object sender, EventArgs e)
        {
            var query = _reportForm.UniDb.VisitDetails.Where(x => x.CompanyName.Contains(txtCompanyName.Text)
                                                                  && x.ContactPerson.Contains(txtContactPerson.Text)
                                                                  && x.VisitType.Contains(txtVisitType.Text)
                                                                  && x.ExecutiveName.Contains(txtExcecutiveName.Text)
                                                                  && (!cbSearchByDate.Checked ||
                                                                      (x.VisitDate.Date >= dtpDateFrom.Value.Date
                                                                       && x.VisitDate.Date <= dtpDateTo.Value.Date))
                                                                  && !x.IsDeleted).OrderByDescending(x => x.SrNo);
            _reportForm.BindGridViewWithFilter(query);
        }

        private void cbSearchByDate_CheckedChanged(object sender, EventArgs e)
        {
            var query = _reportForm.UniDb.VisitDetails.Where(x => x.CompanyName.Contains(txtCompanyName.Text)
                                                                  && x.ContactPerson.Contains(txtContactPerson.Text)
                                                                  && x.VisitType.Contains(txtVisitType.Text)
                                                                  && x.ExecutiveName.Contains(txtExcecutiveName.Text)
                                                                  && (!cbSearchByDate.Checked ||
                                                                      (x.VisitDate.Date >= dtpDateFrom.Value.Date
                                                                       && x.VisitDate.Date <= dtpDateTo.Value.Date))
                                                                  && !x.IsDeleted).OrderByDescending(x=>x.SrNo);
            _reportForm.BindGridViewWithFilter(query);
        }

        private void dtpDateFrom_ValueChanged(object sender, EventArgs e)
        {
            if (!cbSearchByDate.Checked)
                return;

            var query = _reportForm.UniDb.VisitDetails.Where(x => x.CompanyName.Contains(txtCompanyName.Text)
                                                                  && x.ContactPerson.Contains(txtContactPerson.Text)
                                                                  && x.VisitType.Contains(txtVisitType.Text)
                                                                  && x.ExecutiveName.Contains(txtExcecutiveName.Text)
                                                                  && (!cbSearchByDate.Checked ||
                                                                      (x.VisitDate.Date >= dtpDateFrom.Value.Date
                                                                       && x.VisitDate.Date <= dtpDateTo.Value.Date))
                                                                  && !x.IsDeleted).OrderByDescending(x => x.SrNo);
            _reportForm.BindGridViewWithFilter(query);
        }

        private void toolStripButtonSendMail_Click(object sender, EventArgs e)
        {

        }
    }
}
