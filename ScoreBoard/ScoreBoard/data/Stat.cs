using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data
{
    internal class Stat(ushort hp, ushort movement, ushort wisdom, Dictionary<string, CombatStat> combatStats)
    {
        public ushort Hp { get; set; } = hp; // 체력 (예: 10, 20 등)
        public ushort Movement { get; set; } = movement; // 이동 가능 거리 (예: 1, 2, 3 등)
        public ushort Wisdom { get; set; } = wisdom; // 지혜 = 카드 뽑는 횟수 (예: 1, 2, 3 등) - 선택적 속성
        public Dictionary<string, CombatStat> CombatStats { get; set; } = combatStats; // 공격 타입별 공격력 정보 배열 (예: 물리(atk), 마법(sp) 등)
    }
}
