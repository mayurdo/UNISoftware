using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Linq.Expressions;
using System.IO;
using System.Diagnostics;

namespace UNIRecruitmentSoft
{
    public partial class CandidateReport : Form
    {
        PropertyInfo[] columnPropertyInfos;

        private GridPagination<CandidateDetail> pagination;

        UniDBDataContext uniDb;

        public CandidateReport()
        {
            InitializeComponent();

            columnPropertyInfos = typeof(CandidateDetail).GetProperties();
            pagination = new GridPagination<CandidateDetail>();
            //pagination.ReportFor = "Candidate";

            uniDb = new UniDBDataContext();
        }

        #region user define functions

        private void BindGridViewWithFilter()
        {
            List<long> listAge = CommanFuction.GetLongFromString(txtAge.Text);
            List<long> listExpMonth = CommanFuction.GetLongFromString(txtExperience.Text);

            pagination.ReportData = uniDb.CandidateDetails.Where(c => c.Name.Contains(txtName.Text)
                                                                    && ((cmbGender.Text != "" && cmbGender.Text != "Both") ? (c.Gender == cmbGender.Text) : true)
                                                                    && ((txtAge.Text != "") ? listAge.Contains(c.Age) : true)
                                                                     && ((cmbMarritalStatus.Text != "" && cmbMarritalStatus.Text != "Both") ? (c.MarritalStatus == cmbMarritalStatus.Text) : true)
                                                                    && c.Qualification.Contains(txtQualification.Text)
                                                                    && c.JobTitle.Contains(txtJobTital.Text)
                                                                    && (c.PreferedLocation.Contains(txtLocation.Text) || c.PreferedLocation == "Any")
                                                                    && ((txtExperience.Text != "") ? listExpMonth.Contains(c.Experience) : true)
                                                                    && c.Placed == cbPlacedCandidate.Checked
                                                                    && c.Registered == cbRegisteredCandidate.Checked
                                                                    && c.IsDeleted == false
                                                                    && (cbSearchByDate.Checked ?
                                                                        (c.ProcessedDate.Date >= dtpDateFrom.Value.Date && c.ProcessedDate.Date <= dtpDateTo.Value.Date)
                                                                        : true));

            //bindingNavigatorPositionItem.Text = pagination.PageNo.ToString();
            if (bindingNavigatorPositionItem.Text != pagination.TotalItem.ToString())
            {
                bindingSource1.DataSource = Enumerable.Range(1, pagination.TotalPage);
                bindingNavigator1.BindingSource = bindingSource1;
            }

            BindGridView();
        }

        private void BindGridView()
        {
            dataGridView1.DataSource = pagination.PageData;
            dataGridView1.Columns["ProcessedDate"].DefaultCellStyle.Format = "dd/MM/yyyy";
            lblReportStatus.Text = pagination.GetReportStatus();
        }

        private void AutoComplete(TextBox txtBox, string[] items)
        {
            var itemCollection = new AutoCompleteStringCollection();
            itemCollection.AddRange(items);
            txtBox.AutoCompleteCustomSource = itemCollection;
            txtBox.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        #endregion

        #region Form Events

        private void CandidateReport_Load(object sender, EventArgs e)
        {
            BindGridViewWithFilter();
            lblHeader.Width = this.Width;
            panel1.Width = this.Width - 40;
            dataGridView1.Width = panel1.Width;
            buttonRecycle.Location = new Point(12, buttonRecycle.Location.Y);
            btnExportExcel.Location = new Point(100, btnExportExcel.Location.Y);



            AutoComplete(txtName, pagination.ReportData.Select(x => x.Name).Distinct().ToArray());
            AutoComplete(txtLocation, pagination.ReportData.Select(x => x.PreferedLocation).Distinct().ToArray());
            AutoComplete(txtQualification, pagination.ReportData.Select(x => x.Qualification).Distinct().ToArray());
            AutoComplete(txtExperience, pagination.ReportData.Select(x => x.Experience.ToString()).Distinct().ToArray());
            AutoComplete(txtJobTital, pagination.ReportData.Select(x => x.CurrentCTC).Distinct().ToArray());
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {

        }

        private void txtLocation_TextChanged(object sender, EventArgs e)
        {
            BindGridViewWithFilter();
        }

        #endregion

        #region change page

        private void bindingNavigatorMovePreviousItem_Click(object sender, EventArgs e)
        {
            pagination.GoToPriviousPage();
            BindGridView();
        }

        private void bindingNavigatorMoveNextItem_Click(object sender, EventArgs e)
        {
            pagination.GoToNextPage();
            BindGridView();
        }

        private void bindingNavigatorMoveLastItem_Click(object sender, EventArgs e)
        {
            pagination.GoToLastPage();
            BindGridView();
        }

        private void bindingNavigatorMoveFirstItem_Click(object sender, EventArgs e)
        {
            pagination.GoToFirstPage();
            BindGridView();
        }

        private void bindingNavigatorPositionItem_TextChanged(object sender, EventArgs e)
        {
            int pageNo;
            if (!int.TryParse(bindingNavigatorPositionItem.Text, out pageNo))
                return;

            pagination.GoToPage(pageNo);
            BindGridView();
        }

        #endregion

        #region buttons

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            var frm = new Candidatefrm();
            frm.ShowDialog();

            uniDb.Refresh(System.Data.Linq.RefreshMode.KeepChanges, pagination.PageData);
            BindGridView();
        }

        private void bindingNavigatorEditItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count < 1)
                return;

            var srNo = Convert.ToInt64(dataGridView1.SelectedRows[0].Cells["SrNo"].Value);

            // ask for password
            Loginfrm frmLogin = new Loginfrm(HomePage.ExecutiveName);
            frmLogin.ShowDialog();

            if (!frmLogin.IsLogedIn)
                return;

            var frm = new Candidatefrm(srNo);
            frm.ShowDialog();

            uniDb.Refresh(System.Data.Linq.RefreshMode.KeepChanges, pagination.PageData);
            BindGridView();
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count < 1)
                return;

            //var srNo = Convert.ToInt64(dataGridView1.SelectedRows.Cells["SrNo"].Value);

            var srNos = new List<long>();
            for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
            {
                srNos.Add(Convert.ToInt64(dataGridView1.SelectedRows[i].Cells["SrNo"].Value));
            }

            var result = MessageBox.Show(string.Format("Do you want to delete SrNos : {0}",
                                            string.Join(",", srNos.Select(x => x.ToString()).ToArray())),
                                                                        "Delete Alert", MessageBoxButtons.YesNo);

            if (result != DialogResult.Yes)
                return;

            // ask for password
            Loginfrm frm = new Loginfrm(HomePage.ExecutiveName);
            frm.ShowDialog();

            if (!frm.IsLogedIn)
                return;

            //uniDb.CandidateDetails.DeleteOnSubmit(pagination.PageData.Single(x => x.SrNo == srNo));
            //uniDb.SubmitChanges();
            var candidates = uniDb.CandidateDetails.Where(x => srNos.Contains(x.SrNo)).ToList();

            foreach (var candidate in candidates)
            {
                candidate.IsDeleted = true;
                candidate.ExecutiveName = HomePage.ExecutiveName;
            }
            uniDb.SubmitChanges();

            uniDb.Refresh(System.Data.Linq.RefreshMode.KeepChanges, pagination.PageData);
            BindGridView();
        }

        private void markAsSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count < 1)
                return;

            var srNo = Convert.ToInt64(dataGridView1.SelectedRows[0].Cells["SrNo"].Value);

            var result = MessageBox.Show(string.Format("Do you want to Mark SrNo : {0} As Selected ?", srNo), "Selected Alert", MessageBoxButtons.YesNo);

            if (result != DialogResult.Yes)
                return;

            var candidate = uniDb.CandidateDetails.Single(x => x.SrNo == srNo);
            candidate.Placed = true;
            uniDb.SubmitChanges();

            uniDb.Refresh(System.Data.Linq.RefreshMode.KeepChanges, pagination.PageData);
            BindGridView();
        }

        #endregion

        private void txtExperience_KeyPress(object sender, KeyPressEventArgs e)
        {
            CommanFuction.AcceptOnlySearchingNumber(e);
        }

        private void showResumeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count < 1)
                return;

            var srNo = Convert.ToInt64(dataGridView1.SelectedRows[0].Cells["SrNo"].Value);
            var cvPath = pagination.PageData.Single(x => x.SrNo == srNo).CVPath;

            if (string.IsNullOrEmpty(cvPath))
            {
                MessageBox.Show("CV not attached", "Error Message", MessageBoxButtons.OK);
                return;
            }

            var fileInfo = new FileInfo(cvPath);
            if (!fileInfo.Exists)
            {
                MessageBox.Show("CV not found", "Error Message", MessageBoxButtons.OK);
                return;
            }

            Process.Start(fileInfo.FullName);
        }

        private void sendMailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count < 1)
                return;

            var srNo = Convert.ToInt64(dataGridView1.SelectedRows[0].Cells["SrNo"].Value);
            var candidate = pagination.PageData.Single(x => x.SrNo == srNo);

            if (string.IsNullOrEmpty(candidate.Email))
            {
                MessageBox.Show("Candidate don't have email id", "Error Message", MessageBoxButtons.OK);
                return;
            }

            CommanFuction.OpenOutlook(candidate);
        }

        private void sendMessageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count < 1)
                return;

            var srNo = Convert.ToInt64(dataGridView1.SelectedRows[0].Cells["SrNo"].Value);
            var candidate = pagination.PageData.Single(x => x.SrNo == srNo);

            if (string.IsNullOrEmpty(candidate.MobileNo))
            {
                MessageBox.Show("Candidate don't have mobile number", "Error Message", MessageBoxButtons.OK);
                return;
            }

            var frm = new SendSMSfrm(candidate);
            frm.ShowDialog();
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new CandidateRDLSReportPrint(pagination.ReportData);
            frm.ShowDialog();
        }

        private void recalculateAgeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Loginfrm frm = new Loginfrm(HomePage.ExecutiveName);
            frm.ShowDialog();

            if (!frm.IsLogedIn)
                return;

            uniDb.ReCalculateAge();
            uniDb.Refresh(System.Data.Linq.RefreshMode.KeepChanges, pagination.PageData);
            BindGridView();
            MessageBox.Show("Age Recalculate Successfull", "Success Message", MessageBoxButtons.OK);
        }

        private void showCallLatter1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var candidate = GetSelectedCandidate();

            if (candidate == null)
                return;

            ShowFile(candidate.CallLatter1);
        }

        private CandidateDetail GetSelectedCandidate()
        {
            if (dataGridView1.SelectedRows.Count < 1)
                return null;

            var srNo = Convert.ToInt64(dataGridView1.SelectedRows[0].Cells["SrNo"].Value);
            return pagination.PageData.Single(x => x.SrNo == srNo);
        }

        private void ShowFile(string FileFullName)
        {
            if (string.IsNullOrEmpty(FileFullName))
            {
                MessageBox.Show("File not attached", "Error Message", MessageBoxButtons.OK);
                return;
            }

            var fileInfo = new FileInfo(FileFullName);
            if (!fileInfo.Exists)
            {
                MessageBox.Show(string.Format("File :{0} not found", FileFullName), "Error Message", MessageBoxButtons.OK);
                return;
            }

            Process.Start(fileInfo.FullName);
        }

        private void callLatter2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var candidate = GetSelectedCandidate();

            if (candidate == null)
                return;

            ShowFile(candidate.CallLatter2);
        }

        private void callLatter3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var candidate = GetSelectedCandidate();

            if (candidate == null)
                return;

            ShowFile(candidate.CallLatter3);
        }

        private void callLatter5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var candidate = GetSelectedCandidate();

            if (candidate == null)
                return;

            ShowFile(candidate.CallLatter4);
        }

        private void callLatter5ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var candidate = GetSelectedCandidate();

            if (candidate == null)
                return;

            ShowFile(candidate.CallLatter5);
        }

        private void buttonRecycle_Click(object sender, EventArgs e)
        {
            var frm = new CandidateRecycle();
            frm.ShowDialog();
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            //CommanFuction.Export_To_Excel2<CandidateDetail>(pagination.ReportData.ToList(), "Candidate Report");
            CommanFuction.Export_To_Excel(ref dataGridView1, "Candidate Report");
        }
    }
}