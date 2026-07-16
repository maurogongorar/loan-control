namespace Cocosoft.Finance.LoanControl.App
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            mainMenuStrip = new MenuStrip();
            homeToolStripMenuItem = new ToolStripMenuItem();
            openLoanToolStripMenuItem = new ToolStripMenuItem();
            addLoanToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            debtorInfoGroupBox = new GroupBox();
            interestcollectedTextBox = new TextBox();
            collectedInterestLabel = new Label();
            feeTextBox = new TextBox();
            feeLabel = new Label();
            addPaymentButton = new Button();
            instalmentsPaidTextBox = new TextBox();
            disbursementDateTextBox = new TextBox();
            instalmentsLabel = new Label();
            disbursementDateLabel = new Label();
            interestTextBox = new TextBox();
            interestLabel = new Label();
            currentDebtTextBox = new TextBox();
            currentDebtLabel = new Label();
            initialLoanTextBox = new TextBox();
            initialLoanLabel = new Label();
            debtorNameTextBox = new TextBox();
            debtorNameLabel = new Label();
            paymentsDataGridView = new DataGridView();
            dateDataGridColumn = new DataGridViewTextBoxColumn();
            amountDataGridColumn = new DataGridViewTextBoxColumn();
            interestDataGridColumn = new DataGridViewTextBoxColumn();
            capitalDataGridColumn = new DataGridViewTextBoxColumn();
            newBalanceDataGridColumn = new DataGridViewTextBoxColumn();
            mainMenuStrip.SuspendLayout();
            debtorInfoGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)paymentsDataGridView).BeginInit();
            SuspendLayout();
            // 
            // mainMenuStrip
            // 
            resources.ApplyResources(mainMenuStrip, "mainMenuStrip");
            mainMenuStrip.BackColor = SystemColors.GradientInactiveCaption;
            mainMenuStrip.ImageScalingSize = new Size(20, 20);
            mainMenuStrip.Items.AddRange(new ToolStripItem[] { homeToolStripMenuItem });
            mainMenuStrip.Name = "mainMenuStrip";
            // 
            // homeToolStripMenuItem
            // 
            resources.ApplyResources(homeToolStripMenuItem, "homeToolStripMenuItem");
            homeToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openLoanToolStripMenuItem, addLoanToolStripMenuItem, exitToolStripMenuItem });
            homeToolStripMenuItem.Name = "homeToolStripMenuItem";
            // 
            // openLoanToolStripMenuItem
            // 
            resources.ApplyResources(openLoanToolStripMenuItem, "openLoanToolStripMenuItem");
            openLoanToolStripMenuItem.Name = "openLoanToolStripMenuItem";
            openLoanToolStripMenuItem.Click += OpenLoanToolStripMenuItem_Click;
            // 
            // addLoanToolStripMenuItem
            // 
            resources.ApplyResources(addLoanToolStripMenuItem, "addLoanToolStripMenuItem");
            addLoanToolStripMenuItem.Name = "addLoanToolStripMenuItem";
            addLoanToolStripMenuItem.Click += AddLoanToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem
            // 
            resources.ApplyResources(exitToolStripMenuItem, "exitToolStripMenuItem");
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Click += ExitToolStripMenuItem_Click;
            // 
            // debtorInfoGroupBox
            // 
            resources.ApplyResources(debtorInfoGroupBox, "debtorInfoGroupBox");
            debtorInfoGroupBox.Controls.Add(interestcollectedTextBox);
            debtorInfoGroupBox.Controls.Add(collectedInterestLabel);
            debtorInfoGroupBox.Controls.Add(feeTextBox);
            debtorInfoGroupBox.Controls.Add(feeLabel);
            debtorInfoGroupBox.Controls.Add(addPaymentButton);
            debtorInfoGroupBox.Controls.Add(instalmentsPaidTextBox);
            debtorInfoGroupBox.Controls.Add(disbursementDateTextBox);
            debtorInfoGroupBox.Controls.Add(instalmentsLabel);
            debtorInfoGroupBox.Controls.Add(disbursementDateLabel);
            debtorInfoGroupBox.Controls.Add(interestTextBox);
            debtorInfoGroupBox.Controls.Add(interestLabel);
            debtorInfoGroupBox.Controls.Add(currentDebtTextBox);
            debtorInfoGroupBox.Controls.Add(currentDebtLabel);
            debtorInfoGroupBox.Controls.Add(initialLoanTextBox);
            debtorInfoGroupBox.Controls.Add(initialLoanLabel);
            debtorInfoGroupBox.Controls.Add(debtorNameTextBox);
            debtorInfoGroupBox.Controls.Add(debtorNameLabel);
            debtorInfoGroupBox.Name = "debtorInfoGroupBox";
            debtorInfoGroupBox.TabStop = false;
            // 
            // interestcollectedTextBox
            // 
            resources.ApplyResources(interestcollectedTextBox, "interestcollectedTextBox");
            interestcollectedTextBox.BackColor = Color.White;
            interestcollectedTextBox.Name = "interestcollectedTextBox";
            interestcollectedTextBox.ReadOnly = true;
            // 
            // collectedInterestLabel
            // 
            resources.ApplyResources(collectedInterestLabel, "collectedInterestLabel");
            collectedInterestLabel.Name = "collectedInterestLabel";
            // 
            // feeTextBox
            // 
            resources.ApplyResources(feeTextBox, "feeTextBox");
            feeTextBox.BackColor = Color.White;
            feeTextBox.Name = "feeTextBox";
            feeTextBox.ReadOnly = true;
            // 
            // feeLabel
            // 
            resources.ApplyResources(feeLabel, "feeLabel");
            feeLabel.Name = "feeLabel";
            // 
            // addPaymentButton
            // 
            resources.ApplyResources(addPaymentButton, "addPaymentButton");
            addPaymentButton.Name = "addPaymentButton";
            addPaymentButton.UseVisualStyleBackColor = true;
            addPaymentButton.Click += AddPaymentButton_Click;
            // 
            // instalmentsPaidTextBox
            // 
            resources.ApplyResources(instalmentsPaidTextBox, "instalmentsPaidTextBox");
            instalmentsPaidTextBox.BackColor = Color.White;
            instalmentsPaidTextBox.Name = "instalmentsPaidTextBox";
            instalmentsPaidTextBox.ReadOnly = true;
            // 
            // disbursementDateTextBox
            // 
            resources.ApplyResources(disbursementDateTextBox, "disbursementDateTextBox");
            disbursementDateTextBox.BackColor = Color.White;
            disbursementDateTextBox.Name = "disbursementDateTextBox";
            disbursementDateTextBox.ReadOnly = true;
            // 
            // instalmentsLabel
            // 
            resources.ApplyResources(instalmentsLabel, "instalmentsLabel");
            instalmentsLabel.Name = "instalmentsLabel";
            // 
            // disbursementDateLabel
            // 
            resources.ApplyResources(disbursementDateLabel, "disbursementDateLabel");
            disbursementDateLabel.Name = "disbursementDateLabel";
            // 
            // interestTextBox
            // 
            resources.ApplyResources(interestTextBox, "interestTextBox");
            interestTextBox.BackColor = Color.White;
            interestTextBox.Name = "interestTextBox";
            interestTextBox.ReadOnly = true;
            // 
            // interestLabel
            // 
            resources.ApplyResources(interestLabel, "interestLabel");
            interestLabel.Name = "interestLabel";
            // 
            // currentDebtTextBox
            // 
            resources.ApplyResources(currentDebtTextBox, "currentDebtTextBox");
            currentDebtTextBox.BackColor = Color.White;
            currentDebtTextBox.Name = "currentDebtTextBox";
            currentDebtTextBox.ReadOnly = true;
            // 
            // currentDebtLabel
            // 
            resources.ApplyResources(currentDebtLabel, "currentDebtLabel");
            currentDebtLabel.Name = "currentDebtLabel";
            // 
            // initialLoanTextBox
            // 
            resources.ApplyResources(initialLoanTextBox, "initialLoanTextBox");
            initialLoanTextBox.BackColor = Color.White;
            initialLoanTextBox.Name = "initialLoanTextBox";
            initialLoanTextBox.ReadOnly = true;
            // 
            // initialLoanLabel
            // 
            resources.ApplyResources(initialLoanLabel, "initialLoanLabel");
            initialLoanLabel.Name = "initialLoanLabel";
            // 
            // debtorNameTextBox
            // 
            resources.ApplyResources(debtorNameTextBox, "debtorNameTextBox");
            debtorNameTextBox.BackColor = Color.White;
            debtorNameTextBox.Name = "debtorNameTextBox";
            debtorNameTextBox.ReadOnly = true;
            // 
            // debtorNameLabel
            // 
            resources.ApplyResources(debtorNameLabel, "debtorNameLabel");
            debtorNameLabel.Name = "debtorNameLabel";
            // 
            // paymentsDataGridView
            // 
            resources.ApplyResources(paymentsDataGridView, "paymentsDataGridView");
            paymentsDataGridView.AllowUserToAddRows = false;
            paymentsDataGridView.AllowUserToDeleteRows = false;
            paymentsDataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            paymentsDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            paymentsDataGridView.Columns.AddRange(new DataGridViewColumn[] { dateDataGridColumn, amountDataGridColumn, interestDataGridColumn, capitalDataGridColumn, newBalanceDataGridColumn });
            paymentsDataGridView.MultiSelect = false;
            paymentsDataGridView.Name = "paymentsDataGridView";
            paymentsDataGridView.RowHeadersVisible = false;
            paymentsDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            // 
            // dateDataGridColumn
            // 
            dateDataGridColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(dateDataGridColumn, "dateDataGridColumn");
            dateDataGridColumn.Name = "dateDataGridColumn";
            // 
            // amountDataGridColumn
            // 
            amountDataGridColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(amountDataGridColumn, "amountDataGridColumn");
            amountDataGridColumn.Name = "amountDataGridColumn";
            // 
            // interestDataGridColumn
            // 
            interestDataGridColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(interestDataGridColumn, "interestDataGridColumn");
            interestDataGridColumn.Name = "interestDataGridColumn";
            // 
            // capitalDataGridColumn
            // 
            capitalDataGridColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(capitalDataGridColumn, "capitalDataGridColumn");
            capitalDataGridColumn.Name = "capitalDataGridColumn";
            // 
            // newBalanceDataGridColumn
            // 
            newBalanceDataGridColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(newBalanceDataGridColumn, "newBalanceDataGridColumn");
            newBalanceDataGridColumn.Name = "newBalanceDataGridColumn";
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(paymentsDataGridView);
            Controls.Add(debtorInfoGroupBox);
            Controls.Add(mainMenuStrip);
            MainMenuStrip = mainMenuStrip;
            Name = "MainForm";
            mainMenuStrip.ResumeLayout(false);
            mainMenuStrip.PerformLayout();
            debtorInfoGroupBox.ResumeLayout(false);
            debtorInfoGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)paymentsDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip mainMenuStrip;
        private ToolStripMenuItem homeToolStripMenuItem;
        private ToolStripMenuItem openLoanToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private GroupBox debtorInfoGroupBox;
        private TextBox currentDebtTextBox;
        private Label currentDebtLabel;
        private TextBox initialLoanTextBox;
        private Label initialLoanLabel;
        private TextBox debtorNameTextBox;
        private Label debtorNameLabel;
        private Label instalmentsLabel;
        private Label disbursementDateLabel;
        private TextBox interestTextBox;
        private Label interestLabel;
        private TextBox disbursementDateTextBox;
        private TextBox instalmentsPaidTextBox;
        private Button addPaymentButton;
        private DataGridView paymentsDataGridView;
        private TextBox feeTextBox;
        private Label feeLabel;
        private ToolStripMenuItem addLoanToolStripMenuItem;
        private TextBox interestcollectedTextBox;
        private Label collectedInterestLabel;
        private DataGridViewTextBoxColumn dateDataGridColumn;
        private DataGridViewTextBoxColumn amountDataGridColumn;
        private DataGridViewTextBoxColumn interestDataGridColumn;
        private DataGridViewTextBoxColumn capitalDataGridColumn;
        private DataGridViewTextBoxColumn newBalanceDataGridColumn;
    }
}
