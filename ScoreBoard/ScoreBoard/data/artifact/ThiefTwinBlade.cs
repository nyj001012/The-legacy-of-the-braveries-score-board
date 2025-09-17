﻿using ScoreBoard.data.character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data.artifact
{
    internal class ThiefTwinBlade : Artifact
    {
        public ThiefTwinBlade()
        {
        }

        public override void Equip(UnitBase member)
        {
            // 공격력 30 증가
            if (member.Stat.CombatStats.TryGetValue("melee", out var melee))
            {
                melee.Value += 30;
            }
            if (member.Stat.CombatStats.TryGetValue("ranged", out var ranged))
            {
                ranged.Value += 30;
            }

            // 이동 속도 1 증가
            member.Stat.Movement++;
        }

        public override void Unequip(UnitBase member)
        {
            // 공격력 30 감소
            if (member.Stat.CombatStats.TryGetValue("melee", out var melee))
            {
                melee.Value = (ushort)Math.Max(0, (int)melee.Value - 30);
            }
            else if (member.Stat.CombatStats.TryGetValue("ranged", out var ranged))
            {
                ranged.Value = (ushort)Math.Max(0, (int)ranged.Value - 30);
            }

            // 이동 속도 1 감소
            member.Stat.Movement = (ushort)Math.Max(0, (int)member.Stat.Movement - 1);
        }
    }
}
