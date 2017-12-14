using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2017
{
    public class Day5 : IPuzzleSolution
    {
        public int Year => 2017;
        public int Day => 5;

        public string SolvePart1(params string[] input)
        {
            var instructions = input.Select(x => int.Parse(x)).ToList();

            int pc = 0, steps = 0;
            while (pc < instructions.Count)
            {
                var instr = instructions[pc];
                instructions[pc]++;
                pc += instr;
                steps++;
            }

            return steps.ToString();
        }

        public string SolvePart2(params string[] input)
        {
            var instructions = input.Select(x => int.Parse(x)).ToList();

            int pc = 0, steps = 0;
            while (pc < instructions.Count)
            {
                var instr = instructions[pc];
                if (instr >= 3)
                {
                    instructions[pc]--;
                }
                else
                {
                    instructions[pc]++;
                }
                pc += instr;
                steps++;
            }

            return steps.ToString();
        }

        public ICollection<bool> Assertions => new[]
        {
            SolvePart1("0", "3", "0", "1", "-3") == "5",
        };
    }
}