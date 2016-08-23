using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using static System.Console;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WriteLine("******************************** Range Expansion *************************************");
            RangeExpansion();
            WriteLine("******************************** Sort By Age *************************************");
            SortByAge();
        }

        static void RangeExpansion()
        {
            // e.g. "2,3-5,7" should be 2,3,4,5,7
            // e.g. "6,1-3,2-4" should be 1,2,3,4,6
            var origin = "6,1-3,2-4,36-41";
            var result = origin
                .Split(',')
                .Select(x => x.Split('-'))
                .Select(p => new { 
                            First = int.Parse(p[0]), 
                            Last = int.Parse(p.Last())})
                .SelectMany(r => Enumerable.Range(
                   r.First, r.Last - r.First + 1))
                .OrderBy(r => r)
                .Distinct();
                
            foreach(var i in result)
            {
                WriteLine(i);
            }
        }

        static void SortByAge()
        {
            var result = "Jason Puncheon, 26/06/1986; Jos Hooiveld, 22/04/1983; Kelvin Davis, 29/09/1976; Luke Shaw, 12/07/1995; Gaston Ramirez, 02/12/1990; Adam Lallana, 10/05/1988"
                .Split(';')
                .Select(n => n.Split(','))
                .Select(n => new { Name = n[0].Trim(), DateOfBirth = ParseDob(n[1])})
                .OrderByDescending(n => n.DateOfBirth)
                .Select(n => new { Name = n.Name, Age = GetAge(n.DateOfBirth) });
            
            foreach(var item in result)
            {
                WriteLine($"{item.Name, -20}: {item.Age}");
            }
                
        }

        static int GetAge(DateTime dateOfBirth)
        {
            var today = DateTime.Today;
            int age = today.Year - dateOfBirth.Year;
            if (dateOfBirth > today.AddYears(-age)) age--;
            return age;
        }
        static DateTime ParseDob(string date) => DateTime.ParseExact(date.Trim(), "d/M/yyyy", CultureInfo.InvariantCulture);
    
    }
}
