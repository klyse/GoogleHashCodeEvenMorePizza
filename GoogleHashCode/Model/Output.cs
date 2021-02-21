using System;
using System.Collections.Generic;
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

		public int GetScore()
		{
			return 0;
			// throw new NotImplementedException();
		}
	}
}