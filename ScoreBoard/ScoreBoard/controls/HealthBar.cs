using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ScoreBoard.controls
{
    public class HealthBar : Control
    {
        [Category("Health Bar")]
        public int MaxValue { get; set; } = 100;

        [Category("Health Bar")]
        public int Health { get; set; } = 70;

        [Category("Health Bar")]
        public int Shield { get; set; } = 30;

        [Category("Appearance")]
        public int CornerRadius { get; set; } = 10;

        [Category("Appearance")]
        public Color HealthColor { get; set; } = Color.LimeGreen;

        [Category("Appearance")]
        public Color ShieldColor { get; set; } = Color.White;

        [Category("Appearance")]
        public Color BorderColor { get; set; } = Color.Black;

        [Category("Appearance")]
        public float BorderThickness { get; set; } = 2f;

        [Category("Appearance")]
        public bool TextVisible { get; set; } = true;

        [Category("Appearance")]
        public Font TextFont { get; set; } = new Font("Segoe UI", 9, FontStyle.Bold);

        [Category("Appearance")]
        public Color TextColor { get; set; } = Color.Black;

        public HealthBar()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.SupportsTransparentBackColor |
                     ControlStyles.UserPaint, true);

            this.BackColor = SystemColors.Control;
        }
        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            if (!DesignMode)
                this.BackColor = Color.Transparent;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            int width = Width;
            int height = Height;

            float healthRatio = Math.Clamp((float)Health / MaxValue, 0, 1);
            float shieldRatio = Math.Clamp((float)Shield / MaxValue, 0, 1);

            int healthWidth = (int)(width * healthRatio);
            int shieldWidth = (int)(width * shieldRatio);

            Rectangle fullRect = new Rectangle(0, 0, width - 1, height - 1); // 테두리 고려
            Rectangle healthRect = new Rectangle(0, 0, healthWidth, height);
            Rectangle shieldRect = new Rectangle(healthWidth, 0, shieldWidth, height);

            using (GraphicsPath path = RoundedRect(fullRect, CornerRadius))
            using (Pen borderPen = new Pen(BorderColor, BorderThickness))
            using (Brush healthBrush = new SolidBrush(HealthColor))
            using (Brush shieldBrush = new SolidBrush(ShieldColor))
            {
                // 클리핑 영역을 둥근 테두리로 제한
                e.Graphics.SetClip(path);

                // 체력 채우기
                if (healthWidth > 0)
                    e.Graphics.FillRectangle(healthBrush, healthRect);

                // 보호막 채우기
                if (shieldWidth > 0)
                    e.Graphics.FillRectangle(shieldBrush, shieldRect);

                // 테두리
                e.Graphics.DrawPath(borderPen, path);

                // 텍스트 표시
                if (TextVisible)
                {
                    string text = $"{Health}" + (Shield > 0 ? $" (+{Shield})" : "") + $" / {MaxValue}";
                    SizeF textSize = e.Graphics.MeasureString(text, TextFont);
                    PointF textPos = new PointF((width - textSize.Width) / 2, (height - textSize.Height) / 2);
                    using (Brush textBrush = new SolidBrush(TextColor))
                    {
                        e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                        e.Graphics.DrawString(text, TextFont, textBrush, textPos);
                    }
                }
            }
        }

        private GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            GraphicsPath path = new();
            if (radius <= 0)
            {
                path.AddRectangle(bounds);
                return path;
            }

            path.AddArc(bounds.X, bounds.Y, diameter, diameter, 180, 90);
            path.AddArc(bounds.Right - diameter, bounds.Y, diameter, diameter, 270, 90);
            path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();
            return path;
        }

        public void SetValues(int health, int shield, int max)
        {
            Health = health;
            Shield = shield;
            MaxValue = max;
            Invalidate();
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            if (BackColor == Color.Transparent && Parent != null)
            {
                using (Bitmap bmp = new Bitmap(Parent.Width, Parent.Height))
                {
                    Parent.DrawToBitmap(bmp, Parent.ClientRectangle);
                    pevent.Graphics.DrawImage(bmp, -Left, -Top);
                }
            }
            else
            {
                base.OnPaintBackground(pevent);
            }
        }
    }
}
