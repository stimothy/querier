﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Querier.Models.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Querier.Models
{        
    //Not to quite sure what this class is for, but it is needed to pass information to the database.
    public class LoginDatabase : IdentityDbContext<User>
    {
        public LoginDatabase(DbContextOptions<LoginDatabase> options) : base(options)
        { }
    }
}