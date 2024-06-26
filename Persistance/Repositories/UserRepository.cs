﻿using Application.Services.Repositories;
using Core.Persistance.Repositories;
using Core.Security.Entities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class UserRepository: EfRepositoryBase<User, int, BaseDbContext>, IUserRepository
    {
        public UserRepository(BaseDbContext context):base(context) { }

    }
}
