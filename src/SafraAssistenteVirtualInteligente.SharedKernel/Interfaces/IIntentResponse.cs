using Alexa.NET.Response;
using System.Threading.Tasks;

namespace SafraAssistenteVirtualInteligente.SharedKernel.Interfaces
{
    interface IIntentResponse 
    {
        public Task<SkillResponse> ExecuteIntentAsync();
    }
}
