using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCustomerDal: EfEntityRepositoryBase<Customer, CarRentContext>, ICustomerDal
    {
        public List<CustomerDetailDto> GetCustomerDetails(Expression<Func<CustomerDetailDto, bool>> filter = null)
        {
            using (CarRentContext context = new CarRentContext())
            {
                var result = from cs in  context.Customers 
                             join u in context.Users
                             on cs.UserId equals u.Id
                             select new CustomerDetailDto
                             {
                                 Id = cs.Id,
                                 UserId = u.Id,
                                 CompanyName = cs.CompanyName,
                                 FirstName = u.FirstName,
                                 LastName = u.LastName,
                                 Email = u.Email,
                                 Status = u.Status,
                                 FindexScore = cs.FindexScore
                             };
                return filter == null ? result.ToList() 
                    : result.Where(filter).ToList();
            }
        }
        public CustomerDetailDto GetByEmail(Expression<Func<CustomerDetailDto, bool>> filter)
        {
            using (var context = new CarRentContext())
            {
                var result = from cs in context.Customers
                             join u in context.Users
                             on cs.UserId equals u.Id
                             select new CustomerDetailDto
                             {
                                 Id = cs.Id,
                                 UserId = cs.UserId,
                                 FirstName = u.FirstName,
                                 LastName = u.LastName,
                                 Email = u.Email,
                                 CompanyName = cs.CompanyName,
                                 FindexScore = (int)cs.FindexScore
                             };

                return result.SingleOrDefault(filter);
            }
        }

        public List<CustomerDetailDto> GetCustomerDetails()
        {
            using (var context = new CarRentContext())
            {
                var result = from cs in context.Customers
                             join u in context.Users
                             on cs.UserId equals u.Id
                             select new CustomerDetailDto
                             {
                                 Id = cs.Id,
                                 UserId = cs.UserId,
                                 FirstName = u.FirstName,
                                 LastName = u.LastName,
                                 Email = u.Email,
                                 CompanyName = cs.CompanyName,
                                 FindexScore = (int)cs.FindexScore
                             };

                return result.ToList();
            }
        }
    }
}
