using GameStore.Client.Services;

namespace GameStore.Client.UI;

public static class Ui
{
    // เรียก REST API แบบ async พร้อมจัดการ error ให้ทุกหน้าจอใช้ร่วมกัน
    //   - ระหว่างรอ : เปลี่ยน cursor เป็นรูปนาฬิกา (หน้าจอไม่ค้าง)
    //   - ถ้า error : แสดงข้อความจาก server ให้ผู้ใช้เห็น
    // คืนค่า true ถ้าสำเร็จ
    public static async Task<bool> RunAsync(Form owner, Func<Task> action)
    {
        owner.UseWaitCursor = true;
        try
        {
            await action();
            return true;
        }
        catch (ApiException ex)
        {
            string caption = ex.StatusCode == 0 ? "ติดต่อ server ไม่ได้" : $"ทำรายการไม่สำเร็จ (HTTP {ex.StatusCode})";
            MessageBox.Show(owner, ex.Message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }
        finally
        {
            owner.UseWaitCursor = false;
        }
    }

    public static void Info(Form owner, string message, string caption = "สำเร็จ") =>
        MessageBox.Show(owner, message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
}
