using SafraAssistenteVirtualInteligente.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SafraAssistenteVirtualInteligente.Infrastructure.Data.Config
{
    public class ToDoConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.Property(t => t.Name)
                .IsRequired();
        }
    }
}
