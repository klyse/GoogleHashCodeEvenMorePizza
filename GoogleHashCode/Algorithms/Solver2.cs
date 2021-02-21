using GoogleHashCode.Base;
using GoogleHashCode.Model;
using System.Collections.Generic;
using System.Linq;

namespace GoogleHashCode.Algorithms
{
    public class Solver2 : ISolver<Input, Output>
    {
        List<Pizza> Pizzas = new();

        public Output Solve(Input input)
        {
            Pizzas = input.Pizzas.OrderByDescending(q => q.ingredients.Count).ToList();
            var pizzaStart = 0;

            var result = new Output();
            foreach (var team in input.NumberOfTeams.OrderByDescending(q => q.Key))
            {
                for (var teamIndex = 0; teamIndex < team.Value; teamIndex++)
                {
                    var pizzaIds = Pizzas.Skip(pizzaStart).Take(team.Key).Select(q => q.id).ToList();
                    if (pizzaIds.Count != team.Key)
                        break;

                    result.Deliveries.Add(new Delivery(team.Key, pizzaIds));

                    pizzaStart += team.Key;
                }
            }

            return result;
        }
    }
}