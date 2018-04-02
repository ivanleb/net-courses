using System;

namespace netExample2
{
    class Car
    {
        public string Name { get; set; }
        public Car(string name)
        {
            Name = name;
        }
        public virtual void Display()
        {
            Console.WriteLine(Name);
        }
    }

    class BMW : Car
    {
        public BMW(string name) : base(name)
        {

        }

        public override void Display()
        {
            Console.WriteLine("I'm BMW :) -> " + Name);            
        }
    }

    delegate Car CarFactory(string name);

    delegate void PrintBMWInfo(BMW client);

    class Program
    {
        static void Main(string[] args)
        {
            CarFactory buildCar = BuildCar; // covariant assign
            Car bmwX5 = buildCar("BMW X5");
            bmwX5.Display();

            PrintBMWInfo getCarInfo = GetCarInfo; // contrvariant assign
            getCarInfo((BMW)bmwX5);
        }

        private static BMW BuildCar(string name)
        {
            return new BMW(name);
        }

        private static void GetCarInfo(Car p){
            p.Display();
        }
    }

   
}
