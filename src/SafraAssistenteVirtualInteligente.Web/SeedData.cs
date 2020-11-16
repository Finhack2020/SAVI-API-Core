using SafraAssistenteVirtualInteligente.Core.Entities;
using SafraAssistenteVirtualInteligente.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace SafraAssistenteVirtualInteligente.Web
{
    public static class SeedData
    {
        public static readonly Account Item1 = new Account
        {
            SchemeName = "SortCodeAccountNumber",
            Identification = "12345678901211",
            Name = "Susan Wojcicki da Silva",
            SecondaryIdentification = "12332111",
            AccountId = "00711234511",
            Currency = "BRL",
            Nickname = "Susan",
        };
        public static readonly Account Item2 = new Account
        {
            SchemeName = "SortCodeAccountNumber",
            Identification = "12345678901233",
            Name = "Mark Zuckerberg da Silva",
            SecondaryIdentification = "12332133",
            AccountId = "00711234533",
            Currency = "BRL",
            Nickname = "Mark",
        };
        public static readonly Account Item3 = new Account
        {
            SchemeName = "SortCodeAccountNumber",
            Identification = "12345678901222",
            Name = "Satya Nadella da Silva",
            SecondaryIdentification = "12332122",
            AccountId = "00711234522",
            Currency = "BRL",
            Nickname = "Satya",
        };

        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var dbContext = new AppDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>());
            // Look for any TODO items.
            if (dbContext.Accounts.Any())
            {
                return;   // DB has been seeded
            }

            PopulateTestData(dbContext);
        }
        public static void PopulateTestData(AppDbContext dbContext)
        {
            foreach (var item in dbContext.Accounts)
            {
                dbContext.Remove(item);
            }
            dbContext.SaveChanges();
            dbContext.Accounts.Add(Item1);
            dbContext.Accounts.Add(Item2);
            dbContext.Accounts.Add(Item3);

            dbContext.SaveChanges();
        }
    }
}
