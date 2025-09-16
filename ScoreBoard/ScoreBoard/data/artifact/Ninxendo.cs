using ScoreBoard.data.character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data.artifact
{
    internal class Ninxendo : Artifact
    {
        public Ninxendo()
        {
        }
        public override void Equip(UnitBase member)
        {
            // 공속 +2
            if (member.Stat.CombatStats.TryGetValue("melee", out var melee))
            {
                melee.AttackCount += 2;
            }
            if (member.Stat.CombatStats.TryGetValue("ranged", out var ranged))
            {
                ranged.AttackCount += 2;
            }
        }

        public override void Unequip(UnitBase member)
        {
            // 공속 -2
            if (member.Stat.CombatStats.TryGetValue("melee", out var melee))
            {
                melee.AttackCount = (ushort)Math.Max(0, (int)melee.AttackCount - 2);
            }
            if (member.Stat.CombatStats.TryGetValue("ranged", out var ranged))
            {
                ranged.AttackCount = (ushort)Math.Max(0, (int)ranged.AttackCount - 2);
            }
        }
    }
}
