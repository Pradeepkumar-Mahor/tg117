using Microsoft.AspNetCore.DataProtection.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tg117.Domain.Core;
using tg117.Domain.DbContext;
using tg117.Domain.Repos.Interface;

namespace tg117.Domain.Repos.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext appDbContext)
            : base(appDbContext)
        {
        }

        //TODO: Write here custom methods that are required for specific requirements.
    }
}