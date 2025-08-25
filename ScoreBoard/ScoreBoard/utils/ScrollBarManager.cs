using ReaLTaiizor.Controls;
using ScoreBoard.controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.utils
{
    public static class ScrollBarManager
    {
        private static readonly Dictionary<CyberScrollBar, CustomFlowLayoutPanel> bindings = [];

        /*
         * ScrollBarManager.SetScrollBar(container, content, scrollBar)
         * - container: 스크롤바가 위치할 컨테이너 (Panel)
         * - content: 스크롤할 컨텐츠. container 내부에 있어야 함 (CustomFlowLayoutPanel)
         * - scrollBar: 스크롤바. container 내부에 있어야 함 (CyberScrollBar)
         */
        public static void SetScrollBar(Control container, Control content, CyberScrollBar bar)
        {
            int contentHeight =
                content is FlowLayoutPanel flp
                ? flp.DisplayRectangle.Height
                : content.Controls.Cast<Control>()
                    .Where(c => c.Visible)
                    .Select(c => c.Bottom + c.Margin.Bottom)
                    .DefaultIfEmpty(0)
                    .Max() + content.Padding.Bottom;

            int viewport = container.ClientSize.Height - container.Padding.Vertical;

            if (contentHeight <= viewport)
            {
                bar.Enabled = false;
                content.Top = container.Padding.Top;
                return;
            }

            bar.Enabled = true;
            bar.Minimum = 0;
            bar.Maximum = Math.Max(0, contentHeight - viewport);
            // bar.SmallStep 등은 기존처럼

            // 위치 이동은 ValueChanged 한 곳에서만
            bar.ValueChanged -= OnScroll;
            bar.ValueChanged += OnScroll;

            void OnScroll(object? s, EventArgs e)
            {
                content.Top = container.Padding.Top - bar.Value;
            }
        }
    }
}
