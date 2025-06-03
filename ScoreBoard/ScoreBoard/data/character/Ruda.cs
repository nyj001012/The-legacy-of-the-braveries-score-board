using ScoreBoard.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

namespace ScoreBoard.data.character
{
    internal class Ruda : CorpsMember
    {
        public Ruda(string id) : base()
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
                MaxHp = statData.MaxHp, // 시작 시, 현재 체력은 최대 체력
                Movement = statData.Movement,
                Wisdom = statData.Wisdom, // nullable 또는 기본값 처리
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

                skill.Execute = p.Name switch
                {
                    "다재다능" => () => skill.isActivated = true,
                    "집행자" => () => skill.isActivated = true,
                    "완벽주의" => () => ActivatePerfectionism(),
                    _ => null
                };
                return skill;
            }).ToList() ?? [];
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
            this.Stat.CombatStats["melee"].AttackCount += (ushort)this.ArtifactSlot.Count;
            this.Stat.CombatStats["ranged"].AttackCount += (ushort)this.ArtifactSlot.Count;
        }
    }
}
