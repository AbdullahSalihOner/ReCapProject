using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Business.Abstract
{
    public interface ICarImageService
    {
        IDataResult<CarImage> GetById(int id);

        IDataResult<List<CarImage>> GetAll();

        IDataResult<List<CarImage>> GetImagesByCarId(int carId);



        IResult Add( CarImage carImage);

        IResult Update( CarImage carImage);

        IResult Delete(CarImage carImage);
    }
}
