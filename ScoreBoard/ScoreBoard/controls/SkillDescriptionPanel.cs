using ReaLTaiizor.Controls;
using ScoreBoard.data.skill;
using ScoreBoard.modals;
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
        private const int MAX_WIDTH = 600;

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
            var skillName = GetSkillNameWithRequiredLevel(active); // 스킬 이름과 레벨을 조합

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
            skillNameLabel.Tag = active.Name; // 스킬 이름을 태그로 저장
            var cooldownIcon = new PictureBox
            {
                Tag = active.Name, // 스킬 이름을 태그로 저장
                Size = new Size(32, 32),
                Image = Resources.ImgCooltime,
                SizeMode = PictureBoxSizeMode.StretchImage,
            };
            var cooldownLabel = CreateLabel($"{active.Cooldown}턴", foreColor);
            cooldownLabel.Tag = active.Name; // 스킬 이름을 태그로 저장
            cooldownLabel.Cursor = Cursors.Hand;
            cooldownLabel.Click += (s, e) => UseAndEditCooldown(active, cooldownLabel);

            cooldownPanel.Controls.Add(skillNameLabel);
            cooldownPanel.Controls.Add(cooldownIcon);
            cooldownPanel.Controls.Add(cooldownLabel);

            _activePanel.Controls.Add(cooldownPanel);
        }

        /*
         * UseAndEditCooldown(ActiveSkill skill)
         * - 액티브 스킬을 사용하고 쿨타임을 수정하는 메서드입니다.
         */
        private void UseAndEditCooldown(ActiveSkill skill, TransparentTextLabel skillLabel)
        {
            // 스킬 쿨다운을 수정하는 폼을 표시
            DetailEditModal modal = new(skill.CurrentCooldown.ToString())
            {
                StartPosition = FormStartPosition.Manual,
                Location = skillLabel.PointToScreen(Point.Empty),
            };

            if (modal.ShowDialog(this) == DialogResult.OK)
            {
                // 사용자가 입력한 쿨다운 값을 적용
                if (int.TryParse(modal.InputText, out int newCooldown) && newCooldown >= 0)
                {
                    skill.CurrentCooldown = (ushort)newCooldown > skill.Cooldown
                                            ? skill.Cooldown : (ushort)newCooldown;
                    skill.isOnCooldown = newCooldown > 0;
                    // 쿨다운 레이블 업데이트
                    MakeSkillUnavailable(skill);
                }
                else
                {
                    MessageBox.Show($"유효한 쿨다운 값을 입력하세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /*
         * MakeSkillUnavailable(ActiveSkill skill)
         * - 액티브 스킬이 사용 불가능 상태일 때, 패널을 업데이트합니다.
         */
        private void MakeSkillUnavailable(ActiveSkill skill)
        {
            void ApplyToMatchingControls(Control parent)
            {
                foreach (Control control in parent.Controls)
                {
                    if (control.Tag?.ToString() == skill.Name)
                    {
                        bool onCooldown = skill.isOnCooldown;

                        if (control is TransparentTextLabel label)
                        {
                            label.ForeColor = onCooldown
                                ? Color.FromArgb(100, 245, 245, 245)
                                : Color.WhiteSmoke;
                        }
                        else if (control is PictureBox pictureBox)
                        {
                            pictureBox.Image = onCooldown
                                ? Resources.ImgCooltimeDisabled
                                : Resources.ImgCooltime;
                        }
                    }

                    if (control.HasChildren)
                        ApplyToMatchingControls(control);
                }
            }

            ApplyToMatchingControls(_activePanel);
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
                descriptionLabel.Tag = active.Name; // 스킬 이름을 태그로 저장
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
