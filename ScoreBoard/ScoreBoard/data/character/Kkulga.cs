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
    internal class Kkulga : CorpsMember
    {
        public Kkulga(string id) : base()
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
                    "장군갑주" => () =>
                    {
                        skill.isActivated = true;
                        WearMasterGear();
                    }
                    ,
                    "지휘관" => () => skill.isActivated = true,
                    "내가 누군지 알어???" => () => skill.isActivated = true,
                    "효율적 전략" => () => skill.isActivated = true,
                    "내가 직접 나서야겠어" => () => skill.isActivated = true,
                    _ => null
                };

                skill.Deactivate = p.Name switch
                {
                    "장군갑주" => () =>
                    {
                        skill.isActivated = false;
                        TakeOffMasterGear();
                    }
                    ,
                    "지휘관" => () => skill.isActivated = false,
                    "내가 누군지 알어???" => () => skill.isActivated = false,
                    "효율적 전략" => () => skill.isActivated = false,
                    "내가 직접 나서야겠어" => () => skill.isActivated = false,
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
                    "강타!" => () => skill.isOnCooldown = true,
                    _ => null
                };
                return skill;
            }).ToList() ?? [];
        }

        /*
         * WearMasterGear()
         * - 장군갑주 착용 활성화 시 호출되는 메서드입니다.
         * - 착용 가능한 유물 슬롯을 1개 추가합니다.
         */
        private void WearMasterGear()
        {
            this.MaxArtifactSlot++;
            this.ArtifactSlot = [.. this.ArtifactSlot, null]; // 유물 슬롯을 하나 추가
        }

        /*
         * TakeOffMasterGear()
         * - 장군갑주 착용 비활성화 시 호출되는 메서드입니다.
         * - 착용 가능한 유물 슬롯을 1개 삭제합니다.
         */
        private void TakeOffMasterGear()
        {
            if (this.MaxArtifactSlot > 3) // 기본 슬롯 수는 3개이므로, 그 이상일 때만 제거
            {
                this.MaxArtifactSlot--;
                this.ArtifactSlot = [.. this.ArtifactSlot.Take(this.MaxArtifactSlot)]; // 마지막 슬롯 제거
            }
        }
    }
}
