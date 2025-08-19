using ScoreBoard.data.character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data.artifact
{
    internal class Suit : Artifact
    {
        public Suit()
        {
        }

        public override void Equip(CorpsMember member)
        {
            // 공격력 50, 공격 속도 1 증가
            if (member.Stat.CombatStats.TryGetValue("melee", out var melee))
            {
                melee.Value += 50;
                melee.AttackCount = (ushort)Math.Max(0, (int)melee.AttackCount + 1);
            }
            if (member.Stat.CombatStats.TryGetValue("ranged", out var ranged))
            {
                ranged.Value += 50;
                ranged.AttackCount = (ushort)Math.Max(0, (int)ranged.AttackCount + 1);
            }
        }

        public override void Unequip(CorpsMember member)
        {
            // 공격력 50, 공격 속도 1 감소
            if (member.Stat.CombatStats.TryGetValue("melee", out var melee))
            {
                melee.Value = (ushort)Math.Max(0, (int)melee.Value - 50);
                melee.AttackCount = (ushort)Math.Max(0, (int)melee.AttackCount - 1);
            }
            else if (member.Stat.CombatStats.TryGetValue("ranged", out var ranged))
            {
                ranged.Value = (ushort)Math.Max(0, (int)ranged.Value - 50);
                ranged.AttackCount = (ushort)Math.Max(0, (int)ranged.AttackCount - 1);
            }
        }
    }
}
