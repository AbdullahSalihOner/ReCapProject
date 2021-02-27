using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
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
        ICarImageDal _carImageDal;

        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImageDal = carImageDal;
        }




        [ValidationAspect(typeof(CarImageValidator))]
        public IResult Add(IFormFile file, CarImage carImage)
        {
            IResult result = BusinessRules.Run(CheckCountOfPicture(carImage.CarId));
            if (result != null)
            {
                return result;
            }
            carImage.ImagePath = FileHelper.Add(file);
            carImage.Date = DateTime.Now;
            _carImageDal.Add(carImage);
            return new SuccessResult();
        }




        public IResult Delete(CarImage carImage)
        {
            _carImageDal.Delete(carImage);
            return new SuccessResult();
        }




        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(), Messages.CarImagesListed);
        }


        public IDataResult<CarImage> Get(int Id)
        {
            return new SuccessDataResult<CarImage>(_carImageDal.Get(c => c.Id == Id));
        }


        public IDataResult<List<CarImage>> GetById(int Id)
        {
            return new SuccessDataResult<List<CarImage>>(CheckIfCarImageNull(Id));
        }




        [ValidationAspect(typeof(CarImageValidator))]
        public IResult Update(IFormFile file, CarImage carImage)
        {
            carImage.ImagePath = FileHelper.Update(_carImageDal.Get(c => c.Id == carImage.Id).ImagePath, file);
            carImage.Date = DateTime.Now;
            _carImageDal.Update(carImage);
            return new SuccessResult(); ;
        }






        private List<CarImage> CheckIfCarImageNull(int id)
        {
            string path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName + @"\Images\carImages\logo.jpg");
            var result = _carImageDal.GetAll(c => c.CarId == id).Any();
            if (!result)
            {
                return new List<CarImage> { new CarImage { CarId = id, ImagePath = path, Date = DateTime.Now } };
            }
            return _carImageDal.GetAll(c => c.CarId == id);
        }


        private IResult CheckCountOfPicture(int carId)
        {
            var carImagecount = _carImageDal.GetAll(p => p.CarId == carId).Count;
            if (carImagecount >= 5)
            {
                return new ErrorResult(Messages.CountOfPictureError);
            }

            return new SuccessResult();
            //if (_carImageDal.GetAll(c => c.CarId == carId).Count >= 5)
            //{
            //    return new ErrorResult(Messages.CountOfPictureError);
            //}

            //return new SuccessResult();
        }
    }
}
