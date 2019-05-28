using System.Threading.Tasks;

namespace EzRide.Infrastructure.Services
{
    public interface IDataInitializer : IService
    {
        Task SeedAsync();
    }
}