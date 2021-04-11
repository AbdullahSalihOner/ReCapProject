using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IPaymentService 
    {
        IDataResult<List<Payment>> GetAll();
        IResult Add(Payment payment);
        IResult Update(Payment payment);
        IResult Delete(Payment payment);
        IDataResult<Payment> Get(Payment payment);
        IResult GetList(List<Payment> list);
        IDataResult<Payment> GetById(int Id);

    }
}
