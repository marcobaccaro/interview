using Beijer.Thesaurus.DataContext;
using Beijer.Thesaurus.Infrastructure.Persistence;
using Beijer.Thesaurus.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Beijer.Thesaurus.WebApi.Extensions {

    public static class DependencyInjectionRegistry {

        public static IServiceCollection RegistryDependencies(this IServiceCollection serviceCollection) {

            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>((x) => { return new UnitOfWork(new ApplicationDbContext()); });
            serviceCollection.AddScoped<IThesaurusService, ThesaurusService>();
            return serviceCollection;

        }

    }

}