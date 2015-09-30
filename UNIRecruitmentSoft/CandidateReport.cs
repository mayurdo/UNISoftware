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

        private readonly GridPagination<CandidateDetail> pagination;

        readonly UniDBDataContext uniDb;

        public CandidateReport()
        {
            InitializeComponent();

            pagination = new GridPagination<CandidateDetail>();
            //pagination.ReportFor = "Candidate";

            uniDb = new UniDBDataContext();
        }

        #region user define functions

        protected void BindGridViewWithFilter()
        {
            List<long> listAge = CommanFuction.GetLongFromString(txtAge.Text);
            List<long> listExpMonth = CommanFuction.GetLongFromString(txtExperience.Text);
            List<long> listCandidateCode = CommanFuction.GetLongFromString(txtCandidateCode.Text);

            var addQulifications = (from object item in lbJobTital.SelectedItems select item.ToString()).ToList();

            pagination.ReportData = uniDb.CandidateDetails.Where(c => c.Name.Contains(txtName.Text)
                                                                    && ((txtCandidateCode.Text == "") || listCandidateCode.Contains(c.CandidateCode.Value))
                                                                    && ((txtMobileNo.Text == "") || ((c.MobileNo == txtMobileNo.Text) || (c.LandLineNo == txtMobileNo.Text)))
                                                                    && ((cmbGender.Text != "" && cmbGender.Text != "Both") ? (c.Gender == cmbGender.Text) : true)
                                                                    && ((txtAge.Text != "") ? listAge.Contains(c.Age) : true)
                                                                     && ((cmbMarritalStatus.Text != "" && cmbMarritalStatus.Text != "Both") ? (c.MarritalStatus == cmbMarritalStatus.Text) : true)
                                                                    && c.Qualification.Contains(txtQualification.Text)
                                                                    && ((!addQulifications.Any()) || addQulifications.Contains(c.JobTitle))
                                                                    && (c.PreferedLocation.Contains(txtLocation.Text) || c.PreferedLocation == "Any")
                                                                    && ((txtExperience.Text != "") ? listExpMonth.Contains(c.Experience) : true)
                                                                    && (!cbPlacedCandidate.Checked || c.Placed == cbPlacedCandidate.Checked)
                                                                    && (!cbRegisteredCandidate.Checked || c.Registered == cbRegisteredCandidate.Checked)
                                                                    && (!cbUnregisteredCandidate.Checked || c.Registered == !cbUnregisteredCandidate.Checked)
                                                                    && ((txtExecutiveName.Text == "") || (c.ExecutiveName == txtExecutiveName.Text))
                                                                    && c.IsDeleted == false
                                                                    && (!cbSearchByDate.Checked || (c.ModifiedDateTime.Value.Date >= dtpDateFrom.Value.Date && c.ModifiedDateTime.Value.Date <= dtpDateTo.Value.Date)));

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
            dataGridView1.Columns["DateOfBirth"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dataGridView1.Columns["ModifiedDateTime"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm tt";
            dataGridView1.Columns["IsDeleted"].Visible = false;
            dataGridView1.Columns["DeletedReason"].Visible = false;
            lblReportStatus.Text = pagination.GetReportStatus();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                var cvPath = row.Cells["CVPath"].Value;

                if (cvPath == null || string.IsNullOrEmpty(cvPath.ToString()))
                    row.DefaultCellStyle.BackColor = Color.Red;
            }
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
            //btnExportExcel.Location = new Point(100, btnExportExcel.Location.Y);



            CommanFuction.AutoComplete(txtName, pagination.ReportData.Select(x => x.Name).Distinct().ToArray());
            CommanFuction.AutoComplete(txtLocation, pagination.ReportData.Select(x => x.PreferedLocation).Distinct().ToArray());
            CommanFuction.AutoComplete(txtQualification, pagination.ReportData.Select(x => x.Qualification).Distinct().ToArray());
            CommanFuction.AutoComplete(txtExperience, pagination.ReportData.Select(x => x.Experience.ToString()).Distinct().ToArray());


            lbJobTital.Items.AddRange(pagination.ReportData.Select(x => x.JobTitle as object).Distinct().ToArray());
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
            //pagination.GoToPriviousPage();
            BindGridView();
        }

        private void bindingNavigatorMoveNextItem_Click(object sender, EventArgs e)
        {
            //pagination.GoToNextPage();
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
            //Loginfrm frmLogin = new Loginfrm(HomePage.UserDetail);
            //frmLogin.ShowDialog();

            //if (!frmLogin.IsLogedIn)
            //    return;

            var frm = new Candidatefrm(srNo);
            frm.ShowDialog();

            uniDb.Refresh(System.Data.Linq.RefreshMode.KeepChanges, pagination.PageData);
            BindGridView();
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count < 1)
                return;

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
            Loginfrm frm = new Loginfrm(HomePage.UserDetail);
            frm.ShowDialog();

            if (!frm.IsLogedIn)
                return;



            var deleteReasionfrm = new DeleteReasionfrm();
            deleteReasionfrm.ShowDialog();

            if (string.IsNullOrEmpty(DeleteReasionfrm.DeleteReasion))
                return;


            var candidates = uniDb.CandidateDetails.Where(x => srNos.Contains(x.SrNo)).ToList();

            foreach (var candidate in candidates)
            {
                candidate.IsDeleted = true;
                candidate.ExecutiveName = HomePage.UserDetail.UserId;
                candidate.ModifiedDateTime = DateTime.Now;
                candidate.DeletedReason = DeleteReasionfrm.DeleteReasion;
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

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            RowSelected();
        }

        private void RowSelected()
        {
            if (dataGridView1.SelectedRows.Count < 1)
                return;

            var cvPath = Convert.ToString(dataGridView1.SelectedRows[0].Cells["CVPath"].Value);

            toolStripButton1.Enabled = !string.IsNullOrEmpty(cvPath);
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


            var fileInfo = new FileInfo(CommanFuction.GetServerPath(cvPath));
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

            var srNos = new List<long>();
            for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
            {
                srNos.Add(Convert.ToInt64(dataGridView1.SelectedRows[i].Cells["SrNo"].Value));
            }

            //var srNo = Convert.ToInt64(dataGridView1.SelectedRows[0].Cells["SrNo"].Value);
            var candidates = pagination.PageData.Where(x => srNos.Contains(x.SrNo)).ToList();

            var emptyEmailIds = candidates.Where(x => string.IsNullOrEmpty(x.Email)).Select(x => x.Name).ToArray();
            if (emptyEmailIds.Any())
            {
                MessageBox.Show(string.Format("{0} Candidate don't have email id", string.Join(",", emptyEmailIds)), "Error Message", MessageBoxButtons.OK);
                return;
            }

            var emailIds = candidates.Select(x => (IEmail)x);
            //CommanFuction.OpenOutlook(emailIds);

            var frm = new SendEmailfrm(emailIds);
            frm.ShowDialog();
        }

        private void sendMessageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count < 1)
                return;

            var srNos = new List<long>();
            for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
            {
                srNos.Add(Convert.ToInt64(dataGridView1.SelectedRows[i].Cells["SrNo"].Value));
            }

            var candidates = pagination.PageData.Where(x => srNos.Contains(x.SrNo)).ToList();

            var emptyMobileNos = candidates.Where(x => string.IsNullOrEmpty(x.Email)).Select(x => x.Name).ToArray();
            if (emptyMobileNos.Any())
            {
                MessageBox.Show(string.Format("{0} Candidate don't have email id", string.Join(",", emptyMobileNos)), "Error Message", MessageBoxButtons.OK);
                return;
            }

            var contactDetail = candidates.Select(x => (IMobile)x);
            var frm = new SendSMSfrm(contactDetail);
            frm.ShowDialog();
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // ask for password
            Loginfrm frmLogin = new Loginfrm(HomePage.UserDetail);
            frmLogin.ShowDialog();

            if (!frmLogin.IsLogedIn)
                return;

            var frm = new CandidateRDLSReportPrint(pagination.ReportData);
            frm.ShowDialog();
        }

        private void recalculateAgeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Loginfrm frm = new Loginfrm(HomePage.UserDetail);
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

            ShowFile(candidate.CallLetter1);
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

            ShowFile(candidate.CallLetter2);
        }

        private void callLatter3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var candidate = GetSelectedCandidate();

            if (candidate == null)
                return;

            ShowFile(candidate.CallLetter3);
        }

        private void callLatter5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var candidate = GetSelectedCandidate();

            if (candidate == null)
                return;

            ShowFile(candidate.CallLetter4);
        }

        private void callLatter5ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var candidate = GetSelectedCandidate();

            if (candidate == null)
                return;

            ShowFile(candidate.CallLetter5);
        }

        private void buttonRecycle_Click(object sender, EventArgs e)
        {
            var frm = new CandidateRecycle();
            frm.ShowDialog();
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            var hasPermition = HomePage.UserDetail.Modules.Split(',').ToList().Contains(UniEnums.Modules.ExportExcel.ToString());

            if (!hasPermition)
            {
                MessageBox.Show("You don't have access to this module", "Access Message", MessageBoxButtons.OK);
                return;
            }

            Loginfrm frm = new Loginfrm(HomePage.UserDetail);
            frm.ShowDialog();

            if (!frm.IsLogedIn)
                return;

            CommanFuction.Export_To_Excel2<CandidateDetail>(pagination.ReportData.ToList(), "Candidate Report");
            //CommanFuction.Export_To_Excel(ref dataGridView1, "Candidate Report");
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnImportFromExcel_Click(object sender, EventArgs e)
        {
            if (HomePage.UserDetail.UserId != "admin")
            {
                MessageBox.Show(@"Only admin can Import data", @"Authentication Error", MessageBoxButtons.OK);
                return;
            }

            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                var excelPath = openFileDialog1.FileName;
                var fileInfo = new FileInfo(excelPath);

                bool dataSaved = false;
                long maxSrNo = 0;
                maxSrNo = uniDb.CandidateDetails.Any() ? uniDb.CandidateDetails.Select(c => c.SrNo).Max() : 0;
                var candidateList = CommanFuction.Import_From_Excel<CandidateDetail>(GetCandidateColumnList(), maxSrNo, fileInfo.FullName);
                uniDb.CandidateDetails.InsertAllOnSubmit(candidateList);

                dataSaved = (candidateList.Count > 0);
                uniDb.SubmitChanges();

                var message = dataSaved ? "Excel data saved" : "Excel data not saved";
                MessageBox.Show(message, "Message", MessageBoxButtons.OK);
            }
        }

        private static List<string> GetCandidateColumnList()
        {
            var columnList = new List<string>
            {
                "SrNo",
                "Link",
                "ProcessedDate",
                "Name",
                "CandidateCode",
                "Gender",
                "DateOfBirth",
                "Age",
                "MarritalStatus",
                "CurrentLocation",
                "PreferedLocation",
                "MobileNo",
                "LandLineNo",
                "Email",
                "Qualification",
                "AdditionalQualification",
                "Experience",
                "ExpSlap",
                "JobTitle",
                "CurrentIndustry",
                "PreferredIndustry",
                "NoticePeriod",
                "CurrentCTC",
                "ExpectedCTC",
                "NoOfCalls",
                "Remarks",
                "Registered",
                "Placed"
            };
            return columnList;
        }

        private void attachResumeFromFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void txtCandidateCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                BindGridViewWithFilter();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindGridViewWithFilter();
        }
    }
}