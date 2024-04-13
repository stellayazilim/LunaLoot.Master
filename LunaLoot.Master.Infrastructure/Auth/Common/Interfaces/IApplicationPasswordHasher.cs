﻿
using LunaLoot.Master.Infrastructure.Persistence.EFCore.Entities;
using Microsoft.AspNetCore.Identity;

namespace LunaLoot.Master.Infrastructure.Auth.Common.Interfaces;

public interface IApplicationPasswordHasher: IPasswordHasher<ApplicationUser>
{
    
}