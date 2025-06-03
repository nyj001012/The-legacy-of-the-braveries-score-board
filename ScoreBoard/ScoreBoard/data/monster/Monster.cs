using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data.monster
{
    internal class Monster
    {
        public ushort Id { get; private set; }
        public required string Name { get; init; }
        public required Stat Stat { get; init;}
        public required ushort[] AttackDiceValue { get; init; }
    }
}
