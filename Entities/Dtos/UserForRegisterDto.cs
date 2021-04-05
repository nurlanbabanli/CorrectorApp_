using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class UserForRegisterDto:IDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public bool Status { get; set; }
        public string DepartmentName { get; set; }
        public string Position { get; set; }
        public bool IsAdmin { get; set; }
        public string HostIp { get; set; }
    }
}
