using SafraAssistenteVirtualInteligente.Core.Interfaces;
using SafraAssistenteVirtualInteligente.SharedKernel;

namespace SafraAssistenteVirtualInteligente.Core.Entities
{
    public class Account : BaseEntity
    {
        public string SchemeName { get; set; }
        public string Identification { get; set; }
        public string Name { get; set; }
        public string SecondaryIdentification { get; set; }
        public string AccountId { get; set; }
        public string Currency { get; set; }
        public string Nickname { get; set; }
    }
}
