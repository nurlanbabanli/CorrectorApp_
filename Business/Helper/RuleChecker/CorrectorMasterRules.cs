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
            var result = correctorMasterDal.GetAll(c => c.SerialNumber.Trim().ToUpper() == serialNumber.Trim().ToUpper()).Any();
            if (result)
            {
                return new ErrorResult(Messages.DeviceSerialNumberIsExists);
            }
            return new SuccessResult();
        }

        public static IResult CheckIfCorrectorNameExist(ICorrectorMasterDal correctorMasterDal, string deviceName)
        {
            var result = correctorMasterDal.GetAll(c => c.DeviceName.Trim().ToUpper() == deviceName.Trim().ToUpper()).Any();
            if (result)
            {
                return new ErrorResult(Messages.DeviceNameIsExists);
            }
            return new SuccessResult();
        }


        public static IResult CheckIfCorrectorIpAddressIsExist(ICorrectorMasterDal correctorMasterDal, string deviceIpAddress)
        {
            var result = correctorMasterDal.GetAll(c => c.IpAddress.Trim().ToUpper() == deviceIpAddress.Trim().ToUpper()).Any();
            if (result)
            {
                return new ErrorResult(Messages.DeviceIpAddressIsExists);
            }
            return new SuccessResult();
        }


        public static IResult CheckIfUpdatedNameIsNotUsed(ICorrectorMasterDal correctorMasterDal, string deviceName, int id)
        {
            var result = correctorMasterDal.GetAll(c => c.DeviceName.Trim().ToUpper() == deviceName.Trim().ToUpper() && c.Id != id).Any();
            if (result)
            {
                return new ErrorResult(Messages.UpdatedDeviceNameIsExists);
            }
            return new SuccessResult();
        }
        
        public static IResult CheckIfUpdatedSerialNumberIsNotUsed(ICorrectorMasterDal correctorMasterDal, string serialNumber, int id)
        {
            var result = correctorMasterDal.GetAll(c => c.SerialNumber.Trim().ToUpper() == serialNumber.Trim().ToUpper() && c.Id != id).Any();
            if (result)
            {
                return new ErrorResult(Messages.UpdatedDeviceSerialNumberIsExists);
            }
            return new SuccessResult();
        }

        public static IResult CheckIfUpdatedIpAddressIsNotUsed(ICorrectorMasterDal correctorMasterDal, string ipAddress, int id)
        {
            var result = correctorMasterDal.GetAll(c => c.IpAddress.Trim().ToUpper() == ipAddress.Trim().ToUpper() && c.Id != id).Any();
            if (result)
            {
                return new ErrorResult(Messages.UpdatedDeviceIpAddressIsExists);
            }
            return new SuccessResult();
        }
    }
}
