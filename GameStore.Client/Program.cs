using GameStore.Client.Forms;

namespace GameStore.Client;

// Presentation Layer (Client Tier) ของ Online Game Store
// ติดต่อกับ Server ผ่าน REST API เท่านั้น (ดู Services/StoreApi.cs)
static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        // วนกลับมาหน้า login เมื่อผู้ใช้กด "ออกจากระบบ"
        while (true)
        {
            using var login = new LoginForm();
            if (login.ShowDialog() != DialogResult.OK)
                return;

            using var main = new MainForm();
            main.ShowDialog();
            if (!main.LogoutRequested)
                return;
        }
    }
}
