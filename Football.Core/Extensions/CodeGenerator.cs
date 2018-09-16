using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Football.Core.Extensions
{
    public static class CodeGenerator
    {
        public static string GetCode()
        {
            var random = new Random();

            const string chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 6)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
