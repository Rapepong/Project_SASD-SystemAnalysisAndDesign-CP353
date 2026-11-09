namespace GameStore.Client.Forms
{
    partial class LoginForm
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
            flowMain = new FlowLayoutPanel();
            lblLogo = new Label();
            lblSubtitle = new Label();
            lblUsername = new Label();
            txtUsername = new TextBox();
            lblPassword = new Label();
            txtPassword = new TextBox();
            lblDisplayName = new Label();
            txtDisplayName = new TextBox();
            lblEmail = new Label();
            txtEmail = new TextBox();
            btnSubmit = new Button();
            lnkToggleMode = new LinkLabel();
            lblHint = new Label();
            flowMain.SuspendLayout();
            SuspendLayout();
            //
            // flowMain
            //
            flowMain.Controls.Add(lblLogo);
            flowMain.Controls.Add(lblSubtitle);
            flowMain.Controls.Add(lblUsername);
            flowMain.Controls.Add(txtUsername);
            flowMain.Controls.Add(lblPassword);
            flowMain.Controls.Add(txtPassword);
            flowMain.Controls.Add(lblDisplayName);
            flowMain.Controls.Add(txtDisplayName);
            flowMain.Controls.Add(lblEmail);
            flowMain.Controls.Add(txtEmail);
            flowMain.Controls.Add(btnSubmit);
            flowMain.Controls.Add(lnkToggleMode);
            flowMain.Controls.Add(lblHint);
            flowMain.AutoSize = true;
            flowMain.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowMain.FlowDirection = FlowDirection.TopDown;
            flowMain.Location = new Point(0, 0);
            flowMain.Name = "flowMain";
            flowMain.Padding = new Padding(40, 28, 40, 20);
            flowMain.Size = new Size(424, 561);
            flowMain.TabIndex = 0;
            flowMain.WrapContents = false;
            //
            // lblLogo
            //
            lblLogo.AutoSize = true;
            lblLogo.Font = new Font("Leelawadee UI", 24F, FontStyle.Bold);
            lblLogo.ForeColor = Color.FromArgb(102, 192, 244);
            lblLogo.Location = new Point(43, 28);
            lblLogo.Name = "lblLogo";
            lblLogo.Size = new Size(234, 45);
            lblLogo.TabIndex = 0;
            lblLogo.Text = "GAME STORE";
            //
            // lblSubtitle
            //
            lblSubtitle.AutoSize = true;
            lblSubtitle.ForeColor = Color.FromArgb(143, 152, 160);
            lblSubtitle.Location = new Point(43, 73);
            lblSubtitle.Margin = new Padding(3, 0, 3, 18);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(180, 23);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "เข้าสู่ระบบเพื่อเลือกซื้อเกม";
            //
            // lblUsername
            //
            lblUsername.AutoSize = true;
            lblUsername.Location = new Point(43, 114);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(152, 23);
            lblUsername.TabIndex = 2;
            lblUsername.Text = "ชื่อผู้ใช้ (Username)";
            //
            // txtUsername
            //
            txtUsername.BackColor = Color.FromArgb(50, 61, 76);
            txtUsername.BorderStyle = BorderStyle.FixedSingle;
            txtUsername.ForeColor = Color.White;
            txtUsername.Location = new Point(43, 140);
            txtUsername.Margin = new Padding(3, 3, 3, 12);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(336, 30);
            txtUsername.TabIndex = 3;
            //
            // lblPassword
            //
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(43, 182);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(68, 23);
            lblPassword.TabIndex = 4;
            lblPassword.Text = "รหัสผ่าน";
            //
            // txtPassword
            //
            txtPassword.BackColor = Color.FromArgb(50, 61, 76);
            txtPassword.BorderStyle = BorderStyle.FixedSingle;
            txtPassword.ForeColor = Color.White;
            txtPassword.Location = new Point(43, 208);
            txtPassword.Margin = new Padding(3, 3, 3, 12);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(336, 30);
            txtPassword.TabIndex = 5;
            txtPassword.UseSystemPasswordChar = true;
            //
            // lblDisplayName
            //
            lblDisplayName.AutoSize = true;
            lblDisplayName.Location = new Point(43, 250);
            lblDisplayName.Name = "lblDisplayName";
            lblDisplayName.Size = new Size(93, 23);
            lblDisplayName.TabIndex = 6;
            lblDisplayName.Text = "ชื่อที่แสดง";
            //
            // txtDisplayName
            //
            txtDisplayName.BackColor = Color.FromArgb(50, 61, 76);
            txtDisplayName.BorderStyle = BorderStyle.FixedSingle;
            txtDisplayName.ForeColor = Color.White;
            txtDisplayName.Location = new Point(43, 276);
            txtDisplayName.Margin = new Padding(3, 3, 3, 12);
            txtDisplayName.Name = "txtDisplayName";
            txtDisplayName.Size = new Size(336, 30);
            txtDisplayName.TabIndex = 7;
            //
            // lblEmail
            //
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(43, 318);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(48, 23);
            lblEmail.TabIndex = 8;
            lblEmail.Text = "อีเมล";
            //
            // txtEmail
            //
            txtEmail.BackColor = Color.FromArgb(50, 61, 76);
            txtEmail.BorderStyle = BorderStyle.FixedSingle;
            txtEmail.ForeColor = Color.White;
            txtEmail.Location = new Point(43, 344);
            txtEmail.Margin = new Padding(3, 3, 3, 12);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(336, 30);
            txtEmail.TabIndex = 9;
            //
            // btnSubmit
            //
            btnSubmit.BackColor = Color.FromArgb(92, 126, 16);
            btnSubmit.Cursor = Cursors.Hand;
            btnSubmit.FlatAppearance.BorderSize = 0;
            btnSubmit.FlatStyle = FlatStyle.Flat;
            btnSubmit.Font = new Font("Leelawadee UI", 11F, FontStyle.Bold);
            btnSubmit.ForeColor = Color.FromArgb(210, 239, 169);
            btnSubmit.Location = new Point(43, 396);
            btnSubmit.Margin = new Padding(3, 10, 3, 10);
            btnSubmit.Name = "btnSubmit";
            btnSubmit.Size = new Size(336, 44);
            btnSubmit.TabIndex = 10;
            btnSubmit.Text = "เข้าสู่ระบบ";
            btnSubmit.UseVisualStyleBackColor = false;
            btnSubmit.Click += btnSubmit_Click;
            //
            // lnkToggleMode
            //
            lnkToggleMode.ActiveLinkColor = Color.White;
            lnkToggleMode.AutoSize = true;
            lnkToggleMode.LinkColor = Color.FromArgb(102, 192, 244);
            lnkToggleMode.Location = new Point(43, 450);
            lnkToggleMode.Name = "lnkToggleMode";
            lnkToggleMode.Size = new Size(212, 23);
            lnkToggleMode.TabIndex = 11;
            lnkToggleMode.TabStop = true;
            lnkToggleMode.Text = "ยังไม่มีบัญชี? สมัครสมาชิก";
            lnkToggleMode.LinkClicked += lnkToggleMode_LinkClicked;
            //
            // lblHint
            //
            lblHint.AutoSize = true;
            lblHint.ForeColor = Color.FromArgb(143, 152, 160);
            lblHint.Location = new Point(43, 483);
            lblHint.Margin = new Padding(3, 10, 3, 0);
            lblHint.Name = "lblHint";
            lblHint.Size = new Size(260, 23);
            lblHint.TabIndex = 12;
            lblHint.Text = "บัญชีทดสอบ: player1 / pass1234";
            //
            // LoginForm
            //
            AcceptButton = btnSubmit;
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.FromArgb(27, 40, 56);
            ClientSize = new Size(424, 561);
            Controls.Add(flowMain);
            Font = new Font("Leelawadee UI", 10F);
            ForeColor = Color.FromArgb(199, 213, 224);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Game Store - เข้าสู่ระบบ";
            flowMain.ResumeLayout(false);
            flowMain.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel flowMain;
        private Label lblLogo;
        private Label lblSubtitle;
        private Label lblUsername;
        private TextBox txtUsername;
        private Label lblPassword;
        private TextBox txtPassword;
        private Label lblDisplayName;
        private TextBox txtDisplayName;
        private Label lblEmail;
        private TextBox txtEmail;
        private Button btnSubmit;
        private LinkLabel lnkToggleMode;
        private Label lblHint;
    }
}
