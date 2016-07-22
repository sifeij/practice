using static System.Console;

namespace SafelyCast
{
    public class SafeCasting
    {
        public static void Main(string[] args)
        {
            SafeCasting app = new SafeCasting();

            var g = new Giraffe();
            app.UseIsOperator(g);

            app.UseAsOperator(g);

            SuperNova sn = new SuperNova();
            app.UseAsOperator(sn);

            int i = 5;
            app.UseAsWithNullable(i);

            double d = 9.78654;
            app.UseAsWithNullable(d);

            WriteLine("Press any key to exit.");
            ReadKey();
        }

        void UseIsOperator(Animal a)
        {
            if (a is Mammal)
            {
                Mammal m = (Mammal)a;
                m.Eat();
            }
        }
        void UseAsOperator(object o)
        {
            var m = o as Mammal;
            if (m != null)
            {
                WriteLine(m.ToString());
            }
            else
            {
                WriteLine($"{o.GetType().Name} is not a Mammal");
            }
        }

        void UseAsWithNullable(System.ValueType val)
        {
            int? j = val as int?;
            if (j != null)
            {
                WriteLine(j);
            }
            else
            {
                WriteLine($"Could not convert {val.ToString()}");
            }
        }
    }
}
