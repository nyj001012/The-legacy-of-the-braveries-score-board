using ReaLTaiizor.Controls;
using ScoreBoard.data.skill;
using ScoreBoard.Properties;
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
        private const int MAX_WIDTH = 599;

        // 패시브, 액티브 기술을 표시하는 패널
        private readonly CustomFlowLayoutPanel _flowLayoutPanel = new()
        {
            Width = MAX_WIDTH,
            AutoScroll = false,
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink,
            WrapContents = false,
            FlowDirection = FlowDirection.TopDown,
            Margin = new Padding(0, 0, 0, 0),
        };

        // 패시브 기술을 표시하는 패널
        private readonly CustomFlowLayoutPanel _passivePanel = new()
        {
            Width = MAX_WIDTH,
            AutoScroll = false,
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink,
            WrapContents = false,
            FlowDirection = FlowDirection.TopDown,
            Margin = new Padding(0, 0, 0, PADDING_BOTTOM),
        };

        // 액티브 기술을 표시하는 패널
        private readonly CustomFlowLayoutPanel _activePanel = new()
        {
            Width = MAX_WIDTH,
            AutoScroll = false,
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink,
            WrapContents = false,
            FlowDirection = FlowDirection.TopDown,
            Margin = new Padding(0, 0, 0, PADDING_BOTTOM),
        };

        public SkillDescriptionPanel(List<PassiveSkill>? passives, List<ActiveSkill>? actives)
        {
            InitializeComponent();
            _passives = passives ?? [];
            _actives = actives ?? [];

            Controls.Clear();
            _flowLayoutPanel.Controls.Add(_passivePanel);
            _flowLayoutPanel.Controls.Add(_activePanel);
            Controls.Add(_flowLayoutPanel);

            ShowPassives();
            ShowActives();
        }

        /*
         * ShowActives()
         * - 액티브 기술을 표시하는 메서드입니다.
         */
        private void ShowActives()
        {
            _activePanel.Controls.Add(CreateLabel("[액티브]", Color.WhiteSmoke));

            if (_actives.Count == 0)
            {
                _activePanel.Controls.Add(CreateLabel("액티브 기술이 없습니다.", Color.WhiteSmoke));
                return;
            }

            foreach (var active in _actives)
            {
                AddActiveSkillToPanel(active);
            }
        }

        /*
         * AddActiveSkillToPanel(ActiveSkill active)
         * - 단일 액티브 스킬 정보를 패널에 추가하는 메서드입니다.
         */
        private void AddActiveSkillToPanel(ActiveSkill active)
        {
            var foreColor = active.isOnCooldown
                ? Color.FromArgb(100, 245, 245, 245)
                : Color.WhiteSmoke;

            AddSkillHeaderRow(active, foreColor);
            AddSkillDescriptions(active, foreColor);
        }

        /*
         * AddSkillHeaderRow(ActiveSkill active, Color foreColor)
         * - 액티브 스킬 이름과 쿨타임을 가로로 정렬하여 표시하는 행을 추가합니다.
         */
        private void AddSkillHeaderRow(ActiveSkill active, Color foreColor)
        {
            var skillName = GetSkillNameWithRequiredLevel(active);

            var cooldownPanel = new CustomFlowLayoutPanel
            {
                Width = MAX_WIDTH,
                MaximumSize = new Size(MAX_WIDTH, 0),
                FlowDirection = FlowDirection.LeftToRight,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Margin = new Padding(0, 0, PADDING_BOTTOM, 0),
            };

            var skillNameLabel = CreateLabel(skillName, foreColor);
            var cooldownIcon = new PictureBox
            {
                Size = new Size(32, 32),
                Image = Resources.ImgCooltime,
                SizeMode = PictureBoxSizeMode.StretchImage,
            };
            var cooldownLabel = CreateLabel($"{active.Cooldown}턴", foreColor);

            cooldownPanel.Controls.Add(skillNameLabel);
            cooldownPanel.Controls.Add(cooldownIcon);
            cooldownPanel.Controls.Add(cooldownLabel);

            _activePanel.Controls.Add(cooldownPanel);
        }

        /*
         * AddSkillDescriptions(ActiveSkill active, Color foreColor)
         * - 액티브 스킬 설명 라인을 추가합니다.
         */
        private void AddSkillDescriptions(ActiveSkill active, Color foreColor)
        {
            foreach (var (desc, idx) in active.Description.Select((d, i) => (d, i)))
            {
                var descriptionLabel = CreateLabel("= " + desc, foreColor);
                if (idx == active.Description.Length - 1)
                    descriptionLabel.Margin = new Padding(0, 0, 0, PADDING_BOTTOM);

                _activePanel.Controls.Add(descriptionLabel);
            }
        }



        /*
         * GetPassiveDisplayName(SkillBase skill)
         * - 패시브 기술의 이름을 레벨에 따라 다르게 표시하는 메서드입니다.
         */
        string GetSkillNameWithRequiredLevel(SkillBase skill)
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

            // 패시브 기술이 없을 경우
            if (_passives.Count == 0)
            {
                _passivePanel.Controls.Add(CreateLabel("패시브 기술이 없습니다.", Color.WhiteSmoke));
                return;
            }

            // 패시브 기술이 있을 경우, 스킬 이름과 설명을 표시합니다.
            foreach (var passive in _passives)
            {
                Color foreColor = passive.isActivated ? Color.WhiteSmoke : Color.FromArgb(100, 245, 245, 245);

                _passivePanel.Controls.Add(CreateLabel(GetSkillNameWithRequiredLevel(passive), foreColor));

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
            TransparentTextLabel label = new()
            {
                AutoSize = true,
                MaximumSize = new Size(MAX_WIDTH, 0),
                Text = text,
                Font = new Font("Danjo-bold", FONT_SIZE),
                ForeColor = foreColor,
                TextAlign = ContentAlignment.MiddleLeft
            };
            return label;
        }
    }
}
