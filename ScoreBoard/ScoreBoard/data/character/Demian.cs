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
    internal class Demian : CorpsMember
    {
        public Demian(string id) : base()
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
                Hp = statData.Hp, // 시작 시, 현재 체력은 최대 체력
                MaxHp = statData.Hp,
                Movement = statData.Movement,
                Wisdom = statData.Wisdom, // nullable 또는 기본값 처리
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
                    "앙 드 뚜와" => () => skill.isActivated = true,
                    "근거있는...자 신 감 과 다 !!!!!" => () => skill.isActivated = true,
                    "용 맹 함" => () => skill.isActivated = true,
                    "결투가의 맹세" => () => skill.isActivated = true,
                    _ => null
                };

                skill.Deactivate = p.Name switch
                {
                    "앙 드 뚜와" => () => skill.isActivated = false,
                    "근거있는...자 신 감 과 다 !!!!!" => () => skill.isActivated = false,
                    "용 맹 함" => () => skill.isActivated = false,
                    "결투가의 맹세" => () => skill.isActivated = false
                    ,
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
                    "[일반] 베어넘기기" => () => skill.isOnCooldown = true,
                    "[특수] 완벽함" => () => skill.isOnCooldown = true,
                    _ => null
                };
                return skill;
            }).ToList() ?? [];
        }
    }
}
