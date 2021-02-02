using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryCarDal : ICarDal
    {
        List<Car> _cars;

        public InMemoryCarDal()
        {
            _cars = new List<Car> {
            new Car{CarId=1,BrandId=new Brand{Id=1,BrandName="Fiat"},ColorId=new Color{Id=1,ColorName="WHİTE" },DailyPrice=99,ModelYear=2018,Description="Linea 1.6",FuelType="Diesel",GearType="Manual" },
            new Car{CarId=2,BrandId=new Brand{Id=1,BrandName="Fiat"},ColorId=new Color{Id=2,ColorName="BLACK" },DailyPrice=170,ModelYear=2019,Description="Egea",FuelType="Diesel",GearType="Manual"  },
            new Car{CarId=3,BrandId=new Brand{Id=2,BrandName="Ford"},ColorId=new Color{Id=1,ColorName="WHİTE" },DailyPrice=200,ModelYear=2019,Description="Focus ",FuelType="Diesel",GearType="Manual"  },
            new Car{CarId=4,BrandId=new Brand{Id=3,BrandName="Mercedes"},ColorId=new Color{Id=3,ColorName="SPACE GRAY" },DailyPrice=350,ModelYear=2019,Description="E250",FuelType="Gasoline",GearType="Automatic"  },
            new Car{CarId=5,BrandId=new Brand{Id=4,BrandName="Porsche"},ColorId=new Color{Id=1,ColorName="WHİTE" },DailyPrice=450,ModelYear=2020,Description="Taycan Turbo S",FuelType="Hybrid",GearType="Automatic"  }
            };
        }

        public void Add(Car car)
        {
            _cars.Add(car);
        }

        public void Delete(Car car)
        {
            Car carToDelete = _cars.SingleOrDefault(c => c.CarId == car.CarId);
            _cars.Remove(carToDelete);
        }

        public List<Car> GetAll()
        {
            return _cars;
        }

        public List<Car> GetById(int CarId)
        {
            return _cars.Where(c => c.CarId == CarId).ToList();
        }

        public void Update(Car car)
        {
            Car carToUpdate = _cars.SingleOrDefault(c=>c.CarId==car.CarId);
            carToUpdate.BrandId = car.BrandId;
            carToUpdate.ColorId = car.ColorId;
            carToUpdate.ModelYear = car.ModelYear;
            carToUpdate.DailyPrice = car.DailyPrice;
            carToUpdate.Description= car.Description;
        }
    }
}
