using System.Threading.Tasks;

using EzRide.Infrastructure.Commands;

namespace EzRide.Infrastructure.Handlers
{
    public interface ICommandHandler<T> where T : ICommand
    {
        Task HandleAsync(T command);
    }
}