using ScoreBoard.data.character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data.artifact
{
    internal class Robe : Artifact
    {
        public Robe()
        {
        }
        public override void Equip(UnitBase member)
        {
            // 주문력 300 증가
            if (member.Stat.SpellPower.HasValue)
            {
                member.Stat.SpellPower = (ushort?)(member.Stat.SpellPower.Value + 300);
            }
        }
        public override void Unequip(UnitBase member)
        {
            // 주문력 300 감소
            if (member.Stat.SpellPower.HasValue)
            {
                member.Stat.SpellPower = (ushort?)(Math.Max(0, member.Stat.SpellPower.Value - 300));
            }
        }
    }
}
