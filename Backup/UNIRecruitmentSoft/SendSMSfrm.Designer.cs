namespace UNIRecruitmentSoft
{
    partial class SendSMSfrm
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
            this.cmbSMSType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.richtxtMessage = new System.Windows.Forms.RichTextBox();
            this.buttonSend = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmbSMSType
            // 
            this.cmbSMSType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSMSType.Font = new System.Drawing.Font("Arial", 12F);
            this.cmbSMSType.FormattingEnabled = true;
            this.cmbSMSType.Location = new System.Drawing.Point(109, 21);
            this.cmbSMSType.Name = "cmbSMSType";
            this.cmbSMSType.Size = new System.Drawing.Size(167, 26);
            this.cmbSMSType.TabIndex = 58;
            this.cmbSMSType.SelectedIndexChanged += new System.EventHandler(this.cmbSMSType_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 15);
            this.label2.TabIndex = 57;
            this.label2.Text = "Message Type :";
            // 
            // richtxtMessage
            // 
            this.richtxtMessage.Location = new System.Drawing.Point(12, 64);
            this.richtxtMessage.Name = "richtxtMessage";
            this.richtxtMessage.Size = new System.Drawing.Size(264, 118);
            this.richtxtMessage.TabIndex = 59;
            this.richtxtMessage.Text = "";
            // 
            // buttonSend
            // 
            this.buttonSend.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonSend.BackColor = System.Drawing.Color.Transparent;
            this.buttonSend.BackgroundImage = global::UNIRecruitmentSoft.Properties.Resources.send_email_user_letter_Button;
            this.buttonSend.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.buttonSend.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonSend.Location = new System.Drawing.Point(224, 190);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(52, 49);
            this.buttonSend.TabIndex = 60;
            this.buttonSend.UseVisualStyleBackColor = false;
            // 
            // SendSMSfrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(293, 244);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.richtxtMessage);
            this.Controls.Add(this.cmbSMSType);
            this.Controls.Add(this.label2);
            this.Name = "SendSMSfrm";
            this.Text = "SendSMSfrm";
            this.Load += new System.EventHandler(this.SendSMSfrm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbSMSType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox richtxtMessage;
        private System.Windows.Forms.Button buttonSend;
    }
}