using ScoreBoard.data.character;
using ScoreBoard.data.statusEffect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data.artifact
{
    internal class Harun : Artifact
    {
        public Harun()
        {
        }

        public override void Equip(CorpsMember member)
        {
            // 공격력 60 증가
            if (member.Stat.CombatStats.TryGetValue("melee", out var melee))
            {
                melee.Value += 60;
            }
            if (member.Stat.CombatStats.TryGetValue("ranged", out var ranged))
            {
                ranged.Value += 60;
            }
            // 체력 200 증가
            member.Stat.MaxHp += 200;
            member.Stat.Hp += 200;

            // 부활 버프 추가
            member.Stat.StatusEffects.Add(new StatusEffect(
                StatusEffectType.Resurrection,
                -1
            ));
        }

        public override void Unequip(CorpsMember member)
        {
            // 공격력 60 감소
            if (member.Stat.CombatStats.TryGetValue("melee", out var melee))
            {
                melee.Value = (ushort)Math.Max(0, (int)melee.Value - 60);
            }
            else if (member.Stat.CombatStats.TryGetValue("ranged", out var ranged))
            {
                ranged.Value = (ushort)Math.Max(0, (int)ranged.Value - 60);
            }
            // 체력 200 감소
            member.Stat.MaxHp = (ushort)Math.Max(0, (int)member.Stat.MaxHp - 200);
            member.Stat.Hp = (ushort)Math.Max(0, (int)member.Stat.Hp - 200);

            // 부활 버프 제거
            StatusEffect? effect = member.Stat.StatusEffects.FirstOrDefault(e => e.Type == StatusEffectType.Resurrection && e.IsInfinite);
            if (effect != default)
            {
                member.Stat.StatusEffects.Remove(effect);
            }
        }
    }
}
