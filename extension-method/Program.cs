using System;
using System.ComponentModel.DataAnnotations;

namespace ExtensionMethod
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Divide(10, 0);
            GetDescriptionFromEnum();
            GetCountWords();
        }

        static int Divide(int amount, int divideBy)
        {
            try 
            {
                return amount / divideBy;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error: " + ex.FullMessage());
                return 0;
            }
        }

        static void GetDescriptionFromEnum()
        {
            Console.WriteLine("Description: " + TestEnums.Intro.GetDescription());
            Console.WriteLine("Name:        " + TestEnums.Intro.GetName());
            Console.WriteLine("Description: " + TestEnums.Beginner.GetDescription());
            Console.WriteLine("Name:        " + TestEnums.Beginner.GetName());
            Console.WriteLine("Description: " + TestEnums.Advanced.GetDescription());
            Console.WriteLine("Name:        " + TestEnums.Advanced.GetName());
            Console.WriteLine("Description: " + TestEnums.Library.GetDescription());
            Console.WriteLine("Name:        " + TestEnums.Library.GetName());
        }

        static void GetCountWords()
        {
            var result = 0;
            result = _testwords.WordCount();
            Console.WriteLine($"Total number of words in '{_testwords}' is {result}"); 
        }

        enum TestEnums
        {
            [Display(Name = "This is introduction course")]
            Intro,
            Beginner,
            [Display(Name = "This is advanced level")]
            Advanced,
            Library
        }

        readonly static string _testwords = "this extension method will count number of words in it.";
    }
}
