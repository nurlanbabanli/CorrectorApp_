using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class UserLog:IEntity
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string HostName { get; set; }
        public DateTime? LoginDate { get; set; }
        public DateTime? LogoutDate { get; set; }
        public string UserName { get; set; }
    }
}
