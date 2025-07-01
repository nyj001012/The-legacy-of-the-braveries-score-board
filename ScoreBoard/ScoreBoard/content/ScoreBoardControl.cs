using ScoreBoard.controls;
using ScoreBoard.data;
using ScoreBoard.data.character;
using ScoreBoard.data.stat;
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
        private readonly Dictionary<string, CorpsMember> _characters;
        private readonly List<(string id, string name, ushort count)> _monsters;
        private const int IconSize = 45; // 아이콘 크기 설정
        private const int MarginInPanel = 10; // 오른쪽 여백 설정

        public ScoreBoardControl(Dictionary<string, CorpsMember> characters, List<(string id, string name, ushort count)> monsters)
        {
            _characters = characters ?? throw new ArgumentNullException(nameof(characters), "캐릭터는 비어있을 수 없습니다.");
            _monsters = monsters ?? throw new ArgumentNullException(nameof(monsters), "몬스터를 선택해야 합니다.");
            InitializeComponent();
        }

        private void ScoreBoardControl_Load(object sender, EventArgs e)
        {
            BeginInvoke(() => InitPlayerList());
            BeginInvoke(() => InitEnemyList());
            BeginInvoke(() => ShowDetail(_characters.ElementAt(0).Value));
            ScrollBarManager.SetScrollBar(playerContainer, playerList, playerScrollBar);
        }

        /*
         * enemyList 컨트롤에 적 정보를 초기화
         */
        private void InitEnemyList()
        {
            enemyList.SuspendLayout();
            enemyList.Controls.Clear(); // 기존 컨트롤 제거

            foreach (var (id, name, count) in _monsters)
            {
                // EnemyPanel 생성자에 id, name, count 받기
                EnemyPanel enemyControl = new EnemyPanel(id, name, count);
                enemyControl.Name = $"pn{id}";
                enemyList.Controls.Add(enemyControl);
            }

            enemyList.ResumeLayout();
        }

        /*
         * playerList 컨트롤에 캐릭터 정보를 초기화
         */
        private void InitPlayerList()
        {
            playerList.SuspendLayout();
            playerList.Controls.Clear(); // 기존 컨트롤 제거

            int index = 1;
            foreach (var character in _characters.Values)
            {
                // 1P는 CurrentPlayerPanel, 나머지는 PlayerPanel
                UserControl panel = (index == 1)
                    ? new CurrentPlayerPanel(character, index)
                    : new PlayerPanel(character, index);

                panel.Name = $"pn{character.Id}";
                panel.Click += (s, e) => ShowDetail(character);

                playerList.Controls.Add(panel);
                index++;
            }
            playerList.ResumeLayout();
        }

        /*
         * ShowDetail(CorpsMember player)
         * - 플레이어 상세 정보를 표시하는 메서드
         */
        private void ShowDetail(CorpsMember player)
        {
            // 플레이어 상세 정보 표시 로직 구현
            ShowBasicInfo(player); // 기본 정보 표시
            ShowHealth(player); // 체력 표시
            ShowStatusEffect(player); // 상태 이상 표시
            ShowMovement(player); // 이동 정보 표시
            ShowAttackRange(player); // 공격 사거리 표시
        }

        /*
         * ShowAttackRange(CorpsMember player)
         * - 플레이어의 공격 사거리를 표시하는 메서드
         * - player: CorpsMember 객체
         */
        private void ShowAttackRange(CorpsMember player)
        {
            fpnRange.SuspendLayout();
            // 전부 invisible로 초기화
            pbMelee.Visible = false;
            pbRanged.Visible = false;
            lblMeleeRange.Visible = false;
            lblRangedRange.Visible = false;

            foreach (var s in player.Stat.CombatStats)
            {
                if (s.Key == "melee")
                {
                    pbMelee.Visible = true; // 근접 공격 아이콘 보이기
                    lblMeleeRange.Visible = true; // 근접 공격 사거리 레이블 보이기
                    lblMeleeRange.Text = $"{s.Value.Range}"; // 근접 공격 사거리 표시
                }
                else
                {
                    pbRanged.Visible = true; // 원거리 공격 아이콘 보이기
                    lblRangedRange.Visible = true; // 원거리 공격 사거리 레이블 보이기
                    lblRangedRange.Text = $"{s.Value.Range}"; // 원거리 공격 사거리 표시
                }
            }
            fpnRange.ResumeLayout();
        }

        /*
         * ShowMovement(CorpsMember player)
         * - 플레이어의 이동 속도를 표시하는 메서드
         * - player: CorpsMember 객체
         */
        private void ShowMovement(CorpsMember player)
        {
            // 이동 정보 표시 로직 구현
            lblMovement.Text = $"{player.Stat.Movement}"; // 현재 이동력 표시
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
                fpnStatusDetail.Controls.Clear(); // 상태 이상이 없으면 컨트롤 비우기
                fpnStatusDetail.Visible = false; // 상태 이상 패널 숨기기
                return;
            }
            fpnStatusDetail.Visible = true; // 상태 이상 패널 보이기
            fpnStatusDetail.Controls.Clear(); // 기존 컨트롤 제거
            fpnStatusDetail.SuspendLayout();
            // TODO => 상태 이상 정보를 표시하는 로직 구현
            fpnStatusDetail.ResumeLayout();
        }

        /*
         * ShowHealth(CorpsMember player)
         * - 플레이어의 체력을 표시하는 메서드
         * - player: CorpsMember 객체
         */
        private void ShowHealth(CorpsMember player)
        {
            // 플레이어의 체력 정보를 표시하는 로직 구현
            lblHealth.Text = $"{player.Stat.Hp}"; // 현재 체력 표시
            lblHealth.Text += player.Stat.Shield > 0 ? $"(+{player.Stat.Shield})" : ""; // 보호막 표시
            lblHealth.Text += $"/{player.Stat.MaxHp}"; // 최대 체력 표시
        }

        /*
         * ShowBasicInfo(CorpsMember player)
         * - 플레이어의 기본 정보를 표시하는 메서드
         * - player: CorpsMember 객체
         */
        private void ShowBasicInfo(CorpsMember player)
        {
            // 플레이어의 기본 정보 표시 로직 구현
            lblName.Text = player.Name;
            if (player.RequiredDiceValues.Count > 0)
            {
                foreach (var diceValue in player.RequiredDiceValues) // key: 주사위 값, value: 치명타 여부
                {
                    TransparentTextLabel label = new TransparentTextLabel
                    {
                        Text = diceValue.Key.ToString(),
                        ForeColor = diceValue.Value ? Color.FromArgb(255, 217, 0) : Color.WhiteSmoke, // 치명타는 노란색
                        AutoSize = true,
                        Margin = new Padding(0, 0, MarginInPanel, 0), // 오른쪽 여백 적용
                    };
                    fpnBasicStatus.Controls.Add(label);
                }
            }
        }

        private void pbDice_Click(object sender, EventArgs e)
        {

        }
    }
}
