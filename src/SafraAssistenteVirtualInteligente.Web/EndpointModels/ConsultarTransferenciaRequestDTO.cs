using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SafraAssistenteVirtualInteligente.Web.EndpointModels
{
    public class ConsultarTransferenciaRequestDTO
    {

        public DestinyAccount destinyAccount;
        public Amount amount;
        public string Type;
        public string TransactionInformation;

        public class DestinyAccount
        {
            public string Bank;
            public string Agency;
            public string Id;
            public string Cpf;
            public string Name;
            public string Goal;
        }

        public class Amount
        {
            public string amount;
            public string Currency;
        }
    }
}
