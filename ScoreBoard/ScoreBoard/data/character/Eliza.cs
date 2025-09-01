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
    internal class Eliza : CorpsMember
    {
        public Eliza(string id)
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
                    "검 이름: 변호사(물리)" => () => skill.isActivated = true,
                    "몰아침" => () => skill.isActivated = true,
                    "대전사" => () => skill.isActivated = true,
                    "나만 몰아침" => () => skill.isActivated = true,
                    "깃털 각인" => () => skill.isActivated = true,
                    "흑깃" => () => skill.isActivated = true,
                    "깃털 파괴" => () => skill.isActivated = true,
                    "[특수] 응징" => () => skill.isActivated = true,
                    _ => null
                };

                skill.Deactivate = p.Name switch
                {
                    "검 이름: 변호사(물리)" => () => skill.isActivated = false,
                    "몰아침" => () => skill.isActivated = false,
                    "대전사" => () => skill.isActivated = false,
                    "나만 몰아침" => () => skill.isActivated = false,
                    "깃털 각인" => () => skill.isActivated = false,
                    "흑깃" => () => skill.isActivated = false,
                    "깃털 파괴" => () => skill.isActivated = false,
                    "[특수] 응징" => () => skill.isActivated = false,
                    _ => null
                };

                return skill;
            }).ToList() ?? [];
        }

        protected override void InitialiseActiveSkills(CorpsMember data)
        {
        }
    }
}
