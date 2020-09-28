using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IMasterService
    {
        IDataResult<Master> GetById(int Id);
        IResult Add(Master master);
        IResult Update(Master master);
        IResult Delete(Master master);
    }
}
