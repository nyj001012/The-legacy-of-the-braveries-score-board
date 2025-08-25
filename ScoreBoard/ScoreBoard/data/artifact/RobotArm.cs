using ScoreBoard.data.character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data.artifact
{
    internal class RobotArm : Artifact
    {
        public RobotArm()
        {
        }

        public override void Equip(CorpsMember member)
        {
            // 공격력 80 증가
            if (member.Stat.CombatStats.TryGetValue("melee", out var melee))
            {
                melee.Value += 80;
            }
            if (member.Stat.CombatStats.TryGetValue("ranged", out var ranged))
            {
                ranged.Value += 80;
            }
            // 체력 400 증가
            member.Stat.MaxHp += 400;
            member.Stat.Hp += 400;
        }

        public override void Unequip(CorpsMember member)
        {
            // 공격력 80 감소
            if (member.Stat.CombatStats.TryGetValue("melee", out var melee))
            {
                melee.Value = (ushort)Math.Max(0, (int)melee.Value - 80);
            }
            else if (member.Stat.CombatStats.TryGetValue("ranged", out var ranged))
            {
                ranged.Value = (ushort)Math.Max(0, (int)ranged.Value - 80);
            }
            // 체력 400 감소
            member.Stat.MaxHp = (ushort)Math.Max(0, (int)member.Stat.MaxHp - 400);
            member.Stat.Hp = (ushort)Math.Max(0, (int)member.Stat.Hp - 400);
        }
    }
}
