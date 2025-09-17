using ScoreBoard.data.skill;
using ScoreBoard.data.stat;
using ScoreBoard.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data.character
{
    internal class Maximus : CorpsMember
    {
        public Maximus(string id) : base()
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
                    "경호 임무" => () => skill.isActivated = true,
                    "사자의 창술" => () => skill.isActivated = true,
                    "근위대" => () => skill.isActivated = true,
                    "월급인상" => () =>
                    {
                        skill.isActivated = true;
                        GetAPayRise();
                    }
                    ,
                    _ => null
                };

                skill.Deactivate = p.Name switch
                {
                    "경호 임무" => () => skill.isActivated = false,
                    "사자의 창술" => () => skill.isActivated = false,
                    "근위대" => () => skill.isActivated = false,
                    "월급인상" => () =>
                    {
                        skill.isActivated = false;
                        GetAPayCut();
                    }
                    ,
                    _ => null
                };

                return skill;
            }).ToList() ?? [];
        }

        /*
         * GetAPayCut()
         * - 월급 삭감 패시브
         * - 체력 - 300, 공격력 -100
         */
        private void GetAPayCut()
        {
            this.Stat.Hp = (ushort)Math.Max(0, this.Stat.Hp - 300);
            this.Stat.MaxHp = (ushort)Math.Max(0, this.Stat.MaxHp - 300);
            this.Stat.CombatStats["melee"].Value = (ushort)Math.Max(0, this.Stat.CombatStats["melee"].Value - 100);
        }

        /*
         * GetAPayRise()
         * - 월급 인상 패시브
         * - 체력 + 300, 공격력 +100
         */
        private void GetAPayRise()
        {
            this.Stat.Hp += 300;
            this.Stat.MaxHp += 300;
            this.Stat.CombatStats["melee"].Value += 100;
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
                    "오만한 대처" => () => skill.isOnCooldown = true,
                    _ => null
                };
                return skill;
            }).ToList() ?? [];
        }
    }
}
