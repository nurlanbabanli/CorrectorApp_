﻿using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Utilities.Security
{
    public class ActiveUser
    {
        public static User activeUser;
        public static DateTime? UserLoginDate;
        public static List<UserAccess> userAccess;
    }
}
