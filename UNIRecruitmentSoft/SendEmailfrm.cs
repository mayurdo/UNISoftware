using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Windows.Forms;

namespace UNIRecruitmentSoft
{
    public partial class SendEmailfrm : Form
    {
        private readonly IEnumerable<IEmail> _emailIds;
        private readonly UniDBDataContext _uniDb;

        public SendEmailfrm(IEnumerable<IEmail> emailIds)
        {
            InitializeComponent();

            _emailIds = emailIds;
            _uniDb = new UniDBDataContext();
        }

        private void SendEmailfrm_Load(object sender, EventArgs e)
        {
            rtbEmailIds.Text = string.Join(",", _emailIds.Select(x => x.Email).ToArray());
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSubject.Text))
            {
                MessageBox.Show("Please Enter Subject", "Validation", MessageBoxButtons.OK);
                txtSubject.Focus();
                return;
            }

            if (string.IsNullOrEmpty(richtxtMessage.Text))
            {
                MessageBox.Show("Please Enter Message", "Validation", MessageBoxButtons.OK);
                richtxtMessage.Focus();
                return;
            }

            var message = richtxtMessage.Text;
            var emailIds = string.Join(",", _emailIds.Select(x => x.Email).ToArray());


            try
            {
                var fromEmailId = ConfigurationSettings.AppSettings["FromEmailId"];
                var fromEmailPassword = ConfigurationSettings.AppSettings["FromEmailPassword"];

                var client = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    EnableSsl = true,
                    Timeout = 100000,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(
                        fromEmailId, fromEmailPassword)
                };

                var msg = new MailMessage();
                //msg.To.Add(emailIds);
                _emailIds.Where(x => !string.IsNullOrEmpty(x.Email)).ToList().ForEach(x => msg.To.Add(x.Email));

                msg.From = new MailAddress(fromEmailId);
                msg.Subject = txtSubject.Text;
                msg.Body = message;
                //Attachment data = new Attachment(textBox_Attachment.Text);
                //msg.Attachments.Add(data);

                client.Send(msg);


                var emailHistory = new EmailHistory()
                {
                    Date = DateTime.Now,
                    EmailIds = emailIds,
                    Subject = txtSubject.Text,
                    Message = message,
                    ExecutiveName = HomePage.UserDetail.UserId
                };

                _uniDb.EmailHistories.InsertOnSubmit(emailHistory);
                _uniDb.SubmitChanges();

                MessageBox.Show(@"Successfully Sent Message.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
