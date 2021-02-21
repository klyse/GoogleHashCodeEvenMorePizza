using System;
using System.Collections.Generic;
using System.Linq;
using GoogleHashCode.Base;
using GoogleHashCode.Model;

namespace GoogleHashCode.Algorithms
{
	public class Solver3 : ISolver<Input, Output>
	{
		public Output Solve(Input input)
		{
			var output = new Output();
			
			var remainingPizzas = input.Pizzas
				.OrderByDescending(r => r.ingredients.Count)
				.ToList();


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
			
			var team2 = input.NumberOfTeams[2];
			var team3 = input.NumberOfTeams[3];
			var team4 = input.NumberOfTeams[4];

			do
			{
				if (team3 > 0)
					Team(3);
				
				if (team2 > 0)
					Team(2);
				
				if (team4 > 0)
					Team(4);

				team2--;
				team3--;
				team4--;
			} while (team2 > 0 || team3 > 0 || team4 > 0);

			void Team(int members)
			{
				if (remainingPizzas.Count < members)
					return;

				List<Pizza> deliveredPizzas = new();

				var containsRare = false;
				for (var pizzaCount = 0; pizzaCount < members; ++pizzaCount)
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

				output.Deliveries.Add(new Delivery(members, deliveredPizzas.Select(r => r.id).ToList()));
			}

			return output;
		}
	}
}