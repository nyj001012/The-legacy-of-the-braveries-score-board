using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data
{
    internal class CorpsMember
    {
        private string id;
        private string name;
        private string corpsId;
        private string[] description;
        private Stat stat;
    }

    internal class Stat
    {
        private ushort hp;
        private ushort movement;
        private CombatStat[] combatStat;
    }

    internal class CombatStat
    {
        private string Type { get; set; } // 공격 종류 (예: 물리(atk), 마법(sp))
        private ushort Range { get; set; } // 공격 사거리 (예: 1, 2, 3 등)
        private ushort AttackCount { get; set; } // 공격 횟수 = 굴릴 수 있는 주사위 개수 (예: 1, 2, 3 등)
        private ushort Value { get; set; } // 공격력 및 주문력 (예: 1, 2, 3 등)

        public CombatStat(string type, ushort range, ushort attackCount, ushort value)
        {
            this.Type = type;
            this.Range = range;
            this.AttackCount = attackCount;
            this.Value = value;
        }
    }
}
