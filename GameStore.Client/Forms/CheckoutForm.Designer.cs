namespace GameStore.Client.Forms
{
    partial class CheckoutForm
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
            lblTitle = new Label();
            lstItems = new ListBox();
            lblTotal = new Label();
            lblBank = new Label();
            cboBankAccount = new ComboBox();
            lblBankNote = new Label();
            btnPay = new Button();
            btnCancel = new Button();
            SuspendLayout();
            //
            // lblTitle
            //
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Leelawadee UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(20, 16);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(110, 36);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "ชำระเงิน";
            //
            // lstItems
            //
            lstItems.BackColor = Color.FromArgb(22, 32, 45);
            lstItems.BorderStyle = BorderStyle.None;
            lstItems.ForeColor = Color.FromArgb(199, 213, 224);
            lstItems.ItemHeight = 23;
            lstItems.Location = new Point(24, 64);
            lstItems.Name = "lstItems";
            lstItems.SelectionMode = SelectionMode.None;
            lstItems.Size = new Size(472, 161);
            lstItems.TabIndex = 1;
            //
            // lblTotal
            //
            lblTotal.Font = new Font("Leelawadee UI", 16F, FontStyle.Bold);
            lblTotal.ForeColor = Color.FromArgb(210, 239, 169);
            lblTotal.Location = new Point(20, 234);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(476, 40);
            lblTotal.TabIndex = 2;
            lblTotal.Text = "ยอดชำระ";
            //
            // lblBank
            //
            lblBank.AutoSize = true;
            lblBank.Location = new Point(24, 284);
            lblBank.Name = "lblBank";
            lblBank.Size = new Size(246, 23);
            lblBank.TabIndex = 3;
            lblBank.Text = "เลขบัญชีธนาคาร (ธนาคารจำลอง)";
            //
            // cboBankAccount
            //
            cboBankAccount.BackColor = Color.FromArgb(50, 61, 76);
            cboBankAccount.FlatStyle = FlatStyle.Flat;
            cboBankAccount.ForeColor = Color.White;
            cboBankAccount.FormattingEnabled = true;
            cboBankAccount.Items.AddRange(new object[] { "111-1-11111-1", "222-2-22222-2", "999-9-99999-9" });
            cboBankAccount.Location = new Point(24, 310);
            cboBankAccount.Name = "cboBankAccount";
            cboBankAccount.Size = new Size(472, 31);
            cboBankAccount.TabIndex = 4;
            //
            // lblBankNote
            //
            lblBankNote.Font = new Font("Leelawadee UI", 9F);
            lblBankNote.ForeColor = Color.FromArgb(143, 152, 160);
            lblBankNote.Location = new Point(24, 348);
            lblBankNote.Name = "lblBankNote";
            lblBankNote.Size = new Size(472, 48);
            lblBankNote.TabIndex = 5;
            lblBankNote.Text = "บัญชีทดสอบ: 111-1-11111-1 (มีเงิน 5,000), 222-2-22222-2 (มีเงิน 300), 999-9-99999-9 (มีเงิน 100,000)  * ไม่ได้เชื่อมต่อธนาคารจริง";
            //
            // btnPay
            //
            btnPay.BackColor = Color.FromArgb(92, 126, 16);
            btnPay.Cursor = Cursors.Hand;
            btnPay.FlatAppearance.BorderSize = 0;
            btnPay.FlatStyle = FlatStyle.Flat;
            btnPay.Font = new Font("Leelawadee UI", 10F, FontStyle.Bold);
            btnPay.ForeColor = Color.FromArgb(210, 239, 169);
            btnPay.Location = new Point(226, 410);
            btnPay.Name = "btnPay";
            btnPay.Size = new Size(170, 44);
            btnPay.TabIndex = 6;
            btnPay.Text = "ยืนยันชำระเงิน";
            btnPay.UseVisualStyleBackColor = false;
            btnPay.Click += btnPay_Click;
            //
            // btnCancel
            //
            btnCancel.BackColor = Color.FromArgb(61, 79, 97);
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(406, 410);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(90, 44);
            btnCancel.TabIndex = 7;
            btnCancel.Text = "ยกเลิก";
            btnCancel.UseVisualStyleBackColor = false;
            //
            // CheckoutForm
            //
            AcceptButton = btnPay;
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(27, 40, 56);
            CancelButton = btnCancel;
            ClientSize = new Size(520, 474);
            Controls.Add(lblTitle);
            Controls.Add(lstItems);
            Controls.Add(lblTotal);
            Controls.Add(lblBank);
            Controls.Add(cboBankAccount);
            Controls.Add(lblBankNote);
            Controls.Add(btnPay);
            Controls.Add(btnCancel);
            Font = new Font("Leelawadee UI", 10F);
            ForeColor = Color.FromArgb(199, 213, 224);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "CheckoutForm";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "ชำระเงิน";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private ListBox lstItems;
        private Label lblTotal;
        private Label lblBank;
        private ComboBox cboBankAccount;
        private Label lblBankNote;
        private Button btnPay;
        private Button btnCancel;
    }
}
