using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        public void Add(Car car)
        {
            if (car.DailyPrice > 0 && car.Descriptions.Length >= 2)
            {

                _carDal.Add(car);
                Console.WriteLine("New Car Succesfully Added");

            }
            else
            {
                Console.WriteLine("The car daily price  must be greater than 0 and  name must be at least 2 characters ");
            }
        }

        public void Delete(Car car)
        {
            _carDal.Delete(car);
            Console.WriteLine("Car Succesfully Deleted");
        }

        public List<Car> GetAll()
        {
            return _carDal.GetAll();
        }

        public Car GetById(int id)
        {
            return _carDal.Get(c=>c.CarId==id);
        }

        public List<CarDetailDto> GetCarDetails()
        {
            return _carDal.GetCarDetails();
        }

        public List<Car> GetCarsByBrandId(int id)
        {
            return _carDal.GetAll(c => c.BrandId == id);
        }

        public List<Car> GetCarsByColorId(int id)
        {
            return _carDal.GetAll(c => c.ColorId == id);
        }

        public void Update(Car car)
        {
            if (car.DailyPrice > 0 && car.Descriptions.Length >= 2)
            {
                _carDal.Update(car);
                Console.WriteLine("Car Succesfully Updated");
            }
            else
            {
                Console.WriteLine("The car daily price  must be greater than 0 and  name must be at least 2 characters ");
            }
        }
    }
}
