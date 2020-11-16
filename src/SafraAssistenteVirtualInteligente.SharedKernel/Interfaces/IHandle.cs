using System.Threading.Tasks;
using SafraAssistenteVirtualInteligente.SharedKernel;

namespace SafraAssistenteVirtualInteligente.SharedKernel.Interfaces
{
    public interface IHandle<in T> where T : BaseDomainEvent
    {
        Task Handle(T domainEvent);
    }
}