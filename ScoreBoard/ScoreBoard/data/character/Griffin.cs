using ScoreBoard.data.artifact;
using ScoreBoard.data.skill;
using ScoreBoard.data.stat;
using ScoreBoard.utils;

namespace ScoreBoard.data.character
{
    internal class Griffin : CorpsMember
    {
        public ushort blackMagicCount = 0; // 흑마법 사용 횟수 카운트

        public Griffin(string id)
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
                    "전투기술(패시브)" => () => skill.isActivated = true,
                    "아퀼론 아머" => () => skill.isActivated = true,
                    "장비 보강" => () =>
                    {
                        skill.isActivated = true;
                        UpgradeGear();
                    }
                    ,
                    "장비 완화" => () => skill.isActivated = true,
                    _ => null
                };

                skill.Deactivate = p.Name switch
                {
                    "전투기술(패시브)" => () => skill.isActivated = false,
                    "아퀼론 아머" => () => skill.isActivated = false,
                    "장비 보강" => () =>
                    {
                        skill.isActivated = false;
                        DowngradeGear();
                    }
                    ,
                    "장비 완화" => () => skill.isActivated = false,
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
                    "전투기술(사용 기술)" => () => skill.isOnCooldown = true,
                    "분노의 빨간버튼" => () => skill.isOnCooldown = true,
                    _ => null
                };
                return skill;
            }).ToList() ?? [];
        }

        /*
         * 장비 보강 활성화
         * - 악세사리 슬롯 1개 추가
         */
        private void UpgradeGear()
        {
            this.MaxArtifactSlot++;
            this.ArtifactSlot.Add(null); // 새로운 슬롯은 null로 초기화
        }

        /*
         * 장비 보강 비활성화
         * - 악세사리 슬롯 1개 제거
         * - 슬롯에 아이템이 있다면 제거
         */
        private void DowngradeGear()
        {
            Artifact? artifact = this.ArtifactSlot.ElementAtOrDefault(3);
            if (artifact != default)
            {
                artifact.Unequip(this);
            }
            this.ArtifactSlot.RemoveAt(this.ArtifactSlot.Count - 1); // 마지막 슬롯 제거
            this.MaxArtifactSlot--; // 최대 슬롯 수 감소
        }
    }
}
