using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ScoreBoard.controls
{
    public class GradientLabel : Control
    {
        public Color GradientStart { get; set; } = ColorTranslator.FromHtml("#D1A261");
        public Color GradientEnd { get; set; } = ColorTranslator.FromHtml("#6B5932");
        private ContentAlignment textAlign = ContentAlignment.MiddleCenter;

        public ContentAlignment TextAlign
        {
            get => textAlign;
            set
            {
                textAlign = value;
                Invalidate(); // 값 바뀌면 다시 그리기
            }
        }

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
                GetAlignments(this.TextAlign, out var hAlign, out var vAlign);

                StringFormat format = new StringFormat
                {
                    Alignment = hAlign,
                    LineAlignment = vAlign
                };

                e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                e.Graphics.DrawString(this.Text, this.Font, brush, this.ClientRectangle, format);
            }
        }


        private void GetAlignments(ContentAlignment align, out StringAlignment horizontal, out StringAlignment vertical)
        {
            switch (align)
            {
                case ContentAlignment.TopLeft:
                    horizontal = StringAlignment.Near;
                    vertical = StringAlignment.Near;
                    break;
                case ContentAlignment.TopCenter:
                    horizontal = StringAlignment.Center;
                    vertical = StringAlignment.Near;
                    break;
                case ContentAlignment.TopRight:
                    horizontal = StringAlignment.Far;
                    vertical = StringAlignment.Near;
                    break;
                case ContentAlignment.MiddleLeft:
                    horizontal = StringAlignment.Near;
                    vertical = StringAlignment.Center;
                    break;
                case ContentAlignment.MiddleCenter:
                    horizontal = StringAlignment.Center;
                    vertical = StringAlignment.Center;
                    break;
                case ContentAlignment.MiddleRight:
                    horizontal = StringAlignment.Far;
                    vertical = StringAlignment.Center;
                    break;
                case ContentAlignment.BottomLeft:
                    horizontal = StringAlignment.Near;
                    vertical = StringAlignment.Far;
                    break;
                case ContentAlignment.BottomCenter:
                    horizontal = StringAlignment.Center;
                    vertical = StringAlignment.Far;
                    break;
                case ContentAlignment.BottomRight:
                    horizontal = StringAlignment.Far;
                    vertical = StringAlignment.Far;
                    break;
                default:
                    horizontal = StringAlignment.Center;
                    vertical = StringAlignment.Center;
                    break;
            }
        }
    }
}
