using GameStore.Client.Services;
using GameStore.Client.UI;
using GameStore.Contracts;

namespace GameStore.Client.Forms
{
    // หน้าเข้าสู่ระบบ / สมัครสมาชิก
    public partial class LoginForm : Form
    {
        private bool registerMode;

        public LoginForm()
        {
            InitializeComponent();
            SetRegisterMode(false);
        }

        private void SetRegisterMode(bool value)
        {
            registerMode = value;

            // ช่องที่ใช้เฉพาะตอนสมัครสมาชิก (FlowLayoutPanel จะเลื่อนช่องอื่นขึ้นมาแทนที่ให้เอง)
            lblDisplayName.Visible = value;
            txtDisplayName.Visible = value;
            lblEmail.Visible = value;
            txtEmail.Visible = value;

            lblSubtitle.Text = value ? "สมัครสมาชิกใหม่" : "เข้าสู่ระบบเพื่อเลือกซื้อเกม";
            btnSubmit.Text = value ? "สมัครสมาชิก" : "เข้าสู่ระบบ";
            lnkToggleMode.Text = value ? "มีบัญชีแล้ว? เข้าสู่ระบบ" : "ยังไม่มีบัญชี? สมัครสมาชิก";
            Text = value ? "Game Store - สมัครสมาชิก" : "Game Store - เข้าสู่ระบบ";
        }

        private void lnkToggleMode_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SetRegisterMode(!registerMode);
        }

        private async void btnSubmit_Click(object sender, EventArgs e)
        {
            btnSubmit.Enabled = false;
            UserDto? user = null;

            bool ok = await Ui.RunAsync(this, async () =>
            {
                if (registerMode)
                {
                    user = await StoreApi.RegisterAsync(new RegisterRequest(
                        txtUsername.Text, txtPassword.Text, txtDisplayName.Text, txtEmail.Text));
                }
                else
                {
                    user = await StoreApi.LoginAsync(txtUsername.Text, txtPassword.Text);
                }
            });

            btnSubmit.Enabled = true;
            if (!ok || user == null)
                return;

            if (registerMode)
                Ui.Info(this, $"สมัครสมาชิกสำเร็จ ยินดีต้อนรับคุณ {user.DisplayName}");

            Session.CurrentUser = user;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
