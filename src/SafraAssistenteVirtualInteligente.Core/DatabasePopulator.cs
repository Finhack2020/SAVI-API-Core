using SafraAssistenteVirtualInteligente.Core.Entities;
using SafraAssistenteVirtualInteligente.SharedKernel.Interfaces;
using System.Linq;

namespace SafraAssistenteVirtualInteligente.Core
{
    public static class DatabasePopulator
    {
        public static int PopulateDatabase(IRepository accountRepository)
        {
            if (accountRepository.ListAsync<Account>().Result.Count() >= 3) return 0;

            accountRepository.AddAsync(new Account
            {
                SchemeName = "SortCodeAccountNumber",
                Identification = "12345678901211",
                Name = "Susan Wojcicki da Silva",
                SecondaryIdentification = "12332111",
                AccountId = "00711234511",
                Currency = "BRL",
                Nickname = "Susan",
        }).Wait();
        accountRepository.AddAsync(new Account
            {
                SchemeName = "SortCodeAccountNumber",
                Identification = "12345678901233",
                Name = "Mark Zuckerberg da Silva",
                SecondaryIdentification = "12332133",
                AccountId = "00711234533",
                Currency = "BRL",
                Nickname = "Mark",
        }).Wait(); 
        accountRepository.AddAsync(new Account
            {
                SchemeName = "SortCodeAccountNumber",
                Identification = "12345678901222",
                Name = "Satya Nadella da Silva",
                SecondaryIdentification = "12332122",
                AccountId = "00711234522",
                Currency = "BRL",
                Nickname = "Satya",
        }).Wait();

            return accountRepository.ListAsync<Account>().Result.Count;
        }
    }
}
