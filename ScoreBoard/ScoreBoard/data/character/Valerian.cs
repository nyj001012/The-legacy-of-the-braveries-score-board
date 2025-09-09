using ScoreBoard.data.artifact;
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
    internal class Valerian : CorpsMember
    {
        public Valerian(string id)
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
                    "죽음의 천사" => () => skill.isActivated = true,
                    "백수지만 능력자!!" => () => skill.isActivated = true,
                    "백수가 되었지만 훈련을 게을리하지않아ㅜㅠㅠㅠㅠ으어헝" => () =>
                    {
                        skill.isActivated = true;
                        TrainHard();
                    }
                    ,
                    "백수가 되어도 굳건한 정신력 엉어유ㅠㅡㅠㅠㅠ" => () =>
                    {
                        skill.isActivated = true;
                        FortifyMind();
                    }
                    ,
                    _ => null
                };

                skill.Deactivate = p.Name switch
                {
                    "죽음의 천사" => () => skill.isActivated = false,
                    "백수지만 능력자!!" => () => skill.isActivated = false,
                    "백수가 되었지만 훈련을 게을리하지않아ㅜㅠㅠㅠㅠ으어헝" => () =>
                    {
                        skill.isActivated = false;
                        SlackOff();
                    }
                    ,
                    "백수가 되어도 굳건한 정신력 엉어유ㅠㅡㅠㅠㅠ" => () =>
                    {
                        skill.isActivated = false;
                        WeakenMind();
                    }
                    ,
                    _ => null
                };

                return skill;
            }).ToList() ?? [];
        }

        /*
         * 백수가 되었지만 훈련을 게을리하지않아ㅜㅠㅠㅠㅠ으어헝
         * 공격력 +100, 공격 횟수 +1, 최대 유물 슬롯 +1
         */
        private void TrainHard()
        {
            this.Stat.CombatStats["melee"].Value += 100;
            this.Stat.CombatStats["melee"].AttackCount += 1;
            this.MaxArtifactSlot += 1;
            this.ArtifactSlot.Add(null);
        }

        /*
         * 백수가 되었지만 훈련을 게을리하지않아ㅜㅠㅠㅠㅠ으어헝 비활성화
         * 공격력 -100, 공격 횟수 -1, 최대 유물 슬롯 -1
         */
        private void SlackOff()
        {
            ushort value = this.Stat.CombatStats["melee"].Value;
            ushort count = this.Stat.CombatStats["melee"].AttackCount;
            this.Stat.CombatStats["melee"].Value = (ushort)Math.Max(0, value - 100);
            this.Stat.CombatStats["melee"].AttackCount = (ushort)Math.Max(0, count - 1);

            Artifact? lastArtifact = this.ArtifactSlot[MaxArtifactSlot - 1];
            if (lastArtifact != null) // 마지막 유물 슬롯에 착용 중인 유물이 있었다면
            {
                lastArtifact.Unequip(this); // 착용 해제
            }
            this.ArtifactSlot.RemoveAt(MaxArtifactSlot - 1);
            this.MaxArtifactSlot = (ushort)Math.Max(0, this.MaxArtifactSlot - 1);
        }

        /*
         * 백수가 되어도 굳건한 정신력 엉어유ㅠㅡㅠㅠㅠ
         * 주문력 +200, 공속 +1
         */
        private void FortifyMind()
        {
            this.Stat.SpellPower = (ushort?)((this.Stat.SpellPower ?? 0) + 200);
            this.Stat.CombatStats["melee"].AttackCount += 1;
        }

        /*
         * 백수가 되어도 굳건한 정신력 엉어유ㅠㅡㅠㅠㅠ 비활성화
         * 주문력 -200, 공속 -1
         */
        private void WeakenMind()
        {
            if (this.Stat.SpellPower != null)
            {
                this.Stat.SpellPower = (ushort)Math.Max(0, (int)this.Stat.SpellPower - 200);
            }

            ushort count = this.Stat.CombatStats["melee"].AttackCount;
            this.Stat.CombatStats["melee"].AttackCount = (ushort)Math.Max(0, count - 1);
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
                    "만능이란 이런 것" => () => skill.isOnCooldown = true,
                    _ => null
                };
                return skill;
            }).ToList() ?? [];
        }
    }
}
