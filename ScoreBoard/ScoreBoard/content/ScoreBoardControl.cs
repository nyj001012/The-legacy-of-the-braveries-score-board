using ScoreBoard.controls;
using ScoreBoard.data;
using ScoreBoard.data.artifact;
using ScoreBoard.data.character;
using ScoreBoard.data.monster;
using ScoreBoard.data.skill;
using ScoreBoard.data.stat;
using ScoreBoard.data.statusEffect;
using ScoreBoard.data.weather;
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
        private readonly List<Monster> _monsters;
        private const int MarginInPanel = 10; // 오른쪽 여백 설정
        private enum SHOWING_DATA_TYPE { Player = 0, Monster = 1 };
        private SHOWING_DATA_TYPE _showingDataType = default;
        private CorpsMember currentShowingPlayer; // 현재 표시 중인 플레이어
        private Monster currentShowingMonster; // 현재 표시 중인 몬스터와 보고 여부
        private const int ICON_SIZE = 45; // 아이콘 크기 설정
        private int currentTurn = 1; // 현재 턴, 초기값은 1로 설정
        private Weather _currentWeather = new();
        private readonly TransparentTextLabel cachedStatusEffectDefault = new()
        {
            Text = "양호",
            Font = new Font("Danjo-bold", 26),
            ForeColor = Color.WhiteSmoke,
            AutoSize = true,
            Margin = new Padding(0, 0, 0, 0),
            Cursor = Cursors.Hand
        };
        public class ArtifactSlotInfo
        {
            public string ArtifactId { get; set; } = "";
            public int SlotIndex { get; set; }
        }

        public ScoreBoardControl(Dictionary<string, CorpsMember> characters, List<Monster> monsters)
        {
            _characters = characters ?? throw new ArgumentNullException(nameof(characters), "캐릭터는 비어있을 수 없습니다.");
            _monsters = monsters ?? throw new ArgumentNullException(nameof(monsters), "몬스터를 선택해야 합니다.");
            InitializeComponent();
            currentShowingMonster = _monsters.First(); // 첫 번째 몬스터를 현재 표시 중인 몬스터로 설정
            currentShowingPlayer = _characters.ElementAtOrDefault(0).Value; // 첫 번째 캐릭터를 현재 표시 중인 플레이어로 설정

            cachedStatusEffectDefault.Click += (s, e) => EditStatusEffect(); // 상태 이상 편집 이벤트 핸들러 등록
        }

        private void ScoreBoardControl_Load(object sender, EventArgs e)
        {
            InitWeather();
            ActivateDefaultPassive();
            InitPlayerList();
            InitEnemyList();
            ShowDetail(_characters.ElementAt(0).Value);
        }

        /*
         * InitWeather()
         * - 날씨를 초기화하는 메서드
         * - 현재 날씨(_currentWeather)를 기준으로 pbWeather와 lblWeather를 초기화
         */
        private void InitWeather()
        {
            Image? weatherImage = DataReader.GetWeatherImage(_currentWeather.Type);
            if (weatherImage == null)
            {
                MessageBox.Show($"{EnumHelper.GetEnumName(_currentWeather.Type)} 이미지가 존재하지 않습니다.", "오류", MessageBoxButtons.OK);
            }
            else
            {
                pbWeather.BackgroundImage = weatherImage;
            }
            lblWeather.Text = _currentWeather.IsInfinite ? "∞" : _currentWeather.Duration.ToString();
        }

        /*
         * ActivateDefaultPassive()
         * - 각 캐릭터별 기본 패시브를 활성화합니다.
         */
        private void ActivateDefaultPassive()
        {
            foreach (var dict in _characters)
            {
                List<PassiveSkill> passiveList = dict.Value.Passives;
                for (int i = 0; i < passiveList.Count; i++)
                {
                    if (passiveList[i].RequiredLevel == 0)
                    {
                        passiveList[i].Activate?.Invoke(); // Null 가능성을 안전하게 처리
                    }
                }
            }
        }

        /*
         * enemyList 컨트롤에 적 정보를 초기화
         */
        private void InitEnemyList()
        {
            enemyList.SuspendLayout();
            enemyList.Controls.Clear();

            foreach (var m in _monsters)
            {
                EnemyPanel enemyControl = new(m)
                {
                    Name = $"pn{m.Id}"
                };
                // 현재 턴보다 스폰 턴이 큰 몬스터는 표시하지 않음
                if (m.SpawnTurn > currentTurn)
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
                UserControl panel = (index == currentTurn % 4)
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
            _showingDataType = SHOWING_DATA_TYPE.Player;

            PrepareViewport();
            ReplaceSkillDescriptionPanel();
            DisplayPlayerStats(player);
            FinalizeViewportLayout();
        }

        /*
         * PrepareViewport()
         * - 상세 정보 뷰포트 초기화
         */
        private void PrepareViewport()
        {
            detailViewport.SuspendLayout();

            foreach (Control control in detailViewport.Controls)
            {
                control.Visible = true;
            }
        }

        /*
         * ReplaceSkillDescriptionPanel()
         * - 기존 스킬 설명 패널을 제거하고 새로 표시
         */
        private void ReplaceSkillDescriptionPanel()
        {
            if (skillDescriptionPanel != null)
            {
                detailViewport.Controls.Remove(skillDescriptionPanel);
                skillDescriptionPanel.Dispose();
                skillDescriptionPanel = null;
                DisplaySkillDescription();
            }
        }

        /*
         * DisplayPlayerStats(CorpsMember player)
         * - 플레이어의 상세 정보를 표시하는 메서드
         */
        private void DisplayPlayerStats(CorpsMember player)
        {
            ShowBasicInfo(player);
            ShowHealth(player);
            ShowStatusEffect(player);
            ShowMovement(player);
            ShowAttackRange(player);
            ShowAttackValue(player);
            ShowSpellPower(player);
            ShowWisdom(player);
            ShowArtifact(player);
            ShowNote(player);
        }

        /*
         * FinalizeViewportLayout()
         * - 상세 정보 뷰포트 레이아웃을 최종적으로 설정
         */
        private void FinalizeViewportLayout()
        {
            detailViewport.ResumeLayout();
            detailViewport.PerformLayout();

            detailViewport.Height = detailViewport.Controls
                .Cast<Control>()
                .Select(c => c.Bounds.Bottom)
                .DefaultIfEmpty(0)
                .Max() + detailViewport.Padding.Bottom;

            ScrollBarManager.SetScrollBar(detailList, detailViewport, detailScrollBar);
        }

        /*
         * ShowArtifact(CorpsMember player)
         * - 플레이어의 유물을 표시하는 메서드
         * - player: CorpsMember 객체
         */
        private void ShowArtifact(CorpsMember player)
        {
            InitializeArtifactSlots(player);

            for (int i = 0; i < player.MaxArtifactSlot; i++)
            {
                var artifact = player.ArtifactSlot.ElementAtOrDefault(i);
                if (artifact != default)
                {
                    AssignArtifactToSlot(artifact, i);
                }
            }
        }

        /*
         * InitializeArtifactSlots(CorpsMember player)
         * - 유물 슬롯을 초기화하는 메서드
         * - player: 유물을 가질 수 있는 플레이어
         */
        private void InitializeArtifactSlots(CorpsMember player)
        {
            PictureBox[] slotPics = { pbWeapon, pbArmour, pbAccessory1, pbAccessory2 };
            string[] resourceNames = { "EmptyWeaponSlot", "EmptyArmourSlot", "EmptyAccessorySlot", "EmptyAccessorySlot" };

            for (int i = 0; i < slotPics.Length; i++)
            {
                slotPics[i].Visible = (i < player.MaxArtifactSlot); // 4번째 슬롯은 조건부
                slotPics[i].BackgroundImage = (Image)Resources.ResourceManager.GetObject(resourceNames[i])!;
                if (player.ArtifactSlot.ElementAtOrDefault(i) == default)
                    slotPics[i].Tag = new ArtifactSlotInfo { ArtifactId = "", SlotIndex = i };
            }
        }

        /*
         * AssignArtifactToSlot(Artifact artifact, int index)
         * - 유물을 해당 슬롯에 할당하는 메서드
         * - artifact: 할당할 Artifact 객체
         * - index: 유물 슬롯 인덱스
         */
        private void AssignArtifactToSlot(Artifact artifact, int index)
        {
            Image? image = DataReader.GetArtifactImage(artifact.Id);
            if (image == null)
            {
                MessageBox.Show($"유물 이미지가 없습니다: {artifact.Id}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            switch (index)
            {
                case 0:
                    SetSlotImage(pbWeapon, artifact.Id, image);
                    break;
                case 1:
                    SetSlotImage(pbArmour, artifact.Id, image);
                    break;
                case 2:
                    SetSlotImage(pbAccessory1, artifact.Id, image);
                    break;
                case 3:
                    SetSlotImage(pbAccessory2, artifact.Id, image);
                    break;
            }
        }

        /*
         * SetSlotImage(PictureBox pictureBox, string id, Image image)
         * - 슬롯 이미지와 태그를 설정하는 메서드
         * - pictureBox: 설정할 PictureBox 컨트롤
         * - id: 유물 ID
         * - image: 설정할 이미지
         */
        private void SetSlotImage(PictureBox pictureBox, string id, Image image)
        {
            ArtifactSlotInfo info = (ArtifactSlotInfo)pictureBox.Tag!;
            pictureBox.Tag = new ArtifactSlotInfo
            {
                ArtifactId = id,
                SlotIndex = info.SlotIndex,
            };
            pictureBox.BackgroundImage = image;
        }

        /*
         * ShowNote(CoprsMember player)
         * - 플레이어의 특이사항을 보여주는 메서드
         * - player: 특이사항을 보여줄 플레이어
         */
        private void ShowNote(CorpsMember player)
        {
            rtbNote.Text = player.Note;
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

            ChangeTextOfAttackValueLabels(player.Stat.CombatStats, player.SEAttackValueModifier);
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
                    ushort range = (ushort)Math.Max(0, combatStat.Range + player.WeatherRangeModifier);
                    lblMeleeRange.Text = range.ToString();
                }
                else
                {
                    pbRanged.Visible = lblRangedRange.Visible = true;
                    ushort range = (ushort)Math.Max(0, combatStat.Range + player.WeatherRangeModifier);
                    lblRangedRange.Text = range.ToString();
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
            ushort movement = (ushort)Math.Max(0, player.Stat.Movement + player.WeatherMovementModifier);
            lblMovement.Text = movement.ToString();
        }

        /*
         * ShowStatusEffect(CorpsMember player)
         * - 플레이어의 상태 이상을 표시하는 메서드
         * - player: CorpsMember 객체
         */
        private void ShowStatusEffect(CorpsMember player)
        {
            fpnStatusDetail.Visible = true;
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
            pbLevel.Visible = true;

            pbLevel.BackgroundImage = player.Level switch
            {
                0 => Properties.Resources.BtnPlus,
                1 => Properties.Resources.Lv1,
                2 => Properties.Resources.Lv2,
                3 => Properties.Resources.Lv3,
                _ => throw new NotImplementedException($"{player.Level}은(는) 유효한 레벨이 아닙니다."),
            };

            fpnDice.Controls.Clear(); // 기존 다이스 값 제거
            if (player.RequiredDiceValues.Count > 0)
            {
                int i = 0;
                foreach (var diceValue in player.RequiredDiceValues)
                {
                    if (i++ < player.WeatherDiceModifier)
                        continue;
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
            if (_showingDataType == SHOWING_DATA_TYPE.Player)
            {
                currentShowingPlayer!.RequiredDiceValues = newDiceDict;
            }
            else
            {
                currentShowingMonster!.RequiredDiceValues = newDiceDict;
            }
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
                detailViewport.SuspendLayout();
                detailViewport.PerformLayout();

                detailViewport.Height = detailViewport.Controls
                    .Cast<Control>()
                    .Select(c => c.Bounds.Bottom)
                    .DefaultIfEmpty(0)
                    .Max() + detailViewport.Padding.Bottom;

                detailViewport.ResumeLayout();

                ScrollBarManager.SetScrollBar(detailList, detailViewport, detailScrollBar);
            }
        }

        /*
         * DisplaySkillDescription()
         * - 스킬 설명 패널을 표시하는 메서드
         */
        private void DisplaySkillDescription()
        {
            skillDescriptionPanel = new SkillDescriptionPanel(currentShowingPlayer!.Level, currentShowingPlayer!.Passives, currentShowingPlayer.Actives);
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
         * - isReported: 몬스터의 상태가 보고되었는지 여부. 한 번 true면 계속 true
         * - monster: Monster 객체
         */
        private void ShowDetail(bool isReported, Monster monster)
        {
            if (isReported)
                monster.IsReported = isReported;
            currentShowingMonster = monster;
            _showingDataType = SHOWING_DATA_TYPE.Monster;

            detailViewport.SuspendLayout();
            pbLevel.Visible = false;
            foreach (Control control in detailViewport.Controls)
            {
                control.Visible = false;
            }
            pnNote.Visible = true;
            ShowBasicInfo(isReported, monster);
            ShowHealth(isReported, monster);
            ShowAttackValue(isReported, monster);
            ShowStatusEffect(monster);
            ShowNote(monster);
            detailViewport.ResumeLayout();

            ScrollBarManager.SetScrollBar(detailList, detailViewport, detailScrollBar);
        }

        /*
         * ShowStatusEffect(Monster monster)
         * - 몬스터의 상태 이상을 표시하는 메서드
         * - monster: Monster 객체
         */
        private void ShowStatusEffect(Monster monster)
        {
            if (monster.IsReported)
            {
                fpnStatusDetail.Visible = true;
                fpnStatusEffect.Controls.Clear();
                if (monster.Stat.StatusEffects.Count == 0)
                {
                    fpnStatusEffect.Controls.Add(cachedStatusEffectDefault);
                    return;
                }

                foreach (var statusEffect in monster.Stat.StatusEffects)
                {
                    var (pb, label) = CreateStatusEffectControl(statusEffect);
                    fpnStatusEffect.Controls.Add(pb);
                    fpnStatusEffect.Controls.Add(label);
                }
            }
            else
            {
                fpnStatusDetail.Visible = false;
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

            ChangeTextOfAttackValueLabels(monster.Stat.CombatStats, monster.SEAttackValueModifier);
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
                lblCurrentHealth.Text = monster.Stat.Hp.ToString();
                if (monster.Stat.Shield > 0)
                {
                    lblCurrentHealth.Text += $"(+{monster.Stat.Shield})";
                }
            }
            lblMaxHealth.Text = $"{monster.Stat.MaxHp}";
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
            if (isReported && monster.RequiredDiceValues.Count > 0)
            {
                int i = 0;
                foreach (var diceValue in monster.RequiredDiceValues)
                {
                    if (i++ < monster.RequiredDiceValues.Count)
                        continue;
                    var label = CreateDiceLabel(diceValue.Key, diceValue.Value);
                    fpnDice.Controls.Add(label);
                }
            }
        }

        /*
         * ShowNote(Monster monster)
         * - 몬스터의 특이사항을 표시하는 메서드
         * - monster: 특이사항을 표시할 몬스터
         */
        private void ShowNote(Monster monster)
        {
            rtbNote.Text = monster.Note;
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
                Image = DataReader.GetStatusEffectImage(statusEffect.Type),
                Size = new Size(ICON_SIZE, ICON_SIZE),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Margin = new Padding(0, 0, MarginInPanel / 2, 0)
            };
            pb.Click += (s, e) => EditStatusEffect();

            TransparentTextLabel label = new()
            {
                Text = duration,
                Font = new Font("Danjo-bold", 26),
                ForeColor = Color.WhiteSmoke,
                AutoSize = true,
                Margin = new Padding(0, 0, MarginInPanel / 2, 0),
            };
            label.Click += (s, e) => EditStatusEffect();

            return (pb, label);
        }

        private void Pb_Click(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        /*
         * ChangeTextOfAttackValueLabels(Dictionary<string, CombatStat> combatStats, double c)
         * - 플레이어의 공격력 레이블을 변경하는 메서드
         * - combatStats: 공격력 정보를 담고 있는 딕셔너리
         * - c: 공격력 보정치
         */
        private void ChangeTextOfAttackValueLabels(Dictionary<string, CombatStat> combatStats, double c)
        {
            pbMeleeAttack.Visible = pbRangedAttack.Visible = false;
            lblMeleeAttack.Visible = lblRangedAttack.Visible = false;
            lblMeleeAttackCount.Visible = lblRangedAttackCount.Visible = false;
            foreach (var (type, combatStat) in combatStats)
            {
                if (type == "melee")
                {
                    pbMeleeAttack.Visible = lblMeleeAttack.Visible = lblMeleeAttackCount.Visible = true;
                    lblMeleeAttack.Text = Math.Round(combatStat.Value * c).ToString();
                    lblMeleeAttackCount.Text = $"{{{combatStat.AttackCount}}}";
                }
                else
                {
                    pbRangedAttack.Visible = lblRangedAttack.Visible = lblRangedAttackCount.Visible = true;
                    lblRangedAttack.Text = Math.Round(combatStat.Value * c).ToString();
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
            if (sender is not TransparentTextLabel label
                || (currentShowingPlayer == null && currentShowingMonster == null))
                return;

            Point labelPos = label.PointToScreen(Point.Empty);
            DetailEditModal modal = new(label.Text)
            {
                StartPosition = FormStartPosition.Manual,
                Location = labelPos,
            };

            if (modal.ShowDialog(this) != DialogResult.OK)
                return;

            if (!ushort.TryParse(modal.InputText, out ushort value))
            {
                MessageBox.Show("유효한 값을 입력하세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var statOwner = _showingDataType == SHOWING_DATA_TYPE.Player
                ? (object)currentShowingPlayer
                : currentShowingMonster;

            var setterMap = CreateStatSetters(statOwner);

            if (setterMap.TryGetValue(label.Name, out var action))
            {
                action(value);
                label.Text = label.Name.Contains("AttackCount") ? $"{{{value}}}" : value.ToString();

                // 체력 수정 시, 체력바 생김새도 수정
                if (label.Name == "lblMaxHealth")
                    UpdateHealthBar();
            }
            else
            {
                MessageBox.Show("알 수 없는 항목입니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /*
         * CreateStatSetters(object target)
         * - value를 인자로 받아 target의 스탯을 설정할 수 있게 하는 메서드를 반환
         * - target: CorpsMember 객체나 Monster 객체
         */
        private Dictionary<string, Action<ushort>> CreateStatSetters(object target)
        {
            dynamic stat = (_showingDataType == SHOWING_DATA_TYPE.Player)
                ? ((CorpsMember)target).Stat
                : ((Monster)target).Stat;

            return new()
            {
                ["lblMaxHealth"] = v => stat.MaxHp = v, // 딕셔너리 생성. 키는 레이블 이름, 값은 인자(value)
                ["lblMovement"] = v => stat.Movement = v,
                ["lblMeleeRange"] = v => stat.CombatStats["melee"].Range = v,
                ["lblRangedRange"] = v => stat.CombatStats["ranged"].Range = v,
                ["lblMeleeAttack"] = v => stat.CombatStats["melee"].Value = v,
                ["lblRangedAttack"] = v => stat.CombatStats["ranged"].Value = v,
                ["lblMeleeAttackCount"] = v => stat.CombatStats["melee"].AttackCount = v,
                ["lblRangedAttackCount"] = v => stat.CombatStats["ranged"].AttackCount = v,
                ["lblSpellPower"] = v => stat.SpellPower = v,
                ["lblWisdom"] = v => stat.Wisdom = v,
            };
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
                if (_showingDataType == SHOWING_DATA_TYPE.Player)
                {
                    currentShowingPlayer!.Stat.Hp = (ushort)hp;
                    currentShowingPlayer.Stat.Shield = shield ?? 0;
                }
                else
                {
                    if (hp == 0 && shield == null)
                    {
                        KillMonster();
                        return;
                    }
                    else
                        hp = UpdateMonsterHp((ushort)hp, shield);
                }

                lblCurrentHealth.Text = $"{hp}{(shield.HasValue ? $"(+{shield})" : "")}";
                UpdateHealthBar();
            }
        }

        /*
         * KillMonster()
         * - 체력과 쉴드가 모두 0일 때 실행
         * - 여러마리였다면 개체 수 - 1
         * - 한 마리였다면 전멸 판정
         */
        private void KillMonster()
        {
            if (currentShowingMonster!.Count == 1)
            {
                int index = _monsters.FindIndex(a => currentShowingMonster.Id == a.Id);
                _monsters.RemoveAt(index);
            }
            else
            {
                currentShowingMonster.Count--;
            }
            InitEnemyList();
        }

        /*
         * UpdateMonsterHp(ushort hp, ushort? shield)
         * - 몬스터의 체력을 갱신하는 메서드
         * - 체력이 최대체력보다 크면 마리 수를 증가시킴
         */
        private ushort UpdateMonsterHp(ushort hp, ushort? shield)
        {
            // 몬스터 마리, 그리고 그 중 한 마리의 체력 계산(예: 5마리 중 4마리는 풀피, 나머지 1마리의 체력 계산
            ushort count = (ushort)(hp / currentShowingMonster!.Stat.MaxHp);
            var newHp = (ushort)(hp % currentShowingMonster.Stat.MaxHp);
            if (newHp == 0) // 모든 몬스터가 풀피라면 현재 hp를 최대 체력과 같게 함
                hp = currentShowingMonster.Stat.MaxHp;
            else // 일부 몬스터가 풀피가 아니라면 나머지 체력을 갖게 함
                hp = newHp;
            if (hp != 0) // 체력이 남아있다면 count + 1 보정.(예: hp = 300, maxhp = 200이면 count = 1, hp = 100이므로, 2마리로 만들어준다.)
                count++;
            int index = _monsters.FindIndex(item => item.Id == currentShowingMonster.Id);
            if (index != -1)
            {
                var monster = _monsters[index];
                if (monster.Count != count) // 몬스터의 마리 수가 달라졌다면 갱신
                {
                    monster.Count = count;
                }
            }

            currentShowingMonster!.Stat.Hp = hp; // 현재 체력 갱신
            currentShowingMonster.Stat.Shield = shield ?? 0; // 방어력 갱신
            InitEnemyList();
            return hp;
        }

        /*
         * UpdateHealthBar()
         * - 현재 디테일 뷰포트에 표시된 플레이어 또는 몬스터의 체력 바를 업데이트하는 메서드
         */
        private void UpdateHealthBar()
        {
            // 현재 디테일 뷰포트에 표시된 플레이어의 체력 바를 업데이트
            // pn1P~4P까지 Name이 pn{currentShowingPlayer.Id}인 패널을 찾아서 HealthBar를 업데이트
            // 현재 플레이어가 1P~4P 중 어느 것인
            if (_showingDataType == SHOWING_DATA_TYPE.Player)
            {
                var healthBar = this.Controls.Find($"hb{currentShowingPlayer!.Id}", true).FirstOrDefault() as HealthBar;
                playerList.SuspendLayout();
                // HealthBar 컨트롤을 찾아서 업데이트
                healthBar?.SetValues(currentShowingPlayer.Stat.Hp,
                                    currentShowingPlayer.Stat.Shield,
                                    currentShowingPlayer.Stat.MaxHp);
                playerList.ResumeLayout();
            }
            else
            {
                var healthBar = this.Controls.Find($"hb{currentShowingMonster!.Id}", true).FirstOrDefault() as HealthBar;
                enemyList.SuspendLayout();
                // HealthBar 컨트롤을 찾아서 업데이트
                healthBar?.SetValues(currentShowingMonster.Stat.Hp,
                                    currentShowingMonster.Stat.Shield,
                                    currentShowingMonster.Stat.MaxHp);
                enemyList.ResumeLayout();
            }
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

        /*
         * EditStatusEffect()
         * - 현재 플레이어의 상태 이상을 편집하는 메서드
         * - 상태 이상이 없거나 현재 플레이어가 선택되지 않은 경우 아무 작업도 하지 않음
         * - 상태 이상 편집 모달을 표시하고, 사용자가 상태 이상을 수정하면 업데이트
         */
        private void EditStatusEffect()
        {
            if (currentShowingPlayer == null || currentShowingMonster == null)
                return;
            Point modalStartPos = new(fpnStatusEffect.PointToScreen(Point.Empty).X, fpnStatusEffect.PointToScreen(Point.Empty).Y + 45);

            List<StatusEffect> statusEffect;
            if (_showingDataType == SHOWING_DATA_TYPE.Player)
                statusEffect = currentShowingPlayer.Stat.StatusEffects;
            else
                statusEffect = currentShowingMonster.Stat.StatusEffects;
            var editModal = new StatusEffectEditModal(statusEffect)
            {
                StartPosition = FormStartPosition.Manual,
                Location = modalStartPos,
            };

            if (editModal.ShowDialog(this) == DialogResult.OK)
            {
                // 상태 이상 업데이트
                if (_showingDataType == SHOWING_DATA_TYPE.Player)
                {
                    currentShowingPlayer.Stat.StatusEffects = editModal.NewStatusEffects;
                    UpdateStatusEffect(currentShowingPlayer);
                    ShowDetail(currentShowingPlayer);
                    InitPlayerList(); // 상태 이상 패널 업데이트
                }
                else
                {
                    currentShowingMonster.Stat.StatusEffects = editModal.NewStatusEffects;
                    UpdateStatusEffect(currentShowingMonster);
                    ShowDetail(currentShowingMonster.IsReported, currentShowingMonster);
                    InitEnemyList();
                }
            }
        }

        /*
         * UpdateStatusEffect(CorpsMember player)
         * - 플레이어의 상태 이상을 업데이트하는 메서드
         * - player: CorpsMember 객체
         */
        private void UpdateStatusEffect(CorpsMember player)
        {
            player.SEAttackValueModifier = 1.0; // 상태 이상에 의한 공격력 보정 초기화
            foreach (var e in player.Stat.StatusEffects)
            {
                e.ApplyStatusEffect(player);
            }
        }

        /*
         * UpdateStatusEffect(Monster monster)
         * - 몬스터의 상태 이상을 업데이트하는 메서드
         * - monster: Monster 객체
         */
        private void UpdateStatusEffect(Monster monster)
        {
            monster.SEAttackValueModifier = 1.0; // 상태 이상에 의한 공격력 보정 초기화
            foreach (var e in monster.Stat.StatusEffects)
            {
                e.ApplyStatusEffect(monster);
            }
        }

        private void pbWeapon_Click(object sender, EventArgs e)
        {
            EditEquipment(ArtifactType.Weapon, (PictureBox)sender);
        }

        private void pbArmour_Click(object sender, EventArgs e)
        {
            EditEquipment(ArtifactType.Armour, (PictureBox)sender);
        }

        private void pbAccessory1_Click(object sender, EventArgs e)
        {
            EditEquipment(ArtifactType.Accessory, (PictureBox)sender);
        }

        private void pbAccessory2_Click(object sender, EventArgs e)
        {
            EditEquipment(ArtifactType.Accessory, (PictureBox)sender);
        }

        /*
         * EditEquipment(ArtifactType type, PictureBox sender)
         * - 장비를 편집하는 메서드
         * - type에 따라 팝업의 리스트를 다르게 표시
         * - sender인 PictureBox의 Image를 선택한 장비로 변경
         * - type: 장비 종류 (weapon, armour, accessory)
         * - sender: 클릭한 PictureBox 컨트롤
         */
        private void EditEquipment(ArtifactType type, PictureBox sender)
        {
            // 만약 fpnArtifact 밑에 창을 띄우면 잘리는 경우
            var modal = new EquipmentEditModal(type)
            {
                StartPosition = FormStartPosition.Manual,
            };
            var point = fpnArtifact.PointToScreen(Point.Empty);

            if (fpnArtifact.Location.Y + modal.Height > 790) // fpnArtifact의 위치가 화면 하단을 넘어가는 경우
            {
                modal.Location = new Point(point.X, point.Y - modal.Height);
            }
            else
            {
                modal.Location = point;
            }
            if (modal.ShowDialog(this) == DialogResult.OK)
            {
                EquipArtifact(sender, modal.SelectedArtifact);
            }
        }

        /*
         * EquipArtifact(PictureBox pb, Artifact? selectedArtifact)
         * - 선택된 유물을 장착하는 메서드
         * - pb: 유물을 장착/해제할 PictureBox 컨트롤
         * - selectedArtifact: 선택된 유물 객체 (null이면 장착 해제)
         */
        private void EquipArtifact(PictureBox pb, Artifact? selectedArtifact)
        {
            if (currentShowingPlayer == null || pb.Tag is not ArtifactSlotInfo info)
                return;

            if (selectedArtifact == null)
            {
                TryUnequipArtifact(info);
                UpdateArtifactTag(pb, "", info.SlotIndex);
            }
            else
            {
                TryUnequipArtifact(info);
                EquipNewArtifact(info, selectedArtifact);
                UpdateArtifactTag(pb, selectedArtifact.Id, info.SlotIndex);
            }

            ShowDetail(currentShowingPlayer);
            InitPlayerList();
        }

        /*
         * TryUnequipArtifact(ArtifactSlotInfo info)
         * - 유물 장착 해제하는 메서드
         * - info: 장착 중인 유물의 id와 인덱스가 담긴 정보
         */
        private void TryUnequipArtifact(ArtifactSlotInfo info)
        {
            if (!string.IsNullOrEmpty(info.ArtifactId))
            {
                currentShowingPlayer!.ArtifactSlot[info.SlotIndex]?.Unequip(currentShowingPlayer);
                currentShowingPlayer.ArtifactSlot[info.SlotIndex] = null;

                // 루다 2강의 경우, 앞으로 장착하는 유물마다 공격속도 -1
                if (currentShowingPlayer is Ruda && currentShowingPlayer.Level >= 2)
                {
                    ushort bonus = ((Ruda)currentShowingPlayer).PerfectionBonus;
                    ushort meleeCount = currentShowingPlayer.Stat.CombatStats["melee"].AttackCount;
                    ushort rangedCount = currentShowingPlayer.Stat.CombatStats["ranged"].AttackCount;

                    ((Ruda)currentShowingPlayer).PerfectionBonus = (ushort)Math.Max(0, bonus - 1);
                    currentShowingPlayer.Stat.CombatStats["melee"].AttackCount = (ushort)Math.Max(0, meleeCount - 1);
                    currentShowingPlayer.Stat.CombatStats["ranged"].AttackCount = (ushort)Math.Max(0, rangedCount - 1); ;
                }
            }

        }

        /*
         * EquipnewArtifact(ArtifactInfo info, Artifact newArtifact)
         * - 새로운 유물을 장착하는 메서드
         * - info: 유물 id와 인덱스가 저장된 객체
         * - newArtifact: 새로 장착할 유물
         */
        private void EquipNewArtifact(ArtifactSlotInfo info, Artifact newArtifact)
        {
            currentShowingPlayer!.ArtifactSlot[info.SlotIndex] = newArtifact;
            newArtifact.Equip(currentShowingPlayer);

            // 루다 2강 효과의 경우, 앞으로 장착하는 유물마다 공격속도 +1
            if (currentShowingPlayer is Ruda && currentShowingPlayer.Level >= 2)
            {
                ((Ruda)currentShowingPlayer).PerfectionBonus++;
                currentShowingPlayer.Stat.CombatStats["melee"].AttackCount++;
                currentShowingPlayer.Stat.CombatStats["ranged"].AttackCount++;
            }
        }

        /*
         * UpdateArtifactTag(PictureBox pb, string artifactId, int slotIndex)
         * - 유물 슬롯의 Tag를 갱신하는 메서드
         * - pb: 유물 슬롯
         * - artifactId: 새로운 태그의 artifactId 값
         * - slotIndex: 새로운 태그의 slotIndex 값
         */
        private void UpdateArtifactTag(PictureBox pb, string artifactId, int slotIndex)
        {
            pb.Tag = new ArtifactSlotInfo
            {
                ArtifactId = artifactId,
                SlotIndex = slotIndex
            };
        }

        private void pbLevel_Click(object sender, EventArgs e)
        {
            var editModal = new DetailEditModal(currentShowingPlayer!.Level.ToString())
            {
                StartPosition = FormStartPosition.Manual,
                Location = pbLevel.PointToScreen(Point.Empty),
            };

            if (editModal.ShowDialog() == DialogResult.OK)
            {
                if (ushort.TryParse(editModal.InputText.Trim(), out ushort newLevel)
                    && newLevel >= 0 && newLevel <= 3)
                {
                    currentShowingPlayer!.Level = newLevel; // 레벨 갱신
                    UpdateSkillsByLevel(newLevel); // 레벨에 따른 스킬 갱신
                    ShowDetail(currentShowingPlayer); // 업데이트된 내용 표시
                    InitPlayerList();
                }
                else
                {
                    MessageBox.Show("유효한 강화 수치(0 ~ 3)가 아닙니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /*
         * UpdateSkillsByLevel(ushort newLevel)
         * - 플레이어의 레벨 업데이트에 따른 패시브 및 액티브 스킬 사용 조건을 갱신하는 메서드
         * - newLevel: 플레이어의 새 레벨
         */
        private void UpdateSkillsByLevel(ushort newLevel)
        {
            // 레벨 갱신에 따른 패시브 스킬 개방
            // 액티브 스킬은 스킬 설명 모달을 다시 띄우면 갱신됨
            foreach (var p in currentShowingPlayer!.Passives)
            {
                // 기존보다 높은 강화수치를 적용했는데, 아직 활성화 되지 않은 패시브가 있다면
                if (p.RequiredLevel <= newLevel && !p.isActivated)
                {
                    p.Activate?.Invoke();
                }
                // 기존보다 낮은 강화수치를 적용했는데, 더 높은 단계에 활성화된 패시브가 있다면
                else if (p.RequiredLevel > newLevel && p.isActivated)
                {
                    p.Deactivate?.Invoke();
                }
            }
        }

        private void rtbNote_TextChanged(object sender, EventArgs e)
        {
            if (_showingDataType == SHOWING_DATA_TYPE.Player)
            {
                currentShowingPlayer!.Note = rtbNote.Text;
            }
            else
            {
                currentShowingMonster!.Note = rtbNote.Text;
            }
            AdjustNoteHeight();
        }

        /*
         * AdjustNoteHeight()
         * - 특이사항을 적는 패널과 RichTextBox의 높이를 조절하는 메서드
         */
        private void AdjustNoteHeight()
        {
            int lineCount = rtbNote.GetLineFromCharIndex(rtbNote.TextLength) + 1;

            int lineHeight = rtbNote.Font.Height;
            int maxLines = 5;
            int padding = 6;

            int newHeight = Math.Min(lineCount, maxLines) * lineHeight + padding;

            // 리치텍스트박스 높이 변경
            if (rtbNote.Height != newHeight)
                rtbNote.Height = newHeight;

            // 패널도 리치텍스트박스를 감쌀 수 있도록 높이 변경
            int extra = rtbNote.Top * 2; // RichTextBox 위아래 여백 고려
            int newPanelHeight = rtbNote.Height + extra;

            if (pnNote.Height != newPanelHeight)
                pnNote.Height = newPanelHeight;
        }

        /*
         * ChangeWeather
         * - 날씨를 바꾸기 위한 모달을 호출하고 그 데이터를 반영하는 메서드
         */
        private void ChangeWeather(object sender, EventArgs e)
        {
            Point startPos = pbWeather.PointToScreen(Point.Empty);
            var modal = new WeatherEditModal(_currentWeather)
            {
                StartPosition = FormStartPosition.Manual,
                Location = new Point(startPos.X, startPos.Y + pbWeather.Height),
            };

            if (modal.ShowDialog() == DialogResult.OK)
            {
                _currentWeather = modal.NewWeather.Duration == 0 ? new Weather() : modal.NewWeather;
                InitWeather();
                UpdateWeather();
                if (_showingDataType == SHOWING_DATA_TYPE.Player)
                {
                    ShowDetail(currentShowingPlayer!);
                }
                else
                {
                    ShowDetail(currentShowingMonster!.IsReported, currentShowingMonster);
                }
            }
        }

        /*
         * UpdateWeather()
         * - 날씨가 바뀜에 따라 플레이어와 몬스터를 업데이트하는 메서드
         */
        private void UpdateWeather()
        {
            foreach (var c in _characters.Values)
            {
                _currentWeather.ApplyWeatherEffect(c);
            }
            foreach (var m in _monsters)
            {
                _currentWeather.ApplyWeatherEffect(m);
            }
        }
    }
}
