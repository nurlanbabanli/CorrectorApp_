using FieldEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldDataAccess.Concrete.Modbus.ModbusHelper
{
    public static class JoinMonthlyType2Parameters
    {
        public static FieldMonthlyType2ArchiveParameter JoinParameters(FieldMonthlyType2ArchivePart1Parameter fieldMonthlyType2ArchivePart1Parameter,
            FieldMonthlyType2ArchivePart2Parameter fieldMonthlyType2ArchivePart2Parameter,
            FieldMonthlyType2ArchivePart3Parameter fieldMonthlyType2ArchivePart3Parameter)
        {
            FieldMonthlyType2ArchiveParameter result = new FieldMonthlyType2ArchiveParameter();

            //join operation code here
            return result;
        }
    }
}
