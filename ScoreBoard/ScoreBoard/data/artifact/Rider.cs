using ScoreBoard.data.character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data.artifact
{
    internal class Rider : Artifact
    {
        public Rider()
        {
        }
        public override void Equip(UnitBase member)
        {
            // 주문력 100 증가
            if (member.Stat.SpellPower.HasValue)
            {
                member.Stat.SpellPower = (ushort?)(member.Stat.SpellPower.Value + 100);
            }

            // 공속 +1
            if (member.Stat.CombatStats.TryGetValue("melee", out var melee))
            {
                melee.AttackCount += 1;
            }
            if (member.Stat.CombatStats.TryGetValue("ranged", out var ranged))
            {
                ranged.AttackCount += 1;
            }
        }
        public override void Unequip(UnitBase member)
        {
            // 주문력 100 감소
            if (member.Stat.SpellPower.HasValue)
            {
                member.Stat.SpellPower = (ushort?)(Math.Max(0, member.Stat.SpellPower.Value - 100));
            }

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
