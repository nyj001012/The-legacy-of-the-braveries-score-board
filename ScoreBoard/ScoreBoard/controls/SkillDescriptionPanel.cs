using ScoreBoard.data.skill;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScoreBoard.controls
{
    public partial class SkillDescriptionPanel : UserControl
    {
        private readonly List<PassiveSkill> _passives;
        private readonly List<ActiveSkill> _actives;

        private const int PADDING_BOTTOM = 30;
        private const int FONT_SIZE = 22;
        private const int MAX_WIDTH = 579;

        private readonly CustomFlowLayoutPanel _passivePanel = new()
        {
            Width = 599,
            AutoScroll = false,
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink,
            WrapContents = false,
            FlowDirection = FlowDirection.TopDown,
            Margin = new Padding(0, 0, 0, 10),
        };

        public SkillDescriptionPanel(List<PassiveSkill>? passives, List<ActiveSkill>? actives)
        {
            InitializeComponent();
            _passives = passives ?? [];
            _actives = actives ?? [];

            Controls.Clear();
            Controls.Add(_passivePanel);

            ShowPassives();
        }

        /*
         * GetPassiveDisplayName(SkillBase skill)
         * - 패시브 기술의 이름을 레벨에 따라 다르게 표시하는 메서드입니다.
         */
        string GetPassiveDisplayName(SkillBase skill)
        {
            return skill.RequiredLevel switch
            {
                0 => $"{skill.Name} (기본)",
                3 => $"{skill.Name} (궁극 강화)",
                _ => $"{skill.Name} ({skill.RequiredLevel}강)"
            };
        }

        /*
         * ShowPassives()
         * - 패시브 기술을 표시하는 메서드입니다.
         */
        private void ShowPassives()
        {
            _passivePanel.Controls.Add(CreateLabel("[패시브]", Color.WhiteSmoke));

            if (_passives.Count == 0)
            {
                _passivePanel.Controls.Add(CreateLabel("패시브 기술이 없습니다.", Color.WhiteSmoke));
                return;
            }

            foreach (var passive in _passives)
            {
                Color foreColor = passive.isActivated ? Color.WhiteSmoke : Color.FromArgb(100, 245, 245, 245);

                _passivePanel.Controls.Add(CreateLabel(GetPassiveDisplayName(passive), foreColor));

                for (int i = 0; i < passive.Description.Length; i++)
                    {
                        var descriptionLabel = CreateLabel("= " + passive.Description[i], foreColor);
                        if (i == passive.Description.Length - 1)
                            descriptionLabel.Margin = new Padding(0, 0, 0, PADDING_BOTTOM);

                        _passivePanel.Controls.Add(descriptionLabel);
                    }
            }
        }

        /*
         * CreateLabel(string text, Color foreColor)
         * - 주어진 텍스트와 색상으로 투명한 레이블을 생성하는 메서드입니다.
         */
        private TransparentTextLabel CreateLabel(string text, Color foreColor)
        {
            return new TransparentTextLabel
            {
                AutoSize = true,
                MaximumSize = new Size(MAX_WIDTH, 0),
                Text = text,
                Font = new Font("Danjo-bold", FONT_SIZE),
                ForeColor = foreColor
            };
        }
    }
}
