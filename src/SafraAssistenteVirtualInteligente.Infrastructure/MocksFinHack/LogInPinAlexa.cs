using SafraAssistenteVirtualInteligente.Core.Entities;
using SafraAssistenteVirtualInteligente.SharedKernel.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SafraAssistenteVirtualInteligente.Infrastructure.MocksFinHack
{
    public class LogInPinAlexa
    {
        private readonly IRepository _repository;

        public LogInPinAlexa(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<Account> LogInAlexaAsync(string pin)
        {
            var items = await _repository.ListAsync<Account>();
            return items.Find(x => x.AccountId.Contains(pin));
        }
    }
}
