using ScoreBoard.data.character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data.artifact
{
    internal class SpaceHelmet : Artifact
    {
        public SpaceHelmet()
        {
        }
        public override void Equip(CorpsMember member)
        {
            // 체력 +200
            member.Stat.Hp += 200;
            member.Stat.MaxHp += 200;

            // 공격력 50 증가
            if (member.Stat.CombatStats.TryGetValue("melee", out var melee))
            {
                melee.Value += 50;
            }
            if (member.Stat.CombatStats.TryGetValue("ranged", out var ranged))
            {
                ranged.Value += 50;
            }

            // 주문력 100 증가
            if (member.Stat.SpellPower.HasValue)
            {
                member.Stat.SpellPower = (ushort?)(member.Stat.SpellPower.Value + 100);
            }
        }

        public override void Unequip(CorpsMember member)
        {            
            // 체력 -200
            member.Stat.MaxHp = (ushort)(Math.Max(0, member.Stat.MaxHp - 200));
            member.Stat.Hp = (ushort)(Math.Max(0, member.Stat.Hp - 200));

            // 공격력 50 감소
            if (member.Stat.CombatStats.TryGetValue("melee", out var melee))
            {
                melee.Value = (ushort)Math.Max(0, (int)melee.Value - 50);
            }
            if (member.Stat.CombatStats.TryGetValue("ranged", out var ranged))
            {
                ranged.Value = (ushort)Math.Max(0, (int)ranged.Value - 50);
            }

            // 주문력 100 감소
            if (member.Stat.SpellPower.HasValue)
            {
                member.Stat.SpellPower = (ushort?)(Math.Max(0, member.Stat.SpellPower.Value - 100));
            }
        }
    }
}
