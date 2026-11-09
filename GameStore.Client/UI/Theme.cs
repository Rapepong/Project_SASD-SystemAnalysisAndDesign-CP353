using System.Globalization;

namespace GameStore.Client.UI;

// สี ฟอนต์ และตัวช่วยจัดรูปแบบที่ใช้ทั้งโปรแกรม (โทนมืดแบบร้านเกม)
public static class Theme
{
    public static readonly Color Background = Color.FromArgb(27, 40, 56);     // #1B2838
    public static readonly Color TopBar = Color.FromArgb(23, 26, 33);         // #171A21
    public static readonly Color Panel = Color.FromArgb(42, 71, 94);          // #2A475E
    public static readonly Color Card = Color.FromArgb(22, 32, 45);           // #16202D
    public static readonly Color CardHover = Color.FromArgb(38, 58, 80);
    public static readonly Color Accent = Color.FromArgb(102, 192, 244);      // #66C0F4
    public static readonly Color Text = Color.FromArgb(199, 213, 224);        // #C7D5E0
    public static readonly Color Muted = Color.FromArgb(143, 152, 160);       // #8F98A0
    public static readonly Color Green = Color.FromArgb(92, 126, 16);         // #5C7E10
    public static readonly Color GreenText = Color.FromArgb(210, 239, 169);
    public static readonly Color Red = Color.FromArgb(150, 50, 50);
    public static readonly Color Neutral = Color.FromArgb(61, 79, 97);

    public const string FontName = "Leelawadee UI";   // รองรับทั้งภาษาไทยและอังกฤษ

    // ---------- การแสดงผลตัวเลข/วันที่ ----------
    public static string Money(decimal amount) =>
        "฿" + amount.ToString("#,0.##", CultureInfo.InvariantCulture);

    public static string DateTimeText(DateTime value) =>
        value.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);

    public static string Playtime(int minutes) =>
        minutes >= 60 ? $"{minutes / 60} ชม. {minutes % 60} นาที" : $"{minutes} นาที";

    // ---------- ปุ่ม ----------
    public static void StyleButton(Button button, Color backColor, Color? foreColor = null)
    {
        button.FlatStyle = FlatStyle.Flat;
        button.FlatAppearance.BorderSize = 0;
        button.BackColor = backColor;
        button.ForeColor = foreColor ?? Color.White;
        button.Cursor = Cursors.Hand;
    }

    // ---------- ตาราง ----------
    public static void StyleGrid(DataGridView grid)
    {
        grid.BackgroundColor = Background;
        grid.BorderStyle = BorderStyle.None;
        grid.GridColor = Panel;
        grid.EnableHeadersVisualStyles = false;
        grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
        grid.ColumnHeadersDefaultCellStyle.BackColor = TopBar;
        grid.ColumnHeadersDefaultCellStyle.ForeColor = Accent;
        grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = TopBar;
        grid.ColumnHeadersDefaultCellStyle.Font = new Font(FontName, 10F, FontStyle.Bold);
        grid.ColumnHeadersDefaultCellStyle.Padding = new Padding(6, 0, 0, 0);
        grid.ColumnHeadersHeight = 38;
        grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        grid.DefaultCellStyle.BackColor = Card;
        grid.DefaultCellStyle.ForeColor = Text;
        grid.DefaultCellStyle.SelectionBackColor = Panel;
        grid.DefaultCellStyle.SelectionForeColor = Color.White;
        grid.DefaultCellStyle.Padding = new Padding(6, 0, 0, 0);
        grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(27, 38, 52);
        grid.RowTemplate.Height = 36;
        grid.RowHeadersVisible = false;
        grid.AllowUserToAddRows = false;
        grid.AllowUserToDeleteRows = false;
        grid.AllowUserToResizeRows = false;
        grid.ReadOnly = true;
        grid.MultiSelect = false;
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
    }

    public static DataGridViewTextBoxColumn AddColumn(DataGridView grid, string header, int fillWeight,
        DataGridViewContentAlignment alignment = DataGridViewContentAlignment.MiddleLeft)
    {
        var column = new DataGridViewTextBoxColumn
        {
            HeaderText = header,
            AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
            FillWeight = fillWeight,
            SortMode = DataGridViewColumnSortMode.NotSortable,
        };
        column.DefaultCellStyle.Alignment = alignment;
        grid.Columns.Add(column);
        return column;
    }

    // ---------- สีประจำแนวเกม (ใช้วาดภาพปกเกม) ----------
    public static Color GenreColor(string? genre) => genre switch
    {
        "Action" => Color.FromArgb(192, 57, 43),
        "Adventure" => Color.FromArgb(22, 160, 133),
        "RPG" => Color.FromArgb(142, 68, 173),
        "Strategy" => Color.FromArgb(211, 84, 0),
        "Simulation" => Color.FromArgb(39, 174, 96),
        "Sports" => Color.FromArgb(41, 128, 185),
        "Racing" => Color.FromArgb(230, 126, 34),
        "Puzzle" => Color.FromArgb(183, 149, 11),
        "Horror" => Color.FromArgb(90, 35, 90),
        "Indie" => Color.FromArgb(26, 188, 156),
        _ => Color.FromArgb(52, 73, 94),
    };
}
