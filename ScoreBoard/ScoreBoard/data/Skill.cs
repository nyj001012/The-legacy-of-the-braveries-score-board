using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data
{
    internal class Skill(string name, ushort requiredLevel, string[] description)
    {
        public string Name { get; set; } = name; // 기술 이름 (예: "불꽃의 검", "치유의 빛" 등)
        public ushort RequiredLevel { get; set; } = requiredLevel; // 기술을 사용할 수 있는 레벨 (예: 1, 2, 3 등)
        public string[] Description { get; set; } = description; // 기술 설명 (예: "적에게 불꽃 피해를 입힙니다.", "아군을 치유합니다." 등)
        public Action? Execute { get; set; } = null; // 기술 실행 시 호출되는 액션 (예: 기술 사용 시 실행할 로직)
    }
}
