﻿using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Helpers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        private readonly ICarImageDal _carImageDal;

        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImageDal = carImageDal;
        }


        public IDataResult<CarImage> GetById(int id)
        {
            return new SuccessDataResult<CarImage>(_carImageDal.Get(c => c.Id == id));
        }



        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll());
        }



        public IDataResult<List<CarImage>> GetImagesByCarId(int carId)
        {
            IResult result = BusinessRules.Run(CheckIfCarImageNull(carId));

            if (result != null)
            {
                return new ErrorDataResult<List<CarImage>>(result.Message);
            }
            return new SuccessDataResult<List<CarImage>>(CheckIfCarImageNull(carId).Data);
        }



        [ValidationAspect(typeof(CarImageValidator))]
        public IResult Add(CarImage carImage)
        {
            var result = BusinessRules.Run(CheckImageLimitByCarId(carImage.CarId));

            if (result != null)
            {
                return result;
            }

            _carImageDal.Add(carImage);

            return new SuccessResult(Messages.CarImageAdded);
        }

        [ValidationAspect(typeof(CarImageValidator))]
        public IResult Update(CarImage carImage)
        {
            _carImageDal.Update(carImage);
            return new SuccessResult(Messages.CarImageUpdated);
        }

        [ValidationAspect(typeof(CarImageValidator))]
        public IResult Delete(CarImage carImage)
        {
            _carImageDal.Delete(carImage);
            return new SuccessResult(Messages.CarImageUpdated);
        }

        //******************************************









        private IResult CheckImageLimitByCarId(int carId)
        {
            var result = _carImageDal.GetAll(c => c.CarId == carId).Count;
            if (result >= 5)
            {
                return new ErrorResult(Messages.CountOfPictureError);
            }

            return new SuccessResult();
        }
        private IDataResult<List<CarImage>> CheckIfCarImageNull(int carId)
        {
            try
            {
                string path = @"\Images\logo.jpg";
                var result = _carImageDal.GetAll(c => c.CarId == carId).Any();

                if (!result)
                {
                    List<CarImage> carImage = new List<CarImage>();
                    carImage.Add(new CarImage { CarId = carId, ImagePath = path });
                    return new SuccessDataResult<List<CarImage>>(carImage);
                }
            }
            catch (Exception exception)
            {

                return new ErrorDataResult<List<CarImage>>(exception.Message);
            }

            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(p => p.CarId == carId));
        }
    }
}
