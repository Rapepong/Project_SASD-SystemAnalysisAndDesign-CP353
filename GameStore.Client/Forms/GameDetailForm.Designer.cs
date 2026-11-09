namespace GameStore.Client.Forms
{
    partial class GameDetailForm
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
            pnlCover = new Panel();
            lblTitle = new Label();
            lblDeveloper = new Label();
            lblRelease = new Label();
            lblGenres = new Label();
            lblPrice = new Label();
            txtDescription = new TextBox();
            btnAddToCart = new Button();
            btnClose = new Button();
            SuspendLayout();
            //
            // pnlCover
            //
            pnlCover.Location = new Point(24, 24);
            pnlCover.Name = "pnlCover";
            pnlCover.Size = new Size(300, 180);
            pnlCover.TabIndex = 0;
            pnlCover.Paint += pnlCover_Paint;
            //
            // lblTitle
            //
            lblTitle.AutoEllipsis = true;
            lblTitle.Font = new Font("Leelawadee UI", 18F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(340, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(356, 44);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "กำลังโหลด...";
            //
            // lblDeveloper
            //
            lblDeveloper.ForeColor = Color.FromArgb(143, 152, 160);
            lblDeveloper.Location = new Point(344, 66);
            lblDeveloper.Name = "lblDeveloper";
            lblDeveloper.Size = new Size(352, 26);
            lblDeveloper.TabIndex = 2;
            //
            // lblRelease
            //
            lblRelease.ForeColor = Color.FromArgb(143, 152, 160);
            lblRelease.Location = new Point(344, 94);
            lblRelease.Name = "lblRelease";
            lblRelease.Size = new Size(352, 26);
            lblRelease.TabIndex = 3;
            //
            // lblGenres
            //
            lblGenres.ForeColor = Color.FromArgb(102, 192, 244);
            lblGenres.Location = new Point(344, 122);
            lblGenres.Name = "lblGenres";
            lblGenres.Size = new Size(352, 26);
            lblGenres.TabIndex = 4;
            //
            // lblPrice
            //
            lblPrice.Font = new Font("Leelawadee UI", 18F, FontStyle.Bold);
            lblPrice.ForeColor = Color.FromArgb(210, 239, 169);
            lblPrice.Location = new Point(340, 158);
            lblPrice.Name = "lblPrice";
            lblPrice.Size = new Size(356, 44);
            lblPrice.TabIndex = 5;
            //
            // txtDescription
            //
            txtDescription.BackColor = Color.FromArgb(22, 32, 45);
            txtDescription.BorderStyle = BorderStyle.None;
            txtDescription.ForeColor = Color.FromArgb(199, 213, 224);
            txtDescription.Location = new Point(24, 224);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.ReadOnly = true;
            txtDescription.Size = new Size(672, 140);
            txtDescription.TabIndex = 6;
            txtDescription.TabStop = false;
            //
            // btnAddToCart
            //
            btnAddToCart.BackColor = Color.FromArgb(92, 126, 16);
            btnAddToCart.Cursor = Cursors.Hand;
            btnAddToCart.FlatAppearance.BorderSize = 0;
            btnAddToCart.FlatStyle = FlatStyle.Flat;
            btnAddToCart.Font = new Font("Leelawadee UI", 10F, FontStyle.Bold);
            btnAddToCart.ForeColor = Color.FromArgb(210, 239, 169);
            btnAddToCart.Location = new Point(416, 386);
            btnAddToCart.Name = "btnAddToCart";
            btnAddToCart.Size = new Size(170, 44);
            btnAddToCart.TabIndex = 7;
            btnAddToCart.Text = "ใส่ตะกร้า";
            btnAddToCart.UseVisualStyleBackColor = false;
            btnAddToCart.Click += btnAddToCart_Click;
            //
            // btnClose
            //
            btnClose.BackColor = Color.FromArgb(61, 79, 97);
            btnClose.Cursor = Cursors.Hand;
            btnClose.DialogResult = DialogResult.Cancel;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.ForeColor = Color.White;
            btnClose.Location = new Point(596, 386);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(100, 44);
            btnClose.TabIndex = 8;
            btnClose.Text = "ปิด";
            btnClose.UseVisualStyleBackColor = false;
            //
            // GameDetailForm
            //
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(27, 40, 56);
            CancelButton = btnClose;
            ClientSize = new Size(720, 454);
            Controls.Add(pnlCover);
            Controls.Add(lblTitle);
            Controls.Add(lblDeveloper);
            Controls.Add(lblRelease);
            Controls.Add(lblGenres);
            Controls.Add(lblPrice);
            Controls.Add(txtDescription);
            Controls.Add(btnAddToCart);
            Controls.Add(btnClose);
            Font = new Font("Leelawadee UI", 10F);
            ForeColor = Color.FromArgb(199, 213, 224);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "GameDetailForm";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "รายละเอียดเกม";
            Load += GameDetailForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel pnlCover;
        private Label lblTitle;
        private Label lblDeveloper;
        private Label lblRelease;
        private Label lblGenres;
        private Label lblPrice;
        private TextBox txtDescription;
        private Button btnAddToCart;
        private Button btnClose;
    }
}
