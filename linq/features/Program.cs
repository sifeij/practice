using System;
using System.Collections.Generic;
using static System.Console;
using System.Linq;

namespace Features
{
    //using Linq;
    public class Program
    {
        public static void Main(string[] args)
        {
            WriteLine("******************** Use Enumerator *******************");
            UseEnumerator();
            WriteLine(" ");
            WriteLine("******************** Use Func and Action *******************");
            UseFuncAndAction();
            WriteLine(" ");
            WriteLine("******************** Use Use Method Syntax *******************");
            UseMethodSyntax();
            WriteLine(" ");
            WriteLine("******************** Use Use Query Syntax *******************");
            UseQuerySyntax();
            WriteLine(" ");
        }

        static void UseQuerySyntax ()
        {
            var developers = new Employee[]
            {
                new Employee { Id = 1, Name = "Scott" },
                new Employee { Id = 2, Name = "Chris" }
            };

            var query = from developer in developers
                         where developer.Name.Length == 5
                         orderby developer.Name
                         select developer;
            foreach (var employee in query)
            {
                WriteLine("developers: " + employee.Name);
            }
        }
        static void UseMethodSyntax()
        {
            var developers = new Employee[]
            {
                new Employee { Id = 1, Name = "Scott" },
                new Employee { Id = 2, Name = "Chris" }
            };
            foreach (var employee in 
                        developers
                            .Where(e => e.Name.Length >= 3)
                            .OrderBy(e => e.Name))
            {
                WriteLine("developers: " + employee.Name);
            }
        }
        static void UseFuncAndAction()
        {
            Func<int, int> square = x => x * x;
            Func<int, int, int> add = (x, y) =>
            {
                int temp = x + y;
                return temp;
            };
            Action<int> write = x => WriteLine(x);
            write(square(add(3, 5)));
        }

        static void UseEnumerator()
        {
            IEnumerable<Employee> developers = new Employee[]
            {
                new Employee { Id = 1, Name = "Scott" },
                new Employee { Id = 2, Name = "Chris" }
            };
            WriteLine(developers.Count() + " developers");

            var enumeratorDeveloper = developers.GetEnumerator();
            while (enumeratorDeveloper.MoveNext())
            {
                WriteLine("developers: " + enumeratorDeveloper.Current.Name);
            }

            WriteLine(" ");

            IEnumerable<Employee> sales = new List<Employee>()
            {
                new Employee { Id = 3, Name = "Alex" }
            };
            WriteLine(sales.Count() + " sales");

            var enumeratorSales = sales.GetEnumerator();
            while (enumeratorSales.MoveNext())
            {
                WriteLine("sales: " + enumeratorSales.Current.Name);
            }
        }

        static bool NameStartsWithS(Employee employee)
        {
            return employee.Name.StartsWith("S");
        }
    }
}
