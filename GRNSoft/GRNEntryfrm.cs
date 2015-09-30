using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GRNSoft.Utility;

namespace GRNSoft
{


    public partial class GRNEntryfrm : Form
    {
        private GRNDetail _grnDetail;
        private UniGRNDbDataContext _dbData = new UniGRNDbDataContext();
        public GRNEntryfrm()
        {
            InitializeComponent();
            _grnDetail = new GRNDetail()
                {
                    GRNDate = DateTime.Today
                };
        }

        private void GRNEntryfrm_Load(object sender, EventArgs e)
        {
            Reset();
        }

        private void Reset()
        {
            FillComboBox();

            txtGRNNo.Text = _grnDetail.GRNNo.ToString(CultureInfo.InvariantCulture);
            dtpGRNDate.Value = _grnDetail.GRNDate;
            txtSupplier.Text = _grnDetail.Supplier;
            txtChallan.Text = _grnDetail.ChallanNo;
            dtpChallanDate.Value = _grnDetail.ChallanDate ?? DateTime.Today;
            txtPONo.Text = _grnDetail.PONo;
            dtpPODate.Value = _grnDetail.PODate ?? DateTime.Today;
            txtVehicalNo.Text = _grnDetail.VehicalNo;
            txtOctroiReceiptNo.Text = _grnDetail.OctroiReceiptNo;
            txtReasonForRejection.Text = _grnDetail.ReasonForRejection;

            BindItemGrid();
            txtSupplier.Focus();

        }

        private void FillComboBox()
        {
            cmbChallanUnit.DataSource = _dbData.GRNItemDetails.Select(x => x.ChallanUnit).Distinct();
            cmbActualUnit.DataSource = _dbData.GRNItemDetails.Select(x => x.ActualUnit).Distinct();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateForm())
                    return;

                _grnDetail.GRNDate = dtpGRNDate.Value;
                _grnDetail.Supplier = txtSupplier.Text;
                _grnDetail.ChallanNo = txtChallanNo.Text;
                _grnDetail.ChallanDate = string.IsNullOrWhiteSpace(_grnDetail.ChallanNo) ? (DateTime?)null : dtpChallanDate.Value;
                _grnDetail.PONo = txtPONo.Text;
                _grnDetail.PODate = string.IsNullOrWhiteSpace(_grnDetail.PONo) ? (DateTime?)null : dtpPODate.Value;
                _grnDetail.VehicalNo = txtVehicalNo.Text;
                _grnDetail.OctroiReceiptNo = txtOctroiReceiptNo.Text;
                _grnDetail.ReasonForRejection = txtReasonForRejection.Text;

                if (_grnDetail.GRNNo < 1)
                {
                    var maxGRNNo = _dbData.GRNDetails.Any() ? _dbData.GRNDetails.Max(x => x.GRNNo) : 0;
                    _grnDetail.GRNNo = maxGRNNo + 1;
                    _dbData.GRNDetails.InsertOnSubmit(_grnDetail);
                    _dbData.SubmitChanges();
                    _grnDetail = new GRNDetail()
                    {
                        GRNDate = DateTime.Today
                    };

                    Reset();
                    return;
                }

                _dbData.SubmitChanges();
                Reset();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.OK);
            }
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtSupplier.Text))
            {
                MessageBox.Show("Please Enter Supplier Name", "Validation Message", MessageBoxButtons.OK);
                txtSupplier.Focus();
                return false;   
            }

            if (dataGridView1.RowCount < 0)
            {
                MessageBox.Show("Please Add Item in Grid", "Validation Message", MessageBoxButtons.OK);
                txtItemCode.Focus();
                return false; 
            }

            return true;
        }


        #region Add Item

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!IsItemValid())
                return;

            var grnItemDetails = new GRNItemDetail();
            grnItemDetails.ItemCode = txtItemCode.Text;
            grnItemDetails.Description = txtDescription.Text;
            grnItemDetails.Challan = Convert.ToInt32(txtChallan.Text);
            grnItemDetails.ChallanUnit = cmbChallanUnit.Text;
            grnItemDetails.Actual = Convert.ToDecimal(txtActual.Text);
            grnItemDetails.ActualUnit = cmbActualUnit.Text;
            grnItemDetails.Accepted = cmbAccepted.Text;
            grnItemDetails.Remarks = txtRemark.Text;
            _grnDetail.GRNItemDetails.Add(grnItemDetails);

            ResetItemDetail();
            BindItemGrid();
        }

        private bool IsItemValid()
        {
            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                MessageBox.Show("Please Enter Item Description", "Validation Message", MessageBoxButtons.OK);
                txtDescription.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtChallan.Text))
            {
                MessageBox.Show("Please Enter Challan of Item", "Validation Message", MessageBoxButtons.OK);
                txtChallan.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtActual.Text))
            {
                MessageBox.Show("Please Enter Actual Quantity of Item", "Validation Message", MessageBoxButtons.OK);
                txtActual.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(cmbAccepted.Text))
            {
                MessageBox.Show("Please Select Accepted or Rejected", "Validation Message", MessageBoxButtons.OK);
                cmbAccepted.Focus();
                return false;
            }

            return true;
        }

        public void ResetItemDetail()
        {
            txtItemCode.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtChallan.Text = string.Empty;
            cmbChallanUnit.Text = string.Empty;
            txtActual.Text = string.Empty;
            cmbActualUnit.Text = string.Empty;
            cmbAccepted.Text = string.Empty;
            txtRemark.Text = string.Empty;
        }

        private void BindItemGrid()
        {
            dataGridView1.DataSource = _grnDetail.GRNItemDetails
                                            .Select(x => new { x.ItemCode, x.Description, x.Challan, x.ChallanUnit, x.Actual, x.ActualUnit, x.Accepted, x.Remarks })
                                            .ToList();

            dataGridView1.Columns["Description"].Width = 500;
        }

        private void txtChallan_KeyPress(object sender, KeyPressEventArgs e)
        {
            UtilityClass.AcceptOnlyNumber(e);
        }

        private void txtActual_KeyPress(object sender, KeyPressEventArgs e)
        {
            UtilityClass.AcceptOnlyDecimal(e);
        }

        #endregion
        
        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
