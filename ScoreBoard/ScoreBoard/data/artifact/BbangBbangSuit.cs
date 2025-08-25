using ScoreBoard.data.character;
using ScoreBoard.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data.artifact
{
    internal class BbangBbangSuit : Artifact
    {
        private ushort hpIncrease = 0; // 체력 증가량

        public BbangBbangSuit()
        {
        }

        public override void Equip(CorpsMember member)
        {
            hpIncrease = Validator.IsTanker(member) ? (ushort)1000 : (ushort)500;

            // 공격력 20 증가
            if (member.Stat.CombatStats.TryGetValue("melee", out var melee))
            {
                melee.Value += 20;
            }
            if (member.Stat.CombatStats.TryGetValue("ranged", out var ranged))
            {
                ranged.Value += 20;
            }
            member.Stat.MaxHp += hpIncrease;
            member.Stat.Hp += hpIncrease;
        }

        public override void Unequip(CorpsMember member)
        {
            // 공격력 20 감소
            if (member.Stat.CombatStats.TryGetValue("melee", out var melee))
            {
                melee.Value = (ushort)Math.Max(0, (int)melee.Value - 20);
            }
            else if (member.Stat.CombatStats.TryGetValue("ranged", out var ranged))
            {
                ranged.Value = (ushort)Math.Max(0, (int)ranged.Value - 20);
            }
            member.Stat.MaxHp = (ushort)Math.Max(0, (int)member.Stat.MaxHp - hpIncrease);
            member.Stat.Hp = (ushort)Math.Max(0, (int)member.Stat.Hp - hpIncrease);
        }
    }
}
