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
                        Margin = new Padding(0, 0, 10, 0)
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
