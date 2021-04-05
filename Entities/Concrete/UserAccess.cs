using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class UserAccess:IEntity
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string AccessCode { get; set; }
    }
}
