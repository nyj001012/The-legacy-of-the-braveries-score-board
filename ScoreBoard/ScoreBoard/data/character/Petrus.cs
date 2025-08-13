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
    internal class Petrus : CorpsMember
    {
        public Petrus(string id)
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
                    "위압감" => () => skill.isActivated = true,
                    "강렬함" => () => skill.isActivated = true,
                    "전투의 열광" => () =>
                    {
                        skill.isActivated = true;
                        this.Stat.Movement += 2;
                    }
                    ,
                    _ => null
                };

                skill.Deactivate = p.Name switch
                {
                    "위압감" => () => skill.isActivated = false,
                    "강렬함" => () => skill.isActivated = false,
                    "전투의 열광" => () =>
                    {
                        skill.isActivated = false;
                        this.Stat.Movement = (ushort)Math.Max(0, this.Stat.Movement - 2);
                    }
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
                    isOnCooldown = false
                };

                skill.Execute = a.Name switch
                {
                    "꿀꿀포 사격" => () => skill.isOnCooldown = true,
                    "정복자" => () => skill.isOnCooldown = true,
                    _ => null
                };
                return skill;
            }).ToList() ?? [];
        }
    }
}
