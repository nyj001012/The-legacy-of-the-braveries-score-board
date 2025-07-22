using ScoreBoard.controls;
using ScoreBoard.data;
using ScoreBoard.data.character;
using ScoreBoard.data.monster;
using ScoreBoard.data.stat;
using ScoreBoard.data.statusEffect;
using ScoreBoard.modals;
using ScoreBoard.Properties;
using ScoreBoard.utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
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
        private int currentTurn = 1; // 현재 턴, 초기값은 1로 설정
        private readonly TransparentTextLabel cachedStatusEffectDefault = new()
        {
            Text = "양호",
            Font = new Font("Danjo-bold", 26),
            ForeColor = Color.WhiteSmoke,
            AutoSize = true,
            Margin = new Padding(0, 0, 0, 0),
            Cursor = Cursors.Hand
        };

        public ScoreBoardControl(Dictionary<string, CorpsMember> characters, List<(string id, string name, ushort count)> monsters)
        {
            _characters = characters ?? throw new ArgumentNullException(nameof(characters), "캐릭터는 비어있을 수 없습니다.");
            _monsters = monsters ?? throw new ArgumentNullException(nameof(monsters), "몬스터를 선택해야 합니다.");
            InitializeComponent();

            cachedStatusEffectDefault.Click += (s, e) => EditStatusEffect(); // 상태 이상 편집 이벤트 핸들러 등록
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
                Monster monster = id switch
                {
                    "2_01_white_soldier" => new WhiteSoldier(id, 0),// 스폰 턴은 0으로 설정
                    "2_02_black_knight" => new BlackKnight(id, 0),
                    _ => throw new ArgumentException($"알 수 없는 몬스터 ID: {id}"),
                };
                EnemyPanel enemyControl = new(monster, count)
                {
                    Name = $"pn{id}"
                };
                // 현재 턴보다 스폰 턴이 큰 몬스터는 표시하지 않음
                if (monster.SpawnTurn > currentTurn)
                {
                    enemyControl.Visible = false; // 스폰 턴이 0이 아닌 적은 숨김 처리
                }
                enemyControl.DetailRequested += (s, e) => ShowDetail(e.Item1, e.Item2); // 상세 정보 요청 이벤트 핸들러 등록
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
                panel.Tag = index; // 패널에 인덱스 저장

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
            foreach (Control control in detailViewport.Controls)
            {
                control.Visible = true;
            }
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

            ChangeTextOfAttackValueLabels(player.Stat.CombatStats);
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
            fpnStatusEffect.Controls.Clear();
            if (player.Stat.StatusEffects.Count == 0)
            {
                fpnStatusEffect.Controls.Add(cachedStatusEffectDefault);
                return;
            }

            foreach (var statusEffect in player.Stat.StatusEffects)
            {
                var (pb, label) = CreateStatusEffectControl(statusEffect);
                fpnStatusEffect.Controls.Add(pb);
                fpnStatusEffect.Controls.Add(label);
            }
        }

        /*
         * ShowHealth(CorpsMember player)
         * - 플레이어의 체력을 표시하는 메서드
         * - player: CorpsMember 객체
         */
        private void ShowHealth(CorpsMember player)
        {
            lblCurrentHealth.Text = player.Stat.Hp.ToString();
            if (player.Stat.Shield > 0)
            {
                lblCurrentHealth.Text += $"(+{player.Stat.Shield})";
            }
            lblMaxHealth.Text = $"{player.Stat.MaxHp}";
        }

        /*
         * ShowBasicInfo(CorpsMember player)
         * - 플레이어의 기본 정보를 표시하는 메서드
         * - player: CorpsMember 객체
         */
        private void ShowBasicInfo(CorpsMember player)
        {
            lblName.Text = player.Name;
            fpnDice.Controls.Clear(); // 기존 다이스 값 제거
            if (player.RequiredDiceValues.Count > 0)
            {
                foreach (var diceValue in player.RequiredDiceValues)
                {
                    var label = CreateDiceLabel(diceValue.Key, diceValue.Value);
                    fpnDice.Controls.Add(label);
                }
            }
        }

        /*
         * pbDice_Click(object sender, EventArgs e)
         * - 주사위 아이콘 클릭 이벤트 핸들러
         * - 주사위 값을 편집할 수 있는 모달을 표시
         */
        private void pbDice_Click(object sender, EventArgs e)
        {
            if (currentShowingPlayer == null)
                return;
            string diceValues = ""; // 초기화
            if (currentShowingPlayer.RequiredDiceValues.Count > 0)
            {
                diceValues = string.Join(", ", currentShowingPlayer.RequiredDiceValues
                .Select(dv => (dv.Value ? "*" : "") + dv.Key));
            }

            Point point = pbDice.PointToScreen(Point.Empty);
            point.X += pbDice.Width + pbDice.Margin.Right; // 오른쪽으로 위치 조정

            var editModal = new DetailEditModal(diceValues)
            {
                StartPosition = FormStartPosition.Manual,
                Location = point,
            };

            if (editModal.ShowDialog() == DialogResult.OK)
            {
                HandleDiceInput(editModal.InputText);
            }
        }

        /*
         * HandleDiceInput(string inputText)
         * - 주사위 값을 입력받아 처리하는 메서드
         * - inputText: 입력된 주사위 값 문자열
         */
        private void HandleDiceInput(string inputText)
        {
            Dictionary<ushort, bool> newDiceDict = ParseDiceString(inputText);

            if (newDiceDict.Count == 0)
            {
                MessageBox.Show("유효한 주사위 값을 입력하세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            currentShowingPlayer!.RequiredDiceValues = newDiceDict;
            UpdateDiceUI(newDiceDict);
        }

        /*
         * UpdateDiceUI(Dictionary<ushort, bool> diceDict)
         * - 주사위 값을 UI에 업데이트하는 메서드
         * - diceDict: 주사위 값과 크리티컬 여부를 담은 딕셔너리
         */
        private void UpdateDiceUI(Dictionary<ushort, bool> diceDict)
        {
            fpnDice.Controls.Clear();

            foreach (var diceValue in diceDict)
            {
                var label = CreateDiceLabel(diceValue.Key, diceValue.Value);
                fpnDice.Controls.Add(label);
            }
        }

        /*
         * ParseDiceString(string input)
         * - 입력 문자열을 파싱하여 주사위 값을 딕셔너리로 변환하는 메서드
         * - input: 주사위 값 문자열 (예: "1, 2, *3, 4"). *이 붙은 값은 크리티컬로 간주
         * - 반환값: 주사위 값과 크리티컬 여부를 담은 딕셔너리
         */
        private Dictionary<ushort, bool> ParseDiceString(string input)
        {
            Dictionary<ushort, bool> diceValues = [];

            // 입력 문자열을 쉼표로 분리하여 각 주사위 값을 처리
            string[] parts = input.Split([','], StringSplitOptions.RemoveEmptyEntries);

            foreach (string part in parts)
            {
                string trimmedPart = part.Trim();
                if (trimmedPart.Length == 0) continue; // 빈 문자열은 무시
                bool isCritical = trimmedPart.StartsWith('*'); // 단일 문자 '*'로 확인
                ushort value;
                if (isCritical)
                {
                    trimmedPart = trimmedPart.Substring(1).Trim(); // 크리티컬 표시 제거
                }
                // 주사위 값이 1~6의 숫자인지 확인
                if (ushort.TryParse(trimmedPart, out value) && 1 <= value && value <= 6)
                {
                    diceValues[value] = isCritical; // 주사위 값과 크리티컬 여부 저장
                }
                else
                {
                    return []; // 잘못된 입력은 빈 딕셔너리 반환
                }
            }
            return diceValues;
        }

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

        /*
         * ShowDetail(bool isReported, Monster monster)
         * - 몬스터의 상세 정보를 표시하는 메서드
         * - isReported: 몬스터의 상태가 보고되었는지 여부
         * - monster: Monster 객체
         */
        private void ShowDetail(bool isReported, Monster monster)
        {
            detailViewport.SuspendLayout();
            foreach (Control control in detailViewport.Controls)
            {
                control.Visible = false;
            }
            ShowBasicInfo(isReported, monster);
            ShowHealth(isReported, monster);
            ShowAttackValue(isReported, monster);
            ShowStatusEffect(isReported, monster);
            detailViewport.ResumeLayout();

            ScrollBarManager.SetScrollBar(detailList, detailViewport, detailScrollBar);
        }

        /*
         * ShowStatusEffect(bool isReported, Monster monster)
         * - 몬스터의 상태 이상을 표시하는 메서드
         * - isReported: 몬스터의 상태가 보고되었는지 여부
         * - monster: Monster 객체
         */
        private void ShowStatusEffect(bool isReported, Monster monster)
        {
            if (monster.Stat.StatusEffects.Count == 0 || !isReported)
            {
                fpnStatusDetail.Controls.Clear();
                fpnStatusDetail.Visible = false;
                return;
            }

            fpnStatusDetail.Visible = true;
            fpnStatusDetail.Controls.Clear();

            foreach (var statusEffect in monster.Stat.StatusEffects)
            {
                var (pb, label) = CreateStatusEffectControl(statusEffect);
                fpnStatusDetail.Controls.Add(pb);
                fpnStatusDetail.Controls.Add(label);
            }
        }

        /*
         * ShowAttackValue(bool isReported, Monster monster)
         * - 몬스터의 공격력과 공격 횟수를 표시하는 메서드
         * - isReported: 몬스터의 상태가 보고되었는지 여부
         * - monster: Monster 객체
         */
        private void ShowAttackValue(bool isReported, Monster monster)
        {
            if (!isReported || monster.Stat.CombatStats.Count == 0)
            {
                fpnAttackValue.Visible = false;
                return;
            }

            fpnAttackValue.Visible = true;

            ChangeTextOfAttackValueLabels(monster.Stat.CombatStats);
        }

        /*
         * ShowHealth(bool isReported, Monster monster)
         * - 몬스터의 체력을 표시하는 메서드
         * - isReported: 몬스터의 상태가 보고되었는지 여부
         * - monster: Monster 객체
         */
        private void ShowHealth(bool isReported, Monster monster)
        {
            lblCurrentHealth.Text = monster.Stat.Hp.ToString();
            if (isReported)
            {
                pnHealth.Visible = true;
                if (monster.Stat.Shield > 0)
                    lblCurrentHealth.Text += $"(+{monster.Stat.Shield})";
                lblCurrentHealth.Text += $"/{monster.Stat.MaxHp}";
            }
        }

        /*
         * ShowBasicInfo(bool isReported, Monster monster)
         * - 몬스터의 기본 정보를 표시하는 메서드
         * - isReported: 몬스터의 상태가 보고되었는지 여부
         * - monster: Monster 객체
         */
        private void ShowBasicInfo(bool isReported, Monster monster)
        {
            fpnBasicStatus.Visible = true;
            lblName.Text = monster.Name;
            fpnDice.Controls.Clear(); // 기존 다이스 값 제거
            if (isReported && monster.RequiredDiceValues.Length > 0)
            {
                foreach (var diceValue in monster.RequiredDiceValues)
                {
                    var label = CreateDiceLabel(diceValue, monster.RequiredDiceValues.Last() == diceValue);
                    fpnDice.Controls.Add(label);
                }
            }
        }

        /*
         * CreateDiceLabel(ushort value, bool isCritical)
         * - 주사위 값을 표시하는 레이블을 생성하는 메서드
         * - value: 주사위 값
         * - isCritical: 크리티컬 여부
         */
        private TransparentTextLabel CreateDiceLabel(ushort value, bool isCritical)
        {
            TransparentTextLabel label = new()
            {
                Text = value.ToString(),
                Font = new Font("Danjo-bold", 26),
                ForeColor = isCritical ? Color.FromArgb(255, 217, 0) : Color.WhiteSmoke,
                Margin = new Padding(0, 0, MarginInPanel / 2, 0),
                AutoSize = true,
                TextAlign = ContentAlignment.BottomCenter
            };
            return label;
        }

        /*
         * CreateStatusEffectControl(StatusEffect statusEffect)
         * - 상태 이상을 표시하는 컨트롤을 생성하는 메서드
         * - statusEffect: 상태 이상 객체
         */
        private (PictureBox, TransparentTextLabel) CreateStatusEffectControl(StatusEffect statusEffect)
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

            return (pb, label);
        }

        /*
         * ChangeTextOfAttackValueLabels(Dictionary<string, CombatStat> combatStats)
         * - 플레이어의 공격력 레이블을 변경하는 메서드
         * - combatStats: 공격력 정보를 담고 있는 딕셔너리
         */
        private void ChangeTextOfAttackValueLabels(Dictionary<string, CombatStat> combatStats)
        {
            pbMeleeAttack.Visible = pbRangedAttack.Visible = false;
            lblMeleeAttack.Visible = lblRangedAttack.Visible = false;
            lblMeleeAttackCount.Visible = lblRangedAttackCount.Visible = false;
            foreach (var (type, combatStat) in combatStats)
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
         * SimpleStatLabel_Click(object sender, EventArgs e)
         * - 플레이어의 간단한 스탯 레이블 클릭 이벤트 핸들러
         * - 레이블을 클릭하면 해당 스탯 값을 편집할 수 있는 모달을 표시
         */
        private void SimpleStatLabel_Click(object sender, EventArgs e)
        {
            if (sender is not TransparentTextLabel label || currentShowingPlayer == null)
                return;
            DetailEditModal modal = new(label.Text)
            {
                StartPosition = FormStartPosition.Manual,
                Location = label.PointToScreen(Point.Empty),
            };

            if (modal.ShowDialog(this) == DialogResult.OK)
            {
                if (ushort.TryParse(modal.InputText, out ushort value) && value >= 0)
                {
                    var labelSetters = new Dictionary<string, Action>
                    {
                        ["lblMaxHealth"] = () =>
                        {
                            currentShowingPlayer.Stat.MaxHp = value;
                            lblMaxHealth.Text = value.ToString();
                            UpdateHealthBar(); // 체력 바 업데이트
                        },
                        ["lblMovement"] = () =>
                        {
                            currentShowingPlayer.Stat.Movement = value;
                            lblMovement.Text = value.ToString();
                        },
                        ["lblMeleeRange"] = () =>
                        {
                            currentShowingPlayer.Stat.CombatStats["melee"].Range = value;
                            lblMeleeRange.Text = value.ToString();
                        },
                        ["lblRangedRange"] = () =>
                        {
                            currentShowingPlayer.Stat.CombatStats["ranged"].Range = value;
                            lblRangedRange.Text = value.ToString();
                        },
                        ["lblMeleeAttack"] = () =>
                        {
                            currentShowingPlayer.Stat.CombatStats["melee"].Value = value;
                            lblMeleeAttack.Text = value.ToString();
                        },
                        ["lblRangedAttack"] = () =>
                        {
                            currentShowingPlayer.Stat.CombatStats["ranged"].Value = value;
                            lblRangedAttack.Text = value.ToString();
                        },
                        ["lblMeleeAttackCount"] = () =>
                        {
                            currentShowingPlayer.Stat.CombatStats["melee"].AttackCount = value;
                            lblMeleeAttackCount.Text = $"{{{value}}}";
                        },
                        ["lblRangedAttackCount"] = () =>
                        {
                            currentShowingPlayer.Stat.CombatStats["ranged"].AttackCount = value;
                            lblRangedAttackCount.Text = $"{{{value}}}";
                        },
                        ["lblSpellPower"] = () =>
                        {
                            currentShowingPlayer.Stat.SpellPower = value;
                            lblSpellPower.Text = value.ToString();
                        },
                        ["lblWisdom"] = () =>
                        {
                            currentShowingPlayer.Stat.Wisdom = value;
                            lblWisdom.Text = value.ToString();
                        },
                    };

                    if (labelSetters.TryGetValue(label.Name, out var setter))
                    {
                        setter();
                    }
                    else
                    {
                        MessageBox.Show("알 수 없는 항목입니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("유효한 값을 입력하세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /*
         * lblHealth_Click(object sender, EventArgs e)
         * - 플레이어의 체력 레이블 클릭 이벤트 핸들러
         * - 레이블을 클릭하면 체력을 편집할 수 있는 모달을 표시
         */
        private void lblCurrentHealth_Click(object sender, EventArgs e)
        {
            DetailEditModal modal = new(lblCurrentHealth.Text)
            {
                StartPosition = FormStartPosition.Manual,
                Location = lblCurrentHealth.PointToScreen(Point.Empty)
            };

            if (modal.ShowDialog(this) == DialogResult.OK)
            {
                var (hp, shield) = ParseHealthString(modal.InputText);
                if (hp == null)
                {
                    MessageBox.Show("유효한 체력 값을 입력하세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                currentShowingPlayer!.Stat.Hp = (ushort)hp;
                currentShowingPlayer.Stat.Shield = shield ?? 0;
                lblCurrentHealth.Text = $"{hp}{(shield.HasValue ? $"(+{shield})" : "")}";
                UpdateHealthBar();
            }
        }

        /*
         * UpdateHealthBar()
         * - 현재 디테일 뷰포트에 표시된 플레이어의 체력 바를 업데이트하는 메서드
         */
        private void UpdateHealthBar()
        {
            // 현재 디테일 뷰포트에 표시된 플레이어의 체력 바를 업데이트
            // pn1P~4P까지 Name이 pn{currentShowingPlayer.Id}인 패널을 찾아서 HealthBar를 업데이트
            // 현재 플레이어가 1P~4P 중 어느 것인지 확인
            var healthBar = this.Controls.Find($"hb{currentShowingPlayer!.Id}", true).FirstOrDefault() as HealthBar;
            playerList.SuspendLayout();
            // HealthBar 컨트롤을 찾아서 업데이트
            healthBar?.SetValues(currentShowingPlayer.Stat.Hp,
                                currentShowingPlayer.Stat.Shield,
                                currentShowingPlayer.Stat.MaxHp);
            playerList.ResumeLayout();
        }

        /*
         * ParseHealthString(string input)
         * - 체력 문자열을 파싱하여 플레이어의 체력을 업데이트하는 메서드
         * - input: 체력 문자열 (예: "100(+20)")
         */
        private static (ushort?, ushort?) ParseHealthString(string input)
        {
            ushort? hp;
            ushort? shield = null;

            // 정규 표현식을 사용하여 체력과 보호막을 추출
            var match = System.Text.RegularExpressions.Regex.Match(input, @"^(\d+)(?:\s*\(\+(\d+)\))?$");

            if (match.Success)
            {
                hp = ushort.Parse(match.Groups[1].Value);
                if (match.Groups[2].Success)
                {
                    shield = ushort.Parse(match.Groups[2].Value);
                }
            }
            else
            {
                return (null, null);
            }
            return (hp, shield);
        }

        private void fpnStatusEffect_Click(object sender, EventArgs e)
        {
            EditStatusEffect();
        }

        private void EditStatusEffect()
        {
            MessageBox.Show("상태 이상 편집 기능은 아직 구현되지 않았습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}