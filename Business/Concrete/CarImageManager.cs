using Business.Abstract;
using Business.Constants;
using Core.Utilities.Helpers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        private ICarImageDal _carImageDal;

        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImageDal = carImageDal;
        }

        public IResult Add(IFormFile file, CarImage carImage)
        {
            //CheckCarImageCount();
            carImage.Date = DateTime.Now;
            carImage.ImagePath = SaveImage(file);
            _carImageDal.Add(carImage);
            return new SuccessResult(Messages.CarImageAdded);
        }

        public IResult Delete(CarImage carImage)
        {
            DeleteImage(carImage.Id);
            _carImageDal.Delete(carImage);

            return new SuccessResult(Messages.CarImageDeleted);
        }

        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(), Messages.ImageListed);
        }

        public IDataResult<CarImage> GetById(int id)
        {
            return new SuccessDataResult<CarImage>(_carImageDal.Get(ci => ci.Id == id));
        }

        public IDataResult<List<CarImage>> GetCarImageByCarId(int carId)
        {
            var getListByCarId = _carImageDal.GetAll(car => car.CarId == carId);

            if (getListByCarId.Count > 0)
                return new SuccessDataResult<List<CarImage>>(getListByCarId);

            var path = "Uploads/Images/CarImages/defaultImage.png";
            var defaultImage = new List<CarImage> { new CarImage { ImagePath = path } };

            return new SuccessDataResult<List<CarImage>>(defaultImage);
        }

        public IResult Update(IFormFile file, CarImage carImage)
        {
            carImage.Date = DateTime.Now;
            carImage.ImagePath = UpdateImage(carImage.Id, file);

            _carImageDal.Update(carImage);

            return new SuccessResult(Messages.CarImageUpdated);
        }

        private IResult CheckCarImageCount(int carId)
        {
            if (_carImageDal.GetAll(ci => ci.CarId == carId).Count >= 5)
            {
                return new ErrorResult(Messages.CarImageCountError);
            }

            return new SuccessResult();
        }

        private string SaveImage(IFormFile file)
        {
            return FileHelperAsync.ImageSave(file).Result.ToString();
        }

        private void DeleteImage(int carImageId)
        {
            var carImage = _carImageDal.Get(c => c.Id == carImageId);
            var path = carImage.ImagePath;

            File.Delete(Directory.GetParent(Directory.GetCurrentDirectory()) + PathNames.BaseName + path);
        }

        private string UpdateImage(int carImageId, IFormFile file)
        {
            DeleteImage(carImageId);
            return SaveImage(file);
        }
    }
}
