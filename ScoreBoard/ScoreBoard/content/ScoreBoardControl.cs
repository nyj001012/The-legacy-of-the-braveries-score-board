using ScoreBoard.controls;
using ScoreBoard.data;
using ScoreBoard.data.character;
using ScoreBoard.data.stat;
using ScoreBoard.Properties;
using ScoreBoard.utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScoreBoard.content
{
    public partial class ScoreBoardControl : UserControl
    {
        private SkillDescriptionPanel? skillDescriptionPanel = null; // 스킬 설명 패널
        private readonly Dictionary<string, CorpsMember> _characters;
        private readonly List<(string id, string name, ushort count)> _monsters;
        private const int MarginInPanel = 10; // 오른쪽 여백 설정
        private CorpsMember? currentShowingPlayer = null; // 현재 표시 중인 플레이어
        private const int ICON_SIZE = 45; // 아이콘 크기 설정

        public ScoreBoardControl(Dictionary<string, CorpsMember> characters, List<(string id, string name, ushort count)> monsters)
        {
            _characters = characters ?? throw new ArgumentNullException(nameof(characters), "캐릭터는 비어있을 수 없습니다.");
            _monsters = monsters ?? throw new ArgumentNullException(nameof(monsters), "몬스터를 선택해야 합니다.");
            InitializeComponent();
        }

        private void ScoreBoardControl_Load(object sender, EventArgs e)
        {
            InitPlayerList();
            InitEnemyList();
            ShowDetail(_characters.ElementAt(0).Value);
        }

        /*
         * enemyList 컨트롤에 적 정보를 초기화
         */
        private void InitEnemyList()
        {
            enemyList.SuspendLayout();
            enemyList.Controls.Clear();

            foreach (var (id, name, count) in _monsters)
            {
                EnemyPanel enemyControl = new EnemyPanel(id, name, count)
                {
                    Name = $"pn{id}"
                };
                enemyList.Controls.Add(enemyControl);
            }

            enemyList.ResumeLayout();
            ScrollBarManager.SetScrollBar(enemyContainer, enemyList, enemyScrollBar);
        }

        /*
         * playerList 컨트롤에 캐릭터 정보를 초기화
         */
        private void InitPlayerList()
        {
            playerList.SuspendLayout();
            playerList.Controls.Clear();

            int index = 1;
            foreach (var character in _characters.Values)
            {
                UserControl panel = (index == 1)
                    ? new CurrentPlayerPanel(character, index)
                    : new PlayerPanel(character, index);

                panel.Name = $"pn{character.Id}";
                panel.Click += (s, e) => ShowDetail(character);

                playerList.Controls.Add(panel);
                index++;
            }

            playerList.ResumeLayout();
            ScrollBarManager.SetScrollBar(playerContainer, playerList, playerScrollBar);
        }

        /*
         * ShowDetail(CorpsMember player)
         * - 플레이어 상세 정보를 표시하는 메서드
         */
        private void ShowDetail(CorpsMember player)
        {
            currentShowingPlayer = player;
            detailViewport.SuspendLayout();

            ShowBasicInfo(player);
            ShowHealth(player);
            ShowStatusEffect(player);
            ShowMovement(player);
            ShowAttackRange(player);
            ShowAttackValue(player);
            ShowSpellPower(player);
            ShowWisdom(player);
            ShowArtifact(player);

            if (skillDescriptionPanel != null)
            {
                skillDescriptionPanel.Dispose();
                skillDescriptionPanel = null;
                DisplaySkillDescription();
            }
            else
            {
                ScrollBarManager.SetScrollBar(detailList, detailViewport, detailScrollBar);
            }
            detailViewport.ResumeLayout();
        }

        /*
         * ShowArtifact(CorpsMember player)
         * - 플레이어의 유물을 표시하는 메서드
         * - player: CorpsMember 객체
         */
        private void ShowArtifact(CorpsMember player)
        {
            PictureBox[] slotPics = { pbWeapon, pbArmour, pbAccessory1, pbAccessory2 };
            string[] emptySlotResourceNames = { "EmptyWeaponSlot", "EmptyArmourSlot", "EmptyAccessorySlot", "EmptyAccessorySlot" };

            for (int i = 0; i < slotPics.Length; i++)
            {
                bool hasArtifact = player.ArtifactSlot.Count > i && player.ArtifactSlot[i] != null;

                slotPics[i].Visible = !(i == 3 && player.ArtifactSlot.Count <= i);
                slotPics[i].BackgroundImage = hasArtifact
                    ? DataReader.GetArtifactImage(player.ArtifactSlot[i].Id)
                    : (Image)Resources.ResourceManager.GetObject(emptySlotResourceNames[i])!;
            }
        }

        /*
         * ShowWisdom(CorpsMember player)
         * - 플레이어의 지혜를 표시하는 메서드
         * - player: CorpsMember 객체
         */
        private void ShowWisdom(CorpsMember player)
        {
            bool hasWisdom = player.Stat.Wisdom != null;
            fpnWisdom.Visible = hasWisdom;
            if (hasWisdom) lblWisdom.Text = player.Stat.Wisdom!.Value.ToString();
        }

        /*
         * ShowSpellPower(CorpsMember player)
         * - 플레이어의 주문력을 표시하는 메서드
         * - player: CorpsMember 객체
         */
        private void ShowSpellPower(CorpsMember player)
        {
            bool hasSpellPower = player.Stat.SpellPower != null;
            fpnSpellPower.Visible = hasSpellPower;
            if (hasSpellPower) lblSpellPower.Text = player.Stat.SpellPower!.Value.ToString();
        }

        /*
         * ShowAttackValue(CorpsMember player)
         * - 플레이어의 공격력, 공격 가능 횟수(속도)을 표시하는 메서드
         * - player: CorpsMember 객체
         */
        private void ShowAttackValue(CorpsMember player)
        {
            if (player.Stat.CombatStats.Count == 0)
            {
                fpnAttackValue.Visible = false;
                return;
            }

            pbMeleeAttack.Visible = pbRangedAttack.Visible = false;
            lblMeleeAttack.Visible = lblRangedAttack.Visible = false;
            lblMeleeAttackCount.Visible = lblRangedAttackCount.Visible = false;

            foreach (var (type, combatStat) in player.Stat.CombatStats)
            {
                if (type == "melee")
                {
                    pbMeleeAttack.Visible = lblMeleeAttack.Visible = lblMeleeAttackCount.Visible = true;
                    lblMeleeAttack.Text = combatStat.Value.ToString();
                    lblMeleeAttackCount.Text = $"{{{combatStat.AttackCount}}}";
                }
                else
                {
                    pbRangedAttack.Visible = lblRangedAttack.Visible = lblRangedAttackCount.Visible = true;
                    lblRangedAttack.Text = combatStat.Value.ToString();
                    lblRangedAttackCount.Text = $"{{{combatStat.AttackCount}}}";
                }
            }
        }

        /*
         * ShowAttackRange(CorpsMember player)
         * - 플레이어의 공격 사거리를 표시하는 메서드
         * - player: CorpsMember 객체
         */
        private void ShowAttackRange(CorpsMember player)
        {
            pbMelee.Visible = pbRanged.Visible = false;
            lblMeleeRange.Visible = lblRangedRange.Visible = false;

            foreach (var (type, combatStat) in player.Stat.CombatStats)
            {
                if (type == "melee")
                {
                    pbMelee.Visible = lblMeleeRange.Visible = true;
                    lblMeleeRange.Text = combatStat.Range.ToString();
                }
                else
                {
                    pbRanged.Visible = lblRangedRange.Visible = true;
                    lblRangedRange.Text = combatStat.Range.ToString();
                }
            }
        }

        /*
         * ShowMovement(CorpsMember player)
         * - 플레이어의 이동 속도를 표시하는 메서드
         * - player: CorpsMember 객체
         */
        private void ShowMovement(CorpsMember player)
        {
            lblMovement.Text = player.Stat.Movement.ToString();
        }

        /*
         * ShowStatusEffect(CorpsMember player)
         * - 플레이어의 상태 이상을 표시하는 메서드
         * - player: CorpsMember 객체
         */
        private void ShowStatusEffect(CorpsMember player)
        {
            if (player.Stat.StatusEffects.Count == 0)
            {
                fpnStatusDetail.Controls.Clear();
                fpnStatusDetail.Visible = false;
                return;
            }

            fpnStatusDetail.Visible = true;
            fpnStatusDetail.Controls.Clear();

            foreach (var statusEffect in player.Stat.StatusEffects)
            {
                string duration = statusEffect.IsInfinite ? "∞" : statusEffect.Duration.ToString();
                PictureBox pb = new()
                {
                    BackgroundImage = DataReader.GetStatusEffectImage(statusEffect.Type),
                    Size = new Size(ICON_SIZE, ICON_SIZE),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                };
                TransparentTextLabel label = new()
                {
                    Text = duration,
                    Font = new Font("Danjo-bold", 26),
                    ForeColor = Color.WhiteSmoke,
                    AutoSize = true,
                    Margin = new Padding(0, 0, MarginInPanel, 0),
                };
                fpnStatusDetail.Controls.Add(pb);
                fpnStatusDetail.Controls.Add(label);
            }
        }

        /*
         * ShowHealth(CorpsMember player)
         * - 플레이어의 체력을 표시하는 메서드
         * - player: CorpsMember 객체
         */
        private void ShowHealth(CorpsMember player)
        {
            lblHealth.Text = player.Stat.Hp.ToString();
            if (player.Stat.Shield > 0)
                lblHealth.Text += $"(+{player.Stat.Shield})";
            lblHealth.Text += $"/{player.Stat.MaxHp}";
        }

        /*
         * ShowBasicInfo(CorpsMember player)
         * - 플레이어의 기본 정보를 표시하는 메서드
         * - player: CorpsMember 객체
         */
        private void ShowBasicInfo(CorpsMember player)
        {
            lblName.Text = player.Name;
            if (player.RequiredDiceValues.Count > 0)
            {
                foreach (var diceValue in player.RequiredDiceValues)
                {
                    TransparentTextLabel label = new TransparentTextLabel
                    {
                        Text = diceValue.Key.ToString(),
                        ForeColor = diceValue.Value ? Color.FromArgb(255, 217, 0) : Color.WhiteSmoke,
                        AutoSize = true,
                        Margin = new Padding(0, 0, MarginInPanel, 0),
                    };
                    fpnBasicStatus.Controls.Add(label);
                }
            }
        }

        private void pbDice_Click(object sender, EventArgs e) { }

        private void pbSkill_Click(object sender, EventArgs e)
        {
            if (skillDescriptionPanel == null)
            {
                DisplaySkillDescription();
            }
            else
            {
                detailViewport.Controls.Remove(skillDescriptionPanel);
                skillDescriptionPanel.Dispose();
                skillDescriptionPanel = null;
                // 스크롤 위치 초기화
                detailViewport.Top = 0;
                detailViewport.Height = detailViewport.Controls.Cast<Control>().Max(c => c.Bottom) + detailViewport.Padding.Bottom;
                detailViewport.PerformLayout();
                detailViewport.Height = detailViewport.Controls.Cast<Control>().Max(c => c.Bottom) + detailViewport.Padding.Bottom;
                ScrollBarManager.SetScrollBar(detailList, detailViewport, detailScrollBar);
            }
        }

        /*
         * DisplaySkillDescription()
         * - 스킬 설명 패널을 표시하는 메서드
         */
        private void DisplaySkillDescription()
        {
            skillDescriptionPanel = new SkillDescriptionPanel(currentShowingPlayer!.Passives, currentShowingPlayer.Actives);
            int insertIndex = detailViewport.Controls.GetChildIndex(customFlowLayoutPanel3) + 1;
            detailViewport.Controls.Add(skillDescriptionPanel);
            detailViewport.Controls.SetChildIndex(skillDescriptionPanel, insertIndex);

            skillDescriptionPanel.PerformLayout();
            skillDescriptionPanel.Height = skillDescriptionPanel.Controls.Cast<Control>().Max(c => c.Bottom) + 10;
            detailViewport.Height = detailViewport.Controls.Cast<Control>().Max(c => c.Bottom) + detailViewport.Padding.Bottom;
            ScrollBarManager.SetScrollBar(detailList, detailViewport, detailScrollBar);
        }

        private void detailList_MouseWheel(object sender, MouseEventArgs e)
        {
            if (!detailScrollBar.Enabled) return;

            int delta = -e.Delta / SystemInformation.MouseWheelScrollDelta * detailScrollBar.SmallStep;
            int newScrollValue = Math.Clamp(detailScrollBar.Value + delta, detailScrollBar.Minimum, detailScrollBar.Maximum);
            detailScrollBar.Value = newScrollValue;
            detailViewport.Top = detailList.Padding.Top - newScrollValue;
        }

        private void detailList_MouseEnter(object sender, EventArgs e)
        {
            detailViewport.Focus();
        }
    }
}