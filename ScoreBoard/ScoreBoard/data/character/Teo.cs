using ScoreBoard.data.skill;
using ScoreBoard.data.stat;
using ScoreBoard.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ScoreBoard.data.character
{
    internal class Teo : CorpsMember
    {
        public Teo(string id) : base()
        {
            Initialise(id);
        }

        protected override void InitialisePasssiveSkills(CorpsMemberDTO data)
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
                    "아티피셔 갑주 --특수 제작된 갑주의 성능을 느껴보세요!" => () => skill.isActivated = true,
                    "햄부거식 위험감지 프로토콜" => () =>
                    {
                        this.Stat.CombatStats["ranged"].Range++;
                        skill.isActivated = true;
                    }
                    ,
                    "전투자극제" => () =>
                    {
                        GetAdrenaline();
                        skill.isActivated = true;
                    }
                    ,
                    _ => null
                };

                skill.Deactivate = p.Name switch
                {
                    "아티피셔 갑주 --특수 제작된 갑주의 성능을 느껴보세요!" => () => skill.isActivated = false,
                    "햄부거식 위험감지 프로토콜" => () =>
                    {
                        this.Stat.CombatStats["ranged"].Range = (ushort)Math.Max(0, this.Stat.CombatStats["ranged"].Range - 1);
                        skill.isActivated = false;
                    }
                    ,
                    "전투자극제" => () =>
                    {
                        LoseAdrenaline();
                        skill.isActivated = false;
                    }
                    ,
                    _ => null
                };

                return skill;
            }).ToList() ?? [];
        }

        /*
         * LoseAdrenaline()
         * - 전투 자극제 패시브
         * - 아드레날린 스킬 쿨타임을 원래대로 되돌림
         */
        private void LoseAdrenaline()
        {
            if (Actives == null || !Actives.Any(a => a.Name == "아드레날린"))
            {
                throw new InvalidOperationException("아드레날린 스킬이 존재하지 않습니다.");
            }
            var adrenalineSkill = Actives.First(a => a.Name == "아드레날린");
            adrenalineSkill.Cooldown = 10; // 쿨타임을 원래대로 되돌림
            adrenalineSkill.CurrentCooldown = (ushort)Math.Min((int)adrenalineSkill.CurrentCooldown, 10); // 현재 쿨타임이 10턴보다 크면 10으로 설정
            adrenalineSkill.isOnCooldown = adrenalineSkill.CurrentCooldown > 0;
        }

        /*
         * GetAdrenaline()
         * - 전투 자극제 패시브
         * - 아드레날린 스킬 쿨타임 5턴으로 감소
         */
        private void GetAdrenaline()
        {
            if (Actives == null || !Actives.Any(a => a.Name == "아드레날린"))
            {
                throw new InvalidOperationException("아드레날린 스킬이 존재하지 않습니다.");
            }
            var adrenalineSkill = Actives.First(a => a.Name == "아드레날린");
            adrenalineSkill.Cooldown = 5; // 쿨타임을 5턴으로 설정
            adrenalineSkill.CurrentCooldown = (ushort)Math.Min((int)adrenalineSkill.CurrentCooldown, 5); // 현재 쿨타임이 5턴보다 크면 5로 설정
            adrenalineSkill.isOnCooldown = adrenalineSkill.CurrentCooldown > 0; // 쿨타임 상태 해제
        }

        protected override void InitialiseActiveSkills(CorpsMemberDTO data)
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
                    "구원자 전술" => () => skill.isOnCooldown = true,
                    _ => null
                };
                return skill;
            }).ToList() ?? [];
        }
    }
}
