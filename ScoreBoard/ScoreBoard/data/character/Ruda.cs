using ScoreBoard.data.skill;
using ScoreBoard.data.stat;
using ScoreBoard.utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

namespace ScoreBoard.data.character
{
    internal class Ruda : CorpsMember
    {
        public ushort PerfectionBonus = 0;

        public Ruda(string id) : base()
        {
            Initialise(id);
        }

        protected override void InitialisePasssiveSkills(CorpsMember data)
        {
            Validator.ValidateNull(data.Passives, nameof(data.Passives));
            Passives = data.Passives?.Select(p =>
            {
                var skill = new PassiveSkill()
                {
                    Name = Validator.ValidateNull(p.Name, nameof(p.Name)),
                    RequiredLevel = p.RequiredLevel,
                    Description = Validator.ValidateNull(p.Description, nameof(p.Description))
                };

                skill.Activate = p.Name switch
                {
                    "다재다능" => () => skill.isActivated = true,
                    "집행자" => () => skill.isActivated = true,
                    "완벽주의" => () =>
                    {
                        skill.isActivated = true;
                        ActivatePerfectionism();
                    }
                    ,
                    _ => null
                };

                skill.Deactivate = p.Name switch
                {
                    "다재다능" => () => skill.isActivated = false,
                    "집행자" => () => skill.isActivated = false,
                    "완벽주의" => () =>
                    {
                        skill.isActivated = false;
                        DeactivatePerfectionism();
                    }
                    ,
                    _ => null
                };

                return skill;
            }).ToList() ?? [];
        }

        protected override void InitialiseActiveSkills(CorpsMember data)
        {
            Validator.ValidateNull(data.Actives, nameof(data.Actives));
            Actives = data.Actives?.Select(a =>
            {
                var skill = new ActiveSkill()
                {
                    Name = Validator.ValidateNull(a.Name, nameof(a.Name)),
                    RequiredLevel = a.RequiredLevel,
                    Description = Validator.ValidateNull(a.Description, nameof(a.Description)),
                    Cooldown = a.Cooldown,
                    isOnCooldown = false,
                };

                skill.Execute = a.Name switch
                {
                    "폭발 탄환" => () => skill.isOnCooldown = true,
                    "결정타" => () => skill.isOnCooldown = true,
                    "신성한 결투" => () => skill.isOnCooldown = true,
                    _ => null
                };
                return skill;
            }).ToList() ?? [];
        }

        /*
         * ActivatePerfectionism()
         * 완벽주의 패시브 스킬을 활성화하는 메서드
         * - 장착한 유물의 개수만큼 attackCount 증가
         */
        private void ActivatePerfectionism()
        {
            for (int i = 0; i < MaxArtifactSlot; i++)
            {
                if (this.ArtifactSlot.ElementAtOrDefault(i) != default)
                    PerfectionBonus++;
            }
            this.Stat.CombatStats["melee"].AttackCount += PerfectionBonus;
            this.Stat.CombatStats["ranged"].AttackCount += PerfectionBonus;
        }

        /*
         * DeactivatePerfectionism()
         * - 완벽주의 패시브 스킬을 비활성화하는 메서드
         * - 증가했던 attackCount만큼 감소
         */
        private void DeactivatePerfectionism()
        {
            ushort oldMeleeCount = this.Stat.CombatStats["melee"].AttackCount;
            ushort oldRangedCount = this.Stat.CombatStats["ranged"].AttackCount;

            this.Stat.CombatStats["melee"].AttackCount = (ushort)Math.Max(0, oldMeleeCount - PerfectionBonus);
            this.Stat.CombatStats["ranged"].AttackCount = (ushort)Math.Max(0, oldRangedCount - PerfectionBonus);
            PerfectionBonus = 0;
        }
    }
}
