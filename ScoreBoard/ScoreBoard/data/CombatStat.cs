using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data
{
    internal class CombatStat()
    {
        public string Type { get; set; } = string.Empty; // 공격 타입 (예: 물리(atk), 마법(sp))
        public ushort Range { get; set; } // 공격 사거리 (예: 1, 2, 3 등)
        public ushort AttackCount { get; set; } // 공격 횟수 = 굴릴 수 있는 주사위 개수 (예: 1, 2, 3 등)
        public ushort Value { get; set; } // 공격력 (예: 10, 20 등)
    }
}
