using ScoreBoard.data.character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data.artifact
{
    internal class Bandage : Artifact
    {
        public Bandage()
        {
        }
        public override void Equip(UnitBase unit)
        {
            // 주문력 100 증가
            if (unit.Stat.SpellPower.HasValue)
            {
                unit.Stat.SpellPower = (ushort?)(unit.Stat.SpellPower.Value + 100);
            }
        }
        public override void Unequip(UnitBase unit)
        {
            // 주문력 100 감소
            if (unit.Stat.SpellPower.HasValue)
            {
                unit.Stat.SpellPower = (ushort?)(Math.Max(0, unit.Stat.SpellPower.Value - 100));
            }
        }
    }
}
