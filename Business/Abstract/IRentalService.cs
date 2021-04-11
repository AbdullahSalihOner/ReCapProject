using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IRentalService
    {
        IDataResult<List<Rental>> GetAll();
        IDataResult<Rental> GetRentalById(int rentalId);
        IDataResult<List<Rental>> GetAllByCarId(int carId);
        IDataResult<List<Rental>> GetByCustomerId(int customerId);
        IResult CheckReturnDate(int carId);
        IDataResult<List<RentalDetailDto>> GetRentalDetails();
        IDataResult<List<RentalDetailDto>> GetRentalDetailsByCarId(int carId);
        IResult IsRentable(Rental rental);

        IResult CheckIfFindex(int carId, int customerId);

        IResult Add(Rental rental);

        IResult Update(Rental rental);

        IResult Delete(Rental rental);
    }
}
