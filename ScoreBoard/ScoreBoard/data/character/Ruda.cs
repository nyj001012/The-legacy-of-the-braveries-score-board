using ScoreBoard.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

namespace ScoreBoard.data.character
{
    internal class Ruda : CorpsMember
    {
        public Ruda(string id) : base("", "", "", Array.Empty<string>(), new Stat(0, 0, 0, new()))
        {
            Validator.ValidateNull(id, nameof(id));

            var data = DataReader.ReadMemberData(id);
            if (data == null)
            {
                throw new ArgumentException($"데이터 불러오기 오류: {id}");
            }

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

            Stat = new Stat(
                hp: statData.Hp,
                movement: statData.Movement,
                wisdom: statData.Wisdom,
                combatStats: statData.CombatStats?.ToDictionary(
                    kv => kv.Key,
                    kv => new CombatStat(
                        kv.Value.Type,
                        kv.Value.Range,
                        kv.Value.AttackCount,
                        kv.Value.Value
                    )
                ) ?? []
            );
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
                    Description = Validator.ValidateNull(p.Description, nameof(p.Description)),
                    Execute = p.Name switch
                    {
                        "전투의 열정" => () => Console.WriteLine("공격력 증가"),
                        "전투의 의지" => () => Console.WriteLine("방어력 증가"),
                        "전투의 집중" => () => Console.WriteLine("명중률 증가"),
                        _ => null
                    }
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
                    Execute = a.Name switch
                    {
                        "폭발 탄환" => () => Console.WriteLine("폭발 피해를 줍니다."),
                        "결정타" => () => Console.WriteLine("2칸 돌진 후 공격"),
                        "신성한 결투" => () => Console.WriteLine("공속만큼 타격합니다."),
                        _ => null
                    }
                };
                return skill;
            }).ToList() ?? [];
        }
    }
}
