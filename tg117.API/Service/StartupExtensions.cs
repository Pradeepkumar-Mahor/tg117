using tg117.Domain.Core;
using tg117.Domain.Repos.Interface;
using tg117.Domain.Repos.Repository;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddDataAccessService(this IServiceCollection services)
        {
            #region PersonPro

            // services.AddTransient<IProductRepository, ProductRepository>();

            #endregion PersonPro

            #region GenericEntityRepos

            _ = services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            _ = services.AddScoped<ICategoryRepository, CategoryRepository>();

            //services.AddTransient<IEmailSender, MailKit>();

            #endregion GenericEntityRepos

            return services;
        }
    }
}