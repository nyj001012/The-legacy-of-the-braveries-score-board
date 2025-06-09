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
        public ScoreBoardControl(Dictionary<string, CorpsMember> characters, List<(string id, string name, ushort count)> monsters)
        {
            InitializeComponent();
        }

        private void ScoreBoardControl_Load(object sender, EventArgs e)
        {
            ScrollBarManager.SetScrollBar(playerContainer, playerList, playerScrollBar);
        }
    }
}
