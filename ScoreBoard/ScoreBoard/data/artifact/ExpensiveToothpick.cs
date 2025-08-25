using ScoreBoard.data.character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data.artifact
{
    internal class ExpensiveToothpick : Artifact
    {
        private ushort criticalNumber = 0;
        public ExpensiveToothpick()
        {
        }

        public override void Equip(CorpsMember member)
        {
            // 공격력 40 증가
            if (member.Stat.CombatStats.TryGetValue("melee", out var melee))
            {
                melee.Value += 40;
            }
            if (member.Stat.CombatStats.TryGetValue("ranged", out var ranged))
            {
                ranged.Value += 40;
            }

            // 이동속도 +1
            member.Stat.Movement++;

            // 치명타 +1. 제일 높은 숫자부터 적용
            foreach (var number in member.RequiredDiceValues.Keys.OrderByDescending(k => k))
            {
                if (!member.RequiredDiceValues[number])
                {
                    member.RequiredDiceValues[number] = true;
                    criticalNumber = number;
                    break;
                }
            }
        }

        public override void Unequip(CorpsMember member)
        {
            // 공격력 40 감소
            if (member.Stat.CombatStats.TryGetValue("melee", out var melee))
            {
                melee.Value = (ushort)Math.Max(0, (int)melee.Value - 40);
            }
            else if (member.Stat.CombatStats.TryGetValue("ranged", out var ranged))
            {
                ranged.Value = (ushort)Math.Max(0, (int)ranged.Value - 40);
            }

            // 이동속도 -1
            member.Stat.Movement = (ushort)Math.Max(0, (int)member.Stat.Movement - 1);

            // 치명타 적용되었던 주사위 숫자의 치명타 해제
            // 만약 치명타가 적용된 숫자가 있다면 해당 숫자의 치명타를 해제하고, 치명타 숫자를 초기화
            if (member.RequiredDiceValues.ContainsKey(criticalNumber))
            {
                member.RequiredDiceValues[criticalNumber] = false;
                criticalNumber = 0; // 치명타 숫자 초기화
            }
        }
    }
}
