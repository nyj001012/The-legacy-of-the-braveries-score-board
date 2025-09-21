using ScoreBoard.data.character;
using ScoreBoard.data.skill;
using ScoreBoard.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data.minion
{
    internal class Luther : Minion
    {
        public Luther(string mid)
        {
            Initialise(mid);
        }

        protected override void InitialisePasssiveSkills(Minion data)
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
                    "화력으로 밀어버리기" => () => skill.isActivated = true,
                    _ => null
                };

                skill.Deactivate = p.Name switch
                {
                    "화력으로 밀어버리기" => () => skill.isActivated = false,
                    _ => null
                };

                return skill;
            }).ToList() ?? [];
        }

        protected override void InitialiseActiveSkills(Minion data)
        {
        }
    }
}
