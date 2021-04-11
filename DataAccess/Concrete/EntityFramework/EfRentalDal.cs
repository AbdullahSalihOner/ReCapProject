using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, CarRentContext>, IRentalDal
    {
        public List<RentalDetailDto> GetRentalDetails(Expression<Func<Rental, bool>> filter = null)
        {

            using (CarRentContext context = new CarRentContext())
            {
                var result = from re in context.Rentals
                             join c in context.Cars on re.CarId equals c.CarId
                             join cs in context.Customers on re.CustomerId equals cs.Id
                             join us in context.Users on cs.UserId equals us.Id
                             join b in context.Brands on c.BrandId equals b.BrandId
                             join clr in context.Colors on c.ColorId equals clr.ColorId
                             select new RentalDetailDto
                             {
                                 Id = re.RentalId,
                                 CarId = c.CarId,
                                 CarName = c.CarName,
                                 BrandName = b.BrandName,
                                 ColorName = clr.ColorName,
                                 DailyPrice = c.DailyPrice,
                                 Description = c.Description,
                                 ModelYear = c.ModelYear,
                                 RentDate = re.RentDate,
                                 ReturnDate = re.ReturnDate,
                                 FirstName = us.FirstName,
                                 LastName = us.LastName,
                                 CompanyName = cs.CompanyName
                             };

                return result.ToList();

            }
        }

       

    }
}
