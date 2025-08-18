using ScoreBoard.data.character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data.artifact
{
    internal class Branch : Artifact
    {
        public Branch()
        {
        }

        public override void Equip(CorpsMember member)
        {
            // 주문력 50 증가
            if (member.Stat.SpellPower.HasValue)
            {
                member.Stat.SpellPower = (ushort?)(member.Stat.SpellPower.Value + 50);
            }
        }

        public override void Unequip(CorpsMember member)
        {
            if (member.Stat.SpellPower.HasValue)
            {
                // 주문력 50 감소
                member.Stat.SpellPower = (ushort?)(Math.Max(0, member.Stat.SpellPower.Value - 50));
            }
        }

        public override void TriggerEffectsOnActionEnd(CorpsMember member)
        {
            // 체력 50 회복
            member.Stat.Hp = (ushort)Math.Min(member.Stat.MaxHp, member.Stat.Hp + 50);
        }
    }
}
