using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryCarDal : ICarDal
    {
        List<Car> _cars;
        public InMemoryCarDal()
        {
            _cars = new List<Car> {
                new Car{Id=1, BrandId=1, ColorId=1, ModelYear=1990, DailyPrice=500, Description="Lüks bmw 90 model"},
                new Car{Id=2, BrandId=1, ColorId=2, ModelYear=1991, DailyPrice=600, Description="Lüks bmw 91 model"},
                new Car{Id=3, BrandId=1, ColorId=3, ModelYear=1992, DailyPrice=700, Description="Lüks bmw 92 model"},
                new Car{Id=4, BrandId=2, ColorId=4, ModelYear=1993, DailyPrice=800, Description="Lüks bmw 93 model"},
                new Car{Id=5, BrandId=2, ColorId=5, ModelYear=1994, DailyPrice=900, Description="Lüks bmw 94 model"}
            };
        }
        public void Add(Car car)
        {
            _cars.Add(car);
        }

        public void Delete(Car car)
        {
            Car carToDelete = _cars.SingleOrDefault(c=>c.Id == car.Id);
            _cars.Remove(carToDelete);
        }

        public Car Get(Expression<Func<Car, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Car> GetAll()
        {
            return _cars;
        }

        public List<Car> GetAll(Expression<Func<Car, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public List<Car> GetById(int Id)
        {
            return _cars.Where(c=>c.Id == Id).ToList();
        }

        public List<CarDetailDto> GetCarDetails()
        {
            throw new NotImplementedException();
        }

        public void Update(Car car)
        {
            Car carToUpdate = _cars.SingleOrDefault(c=>c.Id == car.Id);
            carToUpdate.Description = car.Description;
            carToUpdate.ColorId = car.ColorId;
            carToUpdate.DailyPrice = carToUpdate.DailyPrice;
            carToUpdate.BrandId = car.BrandId;
        }
    }
}
