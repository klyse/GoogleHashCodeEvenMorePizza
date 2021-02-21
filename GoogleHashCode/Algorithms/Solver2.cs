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
        List<string> IngredientOrder = new();


        void RemoveUsed(bool force = false)
        {
            if (Used.Count < 100 && !force)
                return;

            foreach (var item in IngredientPizzas.ToList())
                if (item.Value.Any(q => Used.Contains(q)))
                    item.Value.RemoveAll(q => Used.Contains(q));

            IngredientOrder = IngredientPizzas.Where(q => q.Value.Count > 0).OrderByDescending(q => q.Value.Count).Select(q => q.Key).ToList();
            Used.Clear();
        }

        Pizza FirstPizza()
        {
            var result = FirstUnusedPizza(IngredientPizzas[IngredientOrder[0]]);
            if (result != null)
                return result;

            RemoveUsed(true);

            if (IngredientOrder.Count > 0)
                return FirstUnusedPizza(IngredientPizzas[IngredientOrder[0]]);

            return null;
        }

        Pizza FirstUnusedPizza(List<Pizza> pizzas)
            => pizzas.FirstOrDefault(q => !Used.Contains(q));

        Pizza FirstUnusedPizza()
        {
            foreach (var item in IngredientOrder)
            {
                var pizza = FirstUnusedPizza(IngredientPizzas[item]);
                if (pizza != null)
                    return pizza;

            }

            return null;
        }

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

            RemoveUsed(true);


            var result = new Output();
            var pizzas = new List<Pizza>();
            var usedIngs = new HashSet<string>();

            bool AddPizza(Pizza pizza)
            {
                if (pizza == null)
                    return false;

                pizzas.Add(pizza);
                usedIngs.UnionWith(pizza.ingredients);
                Used.Add(pizza);
                return true;
            }

            foreach (var team in input.NumberOfTeams.OrderByDescending(q => q.Key))
            {
                for (var teamIndex = 0; teamIndex < team.Value; teamIndex++)
                {
                    pizzas.Clear();
                    usedIngs.Clear();

                    if (!AddPizza(FirstPizza()))
                        break;

                    for (var i = 1; i < team.Key; i++)
                    {
                        var added = false;
                        foreach (var ing in IngredientOrder.Where(q => !usedIngs.Contains(q)))
                        {
                            added = AddPizza(FirstUnusedPizza(IngredientPizzas[ing]));
                            if (added)
                                break;
                        }

                        if (!added)
                            AddPizza(FirstUnusedPizza());
                    }

                    if (pizzas.Count != team.Key)
                    {
                        foreach (var item in pizzas)
                            Used.Remove(item);
                        break;
                    }

                    result.Deliveries.Add(new Delivery(team.Key, pizzas.Select(q => q.id).ToList()));

                    RemoveUsed();
                }
            }

            return result;
        }
    }
}