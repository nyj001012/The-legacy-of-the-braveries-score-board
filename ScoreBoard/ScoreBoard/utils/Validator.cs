using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.utils
{
    public static class Validator
    {
        public static string ValidateNull(string? value, string name)
        {
            return value ?? throw new ArgumentNullException(name, $"{name} cannot be null.");
        }

        public static string[] ValidateNull(string[]? value, string name)
        {
            return value ?? throw new ArgumentNullException(name, $"{name} cannot be null.");
        }

        public static T ValidateNull<T>(T? value, string name) where T : class
        {
            return value ?? throw new ArgumentNullException(name, $"{name} cannot be null.");
        }
    }
}
