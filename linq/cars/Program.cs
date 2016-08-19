using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using static System.Console;

namespace Cars
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // SimpleLambdaOperators();
            // WriteLine("*********************************************************************");
            // JoinTwoTablesWithSingleKey();
            // WriteLine("*********************************************************************");
            // JoinTwoTablesWithCompositKey();
            // WriteLine("*********************************************************************");

            // //To Get Top 2 Most Fuel Efficiency Cars Of Each Manufacturer
            // GroupByWithQuerySyntax();
            // WriteLine("*********************************************************************");
            // GroupByWithExensionMethods();
            // WriteLine("*********************************************************************");
            // GroupJoinWithQuerySyntax();
            // WriteLine("*********************************************************************");
            // GroupJoinWithExtensionMethods();
            // WriteLine("*********************************************************************");

            // //Get Top 3 Most Fuel Efficiency Cars Of Each Headquarter
            // DeepGroupJoinWithQuerySyntax();
            // WriteLine("*********************************************************************");
            // DeepGroupJoinWithExtensionMethods();
            // WriteLine("*********************************************************************");

            // //Get max min avg per manufacturer
            // AggregationWithQuerySyntax();
            // WriteLine("*********************************************************************");
            // AggregationWithExtensionMethods();
            // WriteLine("*********************************************************************");
        
            // Linq Xml
            SerializeXmlToXmlWriter();
            SerializeXmlToTextWriter();
            CreateXml();
            QueryXml();
        }

        static void QueryXml()
        {
            var document = XDocument.Load("fuel.xml");

            var query =
                // from element in document.Descendants()
                from element in document.Element("Cars").Elements("Car")
                where element.Attribute("Manufacturer")?.Value == "BMW"
                select element.Attribute("Name").Value;
            
            foreach (var name in query)
            {
                WriteLine(name);
            }
        }

        static void SerializeXmlToXmlWriter()
        {
            var records = ProcessCars("fuel.csv");

            var sb = new StringBuilder();
            var xws = new XmlWriterSettings();
            xws.OmitXmlDeclaration = true;
            xws.Indent = true;

            using (var xw = System.Xml.XmlWriter.Create(sb, xws)) {
                var doc = new XDocument(
                    new XElement("Cars",
                    from record in records
                    select new XElement("Car",
                                    new XAttribute("Name", record.Name),
                                    new XAttribute("Combined", record.Combined),
                                    new XAttribute("Manufacturer", record.Manufacturer))
                )
                );
                doc.Save(xw);
            }

            WriteLine(sb.ToString());
        }
        static void SerializeXmlToTextWriter()
        {
            var records = ProcessCars("fuel.csv");

            var document = new XDocument();
            var cars = new XElement("Cars",

                from record in records
                select new XElement("Car",
                                new XAttribute("Name", record.Name),
                                new XAttribute("Combined", record.Combined),
                                new XAttribute("Manufacturer", record.Manufacturer))
            );
            document.Add(cars);
            
            var sb = new StringBuilder();
            var tr = new StringWriter(sb);

            document.Save(tr);
            WriteLine(sb.ToString());
        }

        static void CreateXml()
        {
            var records = ProcessCars("fuel.csv");

            var document = new XDocument(
                new XElement("Cars",
                    from record in records
                    select new XElement("Car",
                                    new XAttribute("Name", record.Name),
                                    new XAttribute("Combined", record.Combined),
                                    new XAttribute("Manufacturer", record.Manufacturer))
                )
            );

            // document.Save("fuel.xml");
            // WriteLine(File.ReadAllText("fuel.xml"));
        }

        static List<Car> ProcessCars(string path)
        {
            var query =

                File.ReadAllLines(path)
                    .Skip(1)
                    .Where(l => l.Length > 1)
                    .ToCars();

            return query.ToList();
        }

        static void AggregationWithExtensionMethods()
        {
            var cars = ProcessFileUseLambda("fuel.csv");
            var manufacturers = ProcessManufacturers("manufacturers.csv");

            var query =
                cars.GroupBy(c => c.Manufacturer)
                    .Select(g =>
                    {
                        var results = g.Aggregate(
                                                    new CarStatistics(),
                                                    (acc, c) => acc.Accumulate(c),
                                                    acc => acc.Compute()
                                                 );
                        return new
                        {
                            Name = g.Key,
                            Avg = results.Average,
                            Min = results.Min,
                            Max = results.Max
                        };
                    })
                    .OrderByDescending(r => r.Max);

            foreach (var result in query)
            {
                WriteLine($"{result.Name}");
                WriteLine($"\t Max : {result.Max}");
                WriteLine($"\t Min : {result.Min}");
                WriteLine($"\t Avg : {result.Avg}");
            }
        }

        static void AggregationWithQuerySyntax()
        {
            var cars = ProcessFileUseLambda("fuel.csv");
            var manufacturers = ProcessManufacturers("manufacturers.csv");

            var query =
                from car in cars
                group car by car.Manufacturer into carGroup
                select new
                {
                    Name = carGroup.Key,
                    Max = carGroup.Max(c => c.Combined),
                    Min = carGroup.Min(c => c.Combined),
                    Avg = carGroup.Average(c => c.Combined)
                } into result
                orderby result.Max descending
                select result;

            foreach (var result in query)
            {
                WriteLine($"{result.Name}");
                WriteLine($"\t Max : {result.Max}");
                WriteLine($"\t Min : {result.Min}");
                WriteLine($"\t Avg : {result.Avg}");
            }
        }

        static void DeepGroupJoinWithExtensionMethods()
        {
            var cars = ProcessFileUseLambda("fuel.csv");
            var manufacturers = ProcessManufacturers("manufacturers.csv");

            var query = manufacturers.GroupJoin(
                                        cars,
                                        m => m.Name,
                                        c => c.Manufacturer,
                                        (m, g) =>
                                            new
                                            {
                                                Manufacturer = m,
                                                Cars = g
                                            })
                                     .GroupBy(m => m.Manufacturer.Headquarters);

            foreach (var group in query)
            {
                WriteLine($"{group.Key}");
                foreach (var car in group.SelectMany(g => g.Cars)
                                         .OrderByDescending(c => c.Combined)
                                         .Take(3))
                {
                    WriteLine($"\t{car.Name} : {car.Combined}");
                }
            }
        }

        static void DeepGroupJoinWithQuerySyntax()
        {
            var cars = ProcessFileUseLambda("fuel.csv");
            var manufacturers = ProcessManufacturers("manufacturers.csv");

            var query = from manufacturer in manufacturers
                        join car in cars
                            on manufacturer.Name
                            equals car.Manufacturer
                            into carGroup
                        orderby manufacturer.Name
                        select new
                        {
                            Manufacturer = manufacturer,
                            Cars = carGroup
                        } into result
                        group result by result.Manufacturer.Headquarters;

            foreach (var group in query)
            {
                WriteLine($"{group.Key}");
                foreach (var car in group.SelectMany(g => g.Cars)
                                         .OrderByDescending(c => c.Combined)
                                         .Take(3))
                {
                    WriteLine($"\t{car.Name} : {car.Combined}");
                }
            }
        }

        static void GroupJoinWithExtensionMethods()
        {
            var cars = ProcessFileUseLambda("fuel.csv");
            var manufacturers = ProcessManufacturers("manufacturers.csv");

            var query = manufacturers.GroupJoin(
                                        cars,
                                        m => m.Name,
                                        c => c.Manufacturer,
                                        (m, g) =>
                                            new
                                            {
                                                Manufacturer = m,
                                                Cars = g
                                            })
                                     .OrderBy(m => m.Manufacturer.Name);

            foreach (var group in query)
            {
                WriteLine($"{group.Manufacturer.Name} has {group.Manufacturer.Headquarters} cars");
                foreach (var car in group.Cars
                                         .OrderByDescending(c => c.Combined)
                                         .Take(2))
                {
                    WriteLine($"\t{car.Name} : {car.Combined}");
                }
            }
        }

        static void GroupJoinWithQuerySyntax()
        {
            var cars = ProcessFileUseLambda("fuel.csv");
            var manufacturers = ProcessManufacturers("manufacturers.csv");

            var query = from manufacturer in manufacturers
                        join car in cars
                            on manufacturer.Name
                            equals car.Manufacturer
                            into carGroup
                        orderby manufacturer.Name
                        select new
                        {
                            Manufacturer = manufacturer,
                            Cars = carGroup
                        };

            foreach (var group in query)
            {
                WriteLine($"{group.Manufacturer.Name} has {group.Manufacturer.Headquarters} cars");
                foreach (var car in group.Cars
                                         .OrderByDescending(c => c.Combined)
                                         .Take(2))
                {
                    WriteLine($"\t{car.Name} : {car.Combined}");
                }
            }
        }

        static void GroupByWithExensionMethods()
        {
            var cars = ProcessFileUseLambda("fuel.csv");
            var manufacturers = ProcessManufacturers("manufacturers.csv");

            var query = cars.GroupBy(c => c.Manufacturer.ToUpper())
                            .OrderBy(g => g.Key);
        }

        static void GroupByWithQuerySyntax()
        {
            var cars = ProcessFileUseLambda("fuel.csv");
            var manufacturers = ProcessManufacturers("manufacturers.csv");

            var query = from car in cars
                        group car by car.Manufacturer.ToUpper()
                                into manufacturer
                        orderby manufacturer.Key
                        select manufacturer;

            foreach (var group in query)
            {
                WriteLine($"{group.Key} has {group.Count()} cars");
                foreach (var car in group
                                        .OrderByDescending(c => c.Combined)
                                        .Take(2))
                {
                    WriteLine($"\t{car.Name} : {car.Combined}");
                }
            }
        }

        static void JoinTwoTablesWithCompositKey()
        {
            var cars = ProcessFileUseLambda("fuel.csv");
            var manufacturers = ProcessManufacturers("manufacturers.csv");

            // *************************** Use query syntax to join ****************************

            var query =
                    from car in cars
                    join manufacturer in manufacturers
                            on
                            new { car.Manufacturer, car.Year }
                            equals
                            new { Manufacturer = manufacturer.Name, manufacturer.Year }
                    orderby car.Combined descending, car.Name ascending
                    select new
                    {
                        manufacturer.Headquarters,
                        car.Name,
                        car.Combined
                    };

            foreach (var car in query.Take(10))
            {
                WriteLine($"{car.Headquarters,-15} : {car.Name,-20} : {car.Combined}");
            }
            WriteLine(" ");

            // *************************** Use extension method to join ****************************

            // inner sequency should be a smaller list than outer
            var query2 =
                cars.Join(manufacturers,
                            c => new { c.Manufacturer, c.Year },
                            m => new { Manufacturer = m.Name, m.Year },
                            (c, m) => new
                            {
                                m.Headquarters,
                                c.Name,
                                c.Combined
                            })
                    .OrderByDescending(c => c.Combined)
                    .ThenBy(c => c.Name);

            foreach (var car in query2.Take(10))
            {
                WriteLine($"{car.Headquarters,-15} : {car.Name,-20} : {car.Combined}");
            }
            WriteLine(" ");
        }

        static void JoinTwoTablesWithSingleKey()
        {
            var cars = ProcessFileUseLambda("fuel.csv");
            var manufacturers = ProcessManufacturers("manufacturers.csv");

            // *************************** Use query syntax to join ****************************

            var query =
                    from car in cars
                    join manufacturer in manufacturers
                            on car.Manufacturer equals manufacturer.Name
                    orderby car.Combined descending, car.Name ascending
                    select new
                    {
                        manufacturer.Headquarters,
                        car.Name,
                        car.Combined
                    };

            foreach (var car in query.Take(10))
            {
                WriteLine($"{car.Headquarters,-15} : {car.Name,-20} : {car.Combined}");
            }
            WriteLine(" ");

            // *************************** Use extension method to join ****************************

            // inner sequency should be a smaller list than outer
            var query2 =
                cars.Join(manufacturers,
                            c => c.Manufacturer,
                            m => m.Name,
                            (c, m) => new
                            {
                                m.Headquarters,
                                c.Name,
                                c.Combined
                            })
                    .OrderByDescending(c => c.Combined)
                    .ThenBy(c => c.Name);

            foreach (var car in query2.Take(10))
            {
                WriteLine($"{car.Headquarters,-15} : {car.Name,-20} : {car.Combined}");
            }
            WriteLine(" ");
        }

        static List<Manufacturer> ProcessManufacturers(string path)
        {
            var query = File.ReadAllLines(path)
                            .Where(l => l.Length > 1)
                            .Select(l =>
                            {
                                var columns = l.Split(',');
                                return new Manufacturer
                                {
                                    Name = columns[0],
                                    Headquarters = columns[1],
                                    Year = int.Parse(columns[2])
                                };
                            });
            return query.ToList();
        }
        static void SimpleLambdaOperators()
        {
            // ********************** Extension Method Syntax 1 *************************

            var cars = ProcessFileUseLambda("fuel.csv");

            var top = cars.OrderByDescending(c => c.Combined)
                            .ThenBy(c => c.Name)
                            .FirstOrDefault(c => c.Manufacturer == "BMW"
                                            && c.Year == 2016);


            WriteLine($"{top.Manufacturer} : {top.Name,-25} : {top.Combined}");
            WriteLine(" ");

            // ********************** Extension Method Syntax 2 *************************

            var cars1 = ProcessFileUseLambda("fuel.csv");
            var query1 = cars1.Where(c => c.Manufacturer == "BMW"
                                    && c.Year == 2016)
                              .OrderByDescending(c => c.Combined)
                              .ThenBy(c => c.Name);

            foreach (var car in query1.Take(10))
            {
                WriteLine($"{car.Manufacturer} : {car.Name,-25} : {car.Combined}");
            }
            WriteLine(" ");

            // ************************ Query Syntax ***********************

            var cars2 = ProcessFileUseLinqQuery("fuel.csv");
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
                WriteLine($"{car.Manufacturer} : {car.Name,-25} : {car.Combined}");
            }
            WriteLine(" ");

            // ************************ Any() All() Contains() methods ***********************

            var result1 = cars.Any(c => c.Manufacturer == "BMW");
            WriteLine("is there any car's manufacturer BMW? " + result1);
            WriteLine(" ");

            var result2 = cars.All(c => c.Manufacturer == "BMW");
            WriteLine("are there all cars manfacturer BMW? " + result2);
            WriteLine(" ");

            var item = new Car { Manufacturer = "BMW" };
            var result3 = cars.Contains(item);
            WriteLine("does cars contain an item: " + result3);
            WriteLine(" ");

            // ************************ SelectMany() method to flatten data ***********************

            var result = cars.SelectMany(c => c.Name)
                             .OrderBy(c => c);

            foreach (var character in result)
            {
                WriteLine(character);
            }
        }
        static List<Car> ProcessFileUseLambda(string path)
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

        static List<Car> ProcessFileUseLinqQuery(string path)
        {
            var query =
                    from line in File.ReadAllLines(path).Skip(1)
                    where line.Length > 1
                    select Car.ParseFromCsv(line);

            return query.ToList();
        }
    }
}
