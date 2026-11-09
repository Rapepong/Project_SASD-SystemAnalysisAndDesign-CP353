using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace GameStore.Client.UI;

// วาดภาพปกเกมขึ้นมาเอง (ไล่สีตามแนวเกม + ชื่อเกม) จึงไม่ต้องมีไฟล์รูป
public static class CoverPainter
{
    public static void Paint(Graphics g, Rectangle bounds, string title, IReadOnlyList<string> genres)
    {
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

        // วาดเฉพาะในกรอบปก ไม่ให้ลายล้นออกไปนอกกรอบ
        var previousClip = g.Clip;
        g.SetClip(bounds);
        try
        {
            PaintInsideBounds(g, bounds, title, genres);
        }
        finally
        {
            g.Clip = previousClip;
        }
    }

    private static void PaintInsideBounds(Graphics g, Rectangle bounds, string title, IReadOnlyList<string> genres)
    {

        Color first = Theme.GenreColor(genres.Count > 0 ? genres[0] : null);
        Color second = genres.Count > 1 ? Theme.GenreColor(genres[1]) : ControlPaint.Dark(first, 0.3f);

        using (var brush = new LinearGradientBrush(bounds, first, second, LinearGradientMode.ForwardDiagonal))
            g.FillRectangle(brush, bounds);

        // ลายวงกลมจาง ๆ ให้ปกดูมีมิติ
        int seed = title.Aggregate(17, (h, c) => h * 31 + c);
        var random = new Random(seed);
        using (var circle = new SolidBrush(Color.FromArgb(28, Color.White)))
        {
            for (int i = 0; i < 4; i++)
            {
                int size = random.Next(bounds.Height / 2, bounds.Height * 3 / 2);
                int x = bounds.Left + random.Next(-size / 2, bounds.Width);
                int y = bounds.Top + random.Next(-size / 2, bounds.Height);
                g.FillEllipse(circle, x, y, size, size);
            }
        }

        // ชื่อเกม ตัวใหญ่ มีเงา
        float fontSize = Math.Max(11f, bounds.Height / 7.5f);
        using var font = new Font(Theme.FontName, fontSize, FontStyle.Bold);
        var format = new StringFormat
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center,
            Trimming = StringTrimming.EllipsisWord,
        };
        var textArea = Rectangle.Inflate(bounds, -12, -10);
        var shadowArea = textArea;
        shadowArea.Offset(2, 2);
        using (var shadow = new SolidBrush(Color.FromArgb(120, Color.Black)))
            g.DrawString(title.ToUpperInvariant(), font, shadow, shadowArea, format);
        g.DrawString(title.ToUpperInvariant(), font, Brushes.White, textArea, format);
    }
}
