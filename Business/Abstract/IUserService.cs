using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IUserService
    {
        IDataResult<List<OperationClaim>> GetClaims(User user);
        IDataResult<User> GetByUserId(int userId);
        IDataResult<User> GetByEmail(string email);
        IDataResult<List<User>> GetAll();
        IResult EditProfile(UserForUpdateDto user);


        IResult Add(User user);
        IResult Update(User user);
        IResult Delete(User user);
    }
}
