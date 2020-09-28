using Business.Abstract;
using Business.Contants;
using Core.Utilities.Results;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class MasterService : IMasterService
    {
        public EfMasterDal _masterDal { get; set; }
        public MasterService(EfMasterDal masterDal)
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
