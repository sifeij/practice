using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Calculator
{
    using Models;
    using Repository;
    using Services;
    using static Console;
    using Recipe = Dictionary<string, decimal>;

    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                WriteLine("Usage: 'Calculator file1 file2'");
                WriteLine("file1 are the ingredients");
                WriteLine("file2 are the recipes");
                return;
            }
            var repository = BuildRepository(args[0]);
            var recipes    = ReadRecipes(args[1]);
            var service    = new InvoiceCalculator(repository);

            PrintInvoices(recipes, service);
        }

        static IIngredientRepository BuildRepository(string filename)
        {
            var repository = new InMemoryIngredientRepository();
            File.ReadAllLines(filename)
                .Select(s => s.Split(','))
                .Skip(1)                    // header row
                .Select(s => new Ingredient
                {
                    Name         = s[0],
                    CategoryName = Map(s[1]),
                    UnitPrice    = Decimal.Parse(s[2])
                })
                .ToList()
                .ForEach(i => repository.Add(i));
            return repository;
        }

        static Category Map(String str)
        {
            switch (str)
            {
                case "Produce":
                    return Category.Produce;
                case "Meat/poultry":
                    return Category.MeatPoultry;
                case "Pantry":
                    return Category.Pantry;
                default:
                    throw new InvalidDataException($"Unknown category {str}");
            }
        }
        
        static IEnumerable<Recipe> ReadRecipes(string fileName)
        {
            var recipes = File.ReadAllLines(fileName)
                              .Select(s => s.Split(','))
                              .Skip(1)
                              .GroupBy(s => s[0])
                              .Select(g =>
                              {
                                  var recipe = new Recipe();
                                  g.ToList()
                                  .ForEach(v => recipe.Add(
                                                  v[1],                 // Name 
                                                  v[2].ParseToDecimal() // Quantity
                                                  )
                                          );
                                  return recipe;
                              });
            return recipes;
        }

        static void PrintInvoices(IEnumerable<Recipe> recipes, InvoiceCalculator service)
        {
            for (int i = 0; i < recipes.ToList().Count; i++)
            {
                var invoice = service.CalculateCost(recipes.ToList()[i]);
                WriteLine($"Recipe {i + 1}{invoice}\n");
            }
        }
    }
}
