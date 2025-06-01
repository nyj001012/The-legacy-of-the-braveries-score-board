using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ScoreBoard.controls
{
    public class GradientLabel : Control
    {
        public Color GradientStart { get; set; } = ColorTranslator.FromHtml("#D1A261");
        public Color GradientEnd { get; set; } = ColorTranslator.FromHtml("#6B5932");

        public GradientLabel()
        {
            this.SetStyle(ControlStyles.SupportsTransparentBackColor |
                          ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.UserPaint |
                          ControlStyles.OptimizedDoubleBuffer, true);
            this.BackColor = Color.Transparent;
            this.UpdateStyles();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            using (LinearGradientBrush brush = new LinearGradientBrush(
                this.ClientRectangle,
                GradientStart,
                GradientEnd,
                LinearGradientMode.Vertical))
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
