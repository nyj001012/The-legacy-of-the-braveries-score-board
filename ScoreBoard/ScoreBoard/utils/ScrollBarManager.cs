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
        public static void SetScrollBar(System.Windows.Forms.Panel container, CustomFlowLayoutPanel content, CyberScrollBar scrollBar)
        {
            int contentHeight = content.Controls.Cast<Control>()
                                                .Where(c => c.Visible)  // Visible이 true인 컨트롤만 선택
                                                .Sum(c => c.Height + c.Margin.Top + c.Margin.Bottom);

            if (contentHeight <= container.Height)
            {
                scrollBar.Enabled = false;
            }
            else
            {
                scrollBar.Enabled = true;
                scrollBar.Minimum = 0;
                scrollBar.Maximum = Math.Max(0, contentHeight - container.Height);

                // 중복 방지: 기존 핸들러 제거
                scrollBar.ValueChanged -= ScrollBar_ValueChanged;

                // 새로 바인딩
                bindings[scrollBar] = content;
                scrollBar.ValueChanged += ScrollBar_ValueChanged;
            }
        }

        private static void ScrollBar_ValueChanged(object? sender, EventArgs e)
        {
            if (sender is CyberScrollBar sb && bindings.TryGetValue(sb, out var content))
            {
                content.Top = -sb.Value;
            }
        }
    }
}
