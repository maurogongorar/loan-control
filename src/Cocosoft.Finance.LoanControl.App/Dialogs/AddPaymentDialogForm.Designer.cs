namespace Cocosoft.Finance.LoanControl.App.Dialogs
{
    partial class AddPaymentDialogForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddPaymentDialogForm));
            feeLabel = new Label();
            feeTextBox = new TextBox();
            interestLabel = new Label();
            interestTextBox = new TextBox();
            capitalLabel = new Label();
            capitalTextBox = new TextBox();
            okButton = new Button();
            cancelButton = new Button();
            newBalanceLabel = new Label();
            newBalanceTextBox = new TextBox();
            SuspendLayout();
            // 
            // feeLabel
            // 
            resources.ApplyResources(feeLabel, "feeLabel");
            feeLabel.Name = "feeLabel";
            // 
            // feeTextBox
            // 
            resources.ApplyResources(feeTextBox, "feeTextBox");
            feeTextBox.Name = "feeTextBox";
            feeTextBox.TextChanged += FeeTextBox_TextChanged;
            feeTextBox.KeyPress += FeeTextBox_KeyPress;
            // 
            // interestLabel
            // 
            resources.ApplyResources(interestLabel, "interestLabel");
            interestLabel.Name = "interestLabel";
            // 
            // interestTextBox
            // 
            resources.ApplyResources(interestTextBox, "interestTextBox");
            interestTextBox.BackColor = Color.White;
            interestTextBox.Name = "interestTextBox";
            interestTextBox.ReadOnly = true;
            // 
            // capitalLabel
            // 
            resources.ApplyResources(capitalLabel, "capitalLabel");
            capitalLabel.Name = "capitalLabel";
            // 
            // capitalTextBox
            // 
            resources.ApplyResources(capitalTextBox, "capitalTextBox");
            capitalTextBox.BackColor = Color.White;
            capitalTextBox.Name = "capitalTextBox";
            capitalTextBox.ReadOnly = true;
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
            cancelButton.Click += CancelButton_Click;
            // 
            // newBalanceLabel
            // 
            resources.ApplyResources(newBalanceLabel, "newBalanceLabel");
            newBalanceLabel.Name = "newBalanceLabel";
            // 
            // newBalanceTextBox
            // 
            resources.ApplyResources(newBalanceTextBox, "newBalanceTextBox");
            newBalanceTextBox.BackColor = Color.White;
            newBalanceTextBox.Name = "newBalanceTextBox";
            newBalanceTextBox.ReadOnly = true;
            // 
            // AddPaymentDialogForm
            // 
            AcceptButton = okButton;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancelButton;
            Controls.Add(newBalanceTextBox);
            Controls.Add(newBalanceLabel);
            Controls.Add(cancelButton);
            Controls.Add(okButton);
            Controls.Add(capitalTextBox);
            Controls.Add(capitalLabel);
            Controls.Add(interestTextBox);
            Controls.Add(interestLabel);
            Controls.Add(feeTextBox);
            Controls.Add(feeLabel);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AddPaymentDialogForm";
            ShowInTaskbar = false;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label feeLabel;
        private TextBox feeTextBox;
        private Label interestLabel;
        private TextBox interestTextBox;
        private Label capitalLabel;
        private TextBox capitalTextBox;
        private Button okButton;
        private Button cancelButton;
        private Label newBalanceLabel;
        private TextBox newBalanceTextBox;
    }
}