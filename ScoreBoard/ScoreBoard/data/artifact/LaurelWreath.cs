using ScoreBoard.data.character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data.artifact
{
    internal class LaurelWreath : Artifact
    {
        public LaurelWreath()
        {
        }

        public override void Equip(UnitBase member)
        {
            // 공격력 30 증가
            if (member.Stat.CombatStats.TryGetValue("melee", out var melee))
            {
                melee.Value += 30;
            }
            if (member.Stat.CombatStats.TryGetValue("ranged", out var ranged))
            {
                ranged.Value += 30;
            }
            // 체력 100 증가
            member.Stat.MaxHp += 100;
            member.Stat.Hp += 100;
        }

        public override void Unequip(UnitBase member)
        {
            // 공격력 30 감소
            if (member.Stat.CombatStats.TryGetValue("melee", out var melee))
            {
                melee.Value = (ushort)Math.Max(0, (int)melee.Value - 30);
            }
            else if (member.Stat.CombatStats.TryGetValue("ranged", out var ranged))
            {
                ranged.Value = (ushort)Math.Max(0, (int)ranged.Value - 30);
            }
            // 체력 100 감소
            member.Stat.MaxHp = (ushort)Math.Max(0, (int)member.Stat.MaxHp - 100);
            member.Stat.Hp = (ushort)Math.Max(0, (int)member.Stat.Hp - 100);
        }
    }
}
