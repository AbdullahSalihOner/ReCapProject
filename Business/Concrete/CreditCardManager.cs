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
    public class CreditCardManager : ICreditCardService
    {
        ICreditCardDal _creditCardDal;

        public CreditCardManager(ICreditCardDal creditCardDal)
        {
            _creditCardDal = creditCardDal;
        }

        public IResult Add(CreditCard creditCard)
        {
            var card = _creditCardDal.Get(f => f.CardNumber == creditCard.CardNumber && f.CustomerId == creditCard.CustomerId);
            if (card != null)
            {
                return new ErrorResult();
            }
            _creditCardDal.Add(creditCard);
            return new SuccessResult(Messages.CardAdded);
        }

        public IResult Delete(CreditCard creditCard)
        {
            _creditCardDal.Delete(creditCard);
            return new SuccessResult(Messages.CardDeleted);
        }

        public IDataResult<List<CreditCard>> GetAll()
        {
            return new SuccessDataResult<List<CreditCard>>(_creditCardDal.GetAll());
        }

        public IDataResult<List<CreditCard>> GetByCardNumber(string cardNumber)
        {
            return new SuccessDataResult<List<CreditCard>>(_creditCardDal.GetAll(c => c.CardNumber == cardNumber));
        }

        public IDataResult<List<CreditCard>> GetByCustomerId(int id)
        {
            return new SuccessDataResult<List<CreditCard>>(_creditCardDal.GetAll(c => c.CustomerId == id));
        }

        public IResult Update(CreditCard creditCard)
        {
            _creditCardDal.Update(creditCard);
            return new SuccessResult(Messages.CardUpdated);
        }

         public IResult IsCardExist(CreditCard creditCard)
        {
            var result = _creditCardDal.Get(c => c.NameOnTheCard == creditCard.NameOnTheCard && c.CardNumber == creditCard.CardNumber);
            if (result == null)
            {
                return new ErrorResult();
            }
            return new SuccessResult();
        }
        public IDataResult<CreditCard> GetById(int cardId)
        {
            return new SuccessDataResult<CreditCard>(_creditCardDal.Get(c => c.CardId == cardId));
        }
    }
}
