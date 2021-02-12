using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class UserManager:IUserService
    {
        IUserDal  _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public IResult Add(User user)
        {
            if (user.FirstName.Length > 2)
            {

                _userDal.Add(user);
                return new SuccessResult(Messages.UserAdded);


            }
            else
            {
                return new ErrorResult(Messages.UserNameInvalid);

            };
          
        }

        public IResult Delete(User user)
        {
            _userDal.Delete(user);
            return new SuccessResult(Messages.UserDeleted);
        }

        public IDataResult<List<User>> GetAll()
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll(),Messages.UsersListed);
        }

        public IResult Update(User user)
        {
            if (user.FirstName.Length > 2)
            {

                _userDal.Update(user);
                return new SuccessResult(Messages.UserUpdated);


            }
            else
            {
                return new ErrorResult(Messages.UserNameInvalid);

            };
            
        }
    }
}
