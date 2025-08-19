using ScoreBoard.data.character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data.artifact
{
    internal class MagicalHat : Artifact
    {
        public MagicalHat()
        {
        }
        public override void Equip(CorpsMember member)
        {
            // 주문력 700 증가
            if (member.Stat.SpellPower.HasValue)
            {
                member.Stat.SpellPower = (ushort?)(member.Stat.SpellPower.Value + 700);
                member.ArtifactSpellPowerMultiplier *= 2;
            }
        }
        public override void Unequip(CorpsMember member)
        {
            // 주문력 700 감소
            if (member.Stat.SpellPower.HasValue)
            {
                member.Stat.SpellPower = (ushort?)(Math.Max(0, member.Stat.SpellPower.Value - 700));
                member.ArtifactSpellPowerMultiplier /= 2;
            }
        }
    }
}
