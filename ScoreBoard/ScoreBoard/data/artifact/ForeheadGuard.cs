using ScoreBoard.data.character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data.artifact
{
    internal class ForeheadGuard : Artifact
    {
        public ForeheadGuard()
        {
        }

        public override void Equip(UnitBase member)
        {
            // 지혜 1 증가
            if (member.Stat.Wisdom.HasValue)
            {
                member.Stat.Wisdom = (ushort?)(member.Stat.Wisdom.Value + 1);
            }

            // 체력 50 증가
            member.Stat.MaxHp = (ushort)(member.Stat.MaxHp + 50);
            member.Stat.Hp = (ushort)(member.Stat.Hp + 50);
        }
        public override void Unequip(UnitBase member)
        {
            // 지혜 1 감소
            if (member.Stat.Wisdom.HasValue)
            {
                member.Stat.Wisdom = (ushort?)(Math.Max(0, member.Stat.Wisdom.Value - 1));
            }
            
            // 체력 50 감소
            member.Stat.MaxHp = (ushort)(Math.Max(0, member.Stat.MaxHp - 50));
            member.Stat.Hp = (ushort)(Math.Max(0, member.Stat.Hp - 50));
        }
    }
}
