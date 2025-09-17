﻿using ScoreBoard.data.character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data.artifact
{
    internal class Shirt : Artifact
    {
        public Shirt()
        {
        }

        public override void Equip(UnitBase member)
        {
            // 공격력 50 증가
            if (member.Stat.CombatStats.TryGetValue("melee", out var melee))
            {
                melee.Value += 50;
            }
            if (member.Stat.CombatStats.TryGetValue("ranged", out var ranged))
            {
                ranged.Value += 50;
            }
            // 체력 200 증가
            member.Stat.MaxHp += 200;
            member.Stat.Hp += 200;
        }

        public override void Unequip(UnitBase member)
        {
            // 공격력 50 감소
            if (member.Stat.CombatStats.TryGetValue("melee", out var melee))
            {
                melee.Value = (ushort)Math.Max(0, (int)melee.Value - 50);
            }
            else if (member.Stat.CombatStats.TryGetValue("ranged", out var ranged))
            {
                ranged.Value = (ushort)Math.Max(0, (int)ranged.Value - 50);
            }
            // 체력 200 감소
            member.Stat.MaxHp = (ushort)Math.Max(0, (int)member.Stat.MaxHp - 200);
            member.Stat.Hp = (ushort)Math.Max(0, (int)member.Stat.Hp - 200);
        }
    }
}
