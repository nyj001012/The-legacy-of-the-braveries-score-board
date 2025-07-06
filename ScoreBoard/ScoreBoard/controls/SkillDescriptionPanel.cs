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
        private CustomFlowLayoutPanel _passivePanel = new()
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
            this._passives = passives ?? [];
            this._actives = actives ?? [];
            this.Controls.Clear();
            this.Controls.Add(_passivePanel);
            ShowPassives();
        }

        private void ShowPassives()
        {
            TransparentTextLabel label = new()
            {
                AutoSize = true,
                MaximumSize = new Size(579, 0),
                Text = "[패시브]",
                Font = new Font("Danjo-bold", FONT_SIZE),
                ForeColor = Color.WhiteSmoke,
            };
            _passivePanel.Controls.Add(label);

            if (_passives.Count == 0)
            {
                TransparentTextLabel noPassivesLabel = new()
                {
                    AutoSize = true,
                    MaximumSize = new Size(579, 0),
                    Text = "패시브 기술이 없습니다.",
                    Font = new Font("Danjo-bold", FONT_SIZE),
                    ForeColor = Color.WhiteSmoke,
                };
                _passivePanel.Controls.Add(noPassivesLabel);
            }
            else
            {
                foreach (var passive in _passives)
                {
                    Color foreColour = passive.isActivated ? Color.WhiteSmoke : Color.FromArgb(100, 245, 245, 245);
                    TransparentTextLabel passiveLabel = new()
                    {
                        AutoSize = true,
                        MaximumSize = new Size(579, 0),
                        Text = passive.Name,
                        Font = new Font("Danjo-bold", FONT_SIZE),
                        ForeColor = foreColour,
                    };
                    _passivePanel.Controls.Add(passiveLabel);

                    foreach (var description in passive.Description)
                    {
                        TransparentTextLabel descriptionLabel = new()
                        {
                            AutoSize = true,
                            MaximumSize = new Size(579, 0),
                            Text = $"= {description}",
                            Font = new Font("Danjo-bold", FONT_SIZE),
                            ForeColor = foreColour,
                        };

                        // 마지막 줄에만 패딩 추가
                        if (description == passive.Description.Last())
                        {
                            descriptionLabel.Margin = new Padding(0, 0, 0, PADDING_BOTTOM);
                        }

                        _passivePanel.Controls.Add(descriptionLabel);
                    }
                }
            }
        }
    }
}
