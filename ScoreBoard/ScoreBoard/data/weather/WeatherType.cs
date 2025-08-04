using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data.weather
{
    public enum WeatherType
    {
        [Display(Name = "맑음", Description = "효과 없음")]
        Clear,

        [Display(Name = "비", Description = "이동 감소")]
        Rain,

        [Display(Name = "눈", Description = "사거리 감소")]
        Snow,

        [Display(Name = "안개", Description = "명중률 감소")]
        Fog,
    }
}
