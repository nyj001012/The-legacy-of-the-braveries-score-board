using ScoreBoard.data.character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data.artifact
{
    internal class Headband : Artifact
    {
        public Headband()
        {
        }
        public override void Equip(CorpsMember member)
        {
            // 주문력 50 증가
            if (member.Stat.SpellPower.HasValue)
            {
                member.Stat.SpellPower = (ushort?)(member.Stat.SpellPower.Value + 50);
            }
        }
        public override void Unequip(CorpsMember member)
        {
            // 주문력 50 감소
            if (member.Stat.SpellPower.HasValue)
            {
                member.Stat.SpellPower = (ushort?)(Math.Max(0, member.Stat.SpellPower.Value - 50));
            }
        }
    }
}
