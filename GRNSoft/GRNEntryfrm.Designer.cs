namespace GRNSoft
{
    partial class GRNEntryfrm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtSupplier = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpGRNDate = new System.Windows.Forms.DateTimePicker();
            this.label26 = new System.Windows.Forms.Label();
            this.txtGRNNo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtChallanNo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpChallanDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPONo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpPODate = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.txtVehicalNo = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtOctroiReceiptNo = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtChallan = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtActual = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtItemCode = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label14 = new System.Windows.Forms.Label();
            this.cmbAccepted = new System.Windows.Forms.ComboBox();
            this.txtReasonForRejection = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.cmbChallanUnit = new System.Windows.Forms.ComboBox();
            this.cmbActualUnit = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSupplier
            // 
            this.txtSupplier.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSupplier.Location = new System.Drawing.Point(91, 78);
            this.txtSupplier.Name = "txtSupplier";
            this.txtSupplier.Size = new System.Drawing.Size(568, 26);
            this.txtSupplier.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(25, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Supplier :";
            // 
            // dtpGRNDate
            // 
            this.dtpGRNDate.CustomFormat = "dd-MMM-yyyy";
            this.dtpGRNDate.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpGRNDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpGRNDate.Location = new System.Drawing.Point(250, 20);
            this.dtpGRNDate.Name = "dtpGRNDate";
            this.dtpGRNDate.Size = new System.Drawing.Size(167, 26);
            this.dtpGRNDate.TabIndex = 0;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.BackColor = System.Drawing.Color.Transparent;
            this.label26.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(205, 24);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(39, 15);
            this.label26.TabIndex = 56;
            this.label26.Text = "Date :";
            // 
            // txtGRNNo
            // 
            this.txtGRNNo.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGRNNo.Location = new System.Drawing.Point(91, 20);
            this.txtGRNNo.Name = "txtGRNNo";
            this.txtGRNNo.Size = new System.Drawing.Size(72, 26);
            this.txtGRNNo.TabIndex = 58;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(30, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 15);
            this.label2.TabIndex = 59;
            this.label2.Text = "GRN No :";
            // 
            // txtChallanNo
            // 
            this.txtChallanNo.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtChallanNo.Location = new System.Drawing.Point(91, 119);
            this.txtChallanNo.Name = "txtChallanNo";
            this.txtChallanNo.Size = new System.Drawing.Size(106, 26);
            this.txtChallanNo.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 15);
            this.label3.TabIndex = 63;
            this.label3.Text = "Challan No :";
            // 
            // dtpChallanDate
            // 
            this.dtpChallanDate.CustomFormat = "dd-MMM-yyyy";
            this.dtpChallanDate.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpChallanDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpChallanDate.Location = new System.Drawing.Point(248, 119);
            this.dtpChallanDate.Name = "dtpChallanDate";
            this.dtpChallanDate.Size = new System.Drawing.Size(135, 26);
            this.dtpChallanDate.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(203, 125);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 15);
            this.label4.TabIndex = 60;
            this.label4.Text = "Date :";
            // 
            // txtPONo
            // 
            this.txtPONo.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPONo.Location = new System.Drawing.Point(91, 159);
            this.txtPONo.Name = "txtPONo";
            this.txtPONo.Size = new System.Drawing.Size(106, 26);
            this.txtPONo.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(37, 165);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 15);
            this.label5.TabIndex = 67;
            this.label5.Text = "PO No :";
            // 
            // dtpPODate
            // 
            this.dtpPODate.CustomFormat = "dd-MMM-yyyy";
            this.dtpPODate.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpPODate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpPODate.Location = new System.Drawing.Point(248, 159);
            this.dtpPODate.Name = "dtpPODate";
            this.dtpPODate.Size = new System.Drawing.Size(135, 26);
            this.dtpPODate.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(203, 164);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 15);
            this.label6.TabIndex = 64;
            this.label6.Text = "Date :";
            // 
            // txtVehicalNo
            // 
            this.txtVehicalNo.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVehicalNo.Location = new System.Drawing.Point(516, 119);
            this.txtVehicalNo.Name = "txtVehicalNo";
            this.txtVehicalNo.Size = new System.Drawing.Size(143, 26);
            this.txtVehicalNo.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(438, 126);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 15);
            this.label7.TabIndex = 69;
            this.label7.Text = "Vehical No :";
            // 
            // txtOctroiReceiptNo
            // 
            this.txtOctroiReceiptNo.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOctroiReceiptNo.Location = new System.Drawing.Point(516, 158);
            this.txtOctroiReceiptNo.Name = "txtOctroiReceiptNo";
            this.txtOctroiReceiptNo.Size = new System.Drawing.Size(143, 26);
            this.txtOctroiReceiptNo.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(398, 164);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(112, 15);
            this.label8.TabIndex = 71;
            this.label8.Text = "Octroi Receipt No :";
            // 
            // txtDescription
            // 
            this.txtDescription.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescription.Location = new System.Drawing.Point(104, 254);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(237, 26);
            this.txtDescription.TabIndex = 9;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(193, 236);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 15);
            this.label9.TabIndex = 73;
            this.label9.Text = "Description";
            // 
            // txtChallan
            // 
            this.txtChallan.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtChallan.Location = new System.Drawing.Point(347, 254);
            this.txtChallan.Name = "txtChallan";
            this.txtChallan.Size = new System.Drawing.Size(57, 26);
            this.txtChallan.TabIndex = 10;
            this.txtChallan.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtChallan_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(356, 236);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(49, 15);
            this.label10.TabIndex = 75;
            this.label10.Text = "Challan";
            // 
            // txtActual
            // 
            this.txtActual.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtActual.Location = new System.Drawing.Point(505, 254);
            this.txtActual.Name = "txtActual";
            this.txtActual.Size = new System.Drawing.Size(57, 26);
            this.txtActual.TabIndex = 12;
            this.txtActual.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtActual_KeyPress);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(513, 236);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(43, 15);
            this.label11.TabIndex = 77;
            this.label11.Text = "Actual";
            // 
            // txtRemark
            // 
            this.txtRemark.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRemark.Location = new System.Drawing.Point(751, 254);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(155, 26);
            this.txtRemark.TabIndex = 15;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(765, 236);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(52, 15);
            this.label12.TabIndex = 79;
            this.label12.Text = "Remark";
            // 
            // txtItemCode
            // 
            this.txtItemCode.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtItemCode.Location = new System.Drawing.Point(15, 254);
            this.txtItemCode.Name = "txtItemCode";
            this.txtItemCode.Size = new System.Drawing.Size(83, 26);
            this.txtItemCode.TabIndex = 8;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(25, 236);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(64, 15);
            this.label13.TabIndex = 81;
            this.label13.Text = "Item Code";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(15, 286);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(891, 170);
            this.dataGridView1.TabIndex = 82;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(662, 236);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(61, 15);
            this.label14.TabIndex = 98;
            this.label14.Text = "Accepted";
            // 
            // cmbAccepted
            // 
            this.cmbAccepted.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAccepted.Font = new System.Drawing.Font("Arial", 12F);
            this.cmbAccepted.FormattingEnabled = true;
            this.cmbAccepted.Items.AddRange(new object[] {
            "Accepted",
            "Rejected"});
            this.cmbAccepted.Location = new System.Drawing.Point(657, 254);
            this.cmbAccepted.Name = "cmbAccepted";
            this.cmbAccepted.Size = new System.Drawing.Size(88, 26);
            this.cmbAccepted.TabIndex = 14;
            // 
            // txtReasonForRejection
            // 
            this.txtReasonForRejection.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReasonForRejection.Location = new System.Drawing.Point(154, 475);
            this.txtReasonForRejection.Multiline = true;
            this.txtReasonForRejection.Name = "txtReasonForRejection";
            this.txtReasonForRejection.Size = new System.Drawing.Size(458, 79);
            this.txtReasonForRejection.TabIndex = 17;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(15, 481);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(133, 15);
            this.label15.TabIndex = 100;
            this.label15.Text = "Reason For Rejection :";
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnDelete.BackColor = System.Drawing.Color.Transparent;
            this.btnDelete.BackgroundImage = global::GRNSoft.Properties.Resources.delete;
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDelete.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.btnDelete.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnDelete.Location = new System.Drawing.Point(926, 315);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(46, 36);
            this.btnDelete.TabIndex = 104;
            this.btnDelete.UseVisualStyleBackColor = false;
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAdd.BackColor = System.Drawing.Color.Transparent;
            this.btnAdd.BackgroundImage = global::GRNSoft.Properties.Resources.netvibes;
            this.btnAdd.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.btnAdd.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnAdd.Location = new System.Drawing.Point(926, 254);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(46, 30);
            this.btnAdd.TabIndex = 16;
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonClose.BackColor = System.Drawing.Color.Transparent;
            this.buttonClose.BackgroundImage = global::GRNSoft.Properties.Resources.exit;
            this.buttonClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonClose.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.buttonClose.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonClose.Location = new System.Drawing.Point(892, 484);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(70, 70);
            this.buttonClose.TabIndex = 19;
            this.buttonClose.UseVisualStyleBackColor = false;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonSave.BackColor = System.Drawing.Color.Transparent;
            this.buttonSave.BackgroundImage = global::GRNSoft.Properties.Resources.network_save;
            this.buttonSave.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.buttonSave.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonSave.Location = new System.Drawing.Point(795, 484);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(70, 70);
            this.buttonSave.TabIndex = 18;
            this.buttonSave.UseVisualStyleBackColor = false;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // cmbChallanUnit
            // 
            this.cmbChallanUnit.Font = new System.Drawing.Font("Arial", 12F);
            this.cmbChallanUnit.FormattingEnabled = true;
            this.cmbChallanUnit.Location = new System.Drawing.Point(409, 254);
            this.cmbChallanUnit.Name = "cmbChallanUnit";
            this.cmbChallanUnit.Size = new System.Drawing.Size(78, 26);
            this.cmbChallanUnit.TabIndex = 11;
            // 
            // cmbActualUnit
            // 
            this.cmbActualUnit.Font = new System.Drawing.Font("Arial", 12F);
            this.cmbActualUnit.FormattingEnabled = true;
            this.cmbActualUnit.Location = new System.Drawing.Point(568, 254);
            this.cmbActualUnit.Name = "cmbActualUnit";
            this.cmbActualUnit.Size = new System.Drawing.Size(78, 26);
            this.cmbActualUnit.TabIndex = 13;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(426, 236);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(29, 15);
            this.label16.TabIndex = 107;
            this.label16.Text = "Unit";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(582, 236);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(29, 15);
            this.label17.TabIndex = 108;
            this.label17.Text = "Unit";
            // 
            // GRNEntryfrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(984, 570);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.cmbActualUnit);
            this.Controls.Add(this.cmbChallanUnit);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.txtReasonForRejection);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.cmbAccepted);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.txtItemCode);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txtRemark);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtActual);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtChallan);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtOctroiReceiptNo);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtVehicalNo);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtPONo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dtpPODate);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtChallanNo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtpChallanDate);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtGRNNo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpGRNDate);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.txtSupplier);
            this.Controls.Add(this.label1);
            this.Name = "GRNEntryfrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GRN Entry Form";
            this.Load += new System.EventHandler(this.GRNEntryfrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSupplier;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpGRNDate;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox txtGRNNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtChallanNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpChallanDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPONo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpPODate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtVehicalNo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtOctroiReceiptNo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtChallan;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtActual;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtItemCode;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cmbAccepted;
        private System.Windows.Forms.TextBox txtReasonForRejection;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.ComboBox cmbChallanUnit;
        private System.Windows.Forms.ComboBox cmbActualUnit;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
    }
}

