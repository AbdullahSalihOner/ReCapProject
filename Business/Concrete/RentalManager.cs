using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        IRentalDal _rentalDal;

        public RentalManager(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
        }

        [ValidationAspect(typeof(RentalValidator))]
        public IResult Add(Rental rental)
        {
            var result = _rentalDal.Get(p => p.CarId == rental.CarId && p.ReturnDate == null);
            if (result != null)
            {
                return new ErrorResult(Messages.CarInUse);
            }

            _rentalDal.Add(rental);
            return new SuccessResult(Messages.RentalAdded);
        }

        public IResult Delete(int id)
        {
            var result = _rentalDal.Get(p => p.Id == id);
            if (result == null)
            {
                return new ErrorResult(Messages.NoRecordsFound);
            }
            else if(result.ReturnDate == null)
            {
                return new ErrorResult(Messages.CarInUse);
            }
            else
            {
                _rentalDal.Delete(result);
                return new SuccessResult(Messages.RentalDeleted);
            }
            
        }

        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(), Messages.RentalListed);
        }

        public IDataResult<List<Rental>> GetAvailableCars()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(p => p.RentDate == null));
        }

        public IDataResult<Rental> GetRentalById(int id)
        {
            return new SuccessDataResult<Rental>(_rentalDal.Get(c => c.CarId == id));
        }

        public IDataResult<List<RentalDetailDto>> GetRentalDetails(int carId)
        {
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetRentalDetails(x => x.CarId == carId));
        }

        public IDataResult<List<Rental>> GetUnAvailableCars()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(p => p.ReturnDate == null));
        }

        public IResult Update(Rental rental)
        {
            _rentalDal.Update(rental);
            return new SuccessResult(Messages.RentalUpdated); 
        }
    }
}
