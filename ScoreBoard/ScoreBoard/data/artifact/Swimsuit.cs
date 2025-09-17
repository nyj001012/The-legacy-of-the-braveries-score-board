using ScoreBoard.data.character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data.artifact
{
    internal class Swimsuit : Artifact
    {
        public Swimsuit()
        {
        }

        public override void Equip(UnitBase member)
        {
            // 주문력 200, 2배 증가
            if (member.Stat.SpellPower.HasValue)
            {
                member.Stat.SpellPower = (ushort?)(member.Stat.SpellPower.Value + 200);
            }

            // 체력 100 증가
            member.Stat.MaxHp += 100;
            member.Stat.Hp += 100;
        }

        public override void Unequip(UnitBase member)
        {
            // 주문력 200, 2배 감소
            if (member.Stat.SpellPower.HasValue)
            {
                member.Stat.SpellPower = (ushort?)(Math.Max(0, member.Stat.SpellPower.Value - 200));
            }

            // 체력 100 감소
            member.Stat.MaxHp = (ushort)Math.Max(0, member.Stat.MaxHp - 100);
            member.Stat.Hp = (ushort)Math.Max(0, member.Stat.Hp - 100);
        }
    }
}
