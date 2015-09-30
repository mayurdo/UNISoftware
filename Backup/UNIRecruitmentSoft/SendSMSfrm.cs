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
    public partial class SendSMSfrm : Form
    {
        private readonly CandidateDetail _candidate;
        public SendSMSfrm(CandidateDetail candidate)
        {
            InitializeComponent();
            _candidate = candidate;
        }

        private void SendSMSfrm_Load(object sender, EventArgs e)
        {
            cmbSMSType.DataSource = Enum.GetNames(typeof(SMSType));
        }

        private enum SMSType
        {
            SelectType,
            SMS_1,
            SMS_2,
            SMS_3,
            SMS_4
        }

        private void cmbSMSType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSMSType.SelectedIndex < 0)
                return;

            var smsType = (SMSType)Enum.Parse(typeof(SMSType), cmbSMSType.Text);
            richtxtMessage.Text = GetMessage(smsType, _candidate);
        }

        private string GetMessage(SMSType smsType, CandidateDetail candidate)
        {
            switch (smsType)
            {
                case SMSType.SelectType:
                    return string.Empty;
                case SMSType.SMS_1:
                    return string.Format("Dear Candidate this is sms 1");
                case SMSType.SMS_2:
                    return string.Format("Dear Candidate this is sms 2");
                case SMSType.SMS_3:
                    return string.Format("Dear Candidate this is sms 3");
                case SMSType.SMS_4:
                    return string.Format("Dear {0} this is sms 4", candidate.Name);
                default:
                    MessageBox.Show("Selected message type is not correct");
                    return string.Empty;
            }
        }
    }
}
