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
    internal class Rudeus : CorpsMember
    {
        public Rudeus(string id)
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
                    "마검" => () => skill.isActivated = true,
                    "오늘 흑마법 배웠어요 ^^" => () =>
                    {
                        skill.isActivated = true;
                        LearnBlackMagic();
                    }
                    ,
                    "흑마법에 익숙해지다" => () =>
                    {
                        skill.isActivated = true;
                        BeSkilledInBlackMagic();
                    }
                    ,
                    "검은 불길" => () => skill.isActivated = true,
                    _ => null
                };

                skill.Deactivate = p.Name switch
                {
                    "마검" => () => skill.isActivated = false,
                    "오늘 흑마법 배웠어요 ^^" => () =>
                    {
                        skill.isActivated = false;
                        UnlearnBlackMagic();
                    }
                    ,
                    "흑마법에 익숙해지다" => () =>
                    {
                        skill.isActivated = false;
                        LoseBlackMagicMastery();
                    }
                    ,
                    "검은 불길" => () => skill.isActivated = false,
                    _ => null
                };

                return skill;
            }).ToList() ?? [];
        }

        /*
         * 오늘 흑마법 배웠어요 ^^
         * 이동거리 +1, 근접 공격력 +100, 주문력 +200
         */
        private void LearnBlackMagic()
        {
            this.Stat.Movement += 1; // 이동 속도 증가
            this.Stat.CombatStats["melee"].Value += 100; // 근접 공격력 증가
            this.Stat.SpellPower = (ushort?)((this.Stat.SpellPower ?? 0) + 200); // 주문력 증가
        }

        /*
         * 오늘 흑마법 배웠어요 ^^ 비활성화
         * - 이동거리 -1, 근접 공격력 -100, 주문력 -200
         */
        private void UnlearnBlackMagic()
        {
            ushort attackValue = this.Stat.CombatStats["melee"].Value;
            ushort? spellPower = this.Stat.SpellPower;

            this.Stat.Movement = (ushort)Math.Max(0, this.Stat.Movement - 1);
            this.Stat.CombatStats["melee"].Value = (ushort)Math.Max(0, attackValue - 100);
            if (spellPower != null)
            {
                this.Stat.SpellPower = (ushort)Math.Max(0, (int)spellPower - 200);
            }
        }

        /*
         * 흑마법에 익숙해지다
         * 체력 +200, 지혜 +1, 마법 사거리 및 스킬 범위 +1은 직접 계산
         */
        private void BeSkilledInBlackMagic()
        {
            this.Stat.MaxHp += 200; // 최대 체력 증가
            this.Stat.Hp += 200; // 현재 체력 증가
            this.Stat.Wisdom = (ushort?)((this.Stat.Wisdom ?? 0) + 1); // 지혜 증가
        }

        /*
         * 흑마법에 익숙해지다 비활성화
         * 체력 -200, 지혜 -1, 마법 사거리 및 스킬 범위 -1은 직접 계산
         */
        private void LoseBlackMagicMastery()
        {
            this.Stat.MaxHp = (ushort)Math.Max(0, this.Stat.MaxHp - 200);
            this.Stat.Hp = (ushort)Math.Max(0, this.Stat.Hp - 200);
            if (this.Stat.Wisdom != null)
            {
                this.Stat.Wisdom = (ushort)Math.Max(0, (int)this.Stat.Wisdom - 1);
            }
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
                    "검!풍!" => () => skill.isOnCooldown = true,
                    _ => null
                };
                return skill;
            }).ToList() ?? [];
        }
    }
}
