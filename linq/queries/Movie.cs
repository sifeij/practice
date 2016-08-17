using static System.Console;

namespace Queries
{
    public class Movie
    {
        public string Title { get; set; }
        public float Rating { get; set; }

        int _year;
        public int Year
        {   get
            {
                WriteLine($"Returning {_year} for {Title}");                
                return _year;
            }
            set
            {
                _year = value;
            }
        }
    }
}
