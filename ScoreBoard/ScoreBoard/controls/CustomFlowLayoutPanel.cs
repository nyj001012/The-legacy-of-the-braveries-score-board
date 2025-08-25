using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace ScoreBoard.controls
{
    public class CustomFlowLayoutPanel : FlowLayoutPanel
    {
        public int BorderThickness { get; set; } = 0;
        public Color BorderColor { get; set; } = Color.Transparent;

        public CustomFlowLayoutPanel()
        {
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.ResizeRedraw
                        | ControlStyles.UserPaint
                        | ControlStyles.AllPaintingInWmPaint
                        | ControlStyles.SupportsTransparentBackColor, true);
        }

        protected override void OnScroll(ScrollEventArgs se)
        {
            base.OnScroll(se);
            this.Invalidate(); // 강제 다시 그리기
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // 테두리 그리기
            using Pen pen = new Pen(BorderColor, BorderThickness)
            {
                Alignment = System.Drawing.Drawing2D.PenAlignment.Inset
            };
            e.Graphics.DrawRectangle(pen, 0, 0, this.Width - 1, this.Height - 1);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // WS_EX_COMPOSITED (깜빡임 방지)
                return cp;
            }
        }
    }
}
