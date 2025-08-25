using ScoreBoard.data.character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data.artifact
{
    internal class PetitShieldClothes : Artifact
    {
        public PetitShieldClothes()
        {
        }

        public override void Equip(CorpsMember member)
        {
            // 체력 300 증가
            member.Stat.MaxHp += 300;
            member.Stat.Hp += 300;
        }

        public override void Unequip(CorpsMember member)
        {
            // 체력 300 감소
            member.Stat.MaxHp = (ushort)Math.Max(0, member.Stat.MaxHp - 300);
            member.Stat.Hp = (ushort)Math.Max(0, member.Stat.Hp - 300);
        }
    }
}
