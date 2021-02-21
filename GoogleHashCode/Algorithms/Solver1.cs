using System.Linq;
using GoogleHashCode.Base;
using GoogleHashCode.Model;

namespace GoogleHashCode.Algorithms
{
	public class Solver1 : ISolver<Input, Output>
	{
		public Output Solve(Input input)
		{
			var remainingPizzas = input.Pizzas;

			var output = new Output();
			foreach (var inputNumberOfTeam in input.NumberOfTeams)
			{
				for (var x = 0; x < inputNumberOfTeam.Value && remainingPizzas.Count >= inputNumberOfTeam.Key; ++x)
				{
					var deliveredPizzas = remainingPizzas.Take(inputNumberOfTeam.Key).ToList();
					foreach (var deliveredPizza in deliveredPizzas)
					{
						remainingPizzas.Remove(deliveredPizza);
					}
					
					output.Deliveries.Add(new Delivery(inputNumberOfTeam.Key, deliveredPizzas.Select(r => r.id).ToList()));
				}
			}

			return output;
		}
	}
}