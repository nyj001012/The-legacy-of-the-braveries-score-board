using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace TheLagacyOfTheBraveriesScoreBoard.controls
{
    public class GradientLabel : Control
    {
        public Color GradientStart { get; set; } = Color.Blue;
        public Color GradientEnd { get; set; } = Color.Red;

        public GradientLabel()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.UserPaint |
                          ControlStyles.OptimizedDoubleBuffer, true);
            this.UpdateStyles();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            using (LinearGradientBrush brush = new LinearGradientBrush(
                this.ClientRectangle,
                GradientStart,
                GradientEnd,
                LinearGradientMode.Horizontal))
            {
                StringFormat format = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };

                e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                e.Graphics.DrawString(this.Text, this.Font, brush, this.ClientRectangle, format);
            }
        }
    }
}
