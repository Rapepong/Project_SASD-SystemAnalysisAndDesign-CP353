using GameStore.Client.Services;
using GameStore.Client.UI;
using GameStore.Contracts;

namespace GameStore.Client.Forms
{
    // หน้าจอหลัก มี 4 หน้า: ร้านค้า / ตะกร้า / Game List / ประวัติการซื้อ
    // ทุกหน้าดึงข้อมูลผ่าน REST API (StoreApi) ไม่ได้ต่อ database ตรง
    public partial class MainForm : Form
    {
        // เก็บไว้เพื่อแสดงสถานะบนการ์ดเกม (มีแล้ว / ในตะกร้า / ซื้อได้)
        private readonly HashSet<int> ownedGameIds = [];
        private readonly HashSet<int> cartGameIds = [];

        private int? selectedGenreId;
        private CartDto? cart;

        public bool LogoutRequested { get; private set; }

        public MainForm()
        {
            InitializeComponent();
            SetupGrids();

            var user = Session.CurrentUser!;
            lblUser.Text = $"{user.DisplayName}  ({user.Username})";
            Text = $"Game Store - {user.DisplayName}";
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            await Ui.RunAsync(this, async () =>
            {
                List<GenreDto> genres = await StoreApi.GetGenresAsync();
                BuildGenreChips(genres);
                await RefreshOwnershipAsync();
            });
            await ShowPageAsync(pnlStore);
        }

        // =================================================================
        //  การสลับหน้า (Navigation)
        // =================================================================
        private async Task ShowPageAsync(Panel page)
        {
            foreach (Panel p in new[] { pnlStore, pnlCart, pnlLibrary, pnlHistory })
                p.Visible = p == page;

            var navButtons = new Dictionary<Panel, Button>
            {
                [pnlStore] = btnNavStore,
                [pnlCart] = btnNavCart,
                [pnlLibrary] = btnNavLibrary,
                [pnlHistory] = btnNavHistory,
            };
            foreach (var (panel, button) in navButtons)
            {
                bool active = panel == page;
                Theme.StyleButton(button, active ? Theme.Panel : Theme.TopBar, active ? Color.White : Theme.Text);
            }

            if (page == pnlStore) await LoadStoreAsync();
            else if (page == pnlCart) await LoadCartAsync();
            else if (page == pnlLibrary) await LoadLibraryAsync();
            else if (page == pnlHistory) await LoadHistoryAsync();
        }

        private async void btnNavStore_Click(object sender, EventArgs e) => await ShowPageAsync(pnlStore);
        private async void btnNavCart_Click(object sender, EventArgs e) => await ShowPageAsync(pnlCart);
        private async void btnNavLibrary_Click(object sender, EventArgs e) => await ShowPageAsync(pnlLibrary);
        private async void btnNavHistory_Click(object sender, EventArgs e) => await ShowPageAsync(pnlHistory);

        private void btnLogout_Click(object sender, EventArgs e)
        {
            LogoutRequested = true;
            Session.CurrentUser = null;
            Close();
        }

        private void SetStatus(string message) => lblStatus.Text = message;

        // ดึงรายการเกมที่มีแล้วกับที่อยู่ในตะกร้า เพื่อใช้แสดงสถานะบนการ์ด
        private async Task RefreshOwnershipAsync()
        {
            var library = await StoreApi.GetLibraryAsync(Session.UserId);
            cart = await StoreApi.GetCartAsync(Session.UserId);

            ownedGameIds.Clear();
            ownedGameIds.UnionWith(library.Select(l => l.GameId));
            cartGameIds.Clear();
            cartGameIds.UnionWith(cart.Items.Select(i => i.GameId));

            btnNavCart.Text = cart.Items.Count > 0 ? $"ตะกร้า ({cart.Items.Count})" : "ตะกร้า";
        }

        private GameOwnership OwnershipOf(int gameId) =>
            ownedGameIds.Contains(gameId) ? GameOwnership.Owned
            : cartGameIds.Contains(gameId) ? GameOwnership.InCart
            : GameOwnership.Available;

        // =================================================================
        //  หน้าร้านค้า: ค้นหาชื่อเกม + กดเลือก genre
        // =================================================================
        private void BuildGenreChips(List<GenreDto> genres)
        {
            flowGenres.Controls.Clear();
            flowGenres.Controls.Add(new Label
            {
                Text = "แนวเกม (Genre)",
                ForeColor = Theme.Muted,
                AutoSize = false,
                Size = new Size(168, 30),
                TextAlign = ContentAlignment.MiddleLeft,
            });
            AddGenreChip("ทั้งหมด", null);
            foreach (var genre in genres)
                AddGenreChip(genre.Name, genre.Id);
            UpdateGenreChipStyles();
        }

        private void AddGenreChip(string text, int? genreId)
        {
            var chip = new Button
            {
                Text = text,
                Tag = genreId,
                Size = new Size(168, 36),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(10, 0, 0, 0),
                Margin = new Padding(0, 2, 0, 2),
            };
            chip.Click += async (_, _) =>
            {
                selectedGenreId = genreId;
                UpdateGenreChipStyles();
                await LoadStoreAsync();
            };
            flowGenres.Controls.Add(chip);
        }

        private void UpdateGenreChipStyles()
        {
            foreach (Button chip in flowGenres.Controls.OfType<Button>())
            {
                bool selected = (int?)chip.Tag == selectedGenreId;
                Theme.StyleButton(chip, selected ? Theme.Accent : Theme.Panel, selected ? Theme.TopBar : Theme.Text);
            }
        }

        private async void btnSearch_Click(object sender, EventArgs e) => await LoadStoreAsync();

        private async void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;   // ไม่ให้มีเสียง beep
                await LoadStoreAsync();
            }
        }

        private async void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            selectedGenreId = null;
            UpdateGenreChipStyles();
            await LoadStoreAsync();
        }

        private async Task LoadStoreAsync()
        {
            await Ui.RunAsync(this, async () =>
            {
                List<GameSummaryDto> games = await StoreApi.SearchGamesAsync(txtSearch.Text, selectedGenreId);

                flowGames.SuspendLayout();
                foreach (Control old in flowGames.Controls.Cast<Control>().ToList())
                    old.Dispose();

                foreach (var game in games)
                {
                    var card = new GameCard(game, OwnershipOf(game.Id));
                    card.OpenRequested += async (_, _) => await OpenGameDetailAsync(game.Id);
                    card.AddToCartRequested += async (_, _) => await AddToCartAsync(game.Id, game.Title);
                    flowGames.Controls.Add(card);
                }
                flowGames.ResumeLayout();

                lblResultCount.Text = $"พบ {games.Count} เกม";
                SetStatus(string.IsNullOrWhiteSpace(txtSearch.Text) && selectedGenreId == null
                    ? "แสดงเกมทั้งหมดที่วางขาย"
                    : $"ผลการค้นหา: {games.Count} เกม");
            });
        }

        private async Task OpenGameDetailAsync(int gameId)
        {
            using var form = new GameDetailForm(gameId, OwnershipOf(gameId));
            if (form.ShowDialog(this) == DialogResult.OK && form.AddToCartClicked)
                await AddToCartAsync(gameId, form.GameTitle);
        }

        private async Task AddToCartAsync(int gameId, string title)
        {
            bool ok = await Ui.RunAsync(this, async () =>
            {
                await StoreApi.AddToCartAsync(Session.UserId, gameId);
                await RefreshOwnershipAsync();
            });
            if (!ok) return;

            foreach (GameCard card in flowGames.Controls.OfType<GameCard>())
                card.SetOwnership(OwnershipOf(card.Game.Id));
            SetStatus($"เพิ่ม '{title}' ลงตะกร้าแล้ว");
        }

        // =================================================================
        //  หน้าตะกร้า
        // =================================================================
        private async Task LoadCartAsync()
        {
            await Ui.RunAsync(this, async () =>
            {
                await RefreshOwnershipAsync();

                gridCart.Rows.Clear();
                foreach (var item in cart!.Items)
                {
                    int row = gridCart.Rows.Add(item.Title, Theme.Money(item.Price), Theme.DateTimeText(item.AddedAt));
                    gridCart.Rows[row].Tag = item;
                }

                lblCartTotal.Text = Theme.Money(cart.TotalAmount);
                lblCartCount.Text = $"{cart.Items.Count} เกม";
                btnCheckout.Enabled = cart.Items.Count > 0;
                btnRemoveFromCart.Enabled = cart.Items.Count > 0;
                SetStatus(cart.Items.Count == 0 ? "ตะกร้าว่าง - ไปเลือกเกมที่หน้าร้านค้าได้เลย" : $"มี {cart.Items.Count} เกมในตะกร้า");
            });
        }

        private async void btnRemoveFromCart_Click(object sender, EventArgs e)
        {
            if (gridCart.CurrentRow?.Tag is not CartItemDto item)
                return;

            bool ok = await Ui.RunAsync(this, () => StoreApi.RemoveFromCartAsync(Session.UserId, item.GameId));
            if (ok)
            {
                await LoadCartAsync();
                SetStatus($"นำ '{item.Title}' ออกจากตะกร้าแล้ว");
            }
        }

        private async void btnCheckout_Click(object sender, EventArgs e)
        {
            if (cart == null || cart.Items.Count == 0)
                return;

            using var form = new CheckoutForm(cart);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                // ซื้อสำเร็จ -> ไปหน้า Game List
                await ShowPageAsync(pnlLibrary);
                SetStatus("ซื้อเกมสำเร็จ เกมถูกเพิ่มเข้า Game List แล้ว");
            }
            else
            {
                await LoadCartAsync();
            }
        }

        // =================================================================
        //  หน้า Game List
        // =================================================================
        private async Task LoadLibraryAsync(int? selectGameId = null)
        {
            await Ui.RunAsync(this, async () =>
            {
                List<LibraryItemDto> library = await StoreApi.GetLibraryAsync(Session.UserId);

                gridLibrary.Rows.Clear();
                foreach (var item in library)
                {
                    int row = gridLibrary.Rows.Add(
                        item.Title,
                        item.Developer,
                        Theme.Playtime(item.PlaytimeMinutes),
                        item.LastPlayedAt == null ? "-" : Theme.DateTimeText(item.LastPlayedAt.Value),
                        Theme.DateTimeText(item.AcquiredAt),
                        Theme.Money(item.PricePaid),
                        item.RefundNote);
                    gridLibrary.Rows[row].Tag = item;
                    gridLibrary.Rows[row].Cells[6].Style.ForeColor = item.IsRefundable ? Theme.GreenText : Theme.Muted;
                    if (item.GameId == selectGameId)
                        gridLibrary.CurrentCell = gridLibrary.Rows[row].Cells[0];
                }

                lblLibraryTitle.Text = $"คลังเกมของฉัน (Game List) - {library.Count} เกม";
                UpdateLibraryButtons();
                SetStatus(library.Count == 0
                    ? "ยังไม่มีเกม - ไปเลือกซื้อที่หน้าร้านค้าได้เลย"
                    : "เลือกเกมในตาราง แล้วกด 'เล่น' หรือ 'ขอคืนเงิน'");
            });
        }

        private void gridLibrary_SelectionChanged(object sender, EventArgs e) => UpdateLibraryButtons();

        private void UpdateLibraryButtons()
        {
            var item = gridLibrary.CurrentRow?.Tag as LibraryItemDto;
            btnPlay.Enabled = item != null;
            btnRefund.Enabled = item?.IsRefundable == true;
        }

        private async void btnPlay_Click(object sender, EventArgs e)
        {
            if (gridLibrary.CurrentRow?.Tag is not LibraryItemDto item)
                return;

            LibraryItemDto? updated = null;
            bool ok = await Ui.RunAsync(this, async () =>
                updated = await StoreApi.PlayAsync(Session.UserId, item.GameId));
            if (!ok || updated == null) return;

            await LoadLibraryAsync(item.GameId);
            SetStatus($"เล่น '{updated.Title}' ไปแล้ว 30 นาที (รวม {Theme.Playtime(updated.PlaytimeMinutes)})");
        }

        private async void btnRefund_Click(object sender, EventArgs e)
        {
            if (gridLibrary.CurrentRow?.Tag is not LibraryItemDto item)
                return;

            using var form = new RefundForm(item);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                await RefreshOwnershipAsync();
                await LoadLibraryAsync();
                SetStatus($"คืนเงินเกม '{item.Title}' สำเร็จ");
            }
        }

        // =================================================================
        //  หน้าประวัติการซื้อ / การคืนเงิน
        // =================================================================
        private async Task LoadHistoryAsync()
        {
            await Ui.RunAsync(this, async () =>
            {
                List<OrderDto> orders = await StoreApi.GetOrdersAsync(Session.UserId);
                List<RefundDto> refunds = await StoreApi.GetRefundsAsync(Session.UserId);

                gridOrders.Rows.Clear();
                foreach (var order in orders)
                {
                    foreach (var item in order.Items)
                    {
                        int row = gridOrders.Rows.Add(
                            $"#{order.Id}",
                            Theme.DateTimeText(order.OrderedAt),
                            item.Title,
                            Theme.Money(item.Price),
                            item.IsRefunded ? "คืนเงินแล้ว" : "ซื้อแล้ว",
                            order.BankAccountNo,
                            order.PaymentRef);
                        gridOrders.Rows[row].Cells[4].Style.ForeColor = item.IsRefunded ? Theme.Muted : Theme.GreenText;
                    }
                }

                gridRefunds.Rows.Clear();
                foreach (var refund in refunds)
                {
                    gridRefunds.Rows.Add(
                        Theme.DateTimeText(refund.RefundedAt),
                        refund.Title,
                        Theme.Money(refund.Amount),
                        string.IsNullOrEmpty(refund.Reason) ? "-" : refund.Reason,
                        refund.PaymentRef);
                }

                SetStatus($"คำสั่งซื้อ {orders.Count} รายการ, คืนเงิน {refunds.Count} รายการ");
            });
        }

        // =================================================================
        //  ตั้งค่าคอลัมน์ของตาราง
        // =================================================================
        private void SetupGrids()
        {
            var right = DataGridViewContentAlignment.MiddleRight;
            var center = DataGridViewContentAlignment.MiddleCenter;

            Theme.StyleGrid(gridCart);
            Theme.AddColumn(gridCart, "ชื่อเกม", 50);
            Theme.AddColumn(gridCart, "ราคา", 20, right);
            Theme.AddColumn(gridCart, "เพิ่มลงตะกร้าเมื่อ", 30, center);

            Theme.StyleGrid(gridLibrary);
            Theme.AddColumn(gridLibrary, "ชื่อเกม", 22);
            Theme.AddColumn(gridLibrary, "ผู้พัฒนา", 15);
            Theme.AddColumn(gridLibrary, "เวลาเล่น", 11, center);
            Theme.AddColumn(gridLibrary, "เล่นล่าสุด", 14, center);
            Theme.AddColumn(gridLibrary, "ได้รับเมื่อ", 14, center);
            Theme.AddColumn(gridLibrary, "ราคาที่จ่าย", 9, right);
            Theme.AddColumn(gridLibrary, "สิทธิ์คืนเงิน", 30);

            Theme.StyleGrid(gridOrders);
            Theme.AddColumn(gridOrders, "คำสั่งซื้อ", 8, center);
            Theme.AddColumn(gridOrders, "วันที่ซื้อ", 14, center);
            Theme.AddColumn(gridOrders, "ชื่อเกม", 22);
            Theme.AddColumn(gridOrders, "ราคา", 9, right);
            Theme.AddColumn(gridOrders, "สถานะ", 10, center);
            Theme.AddColumn(gridOrders, "บัญชีที่จ่าย", 14, center);
            Theme.AddColumn(gridOrders, "เลขอ้างอิงธนาคาร", 20);

            Theme.StyleGrid(gridRefunds);
            Theme.AddColumn(gridRefunds, "วันที่คืนเงิน", 14, center);
            Theme.AddColumn(gridRefunds, "ชื่อเกม", 22);
            Theme.AddColumn(gridRefunds, "จำนวนเงิน", 10, right);
            Theme.AddColumn(gridRefunds, "เหตุผล", 30);
            Theme.AddColumn(gridRefunds, "เลขอ้างอิงธนาคาร", 20);
        }
    }
}
