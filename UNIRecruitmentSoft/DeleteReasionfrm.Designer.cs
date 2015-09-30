namespace UNIRecruitmentSoft
{
    partial class DeleteReasionfrm
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
            this.txtDeleteReasion = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtDeleteReasion
            // 
            this.txtDeleteReasion.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDeleteReasion.Location = new System.Drawing.Point(116, 25);
            this.txtDeleteReasion.Multiline = true;
            this.txtDeleteReasion.Name = "txtDeleteReasion";
            this.txtDeleteReasion.Size = new System.Drawing.Size(255, 63);
            this.txtDeleteReasion.TabIndex = 5;
            this.txtDeleteReasion.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDeleteReasion_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "Delete Reasion :";
            // 
            // DeleteReasionfrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(438, 121);
            this.Controls.Add(this.txtDeleteReasion);
            this.Controls.Add(this.label2);
            this.Name = "DeleteReasionfrm";
            this.Text = "DeleteReasionfrm";
            this.Load += new System.EventHandler(this.DeleteReasionfrm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDeleteReasion;
        private System.Windows.Forms.Label label2;
    }
}