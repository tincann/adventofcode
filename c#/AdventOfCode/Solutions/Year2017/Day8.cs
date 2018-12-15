using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Year2017
{
    public class Day8 : IPuzzleSolution
    {
        public int Year => 2017;
        public int Day => 8;

        private readonly Regex _r =
            new Regex(@"(?<reg>^\w+) (?<op>inc|dec) (?<val>-?\d+) if (?<creg>\w+) (?<cop>[<=>!]+) (?<cval>-?\d+)$");

        public string SolvePart1(params string[] input)
        {
            var registers = new Dictionary<string, Register>();
            var highestValue = int.MinValue;
            foreach (var line in input)
            {
                var m = _r.Match(line);
                var reg = m.Groups["reg"].Value;
                var inc = m.Groups["op"].Value == "inc" ? 1 : -1;
                var val = int.Parse(m.Groups["val"].Value);
                var creg = m.Groups["creg"].Value;
                var cop = m.Groups["cop"].Value;
                var cval = int.Parse(m.Groups["cval"].Value);

                var cr = GetRegister(creg, registers);
                var vreg = GetRegister(reg, registers);

                if (CheckCondition(cop, cr.Value, cval))
                {
                    vreg.Value += inc * val;
                }

                highestValue = Math.Max(highestValue, vreg.Value);
            }

            Console.WriteLine("highest " + highestValue);

            return registers.Max(x => x.Value.Value).ToString();
        }

        private static bool CheckCondition(string compOp, int value, int cval)
        {
            switch (compOp)
            {
                case "==":
                    return value == cval;
                case "<=":
                    return value <= cval;
                case ">=":
                    return value >= cval;
                case "<":
                    return value < cval;
                case ">":
                    return value > cval;
                case "!=":
                    return value != cval;
                default:
                    throw new Exception($"Op {compOp} doesn't exist");
            }
        }


        private static Register GetRegister(string name, Dictionary<string, Register> d)
        {
            if (!d.ContainsKey(name))
            {
                d.Add(name, new Register());
            }

            return d[name];
        }

        private class Register
        {
            public int Value;
        }

        public string SolvePart2(params string[] input)
        {
            throw new NotImplementedException();
        }

        public ICollection<bool> Assertions => new[]
        {
            true,
            //SolvePart1("") == "",
            //SolvePart2("") == "",
        };
    }
}