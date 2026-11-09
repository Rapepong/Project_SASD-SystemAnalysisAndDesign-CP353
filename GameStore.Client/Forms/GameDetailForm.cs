using System.Globalization;
using GameStore.Client.Services;
using GameStore.Client.UI;
using GameStore.Contracts;

namespace GameStore.Client.Forms
{
    // หน้ารายละเอียดเกม (เปิดเมื่อกดที่การ์ดเกม)
    public partial class GameDetailForm : Form
    {
        private readonly int gameId;
        private readonly GameOwnership ownership;
        private GameDetailDto? game;

        public bool AddToCartClicked { get; private set; }
        public string GameTitle => game?.Title ?? "";

        public GameDetailForm(int gameId, GameOwnership ownership)
        {
            InitializeComponent();
            this.gameId = gameId;
            this.ownership = ownership;

            switch (ownership)
            {
                case GameOwnership.Owned:
                    btnAddToCart.Text = "มีใน Game List แล้ว";
                    btnAddToCart.Enabled = false;
                    break;
                case GameOwnership.InCart:
                    btnAddToCart.Text = "อยู่ในตะกร้าแล้ว";
                    btnAddToCart.Enabled = false;
                    break;
            }
        }

        private async void GameDetailForm_Load(object sender, EventArgs e)
        {
            bool ok = await Ui.RunAsync(this, async () => game = await StoreApi.GetGameAsync(gameId));
            if (!ok || game == null)
            {
                Close();
                return;
            }

            Text = game.Title;
            lblTitle.Text = game.Title;
            lblDeveloper.Text = $"ผู้พัฒนา: {game.Developer}";
            lblRelease.Text = $"วางจำหน่าย: {game.ReleaseDate.ToString("dd MMM yyyy", CultureInfo.InvariantCulture)}";
            lblGenres.Text = string.Join("  ·  ", game.Genres);
            lblPrice.Text = Theme.Money(game.Price);
            txtDescription.Text = game.Description;
            pnlCover.Invalidate();
        }

        private void pnlCover_Paint(object sender, PaintEventArgs e)
        {
            if (game != null)
                CoverPainter.Paint(e.Graphics, pnlCover.ClientRectangle, game.Title, game.Genres);
        }

        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            if (ownership != GameOwnership.Available || game == null)
                return;

            // ให้ MainForm เป็นคนเรียก API เพิ่มลงตะกร้า (จะได้อัปเดตหน้าร้านค้าได้ในที่เดียว)
            AddToCartClicked = true;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
