using System;
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
                Invalidate();
            }
        }

        private bool autoSize = false;
        public new bool AutoSize
        {
            get => autoSize;
            set
            {
                if (autoSize != value)
                {
                    autoSize = value;
                    AdjustSize();
                }
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

        public override string Text
        {
            get => base.Text ?? string.Empty; // Ensure a non-null value is returned
            set
            {
                base.Text = value ?? string.Empty; // Ensure null is not assigned
                if (AutoSize) AdjustSize();
                Invalidate();
            }
        }

        public override Font Font
        {
            get => base.Font;
            set
            {
                base.Font = value ?? SystemFonts.DefaultFont; // Ensure null is not assigned
                if (AutoSize) AdjustSize();
                Invalidate();
            }
        }
        protected override void OnPaddingChanged(EventArgs e)
        {
            base.OnPaddingChanged(e);
            if (AutoSize) AdjustSize();
        }

        private void AdjustSize()
        {
            using (Graphics g = this.CreateGraphics())
            {
                SizeF textSize = g.MeasureString(this.Text, this.Font);
                int width = (int)Math.Ceiling(textSize.Width) + this.Padding.Left + this.Padding.Right;
                int height = (int)Math.Ceiling(textSize.Height) + this.Padding.Top + this.Padding.Bottom;
                this.Size = new Size(width, height);
            }
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
