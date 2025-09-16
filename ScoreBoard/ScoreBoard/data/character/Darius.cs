using ScoreBoard.data.minion;
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
    internal class Darius : CorpsMember
    {
        public Darius(string id)
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
                    "수완가" => () => skill.isActivated = true,
                    "위치 특정" => () => skill.isActivated = true,
                    "재벌가" => () => skill.isActivated = true,
                    "(소환) 햄부기 전차" => () =>
                    {
                        skill.isActivated = true;
                        Minion minion = this.Minions[0];
                        minion.IsSummonable = true;
                        minion.Stat.Hp = minion.Stat.MaxHp; // 소환시 체력 = 최대 체력
                        minion.Stat.CombatStats["ranged"].Value = (ushort)(Stat.CombatStats["ranged"].Value + (Stat.SpellPower ?? 0));
                    },
                    _ => null
                };

                skill.Deactivate = p.Name switch
                {
                    "수완가" => () => skill.isActivated = false,
                    "위치 특정" => () => skill.isActivated = false,
                    "재벌가" => () => skill.isActivated = false,
                    "(소환) 햄부기 전차" => () =>
                    {
                        skill.isActivated = false;
                        Minion minion = this.Minions.Find(m => m.Name == "햄부기 전차")!;
                        minion.IsSummonable = false;
                        minion.Stat.Hp = 0; // 소환 해제 시 체력 0
                    }
                    ,
                    _ => null
                };

                return skill;
            }).ToList() ?? [];
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
                    "햄부거의 정보력" => () => skill.isOnCooldown = true,
                    _ => null
                };
                return skill;
            }).ToList() ?? [];
        }
    }
}
