using ScoreBoard.data.character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data.artifact
{
    internal class Valhalla : Artifact
    {
        public Valhalla()
        {
        }

        public override void Equip(UnitBase member)
        {
            // 체력 600 증가
            member.Stat.MaxHp += 600;
            member.Stat.Hp += 600;

            // 이동 속도 +1
            member.Stat.Movement++;
        }

        public override void Unequip(UnitBase member)
        {
            // 체력 600 감소
            member.Stat.MaxHp = (ushort)Math.Max(0, member.Stat.MaxHp - 600);
            member.Stat.Hp = (ushort)Math.Max(0, member.Stat.Hp - 600);

            // 이동 속도 -1
            member.Stat.Movement = (ushort)Math.Max(0, member.Stat.Movement - 1);
        }
    }
}
