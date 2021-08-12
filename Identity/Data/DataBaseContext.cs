using Identity.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Data
{
    public class DataBaseContext : IdentityDbContext<AppUser,AppRole,string> //IdentityDbContext<IdentityUser, IdentityRole, string> Default
    {
        public DataBaseContext(DbContextOptions options)
            :base(options)
        {

        }
    }
}
