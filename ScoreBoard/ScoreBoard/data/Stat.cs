using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ScoreBoard.data
{
    internal class Stat
    {
        public ushort Hp { get; set; } // 체력 (예: 10, 20 등)
        public ushort Movement { get; set; } // 이동 가능 거리 (예: 1, 2, 3 등)
        public ushort? Wisdom { get; set; } // 지혜 = 카드 뽑는 횟수 (예: 1, 2, 3 등) - 선택적 속성

        [JsonPropertyName("combatstat")]
        public Dictionary<string, CombatStat> CombatStats { get; set; } = []; // 공격 타입별 공격력 정보 배열 (예: 물리(atk), 마법(sp) 등)
    }
}
