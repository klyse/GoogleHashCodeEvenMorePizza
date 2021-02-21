using System.Collections.Generic;
using System.Linq;

namespace GoogleHashCode.Model
{
	public record Pizza(int id, HashSet<string> ingredients);

	public record Input
	{
		public int NumberOfPizzas { get; set; }
		public SortedDictionary<int, int> NumberOfTeams { get; set; } = new();
		public List<Pizza> Pizzas = new();

		static Pizza ParsePizza(int id, string line)
			=> new Pizza(id, line.Split(' ').Skip(1).ToHashSet());

		public static Input Parse(string[] values)
		{
			var first = values[0].Split(' ', System.StringSplitOptions.RemoveEmptyEntries).Select(q => int.Parse(q)).ToArray();
			var id = 0;
			var result = new Input
			{
				NumberOfPizzas = first[0],
				NumberOfTeams = new SortedDictionary<int, int> { { 2, first[1] }, { 3, first[2] }, { 4, first[3] } },
				Pizzas = values.Skip(1).Select(q => ParsePizza(id++, q)).ToList()
			};

			return result;
		}
	}
}