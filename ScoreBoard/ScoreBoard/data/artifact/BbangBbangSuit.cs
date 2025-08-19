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
        public BbangBbangSuit()
        {
        }

        public override void Equip(CorpsMember member)
        {
            // 공격력 20 증가
            if (member.Stat.CombatStats.TryGetValue("melee", out var melee))
            {
                melee.Value += 20;
            }
            if (member.Stat.CombatStats.TryGetValue("ranged", out var ranged))
            {
                ranged.Value += 20;
            }
            if (Validator.IsTanker(member))
            {
                // 체력 1000 증가
                member.Stat.MaxHp += 1000;
                member.Stat.Hp += 1000;
            }
            else
            {
                // 체력 500 증가
                member.Stat.MaxHp += 500;
                member.Stat.Hp += 500;
            }
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
            if (Validator.IsTanker(member))
            {
                // 체력 1000 감소
                member.Stat.MaxHp = (ushort)Math.Max(0, (int)member.Stat.MaxHp - 1000);
                member.Stat.Hp = (ushort)Math.Max(0, (int)member.Stat.Hp - 1000);
            }
            else
            {
                // 체력 500 감소
                member.Stat.MaxHp = (ushort)Math.Max(0, (int)member.Stat.MaxHp - 500);
                member.Stat.Hp = (ushort)Math.Max(0, (int)member.Stat.Hp - 500);
            }
        }
    }
}
