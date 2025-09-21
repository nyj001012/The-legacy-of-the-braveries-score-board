using ScoreBoard.data.artifact;
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
    internal class Julius : CorpsMember
    {
        private List<CorpsMember> _allies = [];
        private string[] _alliesCorpsIds = [];

        public List<CorpsMember> GetAllies()
        {
            return _allies;
        }

        public void SetAllies(List<CorpsMember> members)
        {
            Validator.ValidateNull(members, nameof(members));
            _allies = members.Except([this]).ToList(); // 자신 제외
            _alliesCorpsIds = _allies.Select(a => a.CorpsId).Distinct().ToArray();
        }

        public Julius(string id) : base()
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
                    "제국의 황제" => () => skill.isActivated = true,
                    "황제의 전선" => () =>
                    {
                        skill.isActivated = true;
                        GoToBattleFront();
                    }
                    ,
                    "장비 지원" => () =>
                    {
                        skill.isActivated = true;
                        SupportEquip();
                    }
                    ,
                    "권위 세우기" => () =>
                    {
                        skill.isActivated = true;
                        EstablishAuthority();
                    }
                    ,
                    "자동사격 시스템" => () => skill.isActivated = true,
                    _ => null
                };

                skill.Deactivate = p.Name switch
                {
                    "제국의 황제" => () => skill.isActivated = false,
                    "황제의 전선" => () =>
                    {
                        skill.isActivated = false;
                        Retreat();
                    }
                    ,
                    "장비 지원" => () =>
                    {
                        skill.isActivated = false;
                        StopSupporting();
                    }
                    ,
                    "권위 세우기" => () =>
                    {
                        skill.isActivated = false;
                        CollapsedAuthority();
                    }
                    ,
                    "자동사격 시스템" => () => skill.isActivated = false,
                    _ => null
                };

                return skill;
            }).ToList() ?? [];
        }

        /*
         * 황제의 전선
         * - 군단 별 조건 달성 시 활성화
         */
        private void GoToBattleFront()
        {
            PassiveSkill s = this.Passives.Find(p => p.Name == "황제의 전선")!;
            string[] description = s.Description;

            if (_alliesCorpsIds.Length != 1)// 아군이 여러 군단에 속해있다면 패스
            {
                description = ["적용되지 않습니다."];
                return;
            }

            switch (_alliesCorpsIds[0]) // 1군단
            {
                case "201": // 1군단
                    // 1군단 효과만 적용. 나머지 군단 효과 설명 삭제
                    s.Description = description[3..6];
                    Minions.Find(m => m.Name == "밥")!.SummonAvailableTurn = 3; // 밥 3턴부터 소환 가능
                    break;
                case "202": // 2군단
                    s.Description = description[6..10];
                    break;
                case "203": // 3군단
                    s.Description = description[10..14];
                    _allies.ForEach(a => a.Stat.Wisdom++); // 아군 지혜 +1
                    break;
                case "204": // 4군단
                    s.Description = description[14..17];
                    Minions.Find(m => m.Name == "루터")!.SummonAvailableTurn = 2; // 루터 2턴부터 소환 가능
                    break;
            }

        }

        /*
         * 장비 지원
         * - 근위대 장비 착용 가능
         */
        private void SupportEquip()
        {
            Minions.ForEach(m =>
            {
                m.MaxArtifactSlot = 3;
                m.ArtifactSlot = [.. Enumerable.Repeat<Artifact?>(null, m.MaxArtifactSlot)];
            });
        }

        /*
         * 권위 세우기
         * - 체력 +500, 사거리 +1, 공격력 +200, 이동거리 +1
         */
        private void EstablishAuthority()
        {
            this.Stat.Hp += 500;
            this.Stat.MaxHp += 500; // 최대 체력 500 증가
            this.Stat.CombatStats["ranged"].Range++; // 원거리 공격력 50 증가
            this.Stat.CombatStats["ranged"].Value += 200; // 원거리 공격력 50 증가
            this.Stat.Movement++; // 이동 거리 1 증가
        }

        /*
         * 황제의 전선 비활성화
         * - 군단 별 조건 달성 시 활성화
         */
        private void Retreat()
        {
            if (_alliesCorpsIds[0] == "203") // 3군단
            {
                _allies.ForEach(a => a.Stat.Wisdom = (ushort)Math.Max(0, a.Stat.Wisdom ?? 0 - 1)); // 아군 지혜 -1
            }
        }


        /*
         * 장비 지원 비활성화
         * - 장비 착용 가능
         */
        private void StopSupporting()
        {
            Minions.ForEach(m =>
            {
                if (m.ArtifactSlot.Count > 0)
                {
                    // 착용 중인 장비 모두 해제
                    m.ArtifactSlot.ForEach(a =>
                    {
                        if (a != null)
                            a.Unequip(m);
                    });
                }
                m.MaxArtifactSlot = 0; // 장비 착용 불가
                m.ArtifactSlot = [];
            });
        }

        /*
         * 권위 세우기 비활성화
         * - 체력 -500, 사거리 -1, 공격력 -200, 이동거리 -1
         */
        private void CollapsedAuthority()
        {
            this.Stat.Hp = (ushort)Math.Max(1, this.Stat.Hp - 500);
            this.Stat.MaxHp = (ushort)Math.Max(1, this.Stat.MaxHp - 500); // 최대 체력 -500
            this.Stat.CombatStats["ranged"].Range = (ushort)Math.Max(0, this.Stat.CombatStats["ranged"].Range - 1); // 원거리 사거리 -1
            this.Stat.CombatStats["ranged"].Value = (ushort)Math.Max(0, this.Stat.CombatStats["ranged"].Value - 200); // 원거리 공격력 -200
            this.Stat.Movement = (ushort)Math.Max(0, this.Stat.Movement - 1); // 이동 거리 -1
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
                    "내가 길을 열겠따" => () => skill.isOnCooldown = true,
                    "명령 하달" => () => skill.isOnCooldown = true,
                    _ => null
                };
                return skill;
            }).ToList() ?? [];
        }
    }
}
