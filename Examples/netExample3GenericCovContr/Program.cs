using System;

namespace netExample3
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

    class Program
    {
        delegate T Builder<out T>(string name); // out is important for covariant
        delegate void GetInfo<in T>(T item); // in is important for covariant

        static void Main(string[] args)
        {
            Builder<Car> carBuilder = BuildCar;
            Builder<BMW> bmwBuilder = BuildBMW;

            carBuilder = bmwBuilder; // covariant

            Car bmwX11 = carBuilder("X11");
            bmwX11.Display();

            GetInfo<BMW> bmwInfo = BmwInfo;
            GetInfo<Car> carInfo = CarInfo;

            bmwInfo = carInfo; // contravariant

            carInfo(bmwX11);
        }

        private static void BmwInfo(BMW bmw)
        {
            bmw.Display();
        }
 
        private static void CarInfo(Car car)
        {
            car.Display();
        }

        private static Car BuildCar(string name)
        {
            return new Car(name);
        }
        private static BMW BuildBMW(string name)
        {
            return new BMW(name);
        }
    }
}
