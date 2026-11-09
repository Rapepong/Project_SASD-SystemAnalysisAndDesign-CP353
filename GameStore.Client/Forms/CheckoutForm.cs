using GameStore.Client.Services;
using GameStore.Client.UI;
using GameStore.Contracts;

namespace GameStore.Client.Forms
{
    // หน้าชำระเงิน: เลือกบัญชีธนาคาร (จำลอง) แล้วยืนยัน
    public partial class CheckoutForm : Form
    {
        public CheckoutForm(CartDto cart)
        {
            InitializeComponent();

            foreach (var item in cart.Items)
                lstItems.Items.Add($"{item.Title}    {Theme.Money(item.Price)}");

            lblTotal.Text = $"ยอดชำระ {Theme.Money(cart.TotalAmount)}  ({cart.Items.Count} เกม)";
            cboBankAccount.Text = Session.LastBankAccountNo ?? "111-1-11111-1";
        }

        private async void btnPay_Click(object sender, EventArgs e)
        {
            string accountNo = cboBankAccount.Text.Trim();
            PurchaseReceiptDto? receipt = null;

            btnPay.Enabled = false;
            bool ok = await Ui.RunAsync(this, async () =>
                receipt = await StoreApi.CheckoutAsync(Session.UserId, accountNo));
            btnPay.Enabled = true;

            // ไม่สำเร็จ (เช่น เงินไม่พอ -> HTTP 402) : Ui.RunAsync แสดงข้อความแล้ว ให้ผู้ใช้ลองบัญชีอื่นได้
            if (!ok || receipt == null)
                return;

            Session.LastBankAccountNo = accountNo;

            var order = receipt.Order;
            string games = string.Join("\n", order.Items.Select(i => $"   • {i.Title}  {Theme.Money(i.Price)}"));
            Ui.Info(this,
                "ชำระเงินสำเร็จ!\n\n" +
                $"เลขที่คำสั่งซื้อ: #{order.Id}\n" +
                $"{games}\n\n" +
                $"ยอดชำระ: {Theme.Money(order.TotalAmount)}\n" +
                $"เลขอ้างอิงธนาคาร: {order.PaymentRef}\n" +
                $"ยอดคงเหลือในบัญชี {order.BankAccountNo}: {Theme.Money(receipt.BankBalanceAfter)}\n\n" +
                "เกมถูกเพิ่มเข้า Game List ของคุณแล้ว",
                "ใบเสร็จ");

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
