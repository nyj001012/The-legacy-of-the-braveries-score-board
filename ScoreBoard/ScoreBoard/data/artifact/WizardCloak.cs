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
        public WizardCloak()
        {
        }

        public override void Equip(UnitBase member)
        {
            // 주문력 500, 2배 증가
            if (member.Stat.SpellPower.HasValue)
            {
                member.ArtifactSpellPowerMultiplier *= 2;
                member.Stat.SpellPower  = (ushort?)(member.Stat.SpellPower.Value + 500);
            }

            // 체력 300 증가
            member.Stat.MaxHp += 300;
            member.Stat.Hp += 300;
        }

        public override void Unequip(UnitBase member)
        {
            // 주문력 500, 2배 감소
            if (member.Stat.SpellPower.HasValue)
            {
                member.ArtifactSpellPowerMultiplier /= 2;
                member.Stat.SpellPower = (ushort?)(Math.Max(0, member.Stat.SpellPower.Value - 500));
            }

            // 체력 300 감소
            member.Stat.MaxHp = (ushort)Math.Max(0, member.Stat.MaxHp - 300);
            member.Stat.Hp = (ushort)Math.Max(0, member.Stat.Hp - 300);
        }
    }
}
