using System;
using System.Collections.Generic;
using System.Linq;
using GoogleHashCode.Base;

namespace GoogleHashCode.Model
{
	public record Delivery(int teamSize, List<int> pizzaIds);


	public record Output : IOutput
	{
		public List<Delivery> Deliveries = new List<Delivery>();

		public string[] GetOutputFormat()
		{
			var result = new List<string>();
			result.Add(Deliveries.Count.ToString());
			foreach (var item in Deliveries)
				result.Add($"{item.teamSize} {string.Join(' ', item.pizzaIds)}");

			return result.ToArray();
		}

		public int GetScore(Input input)
		{
			return (int) Deliveries.Sum(r => Math.Pow(r.pizzaIds.SelectMany(p => input.Pizzas[p].ingredients).Distinct().Count(), 2));
		}
	}
}