using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Querier.Models.Login
{
    //NOTE: IdentityUser already has its own username and password members.
    public class User : IdentityUser
    {
        //Here is where you would add other userdata other than username and password.
    }
}
