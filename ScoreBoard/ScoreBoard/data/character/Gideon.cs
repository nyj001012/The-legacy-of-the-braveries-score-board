using ScoreBoard.data.artifact;
using ScoreBoard.data.skill;
using ScoreBoard.data.stat;
using ScoreBoard.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data.character
{
    internal class Gideon : CorpsMember
    {
        public Gideon(string id)
        {
            Validator.ValidateNull(id, nameof(id));
            var data = DataReader.ReadMemberData(id) ?? throw new ArgumentException($"데이터 불러오기 오류: {id}");
            // 필드 초기화
            InitialiseBasicInfo(data);
            // Stat 초기화
            InitialiseStat(data.Stat);
            // 스킬 초기화
            InitialisePasssiveSkills(data);
            InitialiseActiveSkills(data);
        }

        private void InitialiseBasicInfo(CorpsMember data)
        {
            Id = Validator.ValidateNull(data.Id, nameof(data.Id));
            Name = Validator.ValidateNull(data.Name, nameof(data.Name));
            CorpsId = Validator.ValidateNull(data.CorpsId, nameof(data.CorpsId));
            Description = Validator.ValidateNull(data.Description, nameof(data.Description));
        }

        private void InitialiseStat(Stat statData)
        {
            Validator.ValidateNull(statData, nameof(statData));
            Validator.ValidateNull(statData.CombatStats, nameof(statData.CombatStats));
            Stat = new Stat
            {
                Hp = statData.Hp,
                MaxHp = statData.Hp, // 시작 시, 현재 체력은 최대 체력
                Movement = statData.Movement,
                Wisdom = statData.Wisdom,
                SpellPower = statData.SpellPower,
                CombatStats = statData.CombatStats.ToDictionary(
                    kv => kv.Key,
                    kv => new CombatStat
                    {
                        Type = kv.Value.Type,
                        Range = kv.Value.Range,
                        AttackCount = kv.Value.AttackCount,
                        Value = kv.Value.Value
                    }
                ) ?? []
            };
        }

        private void InitialisePasssiveSkills(CorpsMember data)
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
                    "라이온 가드" => () =>
                    {
                        skill.isActivated = true;
                        ActivateLionGuard();
                    }
                    ,
                    "비싸고 좀 가벼운 황금 사자 방패" => () =>
                    {
                        skill.isActivated = true;
                        this.Stat.Movement++;
                    }
                    ,
                    "위풍당당" => () => skill.isActivated = true,
                    _ => null
                };

                skill.Deactivate = p.Name switch
                {
                    "라이온 가드" => () =>
                    {
                        skill.isActivated = false;
                        DeactivateLionGuard();
                    }
                    ,
                    "비싸고 좀 가벼운 황금 사자 방패" => () =>
                    {
                        skill.isActivated = false;
                        this.Stat.Movement = (ushort)Math.Max(0, this.Stat.Movement - 1); // 최소 0으로 유지
                    }
                    ,
                    "위풍당당" => () => skill.isActivated = false,
                    _ => null
                };

                return skill;
            }).ToList() ?? [];
        }

        /*
         * DeactivateLionGuard()
         * - 라이온 가드가 비활성화될 때 호출됩니다.
         * - 유물 슬롯 -1
         */
        private void DeactivateLionGuard()
        {
            Artifact? artifact = ArtifactSlot.ElementAtOrDefault(3);
            if (artifact != default)
            {
                artifact.Unequip(this); // 4번째 슬롯에 있는 유물을 해제합니다.
                this.ArtifactSlot.RemoveAt(3); // 4번째 슬롯 제거
            }
            this.MaxArtifactSlot--;
        }

        /*
         * ActivateLionGuard()
         * - 라이온 가드가 활성화될 때 호출됩니다.
         * - 유물 슬롯 +1
         */
        private void ActivateLionGuard()
        {
            this.MaxArtifactSlot++;
            this.ArtifactSlot.Add(null); // 새로운 슬롯 추가
        }

        private void InitialiseActiveSkills(CorpsMember data)
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
                    isOnCooldown = false
                };

                skill.Execute = a.Name switch
                {
                    "전투개시(방어)" => () => skill.isOnCooldown = true,
                    "전투개시(공격)" => () => skill.isOnCooldown = true,
                    "영원한" => () => skill.isOnCooldown = true,
                    _ => null
                };
                return skill;
            }).ToList() ?? [];
        }
    }
}
