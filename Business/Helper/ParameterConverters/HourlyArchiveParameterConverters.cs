using Entities.Concrete;
using FieldEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Helper.ParameterConverters
{
    public class HourlyArchiveParameterConverters
    {
        public static HourlyArchiveParameter ConvertToDatabaseFormat(FieldHourlyArchiveParameter fieldHourlyArchiveParameter)
        {
            var hourlyArchiveParameter = new HourlyArchiveParameter();

            //codes will be added...

            return hourlyArchiveParameter;
        }
    }
}
