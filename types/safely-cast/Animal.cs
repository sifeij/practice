using static System.Console;

namespace SafelyCast {
    class Animal
    {
        public void Eat() 
        {
            WriteLine("Eating.");
        }
        
        public override string ToString()
        {
            return "I am an animal.";
        }
    }
}

