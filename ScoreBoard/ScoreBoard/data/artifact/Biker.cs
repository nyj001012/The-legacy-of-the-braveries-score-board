using ScoreBoard.data.character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data.artifact
{
    internal class Biker : Artifact
    {
        public Biker()
        {
        }

        public override void Equip(UnitBase unit)
        {
            // 공격력 40 증가
            if (unit.Stat.CombatStats.TryGetValue("melee", out var melee))
            {
                melee.Value += 40;
            }
            if (unit.Stat.CombatStats.TryGetValue("ranged", out var ranged))
            {
                ranged.Value += 40;
            }

            // 이동 속도 1 증가
            unit.Stat.Movement++;
        }

        public override void Unequip(UnitBase unit)
        {
            // 공격력 40 감소
            if (unit.Stat.CombatStats.TryGetValue("melee", out var melee))
            {
                melee.Value = (ushort)Math.Max(0, (int)melee.Value - 40);
            }
            else if (unit.Stat.CombatStats.TryGetValue("ranged", out var ranged))
            {
                ranged.Value = (ushort)Math.Max(0, (int)ranged.Value - 40);
            }

            // 이동 속도 1 감소
            unit.Stat.Movement = (ushort)Math.Max(0, (int)unit.Stat.Movement - 1);
        }
    }
}
