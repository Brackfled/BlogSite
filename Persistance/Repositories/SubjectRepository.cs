using Application.Services.Repositories;
using Core.Persistance.Repositories;
using Domain.Entities;
using Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repositories
{
    public class SubjectRepository: EfRepositoryBase<Subject, Guid, BaseDbContext>, ISubjectRepository
    {
        public SubjectRepository(BaseDbContext context):base(context)
        {
            
        }
    }
}
