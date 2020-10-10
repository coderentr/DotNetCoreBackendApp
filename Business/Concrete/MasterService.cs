using Business.Abstract;
using Business.Contants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class MasterService : IMasterService
    {
        public IMasterDal _masterDal;
        public MasterService(IMasterDal masterDal)
        {
            _masterDal = masterDal;      
        }
        public IResult Add(Master master)
        {
            _masterDal.Add(master);
            return new SuccessResult();
        }

        public IResult Delete(Master master)
        {
            _masterDal.Delete(master);
            return new SuccessResult();
        }

        public IDataResult<Master> GetById(int Id)
        {
            return new SuccessDataResult<Master>(_masterDal.Get(m => m.Id == Id));
        }

        public IResult Update(Master master)
        {
            _masterDal.Update(master);
            return new SuccessResult(Messages.MasterUpdated);
        }
    }
}
