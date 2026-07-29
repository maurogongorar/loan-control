using Cocosoft.Finance.LoanControl.App.ViewModels;

namespace Cocosoft.Finance.LoanControl.App.Dialogs
{
    partial class SelectLoanDialogForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectLoanDialogForm));
            getAllLoansCheckBox = new CheckBox();
            loansLabel = new Label();
            loansDataGridView = new DataGridView();
            loanIdDataGridColumn = new DataGridViewTextBoxColumn();
            debtorDataGridColumn = new DataGridViewTextBoxColumn();
            initialLoanDataGridColumn = new DataGridViewTextBoxColumn();
            currentDebtDataGridColumn = new DataGridViewTextBoxColumn();
            isClosedDataGridColumn = new DataGridViewTextBoxColumn();
            okButton = new Button();
            cancelButton = new Button();
            ((System.ComponentModel.ISupportInitialize)loansDataGridView).BeginInit();
            SuspendLayout();
            // 
            // getAllLoansCheckBox
            // 
            resources.ApplyResources(getAllLoansCheckBox, "getAllLoansCheckBox");
            getAllLoansCheckBox.Name = "getAllLoansCheckBox";
            getAllLoansCheckBox.UseVisualStyleBackColor = true;
            // 
            // loansLabel
            // 
            resources.ApplyResources(loansLabel, "loansLabel");
            loansLabel.Name = "loansLabel";
            // 
            // loansDataGridView
            // 
            loansDataGridView.AllowUserToAddRows = false;
            loansDataGridView.AllowUserToDeleteRows = false;
            loansDataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            loansDataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            loansDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            loansDataGridView.Columns.AddRange(new DataGridViewColumn[] { loanIdDataGridColumn, debtorDataGridColumn, initialLoanDataGridColumn, currentDebtDataGridColumn, isClosedDataGridColumn });
            resources.ApplyResources(loansDataGridView, "loansDataGridView");
            loansDataGridView.MultiSelect = false;
            loansDataGridView.Name = "loansDataGridView";
            loansDataGridView.RowHeadersVisible = false;
            loansDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            // 
            // loanIdDataGridColumn
            // 
            resources.ApplyResources(loanIdDataGridColumn, "loanIdDataGridColumn");
            loanIdDataGridColumn.Name = "loanIdDataGridColumn";
            loanIdDataGridColumn.DataPropertyName = nameof(SelectLoanViewModel.LoanRow.Id);
            // 
            // debtorDataGridColumn
            // 
            debtorDataGridColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(debtorDataGridColumn, "debtorDataGridColumn");
            debtorDataGridColumn.Name = "debtorDataGridColumn";
            debtorDataGridColumn.DataPropertyName = nameof(SelectLoanViewModel.LoanRow.DebtorName);
            // 
            // initialLoanDataGridColumn
            // 
            initialLoanDataGridColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(initialLoanDataGridColumn, "initialLoanDataGridColumn");
            initialLoanDataGridColumn.Name = "initialLoanDataGridColumn";
            initialLoanDataGridColumn.DataPropertyName = nameof(SelectLoanViewModel.LoanRow.InitialAmount);
            initialLoanDataGridColumn.DefaultCellStyle.Format = "C";
            // 
            // currentDebtDataGridColumn
            // 
            currentDebtDataGridColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(currentDebtDataGridColumn, "currentDebtDataGridColumn");
            currentDebtDataGridColumn.Name = "currentDebtDataGridColumn";
            currentDebtDataGridColumn.DataPropertyName = nameof(SelectLoanViewModel.LoanRow.CurrentBalance);
            currentDebtDataGridColumn.DefaultCellStyle.Format = "C";
            // 
            // isClosedDataGridColumn
            // 
            isClosedDataGridColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(isClosedDataGridColumn, "isClosedDataGridColumn");
            isClosedDataGridColumn.Name = "isClosedDataGridColumn";
            isClosedDataGridColumn.DataPropertyName = nameof(SelectLoanViewModel.LoanRow.IsClosed);
            // 
            // okButton
            // 
            resources.ApplyResources(okButton, "okButton");
            okButton.Name = "okButton";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += OkButton_Click;
            // 
            // cancelButton
            // 
            resources.ApplyResources(cancelButton, "cancelButton");
            cancelButton.Name = "cancelButton";
            cancelButton.UseVisualStyleBackColor = true;
            // 
            // SelectLoanDialogForm
            // 
            AcceptButton = okButton;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancelButton;
            Controls.Add(cancelButton);
            Controls.Add(okButton);
            Controls.Add(loansDataGridView);
            Controls.Add(loansLabel);
            Controls.Add(getAllLoansCheckBox);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SelectLoanDialogForm";
            ShowInTaskbar = false;
            ((System.ComponentModel.ISupportInitialize)loansDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckBox getAllLoansCheckBox;
        private Label loansLabel;
        private DataGridView loansDataGridView;
        private Button okButton;
        private Button cancelButton;
        private DataGridViewTextBoxColumn loanIdDataGridColumn;
        private DataGridViewTextBoxColumn debtorDataGridColumn;
        private DataGridViewTextBoxColumn initialLoanDataGridColumn;
        private DataGridViewTextBoxColumn currentDebtDataGridColumn;
        private DataGridViewTextBoxColumn isClosedDataGridColumn;
    }
}