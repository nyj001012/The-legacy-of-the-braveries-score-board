using ScoreBoard.data.character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data.artifact
{
    internal class Icebox : Artifact
    {
        public Icebox()
        {
        }

        public override void Equip(UnitBase member)
        {
            // 체력 500 증가
            member.Stat.MaxHp = (ushort)(member.Stat.MaxHp + 500);
            member.Stat.Hp = (ushort)(member.Stat.Hp + 500);
        }
        public override void Unequip(UnitBase member)
        {
            // 체력 500 감소
            member.Stat.MaxHp = (ushort)(Math.Max(0, member.Stat.MaxHp - 500));
            member.Stat.Hp = (ushort)(Math.Max(0, member.Stat.Hp - 500));
        }
    }
}
