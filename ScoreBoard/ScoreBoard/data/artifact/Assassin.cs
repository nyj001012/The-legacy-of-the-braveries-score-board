using ScoreBoard.data.character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data.artifact
{
    internal class Assassin : Artifact
    {
        public Assassin()
        {
        }

        public override void Equip(UnitBase unit)
        {
            // 공격력 90 증가
            if (unit.Stat.CombatStats.TryGetValue("melee", out var melee))
            {
                melee.Value += 90;
            }
            if (unit.Stat.CombatStats.TryGetValue("ranged", out var ranged))
            {
                ranged.Value += 90;
            }
        }

        public override void Unequip(UnitBase unit)
        {
            // 공격력 20 감소
            if (unit.Stat.CombatStats.TryGetValue("melee", out var melee))
            {
                melee.Value = (ushort)Math.Max(0, (int)melee.Value - 90);
            }
            if (unit.Stat.CombatStats.TryGetValue("ranged", out var ranged))
            {
                ranged.Value = (ushort)Math.Max(0, (int)ranged.Value - 90);
            }
        }
    }
}
