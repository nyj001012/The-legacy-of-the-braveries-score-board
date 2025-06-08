using ScoreBoard.controls;
using ScoreBoard.data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScoreBoard.content
{
    public partial class ScoreBoardControl : UserControl
    {
        public ScoreBoardControl(Dictionary<string, CorpsMember> characters, List<(string id, string name, ushort count)> monsters)
        {
            InitializeComponent();
        }

        /*
         * 패널 내 자식 컨트롤들을 수직 중앙 정렬합니다.
         * - parent: 중앙 정렬의 기준이 되는 부모 컨트롤
         */
        private void CenterPanelChildrenVertically(Control parent)
        {
            // 실제 위치 기준 정렬 (Top 작은 순)
            var children = parent.Controls.Cast<Control>().OrderBy(c => c.Top).ToArray();

            int totalHeight = children.Sum(c => c.Height);
            int startY = (parent.Height - totalHeight) / 2;

            int y = startY;
            foreach (var c in children)
            {
                // Dock, Anchor 영향 없는지 체크!
                c.Dock = DockStyle.None;
                c.Anchor = AnchorStyles.Top | AnchorStyles.Left; // 필요에 따라

                c.Top = y;
                y += c.Height;
            }
        }

        private void pnPlayer_CenterPanelChildrenVertically(object sender, ControlEventArgs e)
        {
            CenterPanelChildrenVertically((Control)sender);
        }
    }
}
