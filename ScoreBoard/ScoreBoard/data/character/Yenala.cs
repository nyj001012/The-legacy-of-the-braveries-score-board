using ScoreBoard.data.skill;
using ScoreBoard.data.stat;
using ScoreBoard.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ScoreBoard.data.character
{
    internal class Yenala : CorpsMember
    {
        private bool isOnceActivated = false; // 스킬이 한 번 활성화되었는지 여부
        private ushort godBonusShield = 0; // 생성된 보호막의 값

        public Yenala(string id) : base()
        {
            Initialise(id);
        }

        protected override void InitialisePasssiveSkills(CorpsMember data)
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
                    "원소 룬 마법" => () => skill.isActivated = true,
                    "숙달됨" => () =>
                    {
                        BeSkilledIn();
                        skill.isActivated = true;
                    }
                    ,
                    "대마법사" => () =>
                    {
                        BecomeArchmage();
                        skill.isActivated = true;
                    }
                    ,
                    "신" => () =>
                    {
                        BecomeGod();
                        skill.isActivated = true;
                    }
                    ,
                    _ => null
                };

                skill.Deactivate = p.Name switch
                {
                    "원소 룬 마법" => () => skill.isActivated = false,
                    "숙달됨" => () =>
                    {
                        skill.isActivated = false;
                        LoseMastery();
                    }
                    ,
                    "대마법사" => () =>
                    {
                        skill.isActivated = false;
                        DemoteArchmageToMage();
                    }
                    ,
                    "신" => () =>
                    {
                        BecomeHuman();
                        skill.isActivated = false;
                    }
                    ,
                    _ => null
                };

                return skill;
            }).ToList() ?? [];
        }

        /*
         * BecomeArchmage()
         * - 마법 사거리 +1
         * - 주문력 +100
         * - 지혜 +1
         */
        private void BecomeArchmage()
        {
            this.Stat.CombatStats["ranged"].Range++;
            this.Stat.SpellPower = (ushort)(this.Stat.SpellPower! + 100);
            this.Stat.Wisdom = (ushort)(this.Stat.Wisdom! + 1);
        }

        /*
         * BecomeHuman()
         * - 마법 사거리 -2
         * - 주문력 -300
         * - 생성된 보호막 삭제
         */
        private void BecomeHuman()
        {
            this.Stat.CombatStats["ranged"].Range = (ushort)Math.Max(0, this.Stat.CombatStats["ranged"].Range - 2);
            this.Stat.SpellPower = (ushort)Math.Max(0, (int)this.Stat.SpellPower! - 300);
            if (godBonusShield > 0)
            {
                this.Stat.Shield = (ushort)Math.Max(0, this.Stat.Shield - godBonusShield);
                godBonusShield = 0; // 보호막 삭제
            }
        }

        /*
         * LoseMastery()
         * - 마법 사거리 -1
         * - 주문력 -100
         */
        private void LoseMastery()
        {
            this.Stat.CombatStats["ranged"].Range = (ushort)Math.Max(0, this.Stat.CombatStats["ranged"].Range - 1);
            this.Stat.SpellPower = (ushort)Math.Max(0, (int)this.Stat.SpellPower! - 100);
        }

        /*
         * BecomeGod()
         * - 마법 사거리 +2
         * - 주문력 +300
         * - 주문력 수치만큼 보호막 생성
         */
        private void BecomeGod()
        {
            this.Stat.CombatStats["ranged"].Range += 2;
            this.Stat.SpellPower = (ushort)(this.Stat.SpellPower! + 300);
            if (isOnceActivated)
            {
                // 이미 보호막이 활성화된 경우, 추가로 생성하지 않음
                return;
            }
            isOnceActivated = true;
            godBonusShield = (ushort)this.Stat.SpellPower;
            this.Stat.Shield = (ushort)(this.Stat.Shield + godBonusShield);
        }

        /*
         * DemoteArchmageToMage()
         * - 마법 사거리 -1
         * - 주문력 -100
         * - 지혜 -1
         */
        private void DemoteArchmageToMage()
        {
            this.Stat.CombatStats["ranged"].Range = (ushort)Math.Max(0, this.Stat.CombatStats["ranged"].Range - 1);
            this.Stat.SpellPower = (ushort)Math.Max(0, (int)this.Stat.SpellPower! - 100);
            this.Stat.Wisdom = (ushort)Math.Max(0, (int)this.Stat.Wisdom! - 1);
        }

        /*
         * BeSkilledIn()
         * - 마법 사거리 +1
         * - 주문력 +100
         */
        private void BeSkilledIn()
        {
            this.Stat.CombatStats["ranged"].Range++;
            this.Stat.SpellPower = (ushort)(this.Stat.SpellPower! + 100);
        }

        protected override void InitialiseActiveSkills(CorpsMember data)
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
                    "원소방출" => () => skill.isOnCooldown = true,
                    _ => null
                };
                return skill;
            }).ToList() ?? [];
        }
    }
}
