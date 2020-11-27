using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace DDD.ApplicationLayer
{
    public static class EventDIExtensions
    {
        public static IServiceCollection AddAllQueries
            (this IServiceCollection service, Assembly assembly)
        {
            var queries = assembly.GetTypes()
                .Where(x => !x.IsAbstract && x.IsClass
                && typeof(IQuery).IsAssignableFrom(x));
            foreach (var query in queries)
            {
                var queryInterface = query.GetInterfaces()
                    .Where(i => !i.IsGenericType && typeof(IQuery) != i &&
                    typeof(IQuery).IsAssignableFrom(i))
                    .SingleOrDefault();
                if (queryInterface != null)
                {
                    service.AddTransient(queryInterface, query);
                }
            }
            return service;
        }
    }
}
