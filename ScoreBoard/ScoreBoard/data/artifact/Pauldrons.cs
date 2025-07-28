using ScoreBoard.data.character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data.artifact
{
    internal class Pauldrons : Artifact
    {
        public Pauldrons()
        {
        }
        public override void Equip(CorpsMember member)
        {
            // 주문력 400 증가
            if (member.Stat.SpellPower.HasValue)
            {
                member.Stat.SpellPower = (ushort?)(member.Stat.SpellPower.Value + 400);
            }
        }
        public override void Unequip(CorpsMember member)
        {
            // 주문력 400 감소
            if (member.Stat.SpellPower.HasValue)
            {
                member.Stat.SpellPower = (ushort?)(Math.Max(0, member.Stat.SpellPower.Value - 400));
            }
        }
    }
}
