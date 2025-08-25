using ScoreBoard.data.character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data.artifact
{
    internal class Dandy : Artifact
    {
        public Dandy()
        {
        }

        public override void Equip(CorpsMember member)
        {
            // 근접 공격력 30 증가
            if (member.Stat.CombatStats.TryGetValue("melee", out var melee))
            {
                melee.Value += 30;
            }

            // 체력 300 증가
            member.Stat.MaxHp += 300;
            member.Stat.Hp += 300;
        }

        public override void Unequip(CorpsMember member)
        {
            // 근접 공격력 30 감소
            if (member.Stat.CombatStats.TryGetValue("melee", out var melee))
            {
                melee.Value = (ushort)Math.Max(0, (int)melee.Value - 30);
            }

            // 체력 300 감소
            member.Stat.MaxHp = (ushort)Math.Max(0, (int)member.Stat.MaxHp - 300);
            member.Stat.Hp = (ushort)Math.Max(0, (int)member.Stat.Hp - 300);
        }
    }
}
