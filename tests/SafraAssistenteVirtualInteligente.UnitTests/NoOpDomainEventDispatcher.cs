using System.Threading.Tasks;
using SafraAssistenteVirtualInteligente.SharedKernel.Interfaces;
using SafraAssistenteVirtualInteligente.SharedKernel;

namespace SafraAssistenteVirtualInteligente.UnitTests
{
    public class NoOpDomainEventDispatcher : IDomainEventDispatcher
    {
        public Task Dispatch(BaseDomainEvent domainEvent)
        {
            return Task.CompletedTask;
        }
    }
}
