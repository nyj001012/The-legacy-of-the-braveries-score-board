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
    internal class Rudeus : CorpsMember
    {
        public ushort blackMagicCount = 0; // 흑마법 사용 횟수 카운트

        public Rudeus(string id)
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

                skill.Execute = p.Name switch
                {
                    "마검" => () => skill.isActivated = true,
                    "오늘 흑마법 배웠어요 ^^" => () => BeLearnerOfBlackMagic(),
                    "흑마법에 익숙해지다" => () => BeSkillfulWithBlackMagic(),
                    "검은 불길" => () => skill.isActivated = true,
                    _ => null
                };
                return skill;
            }).ToList() ?? [];
        }

        /*
         * 오늘 흑마법 배웠어요 ^^
         * 이동거리 +1, 근접 공격력 +100, 주문력 +200
         */
        private void BeLearnerOfBlackMagic()
        {
            this.Stat.Movement += 1; // 이동 속도 증가
            this.Stat.CombatStats["melee"].Value += 100; // 근접 공격력 증가
            this.Stat.SpellPower = (ushort?)((this.Stat.SpellPower ?? 0) + 200); // 주문력 증가
        }

        /*
         * 흑마법에 익숙해지다
         * 체력 +200, 지혜 +1, 마법 사거리 및 스킬 범위 +1은 직접 계산
         */
        private void BeSkillfulWithBlackMagic()
        {
            this.Stat.MaxHp += 200; // 최대 체력 증가
            this.Stat.Hp += 200; // 현재 체력 증가
            this.Stat.Wisdom = (ushort?)((this.Stat.Wisdom ?? 0) + 1); // 지혜 증가
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
                    "검!풍!" => () => blackMagicCount = 0,
                    _ => null
                };
                return skill;
            }).ToList() ?? [];
        }
    }
}
