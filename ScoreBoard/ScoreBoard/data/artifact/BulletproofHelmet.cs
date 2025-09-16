using ScoreBoard.data.character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data.artifact
{
    internal class BulletproofHelmet : Artifact
    {
        public BulletproofHelmet()
        {
        }

        public override void Equip(UnitBase unit)
        {
            // 체력 +200
            unit.Stat.Hp += 200;
            unit.Stat.MaxHp += 200;
        }

        public override void Unequip(UnitBase unit)
        {
            // 체력 -200
            unit.Stat.MaxHp = (ushort)(Math.Max(0, unit.Stat.MaxHp - 200));
            unit.Stat.Hp = (ushort)(Math.Max(0, unit.Stat.Hp - 200));
        }
    }
}
