using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data.weather
{
    internal class Weather
    {
        public WeatherType Type { get; set; } = WeatherType.Clear; // 날씨 타입
        public int Duration { get; set; } = -1; // 날씨 지속 턴 수
        public bool IsInfinite => Duration < 0; // 날씨가 무한으로 지속되는지 여부
    }
}
