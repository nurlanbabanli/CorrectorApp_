using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Utilities.Security
{
    public static class MethodAccessCodes
    {       
        public const string BusinessHourlyArchiveManagerGetArchiveFromDatabase = "200";
        public const string BusinessHourlyArchiveManagerGetArchiveFromDevice = "201";
        public const string BusinessEventArchiveManagerGetArchiveFromDatabase = "300";
        public const string BusinessEventArchiveManagerGetArchiveFromDevice = "301";
        public const string BusinessDailyArchiveManagerGetArchiveFromDatabase = "400";
        public const string BusinessDailyArchiveManagerGetArchiveFromDevice = "401";
        public const string BusinessMonthlyType1ArchiveManagerGetArchiveFromDatabase = "500";
        public const string BusinessMonthlyType1ArchiveManagerGetArchiveFromDevice = "501";
        public const string BusinessMonthlyType2ArchiveManagerGetArchiveFromDatabase = "600";
        public const string BusinessMonthlyType2ArchiveManagerGetArchiveFromDevice = "601";
        public const string BusinessCurrentParameterGetFromDevice = "701";
    }
}
