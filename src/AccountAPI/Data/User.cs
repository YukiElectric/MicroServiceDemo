﻿using Microsoft.AspNetCore.Identity;

namespace AccountAPI.Data
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
