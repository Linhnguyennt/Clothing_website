﻿using Microsoft.AspNetCore.Identity;

namespace week05_PTW.Data
{
    public class AppUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string? Address { get; set; }
        public string? Country { get; set; }



    }
}
