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
        private const int PADDING_BOTTOM = 10;
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
                Font = new Font("Danjo-bold", 25),
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
                    Font = new Font("Danjo-bold", 20),
                    ForeColor = Color.WhiteSmoke,
                };
                _passivePanel.Controls.Add(noPassivesLabel);
            }
            else
            {
                foreach (var passive in _passives)
                {
                    TransparentTextLabel passiveLabel = new()
                    {
                        AutoSize = true,
                        MaximumSize = new Size(579, 0),
                        Text = passive.Name,
                        Font = new Font("Danjo-bold", 20),
                        ForeColor = Color.WhiteSmoke,
                    };
                    _passivePanel.Controls.Add(passiveLabel);

                    foreach (var description in passive.Description)
                    {
                        TransparentTextLabel descriptionLabel = new()
                        {
                            AutoSize = true,
                            MaximumSize = new Size(579, 0),
                            Text = $"– {description}",
                            Font = new Font("Danjo-bold", 15),
                            ForeColor = Color.WhiteSmoke,
                        };
                        _passivePanel.Controls.Add(descriptionLabel);
                    }
                }
            }
        }
    }
}
