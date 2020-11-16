using Alexa.NET.Response;
using System.Threading.Tasks;

namespace SafraAssistenteVirtualInteligente.Web.Intents
{
    interface IIntentResponse 
    {
        public Task<SkillResponse> ExecuteIntentAsync();
    }
}
