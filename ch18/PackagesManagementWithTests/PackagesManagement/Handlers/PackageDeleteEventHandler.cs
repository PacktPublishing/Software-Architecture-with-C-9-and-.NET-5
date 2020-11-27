using DDD.ApplicationLayer;
using PackagesManagementDomain.Aggregates;
using PackagesManagementDomain.Events;
using PackagesManagementDomain.IRepositories;
using System.Threading.Tasks;

namespace PackagesManagement.Handlers
{
    public class PackageDeleteEventHandler :
        IEventHandler<PackageDeleteEvent>
    {
        IPackageEventRepository repo;
        public PackageDeleteEventHandler(IPackageEventRepository repo)
        {
            this.repo = repo;
        }
        public Task HandleAsync(PackageDeleteEvent ev)
        {
            repo.New(PackageEventType.Deleted, ev.PackageId, ev.OldVersion);
            return Task.CompletedTask;
        }
    }
}
