using System.ComponentModel;
using GameStore.Contracts;

namespace GameStore.Client.UI;

public enum GameOwnership
{
    Available,  // ซื้อได้
    InCart,     // อยู่ในตะกร้าแล้ว
    Owned,      // มีใน Game List แล้ว
}

// การ์ดแสดงเกม 1 เกมในหน้าร้านค้า (สร้างขึ้นตอน runtime ตามผลการค้นหา)
[DesignerCategory("Code")]
public class GameCard : UserControl
{
    private const int CoverHeight = 120;

    private readonly Label lblTitle = new();
    private readonly Label lblInfo = new();
    private readonly Label lblPrice = new();
    private readonly Button btnAction = new();
    private bool hovering;

    public GameSummaryDto Game { get; }
    public GameOwnership Ownership { get; private set; }

    // ผู้ใช้กดที่การ์ด -> เปิดรายละเอียดเกม
    public event EventHandler? OpenRequested;
    // ผู้ใช้กดปุ่ม "ใส่ตะกร้า"
    public event EventHandler? AddToCartRequested;

    public GameCard(GameSummaryDto game, GameOwnership ownership)
    {
        Game = game;
        DoubleBuffered = true;
        Size = new Size(210, 250);
        Margin = new Padding(10);
        BackColor = Theme.Card;
        Cursor = Cursors.Hand;

        lblTitle.Text = game.Title;
        lblTitle.Font = new Font(Theme.FontName, 11F, FontStyle.Bold);
        lblTitle.ForeColor = Color.White;
        lblTitle.AutoEllipsis = true;
        lblTitle.SetBounds(10, CoverHeight + 8, 190, 24);

        lblInfo.Text = string.Join(" · ", game.Genres);
        lblInfo.Font = new Font(Theme.FontName, 8.5F);
        lblInfo.ForeColor = Theme.Muted;
        lblInfo.AutoEllipsis = true;
        lblInfo.SetBounds(10, CoverHeight + 32, 190, 20);

        lblPrice.Text = Theme.Money(game.Price);
        lblPrice.Font = new Font(Theme.FontName, 11F, FontStyle.Bold);
        lblPrice.ForeColor = Theme.GreenText;
        lblPrice.SetBounds(10, CoverHeight + 78, 85, 30);
        lblPrice.TextAlign = ContentAlignment.MiddleLeft;

        btnAction.Font = new Font(Theme.FontName, 9F, FontStyle.Bold);
        btnAction.SetBounds(95, CoverHeight + 78, 105, 30);
        btnAction.Click += (_, _) => AddToCartRequested?.Invoke(this, EventArgs.Empty);

        Controls.AddRange([lblTitle, lblInfo, lblPrice, btnAction]);

        // คลิกตรงไหนของการ์ดก็ได้ (ยกเว้นปุ่ม) = เปิดรายละเอียด
        foreach (Control c in new Control[] { this, lblTitle, lblInfo, lblPrice })
        {
            c.Click += (_, _) => OpenRequested?.Invoke(this, EventArgs.Empty);
            c.MouseEnter += (_, _) => SetHover(true);
            c.MouseLeave += (_, _) => SetHover(ClientRectangle.Contains(PointToClient(MousePosition)));
        }

        SetOwnership(ownership);
    }

    public void SetOwnership(GameOwnership ownership)
    {
        Ownership = ownership;
        switch (ownership)
        {
            case GameOwnership.Owned:
                btnAction.Text = "มีแล้ว";
                btnAction.Enabled = false;
                Theme.StyleButton(btnAction, Theme.Neutral, Theme.Muted);
                break;
            case GameOwnership.InCart:
                btnAction.Text = "ในตะกร้า";
                btnAction.Enabled = false;
                Theme.StyleButton(btnAction, Theme.Panel, Theme.Accent);
                break;
            default:
                btnAction.Text = "ใส่ตะกร้า";
                btnAction.Enabled = true;
                Theme.StyleButton(btnAction, Theme.Green, Theme.GreenText);
                break;
        }
    }

    private void SetHover(bool value)
    {
        if (hovering == value) return;
        hovering = value;
        BackColor = value ? Theme.CardHover : Theme.Card;
        Invalidate();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        CoverPainter.Paint(e.Graphics, new Rectangle(0, 0, Width, CoverHeight), Game.Title, Game.Genres);

        if (Ownership == GameOwnership.Owned)
        {
            // ป้าย "IN LIBRARY" มุมขวาบน
            using var badgeFont = new Font(Theme.FontName, 8F, FontStyle.Bold);
            var badge = new Rectangle(Width - 92, 8, 84, 22);
            using var badgeBrush = new SolidBrush(Color.FromArgb(220, Theme.TopBar));
            e.Graphics.FillRectangle(badgeBrush, badge);
            TextRenderer.DrawText(e.Graphics, "IN LIBRARY", badgeFont, badge, Theme.Accent,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }
    }
}
