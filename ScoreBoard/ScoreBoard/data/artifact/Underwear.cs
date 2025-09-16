using ScoreBoard.data.character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data.artifact
{
    internal class Underwear : Artifact
    {
        public Underwear()
        {
        }
        public override void Equip(UnitBase member)
        {
            // 주문력 100 증가
            if (member.Stat.SpellPower.HasValue)
            {
                member.Stat.SpellPower = (ushort?)(member.Stat.SpellPower.Value + 100);
            }
        }
        public override void Unequip(UnitBase member)
        {
            // 주문력 100 감소
            if (member.Stat.SpellPower.HasValue)
            {
                member.Stat.SpellPower = (ushort?)(Math.Max(0, member.Stat.SpellPower.Value - 100));
            }
        }
    }
}
