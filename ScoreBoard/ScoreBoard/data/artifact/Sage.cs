using ScoreBoard.data.character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data.artifact
{
    internal class Sage : Artifact
    {
        public Sage()
        {
        }
        public override void Equip(CorpsMember member)
        {
            // 주문력 400 증가 및 2배
            if (member.Stat.SpellPower.HasValue)
            {
                member.ArtifactSpellPowerAdder += 400;
                member.ArtifactSpellPowerMultiplier *= 2;
            }
        }
        public override void Unequip(CorpsMember member)
        {
            // 주문력 400 감소 및 반절
            if (member.Stat.SpellPower.HasValue)
            {
                member.ArtifactSpellPowerAdder = Math.Max(0, member.ArtifactSpellPowerAdder - 400);
                member.ArtifactSpellPowerMultiplier = Math.Max(1, member.ArtifactSpellPowerMultiplier / 2);
            }
        }
    }
}
