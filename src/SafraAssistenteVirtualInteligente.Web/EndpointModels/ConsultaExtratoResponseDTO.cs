using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SafraAssistenteVirtualInteligente.Web.EndpointModels
{
    public class ConsultaExtratoResponseDTO
    {
        public Data2DTO Data { get; set; }
        public LinksDTO Links { get; set; }
    }

    public class BankTransactionCodeDTO
    {
        public string Code { get; set; }
        public string SubCode { get; set; }
    }

    public class ProprietaryBankTransactionCodeDTO
    {
        public string Code { get; set; }
        public string Issuer { get; set; }
    }

    public class TransactionDTO
    {
        public string AccountId { get; set; }
        public string TransactionId { get; set; }
        public AmountDTO Amount { get; set; }
        public string CreditDebitIndicator { get; set; }
        public string Status { get; set; }
        public DateTime BookingDateTime { get; set; }
        public DateTime ValueDateTime { get; set; }
        public string TransactionInformation { get; set; }
        public BankTransactionCodeDTO BankTransactionCode { get; set; }
        public ProprietaryBankTransactionCodeDTO ProprietaryBankTransactionCode { get; set; }
        public BalanceDTO Balance { get; set; }
    }
    
    public class Data2DTO
    {
        public List<TransactionDTO> Transaction { get; set; }
    }
}
