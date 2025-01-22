using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using tg117.Domain.DbContext;

namespace tg117.Domain.Core
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly AppDbContext _appDbContext;

        public Repository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void Add(TEntity entity)
        {
            _ = _appDbContext.Set<TEntity>().Add(entity);
            _ = _appDbContext.SaveChanges();
        }

        public void AddMany(IEnumerable<TEntity> entities)
        {
            _appDbContext.Set<TEntity>().AddRange(entities);
            _ = _appDbContext.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            _ = _appDbContext.Set<TEntity>().Remove(entity);
            _ = _appDbContext.SaveChanges();
        }

        public void DeleteMany(Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> entities = Find(predicate);
            _appDbContext.Set<TEntity>().RemoveRange(entities);
            _ = _appDbContext.SaveChanges();
        }

        public TEntity FindOne(Expression<Func<TEntity, bool>> predicate, FindOptions? findOptions = null)
        {
            return Get(findOptions).FirstOrDefault(predicate)!;
        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, FindOptions? findOptions = null)
        {
            return Get(findOptions).Where(predicate);
        }

        public IQueryable<TEntity> GetAll(FindOptions? findOptions = null)
        {
            return Get(findOptions);
        }

        public void Update(TEntity entity)
        {
            _ = _appDbContext.Set<TEntity>().Update(entity);
            _ = _appDbContext.SaveChanges();
        }

        public bool Any(Expression<Func<TEntity, bool>> predicate)
        {
            return _appDbContext.Set<TEntity>().Any(predicate);
        }

        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return _appDbContext.Set<TEntity>().Count(predicate);
        }

        private DbSet<TEntity> Get(FindOptions? findOptions = null)
        {
            findOptions ??= new FindOptions();
            DbSet<TEntity> entity = _appDbContext.Set<TEntity>();
            if (findOptions.IsAsNoTracking && findOptions.IsIgnoreAutoIncludes)
            {
                _ = entity.IgnoreAutoIncludes().AsNoTracking();
            }
            else if (findOptions.IsIgnoreAutoIncludes)
            {
                _ = entity.IgnoreAutoIncludes();
            }
            else if (findOptions.IsAsNoTracking)
            {
                _ = entity.AsNoTracking();
            }
            return entity;
        }
    }
}