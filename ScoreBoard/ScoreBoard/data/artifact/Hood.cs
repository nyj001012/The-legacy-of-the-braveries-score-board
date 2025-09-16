using ScoreBoard.data.character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data.artifact
{
    internal class Hood : Artifact
    {
        public Hood()
        {
        }
        public override void Equip(UnitBase member)
        {
            // 주문력 500 증가
            if (member.Stat.SpellPower.HasValue)
            {
                member.Stat.SpellPower = (ushort?)(member.Stat.SpellPower.Value + 500);
            }
        }
        public override void Unequip(UnitBase member)
        {
            // 주문력 500 감소
            if (member.Stat.SpellPower.HasValue)
            {
                member.Stat.SpellPower = (ushort?)(Math.Max(0, member.Stat.SpellPower.Value - 500));
            }
        }
    }
}
