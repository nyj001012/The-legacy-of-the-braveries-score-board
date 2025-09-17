using ScoreBoard.data.character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data.artifact
{
    internal class MonsterHelmet : Artifact
    {
        public MonsterHelmet()
        {
        }

        public override void Equip(UnitBase member)
        {
            // 공격력 100 증가
            if (member.Stat.CombatStats.TryGetValue("melee", out var melee))
            {
                melee.Value += 100;
            }
            if (member.Stat.CombatStats.TryGetValue("ranged", out var ranged))
            {
                ranged.Value += 100;
            }

            // 주문력 300 증가
            if (member.Stat.SpellPower.HasValue)
            {
                member.Stat.SpellPower = (ushort?)(member.Stat.SpellPower.Value + 300);
            }

            // 체력 300 증가
            member.Stat.MaxHp += 300;
            member.Stat.Hp += 300;
        }

        public override void Unequip(UnitBase member)
        {
            // 공격력 100 감소
            if (member.Stat.CombatStats.TryGetValue("melee", out var melee))
            {
                melee.Value = (ushort)Math.Max(0, (int)melee.Value - 100);
            }
            else if (member.Stat.CombatStats.TryGetValue("ranged", out var ranged))
            {
                ranged.Value = (ushort)Math.Max(0, (int)ranged.Value - 100);
            }

            // 주문력 300 감소
            if (member.Stat.SpellPower.HasValue)
            {
                member.Stat.SpellPower = (ushort?)(Math.Max(0, member.Stat.SpellPower.Value - 300));
            }

            // 체력 300 감소
            member.Stat.MaxHp = (ushort)Math.Max(0, (int)member.Stat.MaxHp - 300);
            member.Stat.Hp = (ushort)Math.Max(0, (int)member.Stat.Hp - 300);
        }
    }
}
