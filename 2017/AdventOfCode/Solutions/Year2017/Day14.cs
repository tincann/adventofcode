using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions.Year2017
{
	public class Day14 : IPuzzleSolution
	{
		public int Year => 2017;
		public int Day => 14;

		public string SolvePart1(params string[] input)
		{
		    var rows = ToGrid(input[0]);

            return rows.Sum(row => row.Count(x => x == '1')).ToString();
		}

		public string SolvePart2(params string[] input)
		{
		    var rows = ToGrid(input[0]).ToList();

		    var grid = new Grid(rows.Count, rows[0].Length);
            
            var y = 0;
		    foreach (var row in rows)
		    {
		        for (var x = 0; x < row.Length; x++)
		        {
		            grid.Set(x, y, new Cell
		            {
		                Used = row[x] == '1',
		            });
		        }
		        y++;
		    }

		    var unassigned = grid.UsedCells.Where(x => x.GroupId == 0);

		    var groupId = 1;
            // ReSharper disable once PossibleMultipleEnumeration
		    while (unassigned.Any())
		    {
		        var cell = unassigned.First();

                GrowGroup(grid, cell, groupId++);
		    }

		    return (groupId - 1).ToString();
		}

	    private static void GrowGroup(Grid grid, Cell startCell, int groupId)
	    {
	        var q = new Queue<Cell>();
	        startCell.GroupId = groupId;
            q.Enqueue(startCell);
	        while (q.Any())
	        {
	            var cell = q.Dequeue();
	            var unassignedNeighbours = grid.GetNeighbours(cell).Where(x => x.GroupId == 0 && x.Used);
	            foreach (var nb in unassignedNeighbours)
	            {
	                nb.GroupId = groupId;
	                q.Enqueue(nb);
                }
            }
	    }

	    private class Grid
	    {
	        private readonly int _rows;
	        private readonly int _columns;
	        private readonly Cell[] _cells;

	        public IEnumerable<Cell> UsedCells => _cells.Where(x => x.Used);

	        private int To1DIndex(int x, int y) => _columns * y + x;

	        private bool InRange(int x, int y) => x >= 0 && x < _columns && y >= 0 && y < _rows;


            public Grid(int rows, int columns)
	        {
	            _rows = rows;
	            _columns = columns;
	            _cells = new Cell[rows * columns];
            }

	        public void Set(int x, int y, Cell cell)
	        {
	            var i = To1DIndex(x, y);
	            cell.Position = (x, y);

	            _cells[i] = cell;
	        }

	        public IEnumerable<Cell> GetNeighbours(Cell cell)
	        {
	            for (var dy = -1; dy <= 1; dy++)
	            for (var dx = -1; dx <= 1; dx++)
	            {
	                if (Math.Abs(dx) == Math.Abs(dy)) continue;

	                var x = cell.Position.x + dx;
	                var y = cell.Position.y + dy;

	                if (!InRange(x, y)) continue;

	                var i = To1DIndex(x, y);
	                yield return _cells[i];
	            }
	        }
        }

	    private class Cell
	    {
            public bool Used { get; set; }
	        public int GroupId { get; set; }

            public (int x, int y) Position { get; set; }
	    }

	    private static IEnumerable<string> ToGrid(string key)
	    {
	        for (var i = 0; i < 128; i++)
	        {
	            var rowString = $"{key}-{i}";
	            var hash = KnotHash(rowString);
	            var bytes = HexToBytes(hash);

	            var sb = new StringBuilder();
	            foreach (var b in bytes)
	            {
	                sb.Append(Convert.ToString(b, 2).PadLeft(8, '0'));
	            }

	            yield return sb.ToString();
	        }
        }

	    private static byte[] HexToBytes(string hexString)
	    {
	        var bytes = new byte[hexString.Length / 2];
	        for (var i = 0; i < hexString.Length / 2; i++)
	        {
	            bytes[i] = byte.Parse(hexString.Substring(i * 2, 2), NumberStyles.HexNumber);
	        }
	        return bytes;
	    }

	    private static string KnotHash(string input)
	    {
	        var hasher = new Day10();
	        return hasher.SolvePart2(input);
	    }

		public ICollection<bool> Assertions => new[]
		{
            SolvePart1("flqrgnkx") == "8108",
            SolvePart2("flqrgnkx") == "1242"
        };
	}
}
