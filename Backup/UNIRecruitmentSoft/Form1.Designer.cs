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
            this.SuspendLayout();
            // 
            // pnlCandidateDetails
            // 
            this.pnlCandidateDetails.BackColor = System.Drawing.Color.Transparent;
            this.pnlCandidateDetails.Location = new System.Drawing.Point(52, 105);
            this.pnlCandidateDetails.Name = "pnlCandidateDetails";
            this.pnlCandidateDetails.Size = new System.Drawing.Size(238, 52);
            this.pnlCandidateDetails.TabIndex = 18;
            this.pnlCandidateDetails.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pnlCandidateDetails_MouseClick);
            // 
            // HomePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::UNIRecruitmentSoft.Properties.Resources.HomeRequirment;
            this.ClientSize = new System.Drawing.Size(884, 462);
            this.Controls.Add(this.pnlCandidateDetails);
            this.MaximizeBox = false;
            this.Name = "HomePage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Home Page";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlCandidateDetails;
    }
}

