using ScoreBoard.data.character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data.artifact
{
    internal class Shield : Artifact
    {
        public Shield()
        {
        }

        public override void Equip(CorpsMember member)
        {
            // 체력 +200
            member.Stat.Hp += 200;
            member.Stat.MaxHp += 200;
        }

        public override void Unequip(CorpsMember member)
        {
            // 체력 -200
            member.Stat.MaxHp = (ushort)(Math.Max(0, member.Stat.MaxHp - 200));
            member.Stat.Hp = (ushort)(Math.Max(0, member.Stat.Hp - 200));
        }
    }
}
