﻿using ScoreBoard.data.character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data.artifact
{
    internal class HBGRifle : Artifact
    {
        public HBGRifle()
        {
        }

        public override void Equip(UnitBase member)
        {
            // 공격력 150, 공격 속도 2 증가
            if (member.Stat.CombatStats.TryGetValue("melee", out var melee))
            {
                melee.Value += 150;
                melee.AttackCount = (ushort)Math.Max(0, (int)melee.AttackCount + 2);
            }
            if (member.Stat.CombatStats.TryGetValue("ranged", out var ranged))
            {
                ranged.Value += 150;
                ranged.AttackCount = (ushort)Math.Max(0, (int)ranged.AttackCount + 2);
            }
        }

        public override void Unequip(UnitBase member)
        {
            // 공격력 150, 공격 속도 2 감소
            if (member.Stat.CombatStats.TryGetValue("melee", out var melee))
            {
                melee.Value = (ushort)Math.Max(0, (int)melee.Value - 150);
                melee.AttackCount = (ushort)Math.Max(0, (int)melee.AttackCount - 2);
            }
            else if (member.Stat.CombatStats.TryGetValue("ranged", out var ranged))
            {
                ranged.Value = (ushort)Math.Max(0, (int)ranged.Value - 150);
                ranged.AttackCount = (ushort)Math.Max(0, (int)ranged.AttackCount - 2);
            }
        }
    }
}
