using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Text;

namespace ScoreBoard.controls
{
    public class TransparentTextLabel : Label
    {
        private float _lineSpacingMultiplier = 1.0f;

        public TransparentTextLabel()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.UserPaint |
                     ControlStyles.SupportsTransparentBackColor, true);

            BackColor = Color.Transparent;
        }

        public void SetLineSpacing(float multiplier)
        {
            _lineSpacingMultiplier = multiplier <= 0f ? 1f : multiplier;
            UpdateHeight();
            Invalidate();
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            UpdateHeight();
            Invalidate();
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            UpdateHeight();
            Invalidate();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            UpdateHeight();
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

            using var brush = new SolidBrush(ForeColor);
            var format = new StringFormat(StringFormat.GenericTypographic)
            {
                FormatFlags = StringFormatFlags.MeasureTrailingSpaces
            };

            float y = 0f;
            float layoutWidth = ClientRectangle.Width;
            string[] logicalLines = Text?.Replace("\r\n", "\n").Split('\n') ?? [];

            foreach (var logicalLine in logicalLines)
            {
                string remaining = logicalLine;
                while (!string.IsNullOrEmpty(remaining))
                {
                    float x = 0f;
                    float lineHeight = Font.GetHeight(e.Graphics) * _lineSpacingMultiplier;

                    int i = 0;
                    for (; i < remaining.Length; i++)
                    {
                        string charStr = remaining[i].ToString();
                        SizeF charSize = e.Graphics.MeasureString(charStr, Font, new SizeF(layoutWidth, float.MaxValue), format);

                        if (x + charSize.Width > layoutWidth)
                        {
                            break; // 줄바꿈 위치 도달
                        }

                        e.Graphics.DrawString(charStr, Font, brush, x, y, format);
                        x += charSize.Width;
                    }

                    // i == 0 이면 무한루프 방지
                    int cut = (i > 0) ? i : 1;
                    remaining = remaining.Substring(cut);

                    y += lineHeight;
                }
            }

        }

        /*
         * 글자의 높이를 계산하여 Label의 높이를 조정합니다.
         */
        private void UpdateHeight()
        {
            if (string.IsNullOrEmpty(Text) || Width <= 0)
                return;

            using var g = CreateGraphics();
            using var format = new StringFormat(StringFormat.GenericTypographic)
            {
                FormatFlags = StringFormatFlags.MeasureTrailingSpaces
            };

            float y = 0f;
            float layoutWidth = ClientRectangle.Width;
            string[] logicalLines = Text.Replace("\r\n", "\n").Split('\n');

            foreach (var logicalLine in logicalLines)
            {
                string remaining = logicalLine;
                while (!string.IsNullOrEmpty(remaining))
                {
                    float x = 0f;
                    float lineHeight = Font.GetHeight(g) * _lineSpacingMultiplier;

                    int i = 0;
                    for (; i < remaining.Length; i++)
                    {
                        string charStr = remaining[i].ToString();
                        SizeF charSize = g.MeasureString(charStr, Font, new SizeF(layoutWidth, float.MaxValue), format);

                        if (x + charSize.Width > layoutWidth)
                        {
                            break;
                        }

                        x += charSize.Width;
                    }

                    int cut = (i > 0) ? i : 1;
                    remaining = remaining.Substring(cut);

                    y += lineHeight;
                }
            }


            Height = (int)Math.Ceiling(y);
        }
    }
}
