﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy.Security;

namespace ChatApplication.API.User
{
    public class UserIdentity : IUserIdentity
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> Claims { get; set; }
        public long UserId { get; set; }
    }
}