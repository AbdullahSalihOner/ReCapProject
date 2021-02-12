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
        IDataResult<Rental> GetRentalById(int id);
        IDataResult<List<Rental>> GetAvailableCars();
        IDataResult<List<Rental>> GetUnAvailableCars();
        IDataResult<List<RentalDetailDto>> GetRentalDetails(int carId);
        IResult Add(Rental rental);
        IResult Delete(int id);
        IResult Update(Rental rental);
    }
}
