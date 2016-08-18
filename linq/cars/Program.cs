using System.Collections.Generic;
using System.IO;
using System.Linq;
using static System.Console;

namespace Cars
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // ********************** Extension Method Syntax 1 *************************

            var cars = ProcessFile1("fuel.csv");

            var top = cars.OrderByDescending(c => c.Combined)
                            .ThenBy(c => c.Name)
                            .FirstOrDefault(c => c.Manufacturer == "BMW" 
                                            && c.Year == 2016);

                
            WriteLine($"{top.Manufacturer} : {top.Name, -25} : {top.Combined}");
            WriteLine(" ");

            // ********************** Extension Method Syntax 2 *************************

            var cars1 = ProcessFile1("fuel.csv");
            var query1 = cars1.Where(c => c.Manufacturer == "BMW" 
                                    && c.Year == 2016)
                              .OrderByDescending(c => c.Combined)
                              .ThenBy(c => c.Name);

            foreach (var car in query1.Take(10))
            {
                WriteLine($"{car.Manufacturer} : {car.Name, -25} : {car.Combined}");
            }
            WriteLine(" ");

            // ************************ Query Syntax ***********************
            
            var cars2 = ProcessFile2("fuel.csv");
            var query2 = 
                    from car in cars2
                    where car.Manufacturer == "BMW" 
                            && car.Year == 2016
                    orderby car.Name ascending
                    select new                  // returns an anoymous type instead of "select car"
                    {
                        car.Manufacturer,
                        car.Name,
                        car.Combined
                    };

            foreach (var car in query2.Take(10))
            {
                WriteLine($"{car.Manufacturer} : {car.Name, -25} : {car.Combined}");
            }
            WriteLine(" ");

            // ************************ Any() All() Contains() methods ***********************

            var result1 = cars.Any(c => c.Manufacturer == "BMW");
            WriteLine("is there any car's manufacturer BMW? " + result1);
            WriteLine(" ");

            var result2 = cars.All(c => c.Manufacturer == "BMW");
            WriteLine("are there all cars manfacturer BMW? " + result2);
            WriteLine(" ");

            var item = new Car { Manufacturer = "BMW"};
            var result3 = cars.Contains(item);
            WriteLine("does cars contain an item: " + result3);
            WriteLine(" ");

            // ************************ SelectMany() method ***********************

            var result = cars.SelectMany(c => c.Name)
                             .OrderBy(c => c);

            foreach (var character in result)
            {
                WriteLine(character);
            }
        }

        static List<Car> ProcessFile1(string path)
        {
            return
                File.ReadAllLines(path)
                    .Skip(1)
                    .Where(line => line.Length > 1)
                    //.Select(Car.ParseFromCsv) this works too, magically
                    //.Select(l => Car.ParseFromCsv(l))
                    //.ToList();
                    .ToCars()
                    .ToList();
        }

        static List<Car> ProcessFile2(string path)
        {
            var query =
                    from line in File.ReadAllLines(path).Skip(1)
                    where line.Length > 1
                    select Car.ParseFromCsv(line);

            return query.ToList();
        }
    }
}
