using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;
        ICategoryService _categoryService;

        public CarManager(ICarDal carDal,ICategoryService categoryService)
        {
            _carDal = carDal;
            _categoryService = categoryService;
        }


        [ValidationAspect(typeof(CarValidator))]
        public IResult Add(Car car)
        {

            IResult result = BusinessRules.Run(CheckIfCarNameExists(car.CarName),
                 CheckIfCarCountOfCategoryCorrect(car.CategoryId),
                 ChechkIfCategoryLimitExceded());
            if (result != null)
            {
                return result;
            }

            _carDal.Add(car);
            return new SuccessResult(Messages.CarAdded);




        }


        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult(Messages.CarDeleted);
        }


        public IDataResult<List<Car>> GetAll()
        {
            if (DateTime.Now.Hour == 15)
            {
                return new ErrorDataResult<List<Car>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(), Messages.CarsListed);
        }



        public IDataResult<Car> GetById(int id)
        {
            return new SuccessDataResult<Car>(_carDal.Get(c => c.CarId == id));
        }



        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(), Messages.CarDetails);
        }



        public IDataResult<List<Car>> GetCarsByBrandId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.BrandId == id));
        }



        public IDataResult<List<Car>> GetCarsByColorId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.ColorId == id));
        }



        [ValidationAspect(typeof(CarValidator))]
        public IResult Update(Car car)
        {

            _carDal.Update(car);
            return new SuccessResult(Messages.CarUpdated);

        }




        //BUSİNESS CODE

        private IResult CheckIfCarCountOfCategoryCorrect(int categoryId)
        {
            var result = _carDal.GetAll(c => c.CategoryId == categoryId).Count;
            if (result >= 15)
            {
                return new ErrorResult(Messages.CarCountOfCategoryError);
            }
            return new SuccessResult();
        }
        private IResult CheckIfCarNameExists(string carName)
        {
            var result = _carDal.GetAll(c => c.CarName == carName).Any();
            if (result)
            {
                return new ErrorResult(Messages.CarNameAlreadyExists);
            }
            return new SuccessResult();
        }
        private IResult ChechkIfCategoryLimitExceded()
        {
            var result = _categoryService.GetAll();
            if (result.Data.Count>15)
            {
                return new ErrorResult(Messages.CategoryLimitExceded);
            }
            return new SuccessResult();
        }
    }
}
