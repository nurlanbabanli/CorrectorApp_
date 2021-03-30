using Business.BusinessMessages;
using Core.Results.Abstract;
using Core.Results.Concrete;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Helper.RuleChecker
{
    public static class CorrectorMasterRules
    {
        public static IResult CheckIfCorrectorIdExist(ICorrectorMasterDal correctorMasterDal, int id)
        {
            var result = correctorMasterDal.GetAll(c => c.Id == id).Any();
            if (result)
            {
                return new ErrorResult(Messages.DeviceIdIsExists);
            }
            return new SuccessResult();
        }

        public static IResult CheckIfCorrectorSerialNumberExist(ICorrectorMasterDal correctorMasterDal, string serialNumber)
        {
            var result = correctorMasterDal.GetAll(c => c.SerialNumber == serialNumber).Any();
            if (result)
            {
                return new ErrorResult(Messages.DeviceSerialNumberIsExists);
            }
            return new SuccessResult();
        }

        public static IResult CheckIfCorrectorNameExist(ICorrectorMasterDal correctorMasterDal, string deviceName)
        {
            var result = correctorMasterDal.GetAll(c => c.DeviceName == deviceName).Any();
            if (result)
            {
                return new ErrorResult(Messages.DeviceNameIsExists);
            }
            return new SuccessResult();
        }

    }
}
