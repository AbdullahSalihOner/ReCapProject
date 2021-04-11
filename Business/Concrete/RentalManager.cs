using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        IRentalDal _rentalDal;
        ICarService _carService;
        ICustomerService _customerService;

        public RentalManager(IRentalDal rentalDal, ICarService carService, ICustomerService customerService)
        {
            _rentalDal = rentalDal;
            _carService = carService;
            _customerService = customerService;
        }


        public IDataResult<Rental> GetRentalById(int rentalId)
        {
            return new SuccessDataResult<Rental>(_rentalDal.Get(r => r.RentalId == rentalId));
        }


        public IDataResult<List<RentalDetailDto>> GetRentalDetails()
        {
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetRentalDetails());
        }

        [CacheAspect]

        public IDataResult<List<RentalDetailDto>> GetRentalDetailsByCarId(int carId)
        {
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetRentalDetails(r => r.CarId == carId), Messages.RentalListed);
        }

 
        public IDataResult<List<Rental>> GetAllByCarId(int carId)
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(r => r.CarId == carId));
        }

        public IDataResult<List<Rental>> GetByCustomerId(int customerId)
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(r => r.CustomerId == customerId));
        }


        public IResult CheckReturnDate(int carId)
        {
            var result = _rentalDal.GetAll(x => x.CarId == carId && x.ReturnDate == null);
            if (result.Count > 0) return new ErrorResult(Messages.RentalUndeliveredCar);
            return new SuccessResult();
        }


        [ValidationAspect(typeof(RentalValidator))]
        public IResult IsRentable(Rental rental)
        {
            var result = _rentalDal.GetAll(r => r.CarId == rental.CarId);

            if (result.Any(r =>
                r.ReturnDate >= rental.RentDate &&
                r.RentDate <= rental.ReturnDate
            )) return new ErrorResult(Messages.RentalNotAvailable);

            return new SuccessResult();
        }

        public IResult CheckIfFindex(int carId, int customerId)
        {
            var customer = _customerService.GetCustomerById(customerId).Data;
            var car = _carService.GetById(carId).Data;
            if (customer.FindexScore < car.FindexScore)
            {
                return new ErrorResult(Messages.NotEngouhFindex);
            }
            return new SuccessResult(Messages.EngouhFindex);
        }



        [SecuredOperation("user,rental.add,admin")]
        [ValidationAspect(typeof(RentalValidator))]

        public IResult Add(Rental rental)
        {

            var result = BusinessRules.Run(FindeksScoreCheck(rental.CustomerId, rental.RentalId),
            UpdateCustomerFindexPoint(rental.CustomerId, rental.RentalId));

            if (result != null)
                return result;
            _rentalDal.Add(rental);
            return new SuccessResult(Messages.RentalAdded);



        }

        public IResult Delete(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult(Messages.RentalDeleted);
        }
        [CacheAspect]
        public IDataResult<List<Rental>> GetAll()
        {
            if (DateTime.Now.Hour == 22)
            {
                return new ErrorDataResult<List<Rental>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(), Messages.CarsListed);
        }
        [CacheAspect]
        [PerformanceAspect(5)]
        public IDataResult<Rental> GetById(int rentalId)
        {
            return new SuccessDataResult<Rental>(_rentalDal.Get(r => r.RentalId == rentalId));
        }
        [ValidationAspect(typeof(RentalValidator))]
        [CacheRemoveAspect("IRentalService.Get")]
        public IResult Update(Rental rental)
        {
            _rentalDal.Update(rental);
            return new SuccessResult(Messages.RentalUpdated);
        }
        private IResult FindeksScoreCheck(int customerId, int carId)
        {
            var customerFindexPoint = _customerService.GetCustomerById(customerId).Data.FindexScore;

            if (customerFindexPoint == 0)
                return new ErrorResult(Messages.FindexZero);

            var carFindexPoint = _carService.GetById(carId).Data.FindexScore;

            if (customerFindexPoint < carFindexPoint)
                return new ErrorResult(Messages.InsufficientScore);

            return new SuccessResult();
        }

        private IResult UpdateCustomerFindexPoint(int customerId, int carId)
        {
            var customer = _customerService.GetCustomerById(customerId).Data;
            var car = _carService.GetById(carId).Data;

            customer.FindexScore = (car.FindexScore / 2) + customer.FindexScore;

            _customerService.Update(customer);
            return new SuccessResult();
        }
    }
}
