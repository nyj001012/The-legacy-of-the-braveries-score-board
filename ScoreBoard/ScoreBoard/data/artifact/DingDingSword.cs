using ScoreBoard.data.character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data.artifact
{
    internal class DingDingSword : Artifact
    {
        public DingDingSword()
        {
        }

        public override void Equip(UnitBase unit)
        {
            // 공격력 50 증가
            if (unit.Stat.CombatStats.TryGetValue("melee", out var melee))
            {
                melee.Value += 50;
            }
            if (unit.Stat.CombatStats.TryGetValue("ranged", out var ranged))
            {
                ranged.Value += 50;
            }
            // 체력 100 증가
            unit.Stat.MaxHp += 100;
            unit.Stat.Hp += 100;
        }

        public override void Unequip(UnitBase unit)
        {
            // 공격력 50 감소
            if (unit.Stat.CombatStats.TryGetValue("melee", out var melee))
            {
                melee.Value = (ushort)Math.Max(0, (int)melee.Value - 50);
            }
            else if (unit.Stat.CombatStats.TryGetValue("ranged", out var ranged))
            {
                ranged.Value = (ushort)Math.Max(0, (int)ranged.Value - 50);
            }
            // 체력 100 감소
            unit.Stat.MaxHp = (ushort)Math.Max(0, (int)unit.Stat.MaxHp - 100);
            unit.Stat.Hp = (ushort)Math.Max(0, (int)unit.Stat.Hp - 100);
        }
    }
}
