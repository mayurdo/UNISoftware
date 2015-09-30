using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Linq.Expressions;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms.VisualStyles;
using UNIFreelancerDataBaseEntry.CCandidateService;

namespace UNIFreelancerDataBaseEntry
{
    public partial class CandidateReport : Form
    {

        private CandidateDetailResponse _candidateDetailResponse;
        private CandidateDetailSearchRequest _candidateDetailSearchRequest;
        private CandidateServiceClient _client;
        private int _totalPage;

        public CandidateReport()
        {
            InitializeComponent();

            _candidateDetailResponse = new CandidateDetailResponse();
            _candidateDetailSearchRequest = new CandidateDetailSearchRequest();
            //pagination.ReportFor = "Candidate";

            _client = new CandidateServiceClient();

        }

        #region user define functions

        protected void BindGridViewWithFilter()
        {
            _candidateDetailSearchRequest.Name = txtName.Text;
            _candidateDetailSearchRequest.Gender = cmbGender.Text;
            _candidateDetailSearchRequest.AgeList = CommanFuction.GetLongFromString(txtAge.Text).ToArray();
            _candidateDetailSearchRequest.MarritalStatus = cmbMarritalStatus.Text;
            _candidateDetailSearchRequest.Qualification = txtQualification.Text;
            _candidateDetailSearchRequest.JobTitle = txtJobTital.Text;
            _candidateDetailSearchRequest.PreferedLocation = txtLocation.Text;
            _candidateDetailSearchRequest.ExpMonthList = CommanFuction.GetLongFromString(txtExperience.Text).ToArray();
            _candidateDetailSearchRequest.SearchByProcessedDate = cbSearchByDate.Checked;
            _candidateDetailSearchRequest.FromProcessedDate = dtpDateFrom.Value.Date;
            _candidateDetailSearchRequest.ToProcessedDate = dtpDateTo.Value.Date;
            _candidateDetailSearchRequest.PageNo = 1;
            _candidateDetailSearchRequest.PageSize = 20;

            //pagination.ReportData = uniDb.CandidateDetails.Where(c => c.Name.Contains(txtName.Text)
            //                                                        && ((cmbGender.Text != "" && cmbGender.Text != "Both") ? (c.Gender == cmbGender.Text) : true)
            //                                                        && ((txtAge.Text != "") ? listAge.Contains(c.Age) : true)
            //                                                         && ((cmbMarritalStatus.Text != "" && cmbMarritalStatus.Text != "Both") ? (c.MarritalStatus == cmbMarritalStatus.Text) : true)
            //                                                        && c.Qualification.Contains(txtQualification.Text)
            //                                                        && c.JobTitle.Contains(txtJobTital.Text)
            //                                                        && (c.PreferedLocation.Contains(txtLocation.Text) || c.PreferedLocation == "Any")
            //                                                        && ((txtExperience.Text != "") ? listExpMonth.Contains(c.Experience) : true)
            //                                                        && (!cbPlacedCandidate.Checked || c.Placed == cbPlacedCandidate.Checked)
            //                                                        && (!cbRegisteredCandidate.Checked || c.Registered == cbRegisteredCandidate.Checked)
            //                                                        && c.IsDeleted == false
            //                                                        && (cbSearchByDate.Checked ?
            //                                                            (c.ProcessedDate.Date >= dtpDateFrom.Value.Date && c.ProcessedDate.Date <= dtpDateTo.Value.Date)
            //                                                            : true));




            ////bindingNavigatorPositionItem.Text = pagination.PageNo.ToString();
            //if (bindingNavigatorPositionItem.Text != pagination.TotalItem.ToString())
            //{
            //    bindingSource1.DataSource = Enumerable.Range(1, pagination.TotalPage);
            //    bindingNavigator1.BindingSource = bindingSource1;
            //}

            BindGridView();
        }

        private void BindGridView()
        {
            _candidateDetailResponse = _client.GetCandidates(_candidateDetailSearchRequest);

            if (bindingNavigatorPositionItem.Text != _candidateDetailResponse.TotalItem.ToString(CultureInfo.InvariantCulture))
            {
                var extraPage = (_candidateDetailResponse.TotalItem % _candidateDetailSearchRequest.PageSize) > 0 ? 1 : 0;
                _totalPage = ((int)(_candidateDetailResponse.TotalItem / _candidateDetailSearchRequest.PageSize) + extraPage);

                bindingSource1.DataSource = Enumerable.Range(1, _totalPage);
                bindingNavigator1.BindingSource = bindingSource1;
            }

            dataGridView1.DataSource = _candidateDetailResponse.PageData;
            dataGridView1.Columns["ProcessedDate"].DefaultCellStyle.Format = "dd/MM/yyyy";
            
            //lblReportStatus.Text = _candidateDetailResponse.GetReportStatus();
        }


        public string GetCondition<T>(Expression<Func<T, bool>> expression)
        {
            var expBody = expression.Body.ToString();

            var parameterName = expression.Parameters[0].Name;
            var parameterType = expression.Parameters[0].Type.Name;

            var conditionString = expBody.Replace(parameterName + ".", parameterType + ".")
                                            .Replace("AndAlso", "And");

            return conditionString;
        }

        #endregion

        #region Form Events

        private void CandidateReport_Load(object sender, EventArgs e)
        {
            BindGridViewWithFilter();
            lblHeader.Width = this.Width;
            panel1.Width = this.Width - 40;
            dataGridView1.Width = panel1.Width;
            btnSearch.Location = new Point(cbRegisteredCandidate.Location.X + 160, btnSearch.Location.Y);
            //btnExportExcel.Location = new Point(100, btnExportExcel.Location.Y);



            //CommanFuction.AutoComplete(txtName, pagination.ReportData.Select(x => x.Name).Distinct().ToArray());
            //CommanFuction.AutoComplete(txtLocation, pagination.ReportData.Select(x => x.PreferedLocation).Distinct().ToArray());
            //CommanFuction.AutoComplete(txtQualification, pagination.ReportData.Select(x => x.Qualification).Distinct().ToArray());
            //CommanFuction.AutoComplete(txtExperience, pagination.ReportData.Select(x => x.Experience.ToString()).Distinct().ToArray());
            //CommanFuction.AutoComplete(txtJobTital, pagination.ReportData.Select(x => x.CurrentCTC).Distinct().ToArray());
        }

       
     

        #endregion

        #region change page

        private void bindingNavigatorMovePreviousItem_Click(object sender, EventArgs e)
        {
            _candidateDetailSearchRequest.PageNo -= 1;

            //pagination.GoToPriviousPage();
            BindGridView();
        }

        private void bindingNavigatorMoveNextItem_Click(object sender, EventArgs e)
        {
            _candidateDetailSearchRequest.PageNo += 1;

            //pagination.GoToNextPage();
            BindGridView();
        }

        private void bindingNavigatorMoveLastItem_Click(object sender, EventArgs e)
        {
            _candidateDetailSearchRequest.PageNo = _totalPage;

            //pagination.GoToLastPage();
            BindGridView();
        }

        private void bindingNavigatorMoveFirstItem_Click(object sender, EventArgs e)
        {
            _candidateDetailSearchRequest.PageNo = 1;

            //pagination.GoToFirstPage();
            BindGridView();
        }

        private void bindingNavigatorPositionItem_TextChanged(object sender, EventArgs e)
        {
            int pageNo;
            if (!int.TryParse(bindingNavigatorPositionItem.Text, out pageNo))
                return;

            _candidateDetailSearchRequest.PageNo = pageNo;
            //pagination.GoToPage(pageNo);
            BindGridView();
        }

        #endregion

        #region buttons

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            //var frm = new Candidatefrm();
            //frm.ShowDialog();

            //uniDb.Refresh(System.Data.Linq.RefreshMode.KeepChanges, pagination.PageData);
            //BindGridView();
        }

        private void bindingNavigatorEditItem_Click(object sender, EventArgs e)
        {
            //if (dataGridView1.SelectedRows.Count < 1)
            //    return;

            //var srNo = Convert.ToInt64(dataGridView1.SelectedRows[0].Cells["SrNo"].Value);

            //// ask for password
            //Loginfrm frmLogin = new Loginfrm(HomePage.UserDetail);
            //frmLogin.ShowDialog();

            //if (!frmLogin.IsLogedIn)
            //    return;

            //var frm = new Candidatefrm(srNo);
            //frm.ShowDialog();

            //uniDb.Refresh(System.Data.Linq.RefreshMode.KeepChanges, pagination.PageData);
            //BindGridView();
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            //if (dataGridView1.SelectedRows.Count < 1)
            //    return;

            //var srNos = new List<long>();
            //for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
            //{
            //    srNos.Add(Convert.ToInt64(dataGridView1.SelectedRows[i].Cells["SrNo"].Value));
            //}

            //var result = MessageBox.Show(string.Format("Do you want to delete SrNos : {0}",
            //                                string.Join(",", srNos.Select(x => x.ToString()).ToArray())),
            //                                                            "Delete Alert", MessageBoxButtons.YesNo);

            //if (result != DialogResult.Yes)
            //    return;

            //// ask for password
            //Loginfrm frm = new Loginfrm(HomePage.UserDetail);
            //frm.ShowDialog();

            //if (!frm.IsLogedIn)
            //    return;

            //var candidates = uniDb.CandidateDetails.Where(x => srNos.Contains(x.SrNo)).ToList();

            //foreach (var candidate in candidates)
            //{
            //    candidate.IsDeleted = true;
            //    candidate.ExecutiveName = HomePage.UserDetail.UserId;
            //}
            //uniDb.SubmitChanges();

            //uniDb.Refresh(System.Data.Linq.RefreshMode.KeepChanges, pagination.PageData);
            //BindGridView();
        }

        private void markAsSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (dataGridView1.SelectedRows.Count < 1)
            //    return;

            //var srNo = Convert.ToInt64(dataGridView1.SelectedRows[0].Cells["SrNo"].Value);

            //var result = MessageBox.Show(string.Format("Do you want to Mark SrNo : {0} As Selected ?", srNo), "Selected Alert", MessageBoxButtons.YesNo);

            //if (result != DialogResult.Yes)
            //    return;

            //var candidate = uniDb.CandidateDetails.Single(x => x.SrNo == srNo);
            //candidate.Placed = true;
            //uniDb.SubmitChanges();

            //uniDb.Refresh(System.Data.Linq.RefreshMode.KeepChanges, pagination.PageData);
            //BindGridView();
        }

        #endregion

        private void txtExperience_KeyPress(object sender, KeyPressEventArgs e)
        {
            CommanFuction.AcceptOnlySearchingNumber(e);
        }

        private void showResumeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (dataGridView1.SelectedRows.Count < 1)
            //    return;

            //var srNo = Convert.ToInt64(dataGridView1.SelectedRows[0].Cells["SrNo"].Value);
            //var cvPath = pagination.PageData.Single(x => x.SrNo == srNo).CVPath;

            //if (string.IsNullOrEmpty(cvPath))
            //{
            //    MessageBox.Show("CV not attached", "Error Message", MessageBoxButtons.OK);
            //    return;
            //}


            //var fileInfo = new FileInfo(CommanFuction.GetServerPath(cvPath));
            //if (!fileInfo.Exists)
            //{
            //    MessageBox.Show("CV not found", "Error Message", MessageBoxButtons.OK);
            //    return;
            //}

            //Process.Start(fileInfo.FullName);
        }

        private void sendMailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (dataGridView1.SelectedRows.Count < 1)
            //    return;

            //var srNos = new List<long>();
            //for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
            //{
            //    srNos.Add(Convert.ToInt64(dataGridView1.SelectedRows[i].Cells["SrNo"].Value));
            //}

            ////var srNo = Convert.ToInt64(dataGridView1.SelectedRows[0].Cells["SrNo"].Value);
            //var candidates = pagination.PageData.Where(x => srNos.Contains(x.SrNo)).ToList();

            //var emptyEmailIds = candidates.Where(x => string.IsNullOrEmpty(x.Email)).Select(x => x.Name).ToArray();
            //if (emptyEmailIds.Any())
            //{
            //    MessageBox.Show(string.Format("{0} Candidate don't have email id", string.Join(",", emptyEmailIds)), "Error Message", MessageBoxButtons.OK);
            //    return;
            //}

            //var emailIds = candidates.Select(x => (IEmail)x);
            //CommanFuction.OpenOutlook(emailIds);
        }

        private void sendMessageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (dataGridView1.SelectedRows.Count < 1)
            //    return;

            //var srNos = new List<long>();
            //for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
            //{
            //    srNos.Add(Convert.ToInt64(dataGridView1.SelectedRows[i].Cells["SrNo"].Value));
            //}

            //var candidates = pagination.PageData.Where(x => srNos.Contains(x.SrNo)).ToList();

            //var emptyMobileNos = candidates.Where(x => string.IsNullOrEmpty(x.Email)).Select(x => x.Name).ToArray();
            //if (emptyMobileNos.Any())
            //{
            //    MessageBox.Show(string.Format("{0} Candidate don't have email id", string.Join(",", emptyMobileNos)), "Error Message", MessageBoxButtons.OK);
            //    return;
            //}

            //var contactDetail = candidates.Select(x => (IMobile)x);
            //var frm = new SendSMSfrm(contactDetail);
            //frm.ShowDialog();
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //// ask for password
            //Loginfrm frmLogin = new Loginfrm(HomePage.UserDetail);
            //frmLogin.ShowDialog();

            //if (!frmLogin.IsLogedIn)
            //    return;

            //var frm = new CandidateRDLSReportPrint(pagination.ReportData);
            //frm.ShowDialog();
        }

        private void recalculateAgeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Loginfrm frm = new Loginfrm(HomePage.UserDetail);
            //frm.ShowDialog();

            //if (!frm.IsLogedIn)
            //    return;

            //uniDb.ReCalculateAge();
            //uniDb.Refresh(System.Data.Linq.RefreshMode.KeepChanges, pagination.PageData);
            //BindGridView();
            //MessageBox.Show("Age Recalculate Successfull", "Success Message", MessageBoxButtons.OK);
        }

        private void showCallLatter1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var candidate = GetSelectedCandidate();

            if (candidate == null)
                return;

            //ShowFile(candidate.CallLetter1);
        }

        private CandidateDetailDto GetSelectedCandidate()
        {
            if (dataGridView1.SelectedRows.Count < 1)
                return null;

            var srNo = Convert.ToInt64(dataGridView1.SelectedRows[0].Cells["SrNo"].Value);
            return _candidateDetailResponse.PageData.Single(x => x.SrNo == srNo);
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

            //ShowFile(candidate.CallLetter2);
        }

        private void callLatter3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var candidate = GetSelectedCandidate();

            if (candidate == null)
                return;

            //ShowFile(candidate.CallLetter3);
        }

        private void callLatter5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var candidate = GetSelectedCandidate();

            if (candidate == null)
                return;

            //ShowFile(candidate.CallLetter4);
        }

        private void callLatter5ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var candidate = GetSelectedCandidate();

            if (candidate == null)
                return;

            //ShowFile(candidate.CallLetter5);
        }

        private void buttonRecycle_Click(object sender, EventArgs e)
        {
            //var frm = new CandidateRecycle();
            //frm.ShowDialog();
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            //Loginfrm frm = new Loginfrm(HomePage.UserDetail);
            //frm.ShowDialog();

            //if (!frm.IsLogedIn)
            //    return;

            //CommanFuction.Export_To_Excel2<CandidateDetail>(pagination.ReportData.ToList(), "Candidate Report");
            //CommanFuction.Export_To_Excel(ref dataGridView1, "Candidate Report");
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnImportFromExcel_Click(object sender, EventArgs e)
        {
            //DialogResult result = openFileDialog1.ShowDialog();
            //if (result == DialogResult.OK)
            //{
            //    var excelPath = openFileDialog1.FileName;
            //    var fileInfo = new FileInfo(excelPath);

            //    bool dataSaved = false;
            //    long maxSrNo = 0;
            //    maxSrNo = uniDb.CandidateDetails.Any() ? uniDb.CandidateDetails.Select(c => c.SrNo).Max() : 0;
            //    var candidateList = CommanFuction.Import_From_Excel<CandidateDetail>(GetCandidateColumnList(), maxSrNo, fileInfo.FullName);
            //    uniDb.CandidateDetails.InsertAllOnSubmit(candidateList);

            //    dataSaved = (candidateList.Count > 0);
            //    uniDb.SubmitChanges();

            //    var message = dataSaved ? "Excel data saved" : "Excel data not saved";
            //    MessageBox.Show(message, "Message", MessageBoxButtons.OK);
            //}
        }

        private static List<string> GetCandidateColumnList()
        {
            var columnList = new List<string>
            {
                "SrNo",
                "Link",
                "ProcessedDate",
                "Name",
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindGridViewWithFilter();
        }
    }
}