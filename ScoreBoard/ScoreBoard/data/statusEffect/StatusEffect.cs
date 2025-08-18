using ScoreBoard.data.character;
using ScoreBoard.data.monster;
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

        /*
         * ApplyStatusEffect(CorpsMember member)
         * - 상태이상을 CorpsMember에게 적용하는 메소드
         * - 화면상 표시되는 수치에 영향을 주는 상태이상만 적용
         * - member: 상태이상을 적용할 CorpsMember 객체
         */
        public void ApplyStatusEffect(CorpsMember member)
        {
            switch (Type)
            {
                case StatusEffectType.BrokenSword:
                    member.SEAttackValueModifier = 0.5; // 공격력 -50%. 곱연산 활용
                    break;
                default:
                    member.SEAttackValueModifier = 1; // 기본값은 1 (변경 없음)
                    break;
            }
        }

        /*
         * ApplyStatusEffect(Monster monster)
         * - 상태이상을 Monster 적용하는 메소드
         * - 화면상 표시되는 수치에 영향을 주는 상태이상만 적용
         * - monster: 상태이상을 적용할 Monster 객체
         */
        public void ApplyStatusEffect(Monster monster)
        {
            switch (Type)
            {
                case StatusEffectType.BrokenSword:
                    monster.SEAttackValueModifier = 0.5; // 공격력 -50%. 곱연산 활용
                    break;
                default:
                    monster.SEAttackValueModifier = 1; // 기본값은 1 (변경 없음)
                    break;
            }
        }
    }
}
