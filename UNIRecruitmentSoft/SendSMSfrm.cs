using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace UNIRecruitmentSoft
{
    public partial class SendSMSfrm : Form
    {
        private readonly IEnumerable<IMobile> _mobiles;
        private readonly UniDBDataContext _uniDb;

        public SendSMSfrm(IEnumerable<IMobile> mobiles = null)
        {
            InitializeComponent();
            _mobiles = mobiles;

            _uniDb = new UniDBDataContext();
        }

        private void SendSMSfrm_Load(object sender, EventArgs e)
        {
            if (_mobiles != null)
            {
                rtbMobileNos.Text = string.Join(",", _mobiles.Select(x => x.MobileNo).ToArray());
            }
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(richtxtMessage.Text))
            {
                MessageBox.Show("Please Enter Message", "Validation", MessageBoxButtons.OK);
                return;
            }

            var message = richtxtMessage.Text;
            var mobileNos = string.Join(",", _mobiles.Select(x => x.MobileNo).ToArray());
            var ulr =
                "http://api.mVaayoo.com/mvaayooapi/MessageCompose?user=ui.communications@gmail.com:uic123&senderID=TEST SMS&receipientno=" +
                mobileNos + "&dcs=0&msgtxt=" +
                message + "&state=4";

            try
            {
                var wc = new WebClient();
                wc.DownloadString(ulr);

                List<SMSHistory> smsHistories = new List<SMSHistory>();
                foreach (var mobile in _mobiles)
                {
                    smsHistories.Add(new SMSHistory() { Date = DateTime.Now, MobileNo = mobile.MobileNo, SMSText = message, ExecutiveName = HomePage.UserDetail.UserId });
                }

                _uniDb.SMSHistories.InsertAllOnSubmit(smsHistories);
                _uniDb.SubmitChanges();


                MessageBox.Show("Message Send", "Success Message", MessageBoxButtons.OK);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK);
            }

        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
