namespace GameStore.Client.Forms
{
    partial class RefundForm
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
            lblGame = new Label();
            lblDetail = new Label();
            lblReason = new Label();
            txtReason = new TextBox();
            btnConfirm = new Button();
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
            lblTitle.Size = new Size(117, 36);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "ขอคืนเงิน";
            //
            // lblGame
            //
            lblGame.AutoEllipsis = true;
            lblGame.Font = new Font("Leelawadee UI", 12F, FontStyle.Bold);
            lblGame.ForeColor = Color.FromArgb(102, 192, 244);
            lblGame.Location = new Point(24, 62);
            lblGame.Name = "lblGame";
            lblGame.Size = new Size(432, 30);
            lblGame.TabIndex = 1;
            //
            // lblDetail
            //
            lblDetail.ForeColor = Color.FromArgb(143, 152, 160);
            lblDetail.Location = new Point(24, 96);
            lblDetail.Name = "lblDetail";
            lblDetail.Size = new Size(432, 72);
            lblDetail.TabIndex = 2;
            //
            // lblReason
            //
            lblReason.AutoSize = true;
            lblReason.Location = new Point(24, 172);
            lblReason.Name = "lblReason";
            lblReason.Size = new Size(234, 23);
            lblReason.TabIndex = 3;
            lblReason.Text = "เหตุผลในการขอคืนเงิน (ไม่บังคับ)";
            //
            // txtReason
            //
            txtReason.BackColor = Color.FromArgb(50, 61, 76);
            txtReason.BorderStyle = BorderStyle.FixedSingle;
            txtReason.ForeColor = Color.White;
            txtReason.Location = new Point(24, 198);
            txtReason.MaxLength = 500;
            txtReason.Multiline = true;
            txtReason.Name = "txtReason";
            txtReason.Size = new Size(432, 96);
            txtReason.TabIndex = 4;
            //
            // btnConfirm
            //
            btnConfirm.BackColor = Color.FromArgb(150, 50, 50);
            btnConfirm.Cursor = Cursors.Hand;
            btnConfirm.FlatAppearance.BorderSize = 0;
            btnConfirm.FlatStyle = FlatStyle.Flat;
            btnConfirm.Font = new Font("Leelawadee UI", 10F, FontStyle.Bold);
            btnConfirm.ForeColor = Color.White;
            btnConfirm.Location = new Point(196, 312);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.Size = new Size(170, 44);
            btnConfirm.TabIndex = 5;
            btnConfirm.Text = "ยืนยันขอคืนเงิน";
            btnConfirm.UseVisualStyleBackColor = false;
            btnConfirm.Click += btnConfirm_Click;
            //
            // btnCancel
            //
            btnCancel.BackColor = Color.FromArgb(61, 79, 97);
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(376, 312);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(80, 44);
            btnCancel.TabIndex = 6;
            btnCancel.Text = "ยกเลิก";
            btnCancel.UseVisualStyleBackColor = false;
            //
            // RefundForm
            //
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(27, 40, 56);
            CancelButton = btnCancel;
            ClientSize = new Size(480, 376);
            Controls.Add(lblTitle);
            Controls.Add(lblGame);
            Controls.Add(lblDetail);
            Controls.Add(lblReason);
            Controls.Add(txtReason);
            Controls.Add(btnConfirm);
            Controls.Add(btnCancel);
            Font = new Font("Leelawadee UI", 10F);
            ForeColor = Color.FromArgb(199, 213, 224);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "RefundForm";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "ขอคืนเงิน";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Label lblGame;
        private Label lblDetail;
        private Label lblReason;
        private TextBox txtReason;
        private Button btnConfirm;
        private Button btnCancel;
    }
}
