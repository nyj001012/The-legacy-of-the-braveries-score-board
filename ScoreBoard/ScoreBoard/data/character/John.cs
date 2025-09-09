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
    internal class John : CorpsMember
    {
        public John(string id) : base()
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
                    "거 좋은 거요 좋은 거" => () => skill.isActivated = true,
                    "일단 그분께서 지켜주신다 그러라네" => () => skill.isActivated = true,
                    _ => null
                };

                skill.Deactivate = p.Name switch
                {
                    "거 좋은 거요 좋은 거" => () => skill.isActivated = false,
                    "일단 그분께서 지켜주신다 그러라네" => () => skill.isActivated = false,
                    _ => null
                };

                return skill;
            }).ToList() ?? [];
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
                    "예로부터..." => () => skill.isOnCooldown = true,
                    "-다운 게인" => () =>
                    {
                        skill.isOnCooldown = true;
                        ActivateFromOldTimes();
                    }
                    ,
                    "시원한 방" => () => skill.isOnCooldown = true,
                    _ => null
                };
                return skill;
            }).ToList() ?? [];
        }

        /*
         * ActivateFromOldTimes()
         * - 예로부터 활성화
         */
        private void ActivateFromOldTimes()
        {
            ActiveSkill? skill = this.Actives.Find(a => a.Name == "예로부터...");
            if (skill != null)
            {
                skill.isOnCooldown = false;
                skill.CurrentCooldown = 0;
            }
        }
    }
}
