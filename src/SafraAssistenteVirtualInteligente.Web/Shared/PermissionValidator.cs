using Alexa.NET;
using Alexa.NET.LocaleSpeech;
using Alexa.NET.Request;
using Alexa.NET.Response;
using SafraAssistenteVirtualInteligente.Infrastructure.Languages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SafraAssistenteVirtualInteligente.Web.Shared
{
    public class PermissionValidator
    {
        public static async Task<SkillResponse> ValidatorAsync(SkillRequest _input, ILocaleSpeech _locale)
        { 

            //Autorização - Necessario estáo com o pin valido para ter acesso a essa informação
            if (string.IsNullOrEmpty(_input.Session.Attributes["pin"] as String))
            {
                _input.Session.Attributes["ExecIntent"] = "ConsultaExtratoIntent";
                var message = await _locale.Get(LanguageKeys.AcessoProtegido, null);
                SkillResponse response = ResponseBuilder.Ask(message, null, _input.Session);
                return response;
            }

            return null;

        }

    }
}
