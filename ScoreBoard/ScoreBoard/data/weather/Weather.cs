using ScoreBoard.data.character;
using ScoreBoard.data.monster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data.weather
{
    public class Weather
    {
        public WeatherType Type { get; set; } = WeatherType.Clear; // 날씨 타입
        public int Duration { get; set; } = -1; // 날씨 지속 턴 수
        public bool IsInfinite => Duration < 0; // 날씨가 무한으로 지속되는지 여부

        public Weather()
        { }

        public Weather(WeatherType type, int duration)
        {
            Type = type;
            Duration = duration;
        }

        public void ApplyWeatherEffect(CorpsMember member)
        {
            switch (Type)
            {
                case WeatherType.Clear:
                    member.WeatherMovementModifier = 0;
                    member.WeatherRangeModifier = 0;
                    member.WeatherDiceModifier = 0;
                    break;
                case WeatherType.Rain:
                    member.WeatherMovementModifier = -1;
                    member.WeatherRangeModifier = 0;
                    member.WeatherDiceModifier = 0;
                    break;
                case WeatherType.Snow:
                    member.WeatherMovementModifier = 0;
                    member.WeatherRangeModifier = -1;
                    member.WeatherDiceModifier = 0;
                    break;
                case WeatherType.Fog:
                    member.WeatherMovementModifier = 0;
                    member.WeatherRangeModifier = 0;
                    member.WeatherDiceModifier = 1;
                    break;
            }
        }

        public void ApplyWeatherEffect(Monster monster)
        {
            switch (Type)
            {
                case WeatherType.Clear:
                    monster.WeatherMovementModifier = 0;
                    monster.WeatherRangeModifier = 0;
                    monster.WeatherDiceModifier = 0;
                    break;
                case WeatherType.Rain:
                    monster.WeatherMovementModifier = -1;
                    monster.WeatherRangeModifier = 0;
                    monster.WeatherDiceModifier = 0;
                    break;
                case WeatherType.Snow:
                    monster.WeatherMovementModifier = 0;
                    monster.WeatherRangeModifier = -1;
                    monster.WeatherDiceModifier = 0;
                    break;
                case WeatherType.Fog:
                    monster.WeatherMovementModifier = 0;
                    monster.WeatherRangeModifier = 0;
                    monster.WeatherDiceModifier = 1;
                    break;
            }
        }
    }
}
