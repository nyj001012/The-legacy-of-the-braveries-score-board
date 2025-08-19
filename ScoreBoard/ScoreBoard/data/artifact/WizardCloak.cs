using ScoreBoard.data.character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data.artifact
{
    internal class WizardCloak : Artifact
    {
        private int spPowerIncrease = 0; // 주문력 증가량

        public WizardCloak()
        {
        }

        public override void Equip(CorpsMember member)
        {
            // 주문력 500, 2배 증가
            if (member.Stat.SpellPower.HasValue)
            {
                spPowerIncrease = (member.Stat.SpellPower.Value + 500) * 2 - member.Stat.SpellPower.Value;
                member.Stat.SpellPower  = (ushort?)(member.Stat.SpellPower.Value + spPowerIncrease);
            }

            // 체력 300 증가
            member.Stat.MaxHp += 300;
            member.Stat.Hp += 300;
        }

        public override void Unequip(CorpsMember member)
        {
            // 주문력 500, 2배 감소
            if (member.Stat.SpellPower.HasValue && spPowerIncrease > 0)
            {
                member.Stat.SpellPower = (ushort?)(Math.Max(0, member.Stat.SpellPower.Value - spPowerIncrease));
                spPowerIncrease = 0; // 초기화
            }

            // 체력 300 감소
            member.Stat.MaxHp = (ushort)Math.Max(0, member.Stat.MaxHp - 300);
            member.Stat.Hp = (ushort)Math.Max(0, member.Stat.Hp - 300);
        }
    }
}
