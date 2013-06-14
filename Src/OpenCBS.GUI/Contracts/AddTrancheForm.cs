﻿// Octopus MFS is an integrated suite for managing a Micro Finance Institution: 
// clients, contracts, accounting, reporting and risk
// Copyright © 2006,2007 OCTO Technology & OXUS Development Network
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License along
// with this program; if not, write to the Free Software Foundation, Inc.,
// 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
//
// Website: http://www.opencbs.com
// Contact: contact@opencbs.com

using System;
using System.Windows.Forms;
using OpenCBS.CoreDomain.Clients;
using OpenCBS.CoreDomain.Contracts.Loans;
using OpenCBS.ExceptionsHandler;
using OpenCBS.GUI.UserControl;
using OpenCBS.Services;
using OpenCBS.Shared;

namespace OpenCBS.GUI.Contracts
{
    public partial class AddTrancheForm : SweetBaseForm
    {
        private bool _interestRateChanged;
        private bool _chargeInterestDuringShift;
        private bool _chargeInterestDuringGracePeriod;
        private int numberOfMaturity;
        private decimal _IR;
        private int _dateOffsetOrAmount;
        private DateTime _trancheDate;
        private Loan _contract;
        public DialogResult resultReschedulingForm;
        private Loan LoanWithNewTranche;
        private int _gracePeriod;
        private IClient _client;

        public AddTrancheForm(Loan contract, IClient pClient)
        {
            InitializeComponent();
            Setup();
            _client = pClient;
            _contract = contract;
            _IR = Convert.ToDecimal(contract.InterestRate);
            interestRateNumericUpDown.Value = _IR * 100;
            interestRateNumericUpDown.Text = (_IR * 100).ToString();
            if (contract.Product.InterestRate.HasValue) { /* checkBoxIRChanged.Enabled = false; */ }
            else
            {
                interestRateNumericUpDown.Minimum = Convert.ToDecimal(contract.Product.InterestRateMin * 100);
                interestRateNumericUpDown.Maximum = Convert.ToDecimal(contract.Product.InterestRateMax * 100);
            }
            InitializeTrancheComponents();
        }

        public void InitializeTrancheComponents()
        {
            startDateLabel.Visible = true;
            startDateTimePicker.Visible = true;
            applyToOlbCheckbox.Visible = true;
            applyToOlbCheckbox.Enabled = true;
            //labelShiftDateDays.Text = @" + " + Contract.GetRemainAmount().GetFormatedValue(Contract.Product.Currency.UseCents);
            Text = GetString("AddTranche.Text");
            applyToOlbCheckbox.Text = GetString("ApplynewInterestforOLB.Text");

            startDateTimePicker.Value = TimeProvider.Now;

            if (Contract.Product.NbOfInstallments != null)
            {
                installmentsNumericUpDown.Maximum = (decimal)Contract.Product.NbOfInstallments;
                installmentsNumericUpDown.Minimum = (decimal)Contract.Product.NbOfInstallments;
            }
            else
            {
                installmentsNumericUpDown.Maximum = (decimal)Contract.Product.NbOfInstallmentsMax;
                installmentsNumericUpDown.Minimum = (decimal)Contract.Product.NbOfInstallmentsMin;
            }
        }

        private void LoadForm()
        {
            RefreshSchedule(Contract);
        }

        private void Setup()
        {
            Load += (sender, args) => LoadForm();
        }

        private void RefreshSchedule(Loan loan)
        {
            scheduleUserControl.SetScheduleFor(loan);
            //listViewRepayments.Items.Clear();
            //foreach (Installment installment in contractToDisplay.InstallmentList)
            //{
            //    ListViewItem listViewItem = new ListViewItem(installment.Number.ToString());
            //    if (installment.IsRepaid)
            //    {
            //        listViewItem.BackColor = Color.FromArgb(((Byte)(0)), ((Byte)(88)), ((Byte)(56)));
            //        listViewItem.ForeColor = Color.White;
            //    }
            //    listViewItem.Tag = installment;
            //    listViewItem.SubItems.Add(installment.ExpectedDate.ToShortDateString());
            //    listViewItem.SubItems.Add(installment.InterestsRepayment.GetFormatedValue(contractToDisplay.UseCents));
            //    listViewItem.SubItems.Add(installment.CapitalRepayment.GetFormatedValue(contractToDisplay.UseCents));
            //    listViewItem.SubItems.Add(installment.Amount.GetFormatedValue(contractToDisplay.UseCents));

            //    if (ServicesProvider.GetInstance().GetGeneralSettings().IsOlbBeforeRepayment)
            //        listViewItem.SubItems.Add(installment.OLB.GetFormatedValue(contractToDisplay.UseCents));
            //    else
            //        listViewItem.SubItems.Add(installment.OLBAfterRepayment.GetFormatedValue(contractToDisplay.UseCents));

            //    listViewItem.SubItems.Add(installment.PaidInterests.GetFormatedValue(contractToDisplay.UseCents));
            //    listViewItem.SubItems.Add(installment.PaidCapital.GetFormatedValue(contractToDisplay.UseCents));
            //    if (installment.PaidDate.HasValue)
            //        listViewItem.SubItems.Add(installment.PaidDate.Value.ToShortDateString());
            //    else
            //        listViewItem.SubItems.Add("-");
            //    listViewRepayments.Items.Add(listViewItem);
            //}
        }

        public Loan Contract
        {
            get { return _contract; }
        }

        private void _GetParameters()
        {
            numberOfMaturity = Convert.ToInt32(installmentsNumericUpDown.Value);

            try
            {
                _dateOffsetOrAmount = Int32.Parse(tbDateOffset.Text);
            }
            catch
            {
                _dateOffsetOrAmount = 0;
            }

            _interestRateChanged = applyToOlbCheckbox.Checked;

            _chargeInterestDuringShift = false;
            _chargeInterestDuringGracePeriod = false;
            _IR = Convert.ToDecimal(interestRateNumericUpDown.Value / 100);
            _trancheDate = startDateTimePicker.Value.Date;
            _gracePeriod = 0;
        }

        private void numericUpDownNewIR_ValueChanged(object sender, EventArgs e)
        {
            _GetParameters();
            if ((_dateOffsetOrAmount != 0) && (numberOfMaturity > 0))
            {
                LoanWithNewTranche = _contract;

                Loan fakeContract = ServicesProvider.GetInstance().GetContractServices().FakeTranche(LoanWithNewTranche,
                                                                                                     _trancheDate,
                                                                                                     numberOfMaturity,
                                                                                                     _dateOffsetOrAmount,
                                                                                                     _interestRateChanged,
                                                                                                     _IR);
                RefreshSchedule(fakeContract);
            }
        }

        private void numericUpDownMaturity_ValueChanged(object sender, EventArgs e)
        {
            _GetParameters();
            if ((_dateOffsetOrAmount != 0) && (numberOfMaturity > 0))
            {
                LoanWithNewTranche = _contract;

                Loan fakeContract = ServicesProvider.GetInstance().GetContractServices().FakeTranche(LoanWithNewTranche,
                                                                                                     _trancheDate,
                                                                                                     numberOfMaturity,
                                                                                                     _dateOffsetOrAmount,
                                                                                                     _interestRateChanged,
                                                                                                     _IR);
                RefreshSchedule(fakeContract);
            }
        }

        private void tbDateOffset_TextChanged(object sender, EventArgs e)
        {
            _GetParameters();
            if ((_dateOffsetOrAmount != 0) && (numberOfMaturity > 0))
            {
                LoanWithNewTranche = _contract;

                Loan fakeContract = ServicesProvider.GetInstance().GetContractServices().FakeTranche(LoanWithNewTranche,
                                                                                                     _trancheDate,
                                                                                                     numberOfMaturity,
                                                                                                     _dateOffsetOrAmount,
                                                                                                     _interestRateChanged,
                                                                                                     _IR);
                RefreshSchedule(fakeContract);
            }
        }

        private void tbDateOffset_KeyDown(object sender, KeyEventArgs e)
        {
            Keys c = e.KeyCode;

            if (c >= Keys.NumPad0 && c <= Keys.NumPad9) return;
            if (c >= Keys.D0 && c <= Keys.D9) return;
            if (e.KeyValue == 110 || e.KeyValue == 188) return;
            if (e.Control && (c == Keys.X || c == Keys.C || c == Keys.V || c == Keys.Z)) return;
            if (c == Keys.Delete || c == Keys.Back) return;
            if (c == Keys.Left || c == Keys.Right || c == Keys.Up || c == Keys.Down) return;
            e.SuppressKeyPress = true;
        }

        private void tbDateOffset_Enter(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (0 == tb.Text.Length) tb.Text = @"0";
        }

        private void dateTimePickerStartDate_ValueChanged(object sender, EventArgs e)
        {
            _GetParameters();
            if ((_dateOffsetOrAmount != 0) && (numberOfMaturity > 0))
            {
                LoanWithNewTranche = _contract;

                Loan fakeContract =
                    ServicesProvider.GetInstance().GetContractServices().FakeTranche(LoanWithNewTranche,
                                                                                     _trancheDate,
                                                                                     numberOfMaturity,
                                                                                     _dateOffsetOrAmount,
                                                                                     _interestRateChanged,
                                                                                     _IR);
                RefreshSchedule(fakeContract);
            }
        }

        private void cbApplynewInterestforOLB_CheckedChanged(object sender, EventArgs e)
        {
            _GetParameters();
            if ((_dateOffsetOrAmount != 0) && (numberOfMaturity > 0))
            {
                LoanWithNewTranche = _contract;

                Loan fakeContract = ServicesProvider.GetInstance().GetContractServices().FakeTranche(LoanWithNewTranche,
                                                                                                     _trancheDate,
                                                                                                     numberOfMaturity,
                                                                                                     _dateOffsetOrAmount,
                                                                                                     _interestRateChanged,
                                                                                                     _IR);
                RefreshSchedule(fakeContract);
            }
        }

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            string messageConfirm = GetString("ConfirmTrancheContract.Text") + " " + _contract.Code;
            messageConfirm += "\n" + GetString("ChargeInterest.Text") + " " + (_interestRateChanged ? GetString("Yes.Text") : GetString("No.Text"));
            messageConfirm += "\n" + GetString("NewInstallment.Text") + " " + numberOfMaturity;
            messageConfirm += "\n" + GetString("InterestRate.Text") + " " + _IR * 100 + "%";

            resultReschedulingForm = MessageBox.Show(messageConfirm, GetString("ConfirmTheTranche.Text"),
                                                     MessageBoxButtons.OKCancel,
                                                     MessageBoxIcon.Question);

            if (resultReschedulingForm == DialogResult.OK)
            {
                try
                {
                    {

                        _contract = ServicesProvider.GetInstance().GetContractServices().
                            AddTranche(_contract, _client, _trancheDate, numberOfMaturity, _dateOffsetOrAmount,
                                       _interestRateChanged, _IR);
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                }
                catch (Exception ex)
                {
                    new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
                }
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
