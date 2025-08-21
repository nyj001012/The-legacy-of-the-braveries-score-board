using ScoreBoard.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data.monster
{
    internal class Imp : Monster
    {
        public Imp(string id, ushort spawnTurn) : base()
        {
            InitialiseNormalElite(id, spawnTurn);
        }
    }
}
