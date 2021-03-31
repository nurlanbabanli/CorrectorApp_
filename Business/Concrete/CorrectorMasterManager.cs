using Business.Abstract;
using Business.BusinessMessages;
using Business.Helper.RuleChecker;
using Business.Utilities;
using Core.Results.Abstract;
using Core.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CorrectorMasterManager : ICorrectorMasterService
    {
        ICorrectorMasterDal _correctorMasterDal;

        public CorrectorMasterManager(ICorrectorMasterDal correctorMasterDal)
        {
            _correctorMasterDal = correctorMasterDal;
        }

        public IResult Add(CorrectorMaster correctorMaster)
        {
            IResult result = BusinessRules.Run(CorrectorMasterRules.CheckIfCorrectorIdExist(_correctorMasterDal, correctorMaster.Id),
                                               CorrectorMasterRules.CheckIfCorrectorNameExist(_correctorMasterDal,correctorMaster.DeviceName),
                                               CorrectorMasterRules.CheckIfCorrectorSerialNumberExist(_correctorMasterDal,correctorMaster.SerialNumber),
                                               CorrectorMasterRules.CheckIfCorrectorIpAddressIsExist(_correctorMasterDal,correctorMaster.IpAddress));

            if (result!=null)
            {
                return result;
            }
            _correctorMasterDal.Add(correctorMaster);
            return new SuccessResult(Messages.DeviceAdded);
        }

        public IDataResult<CorrectorMaster> GetById(int Id)
        {
            return new SuccessDataResult<CorrectorMaster>(_correctorMasterDal.Get(c => c.Id == Id));
        }

        public IDataResult<List<CorrectorMaster>> GetAll()
        {
            return new SuccessDataResult<List<CorrectorMaster>>(_correctorMasterDal.GetAll().ToList());
        }

        public IResult Update(CorrectorMaster correctorMaster)
        {
            IResult result = BusinessRules.Run(CorrectorMasterRules.CheckIfUpdatedNameIsNotUsed(_correctorMasterDal, correctorMaster.DeviceName, correctorMaster.Id),
                                             CorrectorMasterRules.CheckIfUpdatedSerialNumberIsNotUsed(_correctorMasterDal, correctorMaster.SerialNumber, correctorMaster.Id),
                                             CorrectorMasterRules.CheckIfUpdatedIpAddressIsNotUsed(_correctorMasterDal, correctorMaster.IpAddress, correctorMaster.Id));
            if (result!=null)
            {
                return result;
            }
            _correctorMasterDal.Update(correctorMaster);
            return new SuccessResult(Messages.DeviceUpdated);
        }

        public IDataResult<List<CorrectorMaster>> GetByDeviceType(int deviceType)
        {
            return new SuccessDataResult<List<CorrectorMaster>>(_correctorMasterDal.GetAll(c => c.DeviceType == deviceType));
        }

        public IDataResult<List<CorrectorMaster>> GetByCompany(int companyId)
        {
            return new SuccessDataResult<List<CorrectorMaster>>(_correctorMasterDal.GetAll(c => c.CompanyId == companyId));
        }
    }
}
