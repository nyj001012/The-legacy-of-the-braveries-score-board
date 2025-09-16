using ScoreBoard.data.character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data.artifact
{
    internal class Aegis : Artifact
    {
        public Aegis()
        {
        }

        public override void Equip(UnitBase unit)
        {
            // 체력 400 증가
            unit.Stat.MaxHp += 400;
            unit.Stat.Hp += 400;

            // 지혜 1 증가
            if (unit.Stat.Wisdom.HasValue)
            {
                unit.Stat.Wisdom = (ushort?)(unit.Stat.Wisdom.Value + 1);
            }

            // 스킬 쿨다운 -1
            foreach (var skill in unit.Actives)
            {
                skill.Cooldown = (ushort)Math.Max(0, skill.Cooldown - 1);
                skill.CurrentCooldown = (ushort)Math.Max(0, skill.CurrentCooldown - 1);
                skill.isOnCooldown = skill.Cooldown > 0;
            }
        }

        public override void Unequip(UnitBase unit)
        {
            // 체력 400 감소
            unit.Stat.MaxHp = (ushort)Math.Max(0, unit.Stat.MaxHp - 400);
            unit.Stat.Hp = (ushort)Math.Max(0, unit.Stat.Hp - 400);

            // 주문력 300 감소
            if (unit.Stat.Wisdom.HasValue)
            {
                unit.Stat.Wisdom = (ushort?)(Math.Max(0, unit.Stat.Wisdom.Value - 1));
            }

            // 스킬 쿨다운 +1
            foreach (var skill in unit.Actives)
            {
                skill.Cooldown = (ushort)(skill.Cooldown + 1);
                skill.CurrentCooldown = (ushort)(skill.CurrentCooldown + 1);
                skill.isOnCooldown = skill.Cooldown > 0;
            }
        }
    }
}
