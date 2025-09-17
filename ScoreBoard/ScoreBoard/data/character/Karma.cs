using ScoreBoard.data.artifact;
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
    internal class Karma : CorpsMember
    {
        public Karma(string id) : base()
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
                    "알라루스 갑주" => () => skill.isActivated = true,
                    "아뎁투스 엔진" => () =>
                    {
                        skill.isActivated = true;
                        ActivateAdeptusEngine();
                    }
                    ,
                    "돌 격 전 차" => () => skill.isActivated = true,
                    _ => null
                };

                skill.Deactivate = p.Name switch
                {
                    "알라루스 갑주" => () => skill.isActivated = false,
                    "아뎁투스 엔진" => () =>
                    {
                        skill.isActivated = false;
                        DeactivateAdeptusEngine();
                    }
                    ,
                    "돌 격 전 차" => () => skill.isActivated = false,
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
                    "휩쓸기" => () => skill.isOnCooldown = true,
                    "신중한 전쟁" => () => skill.isOnCooldown = true,
                    _ => null
                };
                return skill;
            }).ToList() ?? [];
        }

        /*
         * ActivateAdeptusEngine()
         * - 아뎁투스 엔진 활성화
         */
        private void ActivateAdeptusEngine()
        {
            this.MaxArtifactSlot++;
            this.ArtifactSlot.Add(null); // 새로운 슬롯은 null로 초기화
        }

        /*
         * DeactivateAdeptusEngine()
         * - 아뎁투스 엔진 비활성화
         */
        private void DeactivateAdeptusEngine()
        {
            Artifact? artifact = this.ArtifactSlot.ElementAtOrDefault(3);
            if (artifact != default)
            {
                artifact.Unequip(this);
            }
            this.ArtifactSlot.RemoveAt(3); // 마지막 슬롯 제거
            this.MaxArtifactSlot--;
        }
    }
}
