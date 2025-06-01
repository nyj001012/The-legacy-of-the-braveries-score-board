using ScoreBoard.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data.character
{
    internal class SkyHaneulSoraTen : CorpsMember
    {
        private List<CorpsMember> _allies = [];

        public List<CorpsMember> GetAllies()
        {
            return _allies;
        }

        public void SetAllies(List<CorpsMember> members)
        {
            Validator.ValidateNull(members, nameof(members));
            _allies = members;
        }

        public SkyHaneulSoraTen(string id) : base()
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
                ) ?? new Dictionary<string, CombatStat>()
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
                    "임페리얼 베리어" => () => skill.isActivated = false, // 일회성 효과
                    "커스텀 튜닝" => () => Tune(),
                    "목청이 터질 정도로! wahhhhhhh!" => () => Roar(),
                    "전투의 함성!" => () => ShoutWarCry(),
                    "진⭐급" => () => GetPromoted(),
                    _ => null
                };
                return skill;
            }).ToList() ?? [];
        }

        /*
         * 커스텀 튜닝
         * 물리 공격력 +20
         */
        private void Tune()
        {
            this.Stat.CombatStats["melee"].Value += 20; // 공격력 증가
        }

        /*
         * 목청이 터질 정도로! wahhhhhhh!
         * 본인을 제외한 모든 아군 원거리 공격력 +100
         */
        private void Roar()
        {
            foreach (var ally in _allies)
            {
                if (ally.Id == this.Id) continue; // 본인은 제외
                if (ally.Stat.CombatStats["ranged"] != null)
                {
                    ally.Stat.CombatStats["ranged"].Value += 100; // 아군 원거리 공격력 증가
                }
            }
        }

        private void ShoutWarCry()
        {
            foreach (var ally in _allies)
            {
                if (ally.Stat.CombatStats["melee"] != null)
                {
                    ally.Stat.CombatStats["melee"].AttackCount++; // 근접 공속 1 증가
                }
                if (ally.Stat.CombatStats["ranged"] != null)
                {
                    ally.Stat.CombatStats["ranged"].AttackCount++; // 원거리 공속 1 증가
                }
            }
        }

        private void GetPromoted()
        {
            this.Stat.Hp += 700;
            this.Stat.MaxHp += 700; // 최대 체력 1000으로 증가
            this.Stat.CombatStats["ranged"].AttackCount += 2; // 공격 횟수 8로 증가 (기본: 6)
            this.Stat.CombatStats["ranged"].Value += 130; // 공격력 200으로 증가 (기본: 70)
            this.Stat.Movement -= 1; // 이동 거리 3으로 감소 (기본: 4)
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
                    "매콤한 주먹" => () => skill.isOnCooldown = true,
                    _ => null
                };
                return skill;
            }).ToList() ?? [];
        }
    }
}
