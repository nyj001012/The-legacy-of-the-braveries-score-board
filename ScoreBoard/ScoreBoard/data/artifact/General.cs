using ScoreBoard.data.character;
using ScoreBoard.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data.artifact
{
    internal class General : Artifact
    {
        private int powerIncrease = 0; // 공격력 증가량

        public General()
        {
        }
        public override void Equip(UnitBase member)
        {
            powerIncrease = Validator.IsDealer(member) ? 300 : 100;

            if (member.Stat.CombatStats.TryGetValue("melee", out var melee))
            {
                melee.Value += (ushort)powerIncrease;
            }
            if (member.Stat.CombatStats.TryGetValue("ranged", out var ranged))
            {
                ranged.Value += (ushort)powerIncrease;
            }
        }

        public override void Unequip(UnitBase member)
        {
            // 공격력 300 감소
            if (member.Stat.CombatStats.TryGetValue("melee", out var melee))
            {
                melee.Value = (ushort)Math.Max(0, (int)melee.Value - powerIncrease);
            }
            if (member.Stat.CombatStats.TryGetValue("ranged", out var ranged))
            {
                ranged.Value = (ushort)Math.Max(0, (int)ranged.Value - powerIncrease);
            }
        }
    }
}
