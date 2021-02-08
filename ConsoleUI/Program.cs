using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            //GetByColorId();
            //GetByBrandId();
            //AddTest();

            JoinTest();
        }

        private static void JoinTest()
        {
            CarManager carManager = new CarManager(new EfCarDal());
            foreach (var car in carManager.GetCarDetails())
            {
                Console.WriteLine(car.CarName + " / / " + car.BrandName + " / / " + car.CategoryName + " / / " +car.ColorName);
            }
        }

        private static void AddTest()
        {
            CarManager carManager = new CarManager(new EfCarDal());
            Car car = new Car
            {
                CarName = "Avansis",
                CategoryId = 1,
                BrandId = 6,
                ColorId = 1,
                ModelYear = 2013,
                DailyPrice = 300,
                Descriptions = "Manuel"

            };
            carManager.Add(car);
        }

        private static void GetByBrandId()
        {
            CarManager carManager = new CarManager(new EfCarDal());
            Console.WriteLine("Mercedes Cars\n____________");
            foreach (var car in carManager.GetCarsByBrandId(3))
            {
                Console.WriteLine(car.CarName);
            }
        }

        private static void GetByColorId()
        {
            CarManager carManager = new CarManager(new EfCarDal());
            Console.WriteLine("White Cars\n____________");
            foreach (var car in carManager.GetCarsByColorId(1))
            {
                Console.WriteLine(car.CarName);
            }
        }

    }
}
