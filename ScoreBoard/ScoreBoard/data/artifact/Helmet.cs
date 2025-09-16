using ScoreBoard.data.character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data.artifact
{
    internal class Helmet : Artifact
    {
        public Helmet()
        {
        }

        public override void Equip(UnitBase member)
        {
            // 체력 400 증가
            member.Stat.Hp += 400;
            member.Stat.MaxHp += 400;

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
            // 체력 400 감소
            member.Stat.Hp = (ushort)Math.Max(0, member.Stat.Hp - 400);
            member.Stat.MaxHp = (ushort)Math.Max(0, member.Stat.MaxHp - 400);

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
