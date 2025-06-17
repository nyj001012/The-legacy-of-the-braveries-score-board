using ScoreBoard.controls;
using ScoreBoard.data;
using ScoreBoard.data.character;
using ScoreBoard.data.stat;
using ScoreBoard.utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
                if (index == 1)
                {
                    CurrentPlayerPanel firstPlayer = new(character, index)
                    {
                        Name = $"pn{character.Id}"
                    };
                    playerList.Controls.Add(firstPlayer);
                }
                else
                {
                    PlayerPanel playerControl = new(character, index)
                    {
                        Name = $"pn{character.Id}"
                    };
                    playerList.Controls.Add(playerControl);
                }
                index++;
            }
            playerList.ResumeLayout();
        }
    }
}
