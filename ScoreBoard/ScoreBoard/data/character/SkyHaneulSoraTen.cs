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
                    "임페리얼 베리어" => () => skill.isActivated = true, // 일회성 효과
                    "커스텀 튜닝" => () =>
                    {
                        skill.isActivated = true;
                        Tune();
                    }
                    ,
                    "목청이 터질 정도로! wahhhhhhh!" => () =>
                    {
                        skill.isActivated = true;
                        Roar();
                    }
                    ,
                    "전투의 함성!" => () =>
                    {
                        skill.isActivated = true;
                        ShoutWarCry();
                    }
                    ,
                    "진⭐급" => () =>
                    {
                        skill.isActivated = true;
                        GetPromoted();
                    }
                    ,
                    _ => null
                };

                skill.Deactivate = p.Name switch
                {
                    "임페리얼 베리어" => () => skill.isActivated = false, // 일회성 효과
                    "커스텀 튜닝" => () =>
                    {
                        skill.isActivated = false;
                        Untune();
                    }
                    ,
                    "목청이 터질 정도로! wahhhhhhh!" => () =>
                    {
                        skill.isActivated = false;
                        Hush();
                    }
                    ,
                    "전투의 함성!" => () =>
                    {
                        skill.isActivated = false;
                        SuppressWarCry();
                    }
                    ,
                    "진⭐급" => () =>
                    {
                        skill.isActivated = false;
                        GetDemoted();
                    }
                    ,
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
            this.Stat.CombatStats["ranged"].Value += 20; // 공격력 증가
        }

        /*
         * 커스텀 튜닝 비활성화
         * 물리 공격력 -20
         */
        private void Untune()
        {
            ushort attackValue = this.Stat.CombatStats["ranged"].Value;

            this.Stat.CombatStats["ranged"].Value = (ushort)Math.Max(0, attackValue - 20);
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
                if (ally.Stat.CombatStats.TryGetValue("ranged", out CombatStat? r))
                {
                    r.Value += 100; // 아군 원거리 공격력 증가
                }
            }
        }

        /*
         * 목청이 터질 정도로! wahhhhhhh! 비활성화
         * 본인을 제외한 모든 아군 원거리 공격력 -100
         */
        private void Hush()
        {
            foreach (var ally in _allies)
            {
                if (ally.Id == this.Id) continue; // 본인은 제외
                if (ally.Stat.CombatStats.TryGetValue("ranged", out CombatStat? r))
                {
                    r.Value = (ushort)Math.Max(0, r.Value - 100);
                }
            }
        }

        /*
         * 전투의 함성!
         * - 근접 공속, 원거리 공속 1씩 증가
         */
        private void ShoutWarCry()
        {
            foreach (var ally in _allies)
            {
                if (ally.Stat.CombatStats.TryGetValue("melee", out CombatStat? m))
                {
                    m.AttackCount++; // 근접 공속 1 증가
                }
                if (ally.Stat.CombatStats.TryGetValue("ranged", out CombatStat? r))
                {
                    r.AttackCount++; // 원거리 공속 1 증가
                }
            }
        }

        /*
         * 전투의 함성! 비활성화
         * - 근접 공속, 원거리 공속 1씩 감소
         */
        private void SuppressWarCry()
        {
            foreach (var ally in _allies)
            {
                if (ally.Stat.CombatStats.TryGetValue("melee", out CombatStat? m))
                {
                    m.AttackCount = (ushort)Math.Max(0, m.AttackCount - 1);
                }
                if (ally.Stat.CombatStats.TryGetValue("ranged", out CombatStat? r))
                {
                    r.AttackCount = (ushort)Math.Max(0, r.AttackCount - 1);
                }
            }
        }

        /*
         * 진⭐급
         * - 최대 체력 700 증가
         * - 공격 횟수 2 증가
         * - 공격력 130 증가
         * - 이동거리 - 1
         */
        private void GetPromoted()
        {
            this.Stat.Hp += 700;
            this.Stat.MaxHp += 700; // 최대 체력 1000으로 증가
            this.Stat.CombatStats["ranged"].AttackCount += 2; // 공격 횟수 8로 증가 (기본: 6)
            this.Stat.CombatStats["ranged"].Value += 130; // 공격력 200으로 증가 (기본: 70)
            this.Stat.Movement = (ushort)Math.Max(0, this.Stat.Movement - 1); // 이동 거리 3으로 감소 (기본: 4)
        }

        /*
         * 진⭐급 비활성화
         * - 최대 체력 700 감소
         * - 공격 횟수 2 감소
         * - 공격력 130 감소
         * - 이동거리 + 1
         */
        private void GetDemoted()
        {
            this.Stat.Hp = (ushort)Math.Max(0, this.Stat.Hp - 700);
            this.Stat.MaxHp = (ushort)Math.Max(0, this.Stat.MaxHp - 700);

            ushort count = this.Stat.CombatStats["ranged"].AttackCount;
            ushort value = this.Stat.CombatStats["ranged"].Value;
            this.Stat.CombatStats["ranged"].AttackCount = (ushort)Math.Max(0, count - 2);
            this.Stat.CombatStats["ranged"].Value = (ushort)Math.Max(0, value - 130);

            this.Stat.Movement++;
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
