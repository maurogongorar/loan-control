namespace Cocosoft.Finance.LoanControl.App.Dialogs
{
    partial class AddLoanDialogForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddLoanDialogForm));
            okButton = new Button();
            cancelButton = new Button();
            debtorNameLabel = new Label();
            debtorNameTextBox = new TextBox();
            annualInterestLabel = new Label();
            annualInterestTextBox = new TextBox();
            amountLabel = new Label();
            amountTextBox = new TextBox();
            numberInstalmentsLabel = new Label();
            numberInstalmentsTextBox = new TextBox();
            feeLabel = new Label();
            monthlyFeeTextBox = new TextBox();
            percentageLabel = new Label();
            monthlyInterestLabel = new Label();
            monthlyInterestTextBox = new TextBox();
            percentage2Label = new Label();
            SuspendLayout();
            // 
            // okButton
            // 
            resources.ApplyResources(okButton, "okButton");
            okButton.Name = "okButton";
            okButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            resources.ApplyResources(cancelButton, "cancelButton");
            cancelButton.Name = "cancelButton";
            cancelButton.UseVisualStyleBackColor = true;
            // 
            // debtorNameLabel
            // 
            resources.ApplyResources(debtorNameLabel, "debtorNameLabel");
            debtorNameLabel.Name = "debtorNameLabel";
            // 
            // debtorNameTextBox
            // 
            resources.ApplyResources(debtorNameTextBox, "debtorNameTextBox");
            debtorNameTextBox.Name = "debtorNameTextBox";
            // 
            // annualInterestLabel
            // 
            resources.ApplyResources(annualInterestLabel, "annualInterestLabel");
            annualInterestLabel.Name = "annualInterestLabel";
            // 
            // annualInterestTextBox
            // 
            resources.ApplyResources(annualInterestTextBox, "annualInterestTextBox");
            annualInterestTextBox.Name = "annualInterestTextBox";
            annualInterestTextBox.Tag = "decimal";
            // 
            // amountLabel
            // 
            resources.ApplyResources(amountLabel, "amountLabel");
            amountLabel.Name = "amountLabel";
            // 
            // amountTextBox
            // 
            resources.ApplyResources(amountTextBox, "amountTextBox");
            amountTextBox.Name = "amountTextBox";
            amountTextBox.Tag = "decimal";
            // 
            // numberInstalmentsLabel
            // 
            resources.ApplyResources(numberInstalmentsLabel, "numberInstalmentsLabel");
            numberInstalmentsLabel.Name = "numberInstalmentsLabel";
            // 
            // numberInstalmentsTextBox
            // 
            resources.ApplyResources(numberInstalmentsTextBox, "numberInstalmentsTextBox");
            numberInstalmentsTextBox.Name = "numberInstalmentsTextBox";
            numberInstalmentsTextBox.Tag = "integer";
            // 
            // feeLabel
            // 
            resources.ApplyResources(feeLabel, "feeLabel");
            feeLabel.Name = "feeLabel";
            // 
            // monthlyFeeTextBox
            // 
            resources.ApplyResources(monthlyFeeTextBox, "monthlyFeeTextBox");
            monthlyFeeTextBox.BackColor = Color.White;
            monthlyFeeTextBox.Name = "monthlyFeeTextBox";
            monthlyFeeTextBox.ReadOnly = true;
            // 
            // percentageLabel
            // 
            resources.ApplyResources(percentageLabel, "percentageLabel");
            percentageLabel.Name = "percentageLabel";
            // 
            // monthlyInterestLabel
            // 
            resources.ApplyResources(monthlyInterestLabel, "monthlyInterestLabel");
            monthlyInterestLabel.Name = "monthlyInterestLabel";
            // 
            // monthlyInterestTextBox
            // 
            resources.ApplyResources(monthlyInterestTextBox, "monthlyInterestTextBox");
            monthlyInterestTextBox.Name = "monthlyInterestTextBox";
            monthlyInterestTextBox.Tag = "decimal";
            // 
            // percentage2Label
            // 
            resources.ApplyResources(percentage2Label, "percentage2Label");
            percentage2Label.Name = "percentage2Label";
            // 
            // AddLoanDialogForm
            // 
            AcceptButton = okButton;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancelButton;
            Controls.Add(percentage2Label);
            Controls.Add(monthlyInterestTextBox);
            Controls.Add(monthlyInterestLabel);
            Controls.Add(percentageLabel);
            Controls.Add(monthlyFeeTextBox);
            Controls.Add(feeLabel);
            Controls.Add(numberInstalmentsTextBox);
            Controls.Add(numberInstalmentsLabel);
            Controls.Add(amountTextBox);
            Controls.Add(amountLabel);
            Controls.Add(annualInterestTextBox);
            Controls.Add(annualInterestLabel);
            Controls.Add(debtorNameTextBox);
            Controls.Add(debtorNameLabel);
            Controls.Add(cancelButton);
            Controls.Add(okButton);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AddLoanDialogForm";
            ShowInTaskbar = false;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button okButton;
        private Button cancelButton;
        private Label debtorNameLabel;
        private TextBox debtorNameTextBox;
        private Label annualInterestLabel;
        private TextBox annualInterestTextBox;
        private Label amountLabel;
        private TextBox amountTextBox;
        private Label numberInstalmentsLabel;
        private TextBox numberInstalmentsTextBox;
        private Label feeLabel;
        private TextBox monthlyFeeTextBox;
        private Label percentageLabel;
        private Label monthlyInterestLabel;
        private TextBox monthlyInterestTextBox;
        private Label percentage2Label;
    }
}