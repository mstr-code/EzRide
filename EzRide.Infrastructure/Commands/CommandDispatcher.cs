using System;
using System.Threading.Tasks;

using EzRide.Infrastructure.Handlers;

using Autofac;

namespace EzRide.Infrastructure.Commands
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IComponentContext context;

        public CommandDispatcher(IComponentContext context)
        {
            this.context = context;
        }
        
        public async Task DispatchAsync<T>(T command) where T : ICommand
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command),
                    $"Command '{typeof(T).Name}' cannot be null.");
            
            ICommandHandler<T> handler = context.Resolve<ICommandHandler<T>>();
            await handler.HandleAsync(command);
        }
    }
}