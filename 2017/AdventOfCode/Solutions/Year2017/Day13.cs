using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Year2017
{
	public class Day13 : IPuzzleSolution
	{
		public int Year => 2017;
		public int Day => 13;



        private Regex _r = new Regex(@"(\d+): (\d+)");
		public string SolvePart1(params string[] input)
		{
		    var layers = input.Select(x => _r.Match(x)).Select(x =>
		        new Layer
		        {
		            Depth = int.Parse(x.Groups[1].Value),
		            Range = int.Parse(x.Groups[2].Value)
		        }).ToDictionary(x => x.Depth);

		    var max = layers.Values.Max(x => x.Depth);

		    var damage = Walk(layers, max);
		    return damage.ToString();
		}

		public string SolvePart2(params string[] input)
		{
		    var layers = input.Select(x => _r.Match(x)).Select(x =>
		        new Layer
		        {
		            Depth = int.Parse(x.Groups[1].Value),
		            Range = int.Parse(x.Groups[2].Value)
		        }).ToDictionary(x => x.Depth);

		    var max = layers.Values.Max(x => x.Depth);

		    var delay = 0;
		    while (true)
		    {
		        var layersCopy = layers.Values.Select(x => x.Copy()).ToDictionary(x => x.Depth);
		        var damage = Walk(layersCopy, max, stopAtDamage: true);
		        if (damage == 0 && delay >= 10)
		        {
		            return delay.ToString();
		        }

                Step(layers.Values);
		        delay++;
		    }
		}

	    private static int Walk(Dictionary<int,Layer> layers, int max, bool stopAtDamage = false)
	    {
	        var damage = 0;
	        for (var position = 0; position <= max; position++)
	        {
	            if (layers.ContainsKey(position))
	            {
	                var layer = layers[position];
	                if (layer.ScannerPosition == 0)
	                {
	                    damage += layer.Depth * layer.Range;
	                    if (stopAtDamage)
	                    {
	                        return 1;
	                    }
                    }
	            }
	            Step(layers.Values);
	        }
	        return damage;
	    }

	    private static void Step(IEnumerable<Layer> layers)
	    {
	        foreach (var layer in layers)
	        {
	            layer.ScannerPosition += layer.Direction;

                if (layer.ScannerPosition == layer.Range - 1)
	            {
	                layer.Direction = -1;
	            }else if (layer.ScannerPosition == 0)
	            {
	                layer.Direction = 1;
	            }
	        }
	    }

	    private class Layer
	    {
	        public int Depth { get; set; }
            public int Range { get; set; }

            public int ScannerPosition { get; set; }
	        public int Direction { get; set; } = 1;

	        public Layer Copy()
	        {
	            return new Layer
	            {
	                Depth = Depth,
	                Range = Range,
	                ScannerPosition = ScannerPosition,
	                Direction = Direction
	            };
	        }

	        public void Reset()
	        {
	            ScannerPosition = 0;
	            Direction = 1;
	        }
	    }

		public ICollection<bool> Assertions => new[]
		{
            true
			//SolvePart1("") == "",
			//SolvePart2("") == "",
		};
	}
}
