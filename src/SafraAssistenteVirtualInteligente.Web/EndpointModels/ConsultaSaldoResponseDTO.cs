using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SafraAssistenteVirtualInteligente.Web.EndpointModels
{
    public class ConsultaSaldoResponseDTO
    {
        public DataDTO Data { get; set; }
        public LinksDTO Links { get; set; }
    }

    public class AmountDTO
    {
        public Decimal amount { get; set; }
        public string Currency { get; set; }
    }

    public class CreditLineDTO
    {
        public bool Included { get; set; }
        public AmountDTO Amount { get; set; }
        public string Type { get; set; }
    }

    public class BalanceDTO
    {
        public string AccountId { get; set; }
        public AmountDTO Amount { get; set; }
        public string CreditDebitIndicator { get; set; }
        public string Type { get; set; }
        public DateTime DateTime { get; set; }
        public List<CreditLineDTO> CreditLine { get; set; }
    }

    public class DataDTO
    {
        public List<BalanceDTO> Balance { get; set; }
    }

    public class LinksDTO
    {
        public string Self { get; set; }
    }
 }
