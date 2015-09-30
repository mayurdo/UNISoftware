using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UNIRecruitmentSoft
{
    public partial class CompanyDetailsfrm : Form
    {
        private readonly ReportForm<VendorDetail> _reportForm;

        private readonly GridPagination<CustomerDetail> _pagination;

        readonly UniDBDataContext _uniDb;

        readonly UniEnums.CompanyType _companyType;

        public CompanyDetailsfrm(UniEnums.CompanyType companyType)
        {
            InitializeComponent();

            _reportForm = new ReportForm<VendorDetail>(bindingNavigator1, dataGridView1,
                                                     bindingNavigatorPositionItem.TextBox, lblReportStatus);

            _pagination = new GridPagination<CustomerDetail>();
            _uniDb = new UniDBDataContext();
            _companyType = companyType;
        }

        #region user define functions

        private void BindGridViewWithFilter()
        {
           
            _pagination.ReportData = _uniDb.CustomerDetails.Where(x => x.CompanyType == _companyType.ToString()
                                                                       && x.CompanyName.Contains(txtCompanyName.Text)
                                                                       && x.ContactPerson.Contains(txtContactPerson.Text)
                                                                       && x.MobileNo.Contains(txtMobileNo.Text)
                                                                       && x.BusinessType.Contains(cmbBusinessType.Text)
                                                                       && x.Sector.Contains(cmbSector.Text)
                                                                       && x.Expert.Contains(cmbExpert.Text)
                                                                       && x.OfficeAddress.Contains(txtLocation.Text)
                                                                       && (!cbNetworking.Checked || x.Networking == cbNetworking.Checked)
                                                                       && !x.IsDeleted
                                                                       && (!cbSearchByDate.Checked
                                                                            || (x.ProcessedDate.Date >= dtpDateFrom.Value.Date
                                                                                 && x.ProcessedDate.Date <= dtpDateTo.Value.Date)));


            if (bindingNavigatorPositionItem.Text != _pagination.TotalItem.ToString(CultureInfo.InvariantCulture))
            {
                bindingSource1.DataSource = Enumerable.Range(1, _pagination.TotalPage);
                bindingNavigator1.BindingSource = bindingSource1;
            }

            BindGridView();
        }

        private void BindGridView()
        {
            dataGridView1.DataSource = _pagination.PageData;
            dataGridView1.Columns["IsDeleted"].Visible = false;
            dataGridView1.Columns["DeletedReason"].Visible = false;
            dataGridView1.Columns["CompanyType"].Visible = false;
            lblReportStatus.Text = _pagination.GetReportStatus();

            lblHeader.Text = (_companyType == UniEnums.CompanyType.Company) ? "Company Detail" : "Customer Detail";


        }

        #endregion

        private void CompanyDetailsfrm_Load(object sender, EventArgs e)
        {
            BindGridViewWithFilter();
            BindControls();

            lblHeader.Width = this.Width;
            panel1.Width = this.Width - 40;
            dataGridView1.Width = panel1.Width;
        }

        private void BindControls()
        {
            // bind text box
            CommanFuction.AutoComplete(txtCompanyName, _pagination.ReportData.Select(x => x.CompanyName).Distinct().ToArray());
            CommanFuction.AutoComplete(txtContactPerson, _pagination.ReportData.Select(x => x.ContactPerson).Distinct().ToArray());
            CommanFuction.AutoComplete(txtMobileNo, _pagination.ReportData.Select(x => x.MobileNo).Distinct().ToArray());


            // bind combo box
            CommanFuction.FillComboBox(cmbBusinessType, _pagination.ReportData.Select(x => x.BusinessType).Distinct().ToArray());
            CommanFuction.FillComboBox(cmbSector, _pagination.ReportData.Select(x => x.Sector).Distinct().ToArray());
            CommanFuction.FillComboBox(cmbExpert, _pagination.ReportData.Select(x => x.Expert).Distinct().ToArray());
        }

        private void txtCompanyName_TextChanged(object sender, EventArgs e)
        {
            BindGridViewWithFilter();
        }


        private void bindingNavigatorPositionItem_TextChanged(object sender, EventArgs e)
        {
            int pageNo;
            if (!int.TryParse(bindingNavigatorPositionItem.Text, out pageNo))
                return;

            _pagination.GoToPage(pageNo);
            BindGridView();
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            var frm = new AddNewCompany(_companyType);
            frm.ShowDialog();

            _uniDb.Refresh(System.Data.Linq.RefreshMode.KeepChanges, _pagination.PageData);
            BindGridView();
            BindControls();
        }

        private void bindingNavigatorEditItem_Click(object sender, EventArgs e)
        {
            var srNos = GetSelectedSrNo();
            if (!srNos.Any())
                return;

            var srNo = srNos[0];

            // ask for password
            Loginfrm frmLogin = new Loginfrm(HomePage.UserDetail);
            frmLogin.ShowDialog();

            if (!frmLogin.IsLogedIn)
                return;

            var frm = new AddNewCompany(srNo, _companyType);
            frm.ShowDialog();

            _uniDb.Refresh(System.Data.Linq.RefreshMode.KeepChanges, _pagination.PageData);
            BindGridView();
            BindControls();
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            var srNos = GetSelectedSrNo();
            if (!srNos.Any())
                return;

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

            var companys = _uniDb.CustomerDetails.Where(x => srNos.Contains(x.SrNo) && x.CompanyType == _companyType.ToString()).ToList();

            foreach (var company in companys)
            {
                company.IsDeleted = true;
                company.ExecutiveName = HomePage.UserDetail.UserId;
                company.ModifiedDateTime = DateTime.Now;
                company.DeletedReason = DeleteReasionfrm.DeleteReasion;
            }
            _uniDb.SubmitChanges();

            _uniDb.Refresh(System.Data.Linq.RefreshMode.KeepChanges, _pagination.PageData);
            BindGridView();
            BindControls();
        }

        private List<long> GetSelectedSrNo()
        {
            if (dataGridView1.SelectedRows.Count < 1)
                return new List<long>();

            var srNos = new List<long>();
            for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
            {
                srNos.Add(Convert.ToInt64(dataGridView1.SelectedRows[i].Cells["SrNo"].Value));
            }

            return srNos;
        }

        private void RefreshGrid()
        {
            _uniDb.Refresh(System.Data.Linq.RefreshMode.KeepChanges, _pagination.PageData);
            BindGridView();
        }

        private void toolStripButtonSendMessage_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count < 1)
                return;

            var srNos = new List<long>();
            for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
            {
                srNos.Add(Convert.ToInt64(dataGridView1.SelectedRows[i].Cells["SrNo"].Value));
            }

            var candidates = _pagination.PageData.Where(x => srNos.Contains(x.SrNo)).ToList();

            var emptyMobileNos = candidates.Where(x => string.IsNullOrEmpty(x.Email)).Select(x => x.CompanyName).ToArray();
            if (emptyMobileNos.Any())
            {
                MessageBox.Show(string.Format("{0} Candidate don't have email id", string.Join(",", emptyMobileNos)), "Error Message", MessageBoxButtons.OK);
                return;
            }

            var contactDetail = candidates.Select(x => (IMobile)x);
            var frm = new SendSMSfrm(contactDetail);
            frm.ShowDialog();
        }

        private void toolStripButtonSendMail_Click(object sender, EventArgs e)
        {
            var srNos = GetSelectedSrNo();
            if (!srNos.Any())
                return;

            var companys = _pagination.PageData.Where(x => srNos.Contains(x.SrNo)).ToList();

            var emptyEmailIds = companys.Where(x => string.IsNullOrEmpty(x.Email)).Select(x => x.CompanyName).ToArray();
            if (emptyEmailIds.Any())
            {
                MessageBox.Show(string.Format("{0} Candidate don't have email id", string.Join(",", emptyEmailIds)), "Error Message", MessageBoxButtons.OK);
                return;
            }

            var emailIds = companys.Select(x => (IEmail)x);
            CommanFuction.OpenOutlook(emailIds);
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var srNos = GetSelectedSrNo();
            if (!srNos.Any())
                return;

            var srNo = srNos[0];
            var company = _pagination.PageData.SingleOrDefault(x => x.SrNo == srNo);

            if (company == null)
                return;

            var frm = new VisitingCardFrm(company);
            frm.ShowDialog();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            var frm = new RecycleDeletedRecord<CustomerDetail>(x => x.CompanyType == _companyType.ToString());
            frm.ShowDialog();

            _uniDb.Refresh(System.Data.Linq.RefreshMode.KeepChanges, _pagination.PageData);
            BindGridView();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            // ask for password
            Loginfrm frm = new Loginfrm(HomePage.UserDetail);
            frm.ShowDialog();

            if (!frm.IsLogedIn)
                return;

            CommanFuction.Export_To_Excel2<CustomerDetail>(_pagination.ReportData.ToList(), "Customer Report");
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // ask for password
            var frmLogin = new Loginfrm(HomePage.UserDetail);
            frmLogin.ShowDialog();

            if (!frmLogin.IsLogedIn)
                return;

            var selectedSrNos = _reportForm.GetSelectedSrNo();
            var uniDb = new UniDBDataContext();
            var selectedCustomers = uniDb.CustomerDetails.Where(x => selectedSrNos.Contains(x.SrNo));
            //uniDb.CustomerDetails.DeleteAllOnSubmit(selectedCustomers);

            if (toolStripComboBox1.Text != UniEnums.CompanyType.Customer.ToString())
            {
                var query = uniDb.VendorDetails.Where(x => x.VendorType == toolStripComboBox1.Text);
                var srNo = query.Any() ? query.Max(c => c.SrNo) : 0;

                var newVendors = new List<VendorDetail>();
                foreach (var customer in selectedCustomers)
                {
                    srNo++;
                    newVendors.Add(new VendorDetail()
                    {
                        SrNo = srNo,
                        AnnualTurnover = customer.AnnualTurnover,
                        BusinessType = customer.BusinessType,
                        CompanyName = customer.CompanyName,
                        ContactPerson = customer.ContactPerson,
                        Email = customer.Email,
                        EstablishedYear = customer.EstablishedYear,
                        ExecutiveName = customer.ExecutiveName,
                        Expert = customer.Expert,
                        FactoryAddress = customer.FactoryAddress,
                        IsDeleted = customer.IsDeleted,
                        MobileNo = customer.MobileNo,
                        Networking = customer.Networking,
                        NoOfEmployees = customer.NoOfEmployees,
                        Note = customer.Note,
                        OfficeAddress = customer.OfficeAddress,
                        PhoneNo = customer.PhoneNo,
                        ProcessedDate = customer.ProcessedDate,
                        Profile1 = customer.Profile1,
                        Profile2 = customer.Profile2,
                        Sector = customer.Sector,
                        Source = customer.Source,
                        Website = customer.Website,
                        VendorType = toolStripComboBox1.Text,
                    });
                    uniDb.VendorDetails.InsertAllOnSubmit(newVendors);
                }
            }
            else
            {
                var srNo = uniDb.CustomerDetails.Any(c => c.CompanyType == UniEnums.CompanyType.Customer.ToString())
                            ? uniDb.CustomerDetails.Where(c => c.CompanyType == UniEnums.CompanyType.Customer.ToString())
                                                   .Max(c => c.SrNo)
                            : 0;

                var newCustomers = new List<CustomerDetail>();
                foreach (var customer in selectedCustomers)
                {
                    srNo++;
                    newCustomers.Add(new CustomerDetail()
                    {
                        SrNo = srNo,
                        CompanyType = UniEnums.CompanyType.Customer.ToString(),
                        AnnualTurnover = customer.AnnualTurnover,
                        BusinessType = customer.BusinessType,
                        CompanyName = customer.CompanyName,
                        ContactPerson = customer.ContactPerson,
                        Email = customer.Email,
                        EstablishedYear = customer.EstablishedYear,
                        ExecutiveName = customer.ExecutiveName,
                        Expert = customer.Expert,
                        FactoryAddress = customer.FactoryAddress,
                        IsDeleted = customer.IsDeleted,
                        MobileNo = customer.MobileNo,
                        Networking = customer.Networking,
                        NoOfEmployees = customer.NoOfEmployees,
                        Note = customer.Note,
                        OfficeAddress = customer.OfficeAddress,
                        PhoneNo = customer.PhoneNo,
                        ProcessedDate = customer.ProcessedDate,
                        Profile1 = customer.Profile1,
                        Profile2 = customer.Profile2,
                        Sector = customer.Sector,
                        Source = customer.Source,
                        Website = customer.Website
                    });
                }
                uniDb.CustomerDetails.InsertAllOnSubmit(newCustomers);
            }

            uniDb.SubmitChanges();
            RefreshGrid();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count < 1)
                return;

            var srNo = Convert.ToInt64(dataGridView1.SelectedRows[0].Cells["SrNo"].Value);
            var profile2 = _pagination.PageData.Single(x => x.SrNo == srNo).Profile2;

            if (string.IsNullOrEmpty(profile2))
            {
                MessageBox.Show("Profile 2 not attached", "Error Message", MessageBoxButtons.OK);
                return;
            }

            var fileInfo = new FileInfo(CommanFuction.GetServerPath(profile2));
            if (!fileInfo.Exists)
            {
                MessageBox.Show("Profile 2 not found", "Error Message", MessageBoxButtons.OK);
                return;
            }

            Process.Start(fileInfo.FullName);
        }

        private void toolStripButtonShowProfile1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count < 1)
                return;

            var srNo = Convert.ToInt64(dataGridView1.SelectedRows[0].Cells["SrNo"].Value);
            var profile1 = _pagination.PageData.Single(x => x.SrNo == srNo).Profile1;

            if (string.IsNullOrEmpty(profile1))
            {
                MessageBox.Show("Profile 1 not attached", "Error Message", MessageBoxButtons.OK);
                return;
            }

            var fileInfo = new FileInfo(CommanFuction.GetServerPath(profile1));
            if (!fileInfo.Exists)
            {
                MessageBox.Show("Profile 1 not found", "Error Message", MessageBoxButtons.OK);
                return;
            }

            Process.Start(fileInfo.FullName);
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            RowSelected();
        }

        private void RowSelected()
        {
            if (dataGridView1.SelectedRows.Count < 1)
                return;

            var profile1 = Convert.ToString(dataGridView1.SelectedRows[0].Cells["Profile1"].Value);
            var profile2 = Convert.ToString(dataGridView1.SelectedRows[0].Cells["Profile2"].Value);

            toolStripButtonShowProfile1.Enabled = !string.IsNullOrEmpty(profile1);
            toolStripButton3.Enabled = !string.IsNullOrEmpty(profile2);
        }
    }
}
