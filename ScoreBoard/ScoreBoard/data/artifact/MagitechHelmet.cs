using ScoreBoard.data.character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data.artifact
{
    internal class MagitechHelmet : Artifact
    {
        public MagitechHelmet()
        {
        }
        public override void Equip(UnitBase member)
        {
            // 주문력 400 증가
            if (member.Stat.SpellPower.HasValue)
            {
                member.Stat.SpellPower = (ushort?)(member.Stat.SpellPower.Value + 400);
            }

            // 이동 속도 +1
            member.Stat.Movement++;
        }
        public override void Unequip(UnitBase member)
        {
            // 주문력 400 감소
            if (member.Stat.SpellPower.HasValue)
            {
                member.Stat.SpellPower = (ushort?)(Math.Max(0, member.Stat.SpellPower.Value - 400));
            }

            // 이동 속도 -1
            member.Stat.Movement = (ushort)Math.Max(0, member.Stat.Movement - 1);
        }
    }
}
