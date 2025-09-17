using ScoreBoard.data.character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data.artifact
{
    internal class BlackGun : Artifact
    {
        private ushort criticalNumber = 0;

        public BlackGun()
        {
        }

        public override void Equip(UnitBase unit)
        {
            // 공격력 60, 공격 속도 1 증가
            if (unit.Stat.CombatStats.TryGetValue("melee", out var melee))
            {
                melee.Value += 60;
                melee.AttackCount = (ushort)Math.Max(0, (int)melee.AttackCount + 1);
            }
            if (unit.Stat.CombatStats.TryGetValue("ranged", out var ranged))
            {
                ranged.Value += 60;
                ranged.AttackCount = (ushort)Math.Max(0, (int)ranged.AttackCount + 1);
            }

            // 치명타 +1. 제일 높은 숫자부터 적용
            foreach (var number in unit.RequiredDiceValues.Keys.OrderByDescending(k => k))
            {
                if (!unit.RequiredDiceValues[number])
                {
                    unit.RequiredDiceValues[number] = true;
                    criticalNumber = number;
                    break;
                }
            }
        }

        public override void Unequip(UnitBase unit)
        {
            // 공격력 60, 공격 속도 1 감소
            if (unit.Stat.CombatStats.TryGetValue("melee", out var melee))
            {
                melee.Value = (ushort)Math.Max(0, (int)melee.Value - 60);
                melee.AttackCount = (ushort)Math.Max(0, (int)melee.AttackCount - 1);
            }
            else if (unit.Stat.CombatStats.TryGetValue("ranged", out var ranged))
            {
                ranged.Value = (ushort)Math.Max(0, (int)ranged.Value - 60);
                ranged.AttackCount = (ushort)Math.Max(0, (int)ranged.AttackCount - 1);
            }

            if (unit.RequiredDiceValues.ContainsKey(criticalNumber))
            {
                unit.RequiredDiceValues[criticalNumber] = false;
                criticalNumber = 0; // 치명타 숫자 초기화
            }
        }
    }
}
