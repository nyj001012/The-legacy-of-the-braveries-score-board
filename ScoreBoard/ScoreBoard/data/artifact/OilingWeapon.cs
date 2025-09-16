using ScoreBoard.data.character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data.artifact
{
    internal class OilingWeapon : Artifact
    {
        public OilingWeapon()
        {
        }
        public override void Equip(UnitBase member)
        {
            // 공속 +1
            if (member.Stat.CombatStats.TryGetValue("melee", out var melee))
            {
                melee.AttackCount++;
            }
            if (member.Stat.CombatStats.TryGetValue("ranged", out var ranged))
            {
                ranged.AttackCount++;
            }
        }

        public override void Unequip(UnitBase member)
        {
            // 공속 -1
            if (member.Stat.CombatStats.TryGetValue("melee", out var melee))
            {
                melee.AttackCount = (ushort)Math.Max(0, (int)melee.AttackCount - 1);
            }
            if (member.Stat.CombatStats.TryGetValue("ranged", out var ranged))
            {
                ranged.AttackCount = (ushort)Math.Max(0, (int)ranged.AttackCount - 1);
            }
        }
    }
}
