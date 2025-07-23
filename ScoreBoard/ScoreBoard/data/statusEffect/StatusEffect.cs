using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data.statusEffect
{
    public class StatusEffect
    {
        public StatusEffectType Type { get; set; } // 상태이상의 종류 (예: Poison, Stun 등)
        public int Duration { get; set; } // 상태이상의 지속 시간 (예: 2, 3 등)
        public bool IsInfinite => Duration < 0; // 무한 지속 여부 (예: true, false)
 
        public StatusEffect(StatusEffectType type, int duration)
        {
            Type = type;
            Duration = duration;
        }
    }
}
