using GameStore.Client.Services;
using GameStore.Client.UI;
using GameStore.Contracts;

namespace GameStore.Client.Forms
{
    // หน้าขอคืนเงิน 1 เกม
    public partial class RefundForm : Form
    {
        private readonly LibraryItemDto item;

        public RefundForm(LibraryItemDto item)
        {
            InitializeComponent();
            this.item = item;

            lblGame.Text = item.Title;
            lblDetail.Text =
                $"ราคาที่จ่าย {Theme.Money(item.PricePaid)}   ·   เล่นไปแล้ว {Theme.Playtime(item.PlaytimeMinutes)}\n" +
                $"{item.RefundNote}\n" +
                "เงินจะคืนเข้าบัญชีธนาคารเดิมที่ใช้จ่าย และเกมจะถูกนำออกจาก Game List";
        }

        private async void btnConfirm_Click(object sender, EventArgs e)
        {
            var answer = MessageBox.Show(this,
                $"ยืนยันขอคืนเงินเกม '{item.Title}' ?",
                "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (answer != DialogResult.Yes)
                return;

            RefundReceiptDto? receipt = null;
            btnConfirm.Enabled = false;
            bool ok = await Ui.RunAsync(this, async () =>
                receipt = await StoreApi.RequestRefundAsync(Session.UserId, item.OrderItemId, txtReason.Text));
            btnConfirm.Enabled = true;

            if (!ok || receipt == null)
                return;

            Ui.Info(this,
                "คืนเงินสำเร็จ!\n\n" +
                $"เกม: {receipt.Refund.Title}\n" +
                $"จำนวนเงินที่คืน: {Theme.Money(receipt.Refund.Amount)}\n" +
                $"เลขอ้างอิงธนาคาร: {receipt.Refund.PaymentRef}\n" +
                $"ยอดคงเหลือในบัญชี: {Theme.Money(receipt.BankBalanceAfter)}",
                "คืนเงินสำเร็จ");

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
