using FieldBusiness.Abstract;
using FieldEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldBusiness.Concrete
{
    public class FieldMonthlyType2ArchiveParameterManager : IFieldMonthlyType2ArchiveParameterService
    {
        public event EventHandler<List<FieldMonthlyType2ArchiveParameter>> OnFieldDataIsReadyEvent;
    }
}
