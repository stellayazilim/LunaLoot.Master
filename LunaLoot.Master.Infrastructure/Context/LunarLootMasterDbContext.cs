using LunaLoot.Master.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LunaLoot.Master.Infrastructure.Context;

public class LunaLootMasterDbContext: IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
   
    public LunaLootMasterDbContext(DbContextOptions optionsBuilder): base(optionsBuilder)
    {
    }
    
    
}