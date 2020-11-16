using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SafraAssistenteVirtualInteligente.Web.EndpointModels
{
    public class ConsultarTransferenciaResponseDTO
    {

        public string AccountId;
        public string Bank;
        public string Agency;
        public string AccountNumber;
        public Amount amount;
        public DestinyAccount destinyAccount;
        public string Type;
        public string Status;
        public DateTime BookingDateTime;
        public DateTime ValueDateTime;
        public string TransactionInformation;

        public class Amount
        {
            public string amount;
            public string Currency;
        }

        public class DestinyAccount
        {
            public string Bank;
            public string Agency;
            public string Number;
            public string Cpf;
            public string Name;
            public string Goal;
        }
    }
}
