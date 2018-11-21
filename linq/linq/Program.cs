using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace test
{
    public class Programm
    {

        public static void Main(string[] args)
        {
            var parkings = new List<Parking> { new Parking(), new Parking(), new Parking() };
            var sumCars = parkings.Sum(p => p.ClientList.Sum(c => c.OwnedCars.Count));
            Console.WriteLine("1.       Sum car " + sumCars);
            var sumEng = parkings.Sum(p => p.ClientList.Sum(c => c.OwnedCars.Sum(z => z.Engine.EngineCapacity)));
            Console.WriteLine("2.       Sum engine capacity " + sumEng);
            var sumEngBmw = parkings.Sum(p => p.ClientList.Sum(c => c.OwnedCars.Where(oc => oc.Manufacturer == Manufacturer.BMW).Sum(z => z.Engine.EngineCapacity)));
            Console.WriteLine("3.       Sum BMW engine capacity " + sumEngBmw);
            var sumHorseAudi = parkings.Sum(p => p.ClientList.Sum(c => c.OwnedCars.Where(oc => oc.Manufacturer == Manufacturer.Audi).Sum(z => z.Engine.HoursePower)));
            Console.WriteLine("4.       Sum horsepower Audi " + sumHorseAudi);
            var audiCars2 = parkings.Select(p => p.ClientList.Where(cl => cl.OwnedCars.Any(oc => oc.Manufacturer == Manufacturer.Ford))).SelectMany(c => c).Select(p => p.Name);
            var a = audiCars2.ToList();
            Console.WriteLine("5.       Name Ford owners :");
            foreach (string i in a)
            {
                Console.WriteLine(i);
            }
            var avAgeSub = parkings.Select(p => p.ClientList.Where(cl => cl.OwnedCars.Any(oc => oc.Manufacturer == Manufacturer.Subaru))).SelectMany(c => c).Select(p => p.Age);
            var aS = avAgeSub.ToList();
            int sumAge = 0;
            int count = 0;
            foreach (int i in aS)
            {
                sumAge = sumAge + i;
                count++;
            }
            Console.WriteLine("6.       Average age of Subaru owners :" + sumAge/count);
        }
    }


    public class Car
    {
        private string[] PredefinedCars = { "One", "Two", "Three" };
        public Car()
        {
            Name = PredefinedCars.GetRandom();
            Manufacturer = (Manufacturer)Helper.GetRandomIndex(0, Enum.GetNames(typeof(Manufacturer)).Length);
            Engine = new Engine();
            ManufactureDate = DateTime.Now - TimeSpan.FromMinutes(Helper.GetRandomIndex(0, 10));
        }

        public string Name { get; set; }

        public Engine Engine { get; set; }

        public DateTime ManufactureDate { get; set; }

        public Manufacturer Manufacturer { get; set; }
    }

    public class Parking
    {
        private string[] PredefinedAddresses = new[] { "Addr1", "Addr2", "Addr3", "Addr4" };
        private int[] PredefinedIds = { 0, 1, 2, 3, 4, 5, 6, 7 };
        private const int MinClientNumber = 100;

        public Parking()
        {
            Id = PredefinedIds.GetRandom();
            Address = PredefinedAddresses.GetRandom();
            ClientList = new List<Person>();

            for (int i = 0; i < MinClientNumber; i++)
            {
                ClientList.Add(new Person(i % 20 != 0));
            }
        }

        public int Id { get; set; }

        public string Address { get; set; }

        public ICollection<Person> ClientList { get; set; }
    }

    public class Engine
    {

        public Engine()
        {
            HoursePower = (short)Helper.GetRandomIndex(100, 400);
            EngineCapacity = Helper.GetRandomIndex(100, 400);
            Type = (EngineType)Helper.GetRandomIndex(0, Enum.GetNames(typeof(EngineType)).Length);
        }

        public short HoursePower { get; set; }

        public EngineType Type { get; set; }

        public float EngineCapacity { get; set; }
    }

    public class Person
    {
        private string[] PredefinedNames = { "Bob", "Jim", "Bim", "Sam", "Roy", "Tim", "Joe" };
        private int[] PredefindedAges = { 21, 33, 19, 18, 66, 45, 43, 26 };

        public Person(bool hasOneCar = true)
        {
            Name = PredefinedNames[Helper.GetRandomIndex(0, PredefinedNames.Length, true)];
            Age = PredefindedAges[Helper.GetRandomIndex(0, PredefindedAges.Length, true)];
            OwnedCars = new List<Car>();

            for (int i = 0; i < (hasOneCar ? 1 : 2); i++)
            {
                OwnedCars.Add(new Car());
            }
        }

        public string Name { get; set; }

        public int Age { get; set; }

        public ICollection<Car> OwnedCars { get; set; }

    }

    public enum EngineType
    {
        Diesel,
        Petrol
    }

    public enum Manufacturer
    {
        Mercedes,
        BMW,
        Audi,
        Subaru,
        Toyota,
        Ford,
        Volkswagen,
        Tesla,
        Jaguar,
        KIA,
        Hyndai,
        Porshe
        //etc
    }

    public static class Helper
    {
        public static int GetRandomIndex(int minNumber, int maxNumber, bool isArray = false)
        {
            Thread.Sleep(1);

            return new Random()
                .Next(minNumber, isArray ? maxNumber : --maxNumber);
        }

        public static T GetRandom<T>(this IEnumerable<T> array)
        {
            var innerArray = array.ToList();
            return innerArray[Helper.GetRandomIndex(0, innerArray.Count, true)];
        }
    }
}