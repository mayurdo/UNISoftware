using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UNIRecruitmentSoft.UserMng;
using UNIRecruitmentSoft.Vendor;

namespace UNIRecruitmentSoft
{
    public partial class HomePage : Form
    {
        //public static string ExecutiveName { get; set; }

        public static UserDetail UserDetail { get; set; }

        private List<string> _moduleList;

        public HomePage()
        {
            InitializeComponent();
            _moduleList = new List<string>();

            //var uniDb = new UniDBDataContext();
            //if (!uniDb.DatabaseExists())
            //    uniDb.CreateDatabase();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Loginfrm frm = new Loginfrm(new UserDetail());
            frm.ShowDialog();

            if (!frm.IsLogedIn)
            {
                this.Close();
                return;
            }

            UserDetail = frm.UserDetail;
            _moduleList.AddRange(UserDetail.Modules.Split(','));
        }

        private bool HasAccess(UniEnums.Modules module)
        {
            if (_moduleList.Contains(module.ToString()))
                return true;

            MessageBox.Show("You don't have access to this module", "Access Message", MessageBoxButtons.OK);
            return false;
        }

        private void pnlCandidateDetails_MouseClick(object sender, MouseEventArgs e)
        {
            if (!HasAccess(UniEnums.Modules.Candidate))
                return;

            CandidateReport frm = new CandidateReport();
            frm.ShowDialog();
        }

        private void pnlCompanyDetail_MouseClick(object sender, MouseEventArgs e)
        {
            if (!HasAccess(UniEnums.Modules.Company))
                return;

            var frm = new CompanyDetailsfrm(UniEnums.CompanyType.Company);
            frm.ShowDialog();
        }


        private void HomePage_FormClosed(object sender, FormClosedEventArgs e)
        {
            var uniDb = new UniDBDataContext();
            uniDb.CreateBackUp();
        }

        private void panalVTCDetails_MouseClick(object sender, MouseEventArgs e)
        {
            if (!HasAccess(UniEnums.Modules.VTC))
                return;

            var frm = new VendorDetailsFrm(UniEnums.VendorType.Vendor);
            frm.ShowDialog();
        }

        private void panelVisitingCardDetail_Click(object sender, EventArgs e)
        {
            if (!HasAccess(UniEnums.Modules.VisitingCard))
                return;

            var frm = new VendorDetailsFrm(UniEnums.VendorType.VisitingCard);
            frm.ShowDialog();
        }

        private void panelClientDetail_MouseClick(object sender, MouseEventArgs e)
        {
            if (!HasAccess(UniEnums.Modules.Client))
                return;

            var frm = new CompanyDetailsfrm(UniEnums.CompanyType.Customer);
            frm.ShowDialog();
        }

        private void panelUsers_MouseClick(object sender, MouseEventArgs e)
        {
            if (!HasAccess(UniEnums.Modules.UserMaster))
                return;

            var frm = new UserDetailFrm();
            frm.ShowDialog();
        }

        private void panelSmsHistory_MouseClick(object sender, MouseEventArgs e)
        {
            if (!HasAccess(UniEnums.Modules.SmsHistory))
                return;

            var frm = new SMSHistoryfrm();
            frm.ShowDialog();
        }

        private void panelExcelFormat_MouseClick(object sender, MouseEventArgs e)
        {
            if (!HasAccess(UniEnums.Modules.ExportExcel))
                return;

            var frm = new ImportFromExcel();
            frm.ShowDialog();

            //DialogResult result = folderBrowserDialog1.ShowDialog();
            //var excelFormatFileName = ConfigurationSettings.AppSettings["ExcelFormatDownloadFileName"];
            //if (result == DialogResult.OK)
            //{
            //    CommanFuction.SaveFile(excelFormatFileName, folderBrowserDialog1.SelectedPath);
            //}
        }

        private void panelSendSms_Click(object sender, EventArgs e)
        {
            var frm = new SendSMSfrm();
            frm.ShowDialog();
        }


    }
}
