using ScoreBoard.data.character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data.artifact
{
    internal class CeremonialSword : Artifact
    {
        public CeremonialSword()
        {
        }

        public override void Equip(CorpsMember member)
        {
            // 공격력 60 증가
            if (member.Stat.CombatStats.TryGetValue("melee", out var melee))
            {
                melee.Value += 60;
            }
            if (member.Stat.CombatStats.TryGetValue("ranged", out var ranged))
            {
                ranged.Value += 60;
            }
        }

        public override void Unequip(CorpsMember member)
        {
            // 공격력 60 감소
            if (member.Stat.CombatStats.TryGetValue("melee", out var melee))
            {
                melee.Value = (ushort)Math.Max(0, (int)melee.Value - 60);
            }
            else if (member.Stat.CombatStats.TryGetValue("ranged", out var ranged))
            {
                ranged.Value = (ushort)Math.Max(0, (int)ranged.Value - 60);
            }
        }
    }
}
