using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldEntities.Concrete
{
    public class FieldCurrentParameter : IFieldEntity
    {
        public int DeviceId { get; set; }
    }
}

