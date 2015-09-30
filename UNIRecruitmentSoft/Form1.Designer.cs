namespace UNIRecruitmentSoft
{
    partial class HomePage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlCandidateDetails = new System.Windows.Forms.Panel();
            this.panalVTCDetails = new System.Windows.Forms.Panel();
            this.pnlCompanyDetail = new System.Windows.Forms.Panel();
            this.panelClientDetail = new System.Windows.Forms.Panel();
            this.panelVisitingCardDetail = new System.Windows.Forms.Panel();
            this.panelSmsHistory = new System.Windows.Forms.Panel();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.panelUsers = new System.Windows.Forms.Panel();
            this.panelSendSms = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // pnlCandidateDetails
            // 
            this.pnlCandidateDetails.BackColor = System.Drawing.Color.Transparent;
            this.pnlCandidateDetails.Location = new System.Drawing.Point(52, 105);
            this.pnlCandidateDetails.Name = "pnlCandidateDetails";
            this.pnlCandidateDetails.Size = new System.Drawing.Size(238, 54);
            this.pnlCandidateDetails.TabIndex = 18;
            this.pnlCandidateDetails.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pnlCandidateDetails_MouseClick);
            // 
            // panalVTCDetails
            // 
            this.panalVTCDetails.BackColor = System.Drawing.Color.Transparent;
            this.panalVTCDetails.Location = new System.Drawing.Point(53, 301);
            this.panalVTCDetails.Name = "panalVTCDetails";
            this.panalVTCDetails.Size = new System.Drawing.Size(238, 54);
            this.panalVTCDetails.TabIndex = 19;
            this.panalVTCDetails.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panalVTCDetails_MouseClick);
            // 
            // pnlCompanyDetail
            // 
            this.pnlCompanyDetail.BackColor = System.Drawing.Color.Transparent;
            this.pnlCompanyDetail.Location = new System.Drawing.Point(52, 239);
            this.pnlCompanyDetail.Name = "pnlCompanyDetail";
            this.pnlCompanyDetail.Size = new System.Drawing.Size(238, 49);
            this.pnlCompanyDetail.TabIndex = 20;
            this.pnlCompanyDetail.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pnlCompanyDetail_MouseClick);
            // 
            // panelClientDetail
            // 
            this.panelClientDetail.BackColor = System.Drawing.Color.Transparent;
            this.panelClientDetail.Location = new System.Drawing.Point(52, 173);
            this.panelClientDetail.Name = "panelClientDetail";
            this.panelClientDetail.Size = new System.Drawing.Size(238, 53);
            this.panelClientDetail.TabIndex = 21;
            this.panelClientDetail.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panelClientDetail_MouseClick);
            // 
            // panelVisitingCardDetail
            // 
            this.panelVisitingCardDetail.BackColor = System.Drawing.Color.Transparent;
            this.panelVisitingCardDetail.Location = new System.Drawing.Point(512, 102);
            this.panelVisitingCardDetail.Name = "panelVisitingCardDetail";
            this.panelVisitingCardDetail.Size = new System.Drawing.Size(238, 57);
            this.panelVisitingCardDetail.TabIndex = 22;
            this.panelVisitingCardDetail.Click += new System.EventHandler(this.panelVisitingCardDetail_Click);
            // 
            // panelSmsHistory
            // 
            this.panelSmsHistory.BackColor = System.Drawing.Color.Transparent;
            this.panelSmsHistory.Location = new System.Drawing.Point(509, 229);
            this.panelSmsHistory.Name = "panelSmsHistory";
            this.panelSmsHistory.Size = new System.Drawing.Size(241, 59);
            this.panelSmsHistory.TabIndex = 19;
            this.panelSmsHistory.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panelSmsHistory_MouseClick);
            // 
            // panelUsers
            // 
            this.panelUsers.BackColor = System.Drawing.Color.Transparent;
            this.panelUsers.Location = new System.Drawing.Point(509, 165);
            this.panelUsers.Name = "panelUsers";
            this.panelUsers.Size = new System.Drawing.Size(241, 54);
            this.panelUsers.TabIndex = 20;
            this.panelUsers.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panelUsers_MouseClick);
            // 
            // panelSendSms
            // 
            this.panelSendSms.BackColor = System.Drawing.Color.Transparent;
            this.panelSendSms.Location = new System.Drawing.Point(510, 294);
            this.panelSendSms.Name = "panelSendSms";
            this.panelSendSms.Size = new System.Drawing.Size(241, 59);
            this.panelSendSms.TabIndex = 20;
            this.panelSendSms.Click += new System.EventHandler(this.panelSendSms_Click);
            // 
            // HomePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::UNIRecruitmentSoft.Properties.Resources.HomePage8;
            this.ClientSize = new System.Drawing.Size(884, 462);
            this.Controls.Add(this.panelSendSms);
            this.Controls.Add(this.panelUsers);
            this.Controls.Add(this.panelSmsHistory);
            this.Controls.Add(this.panelVisitingCardDetail);
            this.Controls.Add(this.panelClientDetail);
            this.Controls.Add(this.pnlCompanyDetail);
            this.Controls.Add(this.panalVTCDetails);
            this.Controls.Add(this.pnlCandidateDetails);
            this.MaximizeBox = false;
            this.Name = "HomePage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Home Page";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.HomePage_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlCandidateDetails;
        private System.Windows.Forms.Panel panalVTCDetails;
        private System.Windows.Forms.Panel pnlCompanyDetail;
        private System.Windows.Forms.Panel panelClientDetail;
        private System.Windows.Forms.Panel panelVisitingCardDetail;
        private System.Windows.Forms.Panel panelSmsHistory;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Panel panelUsers;
        private System.Windows.Forms.Panel panelSendSms;
    }
}

