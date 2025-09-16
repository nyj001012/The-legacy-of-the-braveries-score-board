using ScoreBoard.data.character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data.artifact
{
    internal class Branch : Artifact
    {
        public Branch()
        {
        }

        public override void Equip(UnitBase unit)
        {
            // 주문력 50 증가
            if (unit.Stat.SpellPower.HasValue)
            {
                unit.Stat.SpellPower = (ushort?)(unit.Stat.SpellPower.Value + 50);
            }
        }

        public override void Unequip(UnitBase unit)
        {
            if (unit.Stat.SpellPower.HasValue)
            {
                // 주문력 50 감소
                unit.Stat.SpellPower = (ushort?)(Math.Max(0, unit.Stat.SpellPower.Value - 50));
            }
        }

        public override void TriggerEffectsOnActionEnd(UnitBase unit)
        {
            // 체력 50 회복
            unit.Stat.Hp = (ushort)Math.Min(unit.Stat.MaxHp, unit.Stat.Hp + 50);
        }
    }
}
