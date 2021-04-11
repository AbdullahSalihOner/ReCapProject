using Business.Abstract;
using Business.Constants;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class PaymentManager : IPaymentService
    {
        IPaymentDal _paymentDal;
        ICustomerService _customerService;
        ICreditCardService _creditCardService;
        public PaymentManager(
            IPaymentDal paymentDal,
            ICustomerService customerService,
            ICreditCardService creditCardService
            )
        {
            _paymentDal = paymentDal;
            _creditCardService = creditCardService;
            _customerService = customerService;
        }

        public IResult Add(Payment entity)
        {
            IResult result = BusinessRules.Run(
                CheckIsCreditCardExist(entity.CardNumber, entity.ExpirationDate, entity.CVV));

            if (result != null)
            {
                return result;
            }
            _paymentDal.Add(entity);

            return new SuccessResult("Payment" + Messages.AddSingular);
        }

        public IResult Delete(Payment entity)
        {
            _paymentDal.Delete(entity);
            return new SuccessResult("Payment" + Messages.DeleteSingular);
        }

        public IDataResult<Payment> FindByID(int Id)
        {
            Payment p = new Payment();
            if (_paymentDal.GetAll().Any(x => x.Id == Id))
            {
                p = _paymentDal.GetAll().FirstOrDefault(x => x.Id == Id);
            }
            else Console.WriteLine(Messages.NotExist + "payment");
            return new SuccessDataResult<Payment>(p);
        }

        public IDataResult<Payment> Get(Payment entity)
        {
            return new SuccessDataResult<Payment>(_paymentDal.Get(x => x.Id == entity.Id));
        }

        public IDataResult<List<Payment>> GetAll()
        {
            return new SuccessDataResult<List<Payment>>(_paymentDal.GetAll());
        }

        public IDataResult<Payment> GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public IResult GetList(List<Payment> list)
        {
            throw new NotImplementedException();
        }

        public IResult Update(Payment entity)
        {
            _paymentDal.Update(entity);
            return new SuccessResult("Payment" + Messages.UpdateSingular);
        }

        private IResult CheckIsCreditCardExist(string cardNumber, string expirationDate, string securityCode)
        {
            if (_creditCardService.GetAll().Data.Any(x => x.CardNumber == cardNumber))
            {
                if (!_creditCardService.GetAll().Data.Any(
                    x => x.CardNumber == cardNumber &&
                    x.ExpirationDate == expirationDate &&
                    x.CVV == securityCode
                    ))
                {
                    return new ErrorResult(Messages.NotExist + "credit card");
                }
            }
            return new SuccessResult();
        }
    }
}
