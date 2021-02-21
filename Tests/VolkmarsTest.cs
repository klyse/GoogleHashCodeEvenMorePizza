using System;
using GoogleHashCode;
using GoogleHashCode.Algorithms;
using GoogleHashCode.Model;
using NUnit.Framework;

namespace Tests
{
	public class VolkmarsTest
	{
		[Test]
		[TestCase("a_example.in")]
		[TestCase("b_little_bit_of_everything.in")]
		[TestCase("c_many_ingredients.in")]
		[TestCase("d_many_pizzas.in")]
		[TestCase("e_many_teams.in")]
		public void Solver2(string example)
		{
			var content = example.ReadFromFile();
			var solver = new Solver2();
			var input = Input.Parse(content);
			var output = solver.Solve(input);

			Console.WriteLine($"Total Score: {output.GetScore(input)}");
			example.WriteToFile(output.GetOutputFormat());
			Assert.Pass();
		}
	}
}