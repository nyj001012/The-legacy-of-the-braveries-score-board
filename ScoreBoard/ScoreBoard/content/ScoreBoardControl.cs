using ScoreBoard.controls;
using ScoreBoard.data;
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
            ScrollBarManager.SetScrollBar(playerContainer, playerList, playerScrollBar);
        }
    }
}
