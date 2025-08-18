using ScoreBoard.data.character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data.artifact
{
    internal class AscensionStone : Artifact
    {
        public AscensionStone()
        {
        }

        public override void Equip(CorpsMember member)
        {
            // 체력 800 증가
            member.Stat.MaxHp += 800;
            member.Stat.Hp += 800;
        }

        public override void Unequip(CorpsMember member)
        {
            // 체력 800 감소
            member.Stat.MaxHp = (ushort)Math.Max(0, member.Stat.MaxHp - 800);
            member.Stat.Hp = (ushort)Math.Max(0, member.Stat.Hp - 800);
        }
    }
}
