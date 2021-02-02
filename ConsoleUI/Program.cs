using Business.Concrete;
using DataAccess.Concrete.InMemory;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            CarManager carManager = new CarManager(new InMemoryCarDal());

            Console.WriteLine("==================CARS=========================");
            Console.WriteLine("Car ID" + " |   Year    |"+ "       Model  ");
            Console.WriteLine("_______________________________________________");
            foreach (var car in carManager.GetAll())
            {
                Console.WriteLine(car.CarId + "      | "+car.ModelYear+ " model| "+car.BrandId.BrandName + "-" +car.Description );
            }
            Console.WriteLine("_______________________________________________");

            Console.WriteLine("==============Categorized By Id================");
            foreach (var item in carManager.GetById(2))
            {
                Console.WriteLine("Category ID : "+item.CarId );
                Console.WriteLine("Brand ID    : " + item.BrandId.Id + "(" + item.BrandId.BrandName + ")");
                Console.WriteLine("Model Year  : "+item.ModelYear);
                Console.WriteLine("Color ID    : " + item.ColorId.Id + "(" + item.ColorId.ColorName+")");
                Console.WriteLine("Daily Price : "+item.DailyPrice+" TL");
                Console.WriteLine("Description : "+item.Description);
                Console.WriteLine("Fuel Type   : "+ item.FuelType);
                Console.WriteLine("Gear Type   : "+item.GearType);

            }
            
          
          
        }
    }
}
