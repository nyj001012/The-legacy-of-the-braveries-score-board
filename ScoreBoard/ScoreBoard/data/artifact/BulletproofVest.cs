using ScoreBoard.data.character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data.artifact
{
    internal class BulletproofVest : Artifact
    {
        public BulletproofVest()
        {
        }

        public override void Equip(UnitBase unit)
        {
            // 체력 500 증가
            unit.Stat.MaxHp += 500;
            unit.Stat.Hp += 500;
        }

        public override void Unequip(UnitBase unit)
        {
            // 체력 500 감소
            unit.Stat.MaxHp = (ushort)Math.Max(0, unit.Stat.MaxHp - 500);
            unit.Stat.Hp = (ushort)Math.Max(0, unit.Stat.Hp - 500);
        }
    }
}
