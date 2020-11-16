using System.Threading.Tasks;
using SafraAssistenteVirtualInteligente.SharedKernel;

namespace SafraAssistenteVirtualInteligente.SharedKernel.Interfaces
{
    public interface IDomainEventDispatcher
    {
        Task Dispatch(BaseDomainEvent domainEvent);
    }
}