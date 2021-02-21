using System;
using System.Collections.Generic;
using System.Linq;
using GoogleHashCode.Base;
using GoogleHashCode.Model;

namespace GoogleHashCode.Algorithms
{
	public class Solver1 : ISolver<Input, Output>
	{
		public Output Solve(Input input)
		{
			var rareThreshold = (int) (input.Pizzas.SelectMany(r => r.ingredients).Distinct().Count() * 0.05);
			var rareIngredients = input.Pizzas.SelectMany(r => r.ingredients)
				.GroupBy(r => r)
				.Select(r => new
				{
					Ingredient = r.Key,
					Count = r.Count()
				})
				.OrderBy(r => r.Count)
				.Take(rareThreshold)
				.Select(r => r.Ingredient)
				.ToList();

			var remainingPizzas = input.Pizzas
				.OrderByDescending(r => r.ingredients.Count)
				.ToList();

			var output = new Output();
			foreach (var team in input.NumberOfTeams.OrderByDescending(r => r.Key == 3))
			{
				for (var teamCount = 0; teamCount < team.Value && remainingPizzas.Count >= team.Key; ++teamCount)
				{
					List<Pizza> deliveredPizzas = new();

					var containsRare = false;
					for (var pizzaCount = 0; pizzaCount < team.Key; ++pizzaCount)
					{
						Pizza selectedPizza;

						if (!containsRare)
						{
							selectedPizza = remainingPizzas.FirstOrDefault(r => rareIngredients.Any(q => r.ingredients.Contains(q)));

							if (selectedPizza is null)
								selectedPizza = remainingPizzas.First();
							containsRare = true;
						}
						else
							selectedPizza = remainingPizzas.First();

						deliveredPizzas.Add(selectedPizza);
						remainingPizzas.Remove(selectedPizza);
					}

					output.Deliveries.Add(new Delivery(team.Key, deliveredPizzas.Select(r => r.id).ToList()));
				}
			}

			return output;
		}
	}
}