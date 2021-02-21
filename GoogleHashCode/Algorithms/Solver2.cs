using GoogleHashCode.Base;
using GoogleHashCode.Model;
using System.Collections.Generic;
using System.Linq;

namespace GoogleHashCode.Algorithms
{
    public class Solver2 : ISolver<Input, Output>
    {
        Dictionary<string, List<Pizza>> IngredientPizzas = new();
        HashSet<Pizza> Used = new();

        void RemoveUsed()
        {
            if (Used.Count < 100)
                return;

            foreach (var item in IngredientPizzas.ToList())
                if (item.Value.Any(q => Used.Contains(q)))
                    IngredientPizzas[item.Key] = item.Value.RemoveAll(q => Used.Contains(q)>);
        }

        string GetMostUsedIngredient()
            => IngredientPizzas.OrderByDescending(q => q.Value.Count).Select(q => q.Key).FirstOrDefault();

        Pizza FirstUnusedPizza(List<Pizza> pizzas)
            => pizzas.FirstOrDefault(q => !Used.Contains(q));

        public Output Solve(Input input)
        {
            foreach (var pizza in input.Pizzas)
            {
                foreach (var ing in pizza.ingredients)
                {
                    if (IngredientPizzas.TryGetValue(ing, out var list))
                        list.Add(pizza);
                    else
                        IngredientPizzas.Add(ing, new List<Pizza> { pizza });
                }
            }

            var result = new Output();
            var pizzas = new List<Pizza>();
            foreach (var team in input.NumberOfTeams.OrderByDescending(q => q.Key))
            {
                for (var teamIndex = 0; teamIndex < team.Value; teamIndex++)
                {
                    pizzas.Clear();
                    pizzas.



                    var pizzaIds = Pizzas.Skip(pizzaStart).Take(team.Key).Select(q => q.id).ToList();
                    if (pizzaIds.Count != team.Key)
                        break;

                    result.Deliveries.Add(new Delivery(team.Key, pizzaIds));

                    pizzaStart += team.Key;

                    RemoveUsed();
                }
            }

            return result;
        }
    }
}