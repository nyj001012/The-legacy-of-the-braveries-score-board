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
        /*
         * ScrollBarManager.SetScrollBar(container, content, scrollBar)
         * - container: 스크롤바가 위치할 컨테이너 (Panel)
         * - content: 스크롤할 컨텐츠. container 내부에 있어야 함 (CustomFlowLayoutPanel)
         * - scrollBar: 스크롤바. container 내부에 있어야 함 (CyberScrollBar)
         */
        public static void SetScrollBar(System.Windows.Forms.Panel container, CustomFlowLayoutPanel content, CyberScrollBar scrollBar)
        {
            int contentHeight = content.Controls.Cast<Control>().Max(c => c.Bottom); // 마지막 레이블의 Bottom 위치
            if (contentHeight <= container.Height)
            {
                scrollBar.Enabled = false;
            }
            else
            {
                scrollBar.Enabled = true;
                scrollBar.Minimum = 0;
                // Maximum = (컨텐츠 높이) - (컨테이너 높이)
                scrollBar.Maximum = Math.Max(0, contentHeight - container.Height);
                scrollBar.ValueChanged += (s, e) =>
                {
                    content.Top = -scrollBar.Value;
                };
            }
        }
    }
}
