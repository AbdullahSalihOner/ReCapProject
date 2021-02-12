using Business.Concrete;
using Business.Constants;
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
            //UserTest();
            //JoinTest();
            //CustomerTest();

            //UserAddTest();

            //CustomerAddTest();


            //AddRentalTest();
            RentalManager rentalManager = new RentalManager(new EfRentalDal());
            foreach (var rentalCar in rentalManager.GetAll().Data)
            {
                Console.WriteLine(rentalCar.CustomerId + "// " + rentalCar.CarId + "//" + rentalCar.CarId + "//" + rentalCar.RentDate);
            }



            //GetRentalDetails();

        }

        private static void AddRentalTest()
        {
            RentalManager rentalManager = new RentalManager(new EfRentalDal());
            var result = rentalManager.Add(new Rental
            {

                CarId = 3,
                CategoryId = 1,
                CustomerId = 3,
                RentDate = new DateTime(2021, 3, 15),
            });
            Console.WriteLine(result.Message);
        }

        private static void CustomerAddTest()
        {
            CustomerManager customerManager = new CustomerManager(new EfCustomerDal());
            var result = customerManager.Add(new Customer
            {
                UserId = 3,
                CompanyName = "C Company"
            });
            Console.WriteLine(result.Message);
        }

        private static void GetRentalById()
        {
            RentalManager rentalManager = new RentalManager(new EfRentalDal());

            foreach (var details in rentalManager.GetRentalDetails(1).Data)
            {
                Console.WriteLine(details.UserName);
            }
        }

        private static void UserAddTest()
        {
            UserManager userManager = new UserManager(new EfUserDal());
            User user = new User
            {
                FirstName = "Montero",
                LastName = "E",
                Email = "MonteroE@html.com",
                Password = "7890"
            };
            userManager.Add(user);
        }

        private static void CustomerTest()
        {
            CustomerManager customerManager = new CustomerManager(new EfCustomerDal());
            foreach (var customer in customerManager.GetAll().Data)
            {
                Console.WriteLine(customer.CompanyName);
            }
        }

        private static void UserTest()
        {
            UserManager userManager = new UserManager(new EfUserDal());
            var result = userManager.GetAll();
            foreach (var user in result.Data)
            {
                Console.WriteLine(user.FirstName);
            }
        }

        private static void JoinTest()
        {
            CarManager carManager = new CarManager(new EfCarDal());
            var result = carManager.GetCarDetails();
            if (result.Success==true)
            {
                foreach (var car in result.Data)
                {
                    
                    
                    Console.WriteLine( car.CarName + " / / " + car.BrandName + " / / " + car.CategoryName + " / / " + car.ColorName);

                }

            }
            else
            {
                Console.WriteLine(result.Message);
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
            foreach (var car in carManager.GetCarsByBrandId(3).Data)
            {
                Console.WriteLine(car.CarName);
            }
        }

        private static void GetByColorId()
        {
            CarManager carManager = new CarManager(new EfCarDal());
            Console.WriteLine("White Cars\n____________");
            foreach (var car in carManager.GetCarsByColorId(1).Data)
            {
                Console.WriteLine(car.CarName);
            }
        }

    }
}
