using ScoreBoard.data.character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data.artifact
{
    internal class MagicBracelet : Artifact
    {
        public MagicBracelet()
        {
        }

        public override void Equip(UnitBase member)
        {
            // 주문력 300 증가
            if (member.Stat.SpellPower.HasValue)
            {
                member.Stat.SpellPower = (ushort?)(member.Stat.SpellPower.Value + 300);
            }

            // 스킬 쿨다운 -1
            foreach (var skill in member.Actives)
            {
                skill.Cooldown = (ushort)Math.Max(0, skill.Cooldown - 1);
                skill.CurrentCooldown = (ushort)Math.Max(0, skill.CurrentCooldown - 1);
                skill.isOnCooldown = skill.Cooldown > 0;
            }
        }
        public override void Unequip(UnitBase member)
        {
            // 주문력 300 감소
            if (member.Stat.SpellPower.HasValue)
            {
                member.Stat.SpellPower = (ushort?)(Math.Max(0, member.Stat.SpellPower.Value - 300));
            }

            // 스킬 쿨다운 +1
            foreach (var skill in member.Actives)
            {
                skill.Cooldown = (ushort)(skill.Cooldown + 1);
                skill.CurrentCooldown = (ushort)(skill.CurrentCooldown + 1);
                skill.isOnCooldown = skill.Cooldown > 0;
            }
        }
    }
}
