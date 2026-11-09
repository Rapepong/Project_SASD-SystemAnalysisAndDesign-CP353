namespace GameStore.Client.Forms
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
            pnlTop = new Panel();
            lblLogo = new Label();
            btnNavStore = new Button();
            btnNavCart = new Button();
            btnNavLibrary = new Button();
            btnNavHistory = new Button();
            lblUser = new Label();
            btnLogout = new Button();
            lblStatus = new Label();
            pnlStore = new Panel();
            flowGames = new FlowLayoutPanel();
            pnlStoreFilter = new Panel();
            txtSearch = new TextBox();
            btnSearch = new Button();
            btnClearSearch = new Button();
            lblResultCount = new Label();
            flowGenres = new FlowLayoutPanel();
            pnlCart = new Panel();
            gridCart = new DataGridView();
            pnlCartSpacer = new Panel();
            pnlCartSummary = new Panel();
            lblTotalCaption = new Label();
            lblCartTotal = new Label();
            lblCartCount = new Label();
            btnCheckout = new Button();
            btnRemoveFromCart = new Button();
            lblBankNote = new Label();
            lblCartTitle = new Label();
            pnlLibrary = new Panel();
            gridLibrary = new DataGridView();
            pnlLibraryActions = new Panel();
            btnPlay = new Button();
            btnRefund = new Button();
            lblRefundRule = new Label();
            lblLibraryTitle = new Label();
            pnlHistory = new Panel();
            splitHistory = new SplitContainer();
            gridOrders = new DataGridView();
            lblOrders = new Label();
            gridRefunds = new DataGridView();
            lblRefunds = new Label();
            pnlTop.SuspendLayout();
            pnlStore.SuspendLayout();
            pnlStoreFilter.SuspendLayout();
            pnlCart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridCart).BeginInit();
            pnlCartSummary.SuspendLayout();
            pnlLibrary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridLibrary).BeginInit();
            pnlLibraryActions.SuspendLayout();
            pnlHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitHistory).BeginInit();
            splitHistory.Panel1.SuspendLayout();
            splitHistory.Panel2.SuspendLayout();
            splitHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridOrders).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridRefunds).BeginInit();
            SuspendLayout();
            //
            // pnlTop
            //
            pnlTop.BackColor = Color.FromArgb(23, 26, 33);
            pnlTop.Controls.Add(lblLogo);
            pnlTop.Controls.Add(btnNavStore);
            pnlTop.Controls.Add(btnNavCart);
            pnlTop.Controls.Add(btnNavLibrary);
            pnlTop.Controls.Add(btnNavHistory);
            pnlTop.Controls.Add(lblUser);
            pnlTop.Controls.Add(btnLogout);
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Location = new Point(0, 0);
            pnlTop.Name = "pnlTop";
            pnlTop.Size = new Size(1180, 64);
            pnlTop.TabIndex = 0;
            //
            // lblLogo
            //
            lblLogo.AutoSize = true;
            lblLogo.Font = new Font("Leelawadee UI", 16F, FontStyle.Bold);
            lblLogo.ForeColor = Color.FromArgb(102, 192, 244);
            lblLogo.Location = new Point(20, 15);
            lblLogo.Name = "lblLogo";
            lblLogo.Size = new Size(166, 30);
            lblLogo.TabIndex = 0;
            lblLogo.Text = "GAME STORE";
            //
            // btnNavStore
            //
            btnNavStore.Cursor = Cursors.Hand;
            btnNavStore.FlatAppearance.BorderSize = 0;
            btnNavStore.FlatStyle = FlatStyle.Flat;
            btnNavStore.Font = new Font("Leelawadee UI", 10F, FontStyle.Bold);
            btnNavStore.Location = new Point(210, 14);
            btnNavStore.Name = "btnNavStore";
            btnNavStore.Size = new Size(120, 36);
            btnNavStore.TabIndex = 1;
            btnNavStore.Text = "ร้านค้า";
            btnNavStore.UseVisualStyleBackColor = false;
            btnNavStore.Click += btnNavStore_Click;
            //
            // btnNavCart
            //
            btnNavCart.Cursor = Cursors.Hand;
            btnNavCart.FlatAppearance.BorderSize = 0;
            btnNavCart.FlatStyle = FlatStyle.Flat;
            btnNavCart.Font = new Font("Leelawadee UI", 10F, FontStyle.Bold);
            btnNavCart.Location = new Point(335, 14);
            btnNavCart.Name = "btnNavCart";
            btnNavCart.Size = new Size(120, 36);
            btnNavCart.TabIndex = 2;
            btnNavCart.Text = "ตะกร้า";
            btnNavCart.UseVisualStyleBackColor = false;
            btnNavCart.Click += btnNavCart_Click;
            //
            // btnNavLibrary
            //
            btnNavLibrary.Cursor = Cursors.Hand;
            btnNavLibrary.FlatAppearance.BorderSize = 0;
            btnNavLibrary.FlatStyle = FlatStyle.Flat;
            btnNavLibrary.Font = new Font("Leelawadee UI", 10F, FontStyle.Bold);
            btnNavLibrary.Location = new Point(460, 14);
            btnNavLibrary.Name = "btnNavLibrary";
            btnNavLibrary.Size = new Size(120, 36);
            btnNavLibrary.TabIndex = 3;
            btnNavLibrary.Text = "Game List";
            btnNavLibrary.UseVisualStyleBackColor = false;
            btnNavLibrary.Click += btnNavLibrary_Click;
            //
            // btnNavHistory
            //
            btnNavHistory.Cursor = Cursors.Hand;
            btnNavHistory.FlatAppearance.BorderSize = 0;
            btnNavHistory.FlatStyle = FlatStyle.Flat;
            btnNavHistory.Font = new Font("Leelawadee UI", 10F, FontStyle.Bold);
            btnNavHistory.Location = new Point(585, 14);
            btnNavHistory.Name = "btnNavHistory";
            btnNavHistory.Size = new Size(140, 36);
            btnNavHistory.TabIndex = 4;
            btnNavHistory.Text = "ประวัติการซื้อ";
            btnNavHistory.UseVisualStyleBackColor = false;
            btnNavHistory.Click += btnNavHistory_Click;
            //
            // lblUser
            //
            lblUser.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblUser.ForeColor = Color.FromArgb(199, 213, 224);
            lblUser.Location = new Point(740, 14);
            lblUser.Name = "lblUser";
            lblUser.Size = new Size(300, 36);
            lblUser.TabIndex = 5;
            lblUser.Text = "ผู้ใช้";
            lblUser.TextAlign = ContentAlignment.MiddleRight;
            //
            // btnLogout
            //
            btnLogout.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLogout.BackColor = Color.FromArgb(61, 79, 97);
            btnLogout.Cursor = Cursors.Hand;
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.ForeColor = Color.White;
            btnLogout.Location = new Point(1050, 14);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(110, 36);
            btnLogout.TabIndex = 6;
            btnLogout.Text = "ออกจากระบบ";
            btnLogout.UseVisualStyleBackColor = false;
            btnLogout.Click += btnLogout_Click;
            //
            // lblStatus
            //
            lblStatus.BackColor = Color.FromArgb(23, 26, 33);
            lblStatus.Dock = DockStyle.Bottom;
            lblStatus.ForeColor = Color.FromArgb(143, 152, 160);
            lblStatus.Location = new Point(0, 732);
            lblStatus.Name = "lblStatus";
            lblStatus.Padding = new Padding(12, 0, 0, 0);
            lblStatus.Size = new Size(1180, 28);
            lblStatus.TabIndex = 5;
            lblStatus.Text = "พร้อม";
            lblStatus.TextAlign = ContentAlignment.MiddleLeft;
            //
            // pnlStore
            //
            pnlStore.Controls.Add(flowGames);
            pnlStore.Controls.Add(flowGenres);
            pnlStore.Controls.Add(pnlStoreFilter);
            pnlStore.Dock = DockStyle.Fill;
            pnlStore.Location = new Point(0, 64);
            pnlStore.Name = "pnlStore";
            pnlStore.Size = new Size(1180, 668);
            pnlStore.TabIndex = 1;
            //
            // flowGames
            //
            flowGames.AutoScroll = true;
            flowGames.Dock = DockStyle.Fill;
            flowGames.Location = new Point(200, 66);
            flowGames.Name = "flowGames";
            flowGames.Padding = new Padding(4, 4, 14, 14);
            flowGames.Size = new Size(980, 602);
            flowGames.TabIndex = 2;
            //
            // pnlStoreFilter
            //
            pnlStoreFilter.Controls.Add(txtSearch);
            pnlStoreFilter.Controls.Add(btnSearch);
            pnlStoreFilter.Controls.Add(btnClearSearch);
            pnlStoreFilter.Controls.Add(lblResultCount);
            pnlStoreFilter.Dock = DockStyle.Top;
            pnlStoreFilter.Location = new Point(0, 0);
            pnlStoreFilter.Name = "pnlStoreFilter";
            pnlStoreFilter.Size = new Size(1180, 66);
            pnlStoreFilter.TabIndex = 0;
            //
            // txtSearch
            //
            txtSearch.BackColor = Color.FromArgb(50, 61, 76);
            txtSearch.BorderStyle = BorderStyle.FixedSingle;
            txtSearch.ForeColor = Color.White;
            txtSearch.Location = new Point(24, 18);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "ค้นหาชื่อเกม...";
            txtSearch.Size = new Size(380, 30);
            txtSearch.TabIndex = 0;
            txtSearch.KeyDown += txtSearch_KeyDown;
            //
            // btnSearch
            //
            btnSearch.BackColor = Color.FromArgb(42, 71, 94);
            btnSearch.Cursor = Cursors.Hand;
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.ForeColor = Color.FromArgb(102, 192, 244);
            btnSearch.Location = new Point(412, 16);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(100, 34);
            btnSearch.TabIndex = 1;
            btnSearch.Text = "ค้นหา";
            btnSearch.UseVisualStyleBackColor = false;
            btnSearch.Click += btnSearch_Click;
            //
            // btnClearSearch
            //
            btnClearSearch.BackColor = Color.FromArgb(61, 79, 97);
            btnClearSearch.Cursor = Cursors.Hand;
            btnClearSearch.FlatAppearance.BorderSize = 0;
            btnClearSearch.FlatStyle = FlatStyle.Flat;
            btnClearSearch.ForeColor = Color.White;
            btnClearSearch.Location = new Point(518, 16);
            btnClearSearch.Name = "btnClearSearch";
            btnClearSearch.Size = new Size(80, 34);
            btnClearSearch.TabIndex = 2;
            btnClearSearch.Text = "ล้าง";
            btnClearSearch.UseVisualStyleBackColor = false;
            btnClearSearch.Click += btnClearSearch_Click;
            //
            // lblResultCount
            //
            lblResultCount.AutoSize = true;
            lblResultCount.ForeColor = Color.FromArgb(143, 152, 160);
            lblResultCount.Location = new Point(612, 21);
            lblResultCount.Name = "lblResultCount";
            lblResultCount.Size = new Size(0, 23);
            lblResultCount.TabIndex = 3;
            //
            // flowGenres
            //
            flowGenres.AutoScroll = true;
            flowGenres.Dock = DockStyle.Left;
            flowGenres.FlowDirection = FlowDirection.TopDown;
            flowGenres.Location = new Point(0, 66);
            flowGenres.Name = "flowGenres";
            flowGenres.Padding = new Padding(20, 4, 8, 8);
            flowGenres.Size = new Size(200, 602);
            flowGenres.TabIndex = 1;
            flowGenres.WrapContents = false;
            //
            // pnlCart
            //
            pnlCart.Controls.Add(gridCart);
            pnlCart.Controls.Add(pnlCartSpacer);
            pnlCart.Controls.Add(pnlCartSummary);
            pnlCart.Controls.Add(lblCartTitle);
            pnlCart.Dock = DockStyle.Fill;
            pnlCart.Location = new Point(0, 64);
            pnlCart.Name = "pnlCart";
            pnlCart.Padding = new Padding(24, 12, 24, 24);
            pnlCart.Size = new Size(1180, 668);
            pnlCart.TabIndex = 2;
            pnlCart.Visible = false;
            //
            // gridCart
            //
            gridCart.Dock = DockStyle.Fill;
            gridCart.Location = new Point(24, 60);
            gridCart.Name = "gridCart";
            gridCart.Size = new Size(816, 584);
            gridCart.TabIndex = 1;
            //
            // pnlCartSpacer
            //
            pnlCartSpacer.Dock = DockStyle.Right;
            pnlCartSpacer.Location = new Point(840, 60);
            pnlCartSpacer.Name = "pnlCartSpacer";
            pnlCartSpacer.Size = new Size(16, 584);
            pnlCartSpacer.TabIndex = 2;
            //
            // pnlCartSummary
            //
            pnlCartSummary.BackColor = Color.FromArgb(22, 32, 45);
            pnlCartSummary.Controls.Add(lblTotalCaption);
            pnlCartSummary.Controls.Add(lblCartTotal);
            pnlCartSummary.Controls.Add(lblCartCount);
            pnlCartSummary.Controls.Add(btnCheckout);
            pnlCartSummary.Controls.Add(btnRemoveFromCart);
            pnlCartSummary.Controls.Add(lblBankNote);
            pnlCartSummary.Dock = DockStyle.Right;
            pnlCartSummary.Location = new Point(856, 60);
            pnlCartSummary.Name = "pnlCartSummary";
            pnlCartSummary.Size = new Size(300, 584);
            pnlCartSummary.TabIndex = 3;
            //
            // lblTotalCaption
            //
            lblTotalCaption.AutoSize = true;
            lblTotalCaption.ForeColor = Color.FromArgb(143, 152, 160);
            lblTotalCaption.Location = new Point(20, 20);
            lblTotalCaption.Name = "lblTotalCaption";
            lblTotalCaption.Size = new Size(61, 23);
            lblTotalCaption.TabIndex = 0;
            lblTotalCaption.Text = "ยอดรวม";
            //
            // lblCartTotal
            //
            lblCartTotal.Font = new Font("Leelawadee UI", 22F, FontStyle.Bold);
            lblCartTotal.ForeColor = Color.FromArgb(210, 239, 169);
            lblCartTotal.Location = new Point(16, 46);
            lblCartTotal.Name = "lblCartTotal";
            lblCartTotal.Size = new Size(264, 50);
            lblCartTotal.TabIndex = 1;
            lblCartTotal.Text = "฿0";
            //
            // lblCartCount
            //
            lblCartCount.AutoSize = true;
            lblCartCount.ForeColor = Color.FromArgb(143, 152, 160);
            lblCartCount.Location = new Point(20, 98);
            lblCartCount.Name = "lblCartCount";
            lblCartCount.Size = new Size(50, 23);
            lblCartCount.TabIndex = 2;
            lblCartCount.Text = "0 เกม";
            //
            // btnCheckout
            //
            btnCheckout.BackColor = Color.FromArgb(92, 126, 16);
            btnCheckout.Cursor = Cursors.Hand;
            btnCheckout.FlatAppearance.BorderSize = 0;
            btnCheckout.FlatStyle = FlatStyle.Flat;
            btnCheckout.Font = new Font("Leelawadee UI", 12F, FontStyle.Bold);
            btnCheckout.ForeColor = Color.FromArgb(210, 239, 169);
            btnCheckout.Location = new Point(20, 140);
            btnCheckout.Name = "btnCheckout";
            btnCheckout.Size = new Size(260, 50);
            btnCheckout.TabIndex = 3;
            btnCheckout.Text = "ชำระเงิน";
            btnCheckout.UseVisualStyleBackColor = false;
            btnCheckout.Click += btnCheckout_Click;
            //
            // btnRemoveFromCart
            //
            btnRemoveFromCart.BackColor = Color.FromArgb(61, 79, 97);
            btnRemoveFromCart.Cursor = Cursors.Hand;
            btnRemoveFromCart.FlatAppearance.BorderSize = 0;
            btnRemoveFromCart.FlatStyle = FlatStyle.Flat;
            btnRemoveFromCart.ForeColor = Color.White;
            btnRemoveFromCart.Location = new Point(20, 202);
            btnRemoveFromCart.Name = "btnRemoveFromCart";
            btnRemoveFromCart.Size = new Size(260, 40);
            btnRemoveFromCart.TabIndex = 4;
            btnRemoveFromCart.Text = "นำเกมที่เลือกออกจากตะกร้า";
            btnRemoveFromCart.UseVisualStyleBackColor = false;
            btnRemoveFromCart.Click += btnRemoveFromCart_Click;
            //
            // lblBankNote
            //
            lblBankNote.Font = new Font("Leelawadee UI", 9F);
            lblBankNote.ForeColor = Color.FromArgb(143, 152, 160);
            lblBankNote.Location = new Point(20, 262);
            lblBankNote.Name = "lblBankNote";
            lblBankNote.Size = new Size(260, 80);
            lblBankNote.TabIndex = 5;
            lblBankNote.Text = "* ระบบจำลองการตัดเงินจากธนาคาร ไม่ได้เชื่อมต่อกับธนาคารจริง";
            //
            // lblCartTitle
            //
            lblCartTitle.Dock = DockStyle.Top;
            lblCartTitle.Font = new Font("Leelawadee UI", 16F, FontStyle.Bold);
            lblCartTitle.ForeColor = Color.White;
            lblCartTitle.Location = new Point(24, 12);
            lblCartTitle.Name = "lblCartTitle";
            lblCartTitle.Size = new Size(1132, 48);
            lblCartTitle.TabIndex = 0;
            lblCartTitle.Text = "ตะกร้าสินค้า";
            //
            // pnlLibrary
            //
            pnlLibrary.Controls.Add(gridLibrary);
            pnlLibrary.Controls.Add(pnlLibraryActions);
            pnlLibrary.Controls.Add(lblLibraryTitle);
            pnlLibrary.Dock = DockStyle.Fill;
            pnlLibrary.Location = new Point(0, 64);
            pnlLibrary.Name = "pnlLibrary";
            pnlLibrary.Padding = new Padding(24, 12, 24, 12);
            pnlLibrary.Size = new Size(1180, 668);
            pnlLibrary.TabIndex = 3;
            pnlLibrary.Visible = false;
            //
            // gridLibrary
            //
            gridLibrary.Dock = DockStyle.Fill;
            gridLibrary.Location = new Point(24, 60);
            gridLibrary.Name = "gridLibrary";
            gridLibrary.Size = new Size(1132, 532);
            gridLibrary.TabIndex = 1;
            gridLibrary.SelectionChanged += gridLibrary_SelectionChanged;
            //
            // pnlLibraryActions
            //
            pnlLibraryActions.Controls.Add(btnPlay);
            pnlLibraryActions.Controls.Add(btnRefund);
            pnlLibraryActions.Controls.Add(lblRefundRule);
            pnlLibraryActions.Dock = DockStyle.Bottom;
            pnlLibraryActions.Location = new Point(24, 592);
            pnlLibraryActions.Name = "pnlLibraryActions";
            pnlLibraryActions.Size = new Size(1132, 64);
            pnlLibraryActions.TabIndex = 2;
            //
            // btnPlay
            //
            btnPlay.BackColor = Color.FromArgb(92, 126, 16);
            btnPlay.Cursor = Cursors.Hand;
            btnPlay.FlatAppearance.BorderSize = 0;
            btnPlay.FlatStyle = FlatStyle.Flat;
            btnPlay.Font = new Font("Leelawadee UI", 10F, FontStyle.Bold);
            btnPlay.ForeColor = Color.FromArgb(210, 239, 169);
            btnPlay.Location = new Point(0, 14);
            btnPlay.Name = "btnPlay";
            btnPlay.Size = new Size(240, 42);
            btnPlay.TabIndex = 0;
            btnPlay.Text = "▶  เล่น (จำลอง +30 นาที)";
            btnPlay.UseVisualStyleBackColor = false;
            btnPlay.Click += btnPlay_Click;
            //
            // btnRefund
            //
            btnRefund.BackColor = Color.FromArgb(150, 50, 50);
            btnRefund.Cursor = Cursors.Hand;
            btnRefund.FlatAppearance.BorderSize = 0;
            btnRefund.FlatStyle = FlatStyle.Flat;
            btnRefund.Font = new Font("Leelawadee UI", 10F, FontStyle.Bold);
            btnRefund.ForeColor = Color.White;
            btnRefund.Location = new Point(250, 14);
            btnRefund.Name = "btnRefund";
            btnRefund.Size = new Size(160, 42);
            btnRefund.TabIndex = 1;
            btnRefund.Text = "ขอคืนเงิน";
            btnRefund.UseVisualStyleBackColor = false;
            btnRefund.Click += btnRefund_Click;
            //
            // lblRefundRule
            //
            lblRefundRule.Font = new Font("Leelawadee UI", 9F);
            lblRefundRule.ForeColor = Color.FromArgb(143, 152, 160);
            lblRefundRule.Location = new Point(424, 14);
            lblRefundRule.Name = "lblRefundRule";
            lblRefundRule.Size = new Size(620, 42);
            lblRefundRule.TabIndex = 2;
            lblRefundRule.Text = "เงื่อนไขการคืนเงิน: ภายใน 14 วันหลังซื้อ และเล่นไม่ถึง 2 ชั่วโมง (เงินคืนเข้าบัญชีเดิมที่ใช้จ่าย)";
            lblRefundRule.TextAlign = ContentAlignment.MiddleLeft;
            //
            // lblLibraryTitle
            //
            lblLibraryTitle.Dock = DockStyle.Top;
            lblLibraryTitle.Font = new Font("Leelawadee UI", 16F, FontStyle.Bold);
            lblLibraryTitle.ForeColor = Color.White;
            lblLibraryTitle.Location = new Point(24, 12);
            lblLibraryTitle.Name = "lblLibraryTitle";
            lblLibraryTitle.Size = new Size(1132, 48);
            lblLibraryTitle.TabIndex = 0;
            lblLibraryTitle.Text = "คลังเกมของฉัน (Game List)";
            //
            // pnlHistory
            //
            pnlHistory.Controls.Add(splitHistory);
            pnlHistory.Dock = DockStyle.Fill;
            pnlHistory.Location = new Point(0, 64);
            pnlHistory.Name = "pnlHistory";
            pnlHistory.Padding = new Padding(24, 12, 24, 24);
            pnlHistory.Size = new Size(1180, 668);
            pnlHistory.TabIndex = 4;
            pnlHistory.Visible = false;
            //
            // splitHistory
            //
            splitHistory.Dock = DockStyle.Fill;
            splitHistory.Location = new Point(24, 12);
            splitHistory.Name = "splitHistory";
            splitHistory.Orientation = Orientation.Horizontal;
            //
            // splitHistory.Panel1
            //
            splitHistory.Panel1.Controls.Add(gridOrders);
            splitHistory.Panel1.Controls.Add(lblOrders);
            //
            // splitHistory.Panel2
            //
            splitHistory.Panel2.Controls.Add(gridRefunds);
            splitHistory.Panel2.Controls.Add(lblRefunds);
            splitHistory.Size = new Size(1132, 632);
            splitHistory.SplitterDistance = 360;
            splitHistory.SplitterWidth = 8;
            splitHistory.TabIndex = 0;
            //
            // gridOrders
            //
            gridOrders.Dock = DockStyle.Fill;
            gridOrders.Location = new Point(0, 48);
            gridOrders.Name = "gridOrders";
            gridOrders.Size = new Size(1132, 312);
            gridOrders.TabIndex = 1;
            //
            // lblOrders
            //
            lblOrders.Dock = DockStyle.Top;
            lblOrders.Font = new Font("Leelawadee UI", 16F, FontStyle.Bold);
            lblOrders.ForeColor = Color.White;
            lblOrders.Location = new Point(0, 0);
            lblOrders.Name = "lblOrders";
            lblOrders.Size = new Size(1132, 48);
            lblOrders.TabIndex = 0;
            lblOrders.Text = "ประวัติการซื้อ";
            //
            // gridRefunds
            //
            gridRefunds.Dock = DockStyle.Fill;
            gridRefunds.Location = new Point(0, 44);
            gridRefunds.Name = "gridRefunds";
            gridRefunds.Size = new Size(1132, 220);
            gridRefunds.TabIndex = 1;
            //
            // lblRefunds
            //
            lblRefunds.Dock = DockStyle.Top;
            lblRefunds.Font = new Font("Leelawadee UI", 13F, FontStyle.Bold);
            lblRefunds.ForeColor = Color.White;
            lblRefunds.Location = new Point(0, 0);
            lblRefunds.Name = "lblRefunds";
            lblRefunds.Size = new Size(1132, 44);
            lblRefunds.TabIndex = 0;
            lblRefunds.Text = "ประวัติการคืนเงิน";
            lblRefunds.TextAlign = ContentAlignment.BottomLeft;
            //
            // MainForm
            //
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(27, 40, 56);
            ClientSize = new Size(1180, 760);
            Controls.Add(pnlStore);
            Controls.Add(pnlCart);
            Controls.Add(pnlLibrary);
            Controls.Add(pnlHistory);
            Controls.Add(lblStatus);
            Controls.Add(pnlTop);
            Font = new Font("Leelawadee UI", 10F);
            ForeColor = Color.FromArgb(199, 213, 224);
            MinimumSize = new Size(1000, 640);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Game Store";
            Load += MainForm_Load;
            pnlTop.ResumeLayout(false);
            pnlTop.PerformLayout();
            pnlStore.ResumeLayout(false);
            pnlStoreFilter.ResumeLayout(false);
            pnlStoreFilter.PerformLayout();
            pnlCart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridCart).EndInit();
            pnlCartSummary.ResumeLayout(false);
            pnlCartSummary.PerformLayout();
            pnlLibrary.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridLibrary).EndInit();
            pnlLibraryActions.ResumeLayout(false);
            pnlHistory.ResumeLayout(false);
            splitHistory.Panel1.ResumeLayout(false);
            splitHistory.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitHistory).EndInit();
            splitHistory.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridOrders).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridRefunds).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlTop;
        private Label lblLogo;
        private Button btnNavStore;
        private Button btnNavCart;
        private Button btnNavLibrary;
        private Button btnNavHistory;
        private Label lblUser;
        private Button btnLogout;
        private Label lblStatus;
        private Panel pnlStore;
        private FlowLayoutPanel flowGames;
        private Panel pnlStoreFilter;
        private TextBox txtSearch;
        private Button btnSearch;
        private Button btnClearSearch;
        private Label lblResultCount;
        private FlowLayoutPanel flowGenres;
        private Panel pnlCart;
        private DataGridView gridCart;
        private Panel pnlCartSpacer;
        private Panel pnlCartSummary;
        private Label lblTotalCaption;
        private Label lblCartTotal;
        private Label lblCartCount;
        private Button btnCheckout;
        private Button btnRemoveFromCart;
        private Label lblBankNote;
        private Label lblCartTitle;
        private Panel pnlLibrary;
        private DataGridView gridLibrary;
        private Panel pnlLibraryActions;
        private Button btnPlay;
        private Button btnRefund;
        private Label lblRefundRule;
        private Label lblLibraryTitle;
        private Panel pnlHistory;
        private SplitContainer splitHistory;
        private DataGridView gridOrders;
        private Label lblOrders;
        private DataGridView gridRefunds;
        private Label lblRefunds;
    }
}
