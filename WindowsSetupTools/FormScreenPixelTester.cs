using System.Drawing;
using System.Windows.Forms;

namespace WindowsSetupTools
{
    public partial class FormScreenPixelTester : Form
    {
        private readonly Color[] testColors = new Color[]
        {
            Color.Red,          // Kiểm tra Sub-pixel đỏ
            Color.Green,        // Kiểm tra Sub-pixel xanh lá
            Color.Blue,         // Kiểm tra Sub-pixel xanh dương
            Color.White,        // Kiểm tra điểm chết hoàn toàn (Dead Pixel - màu đen)
            Color.Black,        // Kiểm tra điểm kẹt sáng (Stuck Pixel - sáng bất thường)
            Color.Yellow,       // Mở rộng: Đỏ + Xanh lá
            Color.Cyan,         // Mở rộng: Xanh lá + Xanh dương
            Color.Magenta       // Mở rộng: Đỏ + Xanh dương
        };

        private int currentColorIndex = 0;

        public FormScreenPixelTester()
        {
            InitializeComponent();
            SetupFullscreen();
        }

        private void SetupFullscreen()
        {
            // Thiết lập Form ở chế độ Fullscreen không viền
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.TopMost = true; // Luôn hiển thị trên cùng

            // Đặt màu nền ban đầu
            this.BackColor = testColors[currentColorIndex];

            // Đăng ký sự kiện phím và chuột
            this.KeyDown += Form1_KeyDown;
            this.MouseClick += Form1_MouseClick;

            // Đảm bảo Form nhận được phím điều hướng
            this.KeyPreview = true;
        }

        private void NextColor()
        {
            currentColorIndex = (currentColorIndex + 1) % testColors.Length;
            this.BackColor = testColors[currentColorIndex];
        }

        private void PreviousColor()
        {
            currentColorIndex = (currentColorIndex - 1 + testColors.Length) % testColors.Length;
            this.BackColor = testColors[currentColorIndex];
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            else if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Back || e.KeyCode == Keys.Up)
            {
                PreviousColor();
            }
            else if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Right || e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                NextColor();
            }
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                NextColor();
            }
            else if (e.Button == MouseButtons.Right)
            {
                PreviousColor();
            }
        }
    }
}
