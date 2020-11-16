using Alexa.NET;
using Alexa.NET.LocaleSpeech;
using Alexa.NET.Request;
using Alexa.NET.Response;
using Newtonsoft.Json;
using SafraAssistenteVirtualInteligente.Infrastructure;
using SafraAssistenteVirtualInteligente.Infrastructure.Languages;
using SafraAssistenteVirtualInteligente.Web.EndpointModels;
using SafraAssistenteVirtualInteligente.Web.Intents;
using SafraAssistenteVirtualInteligente.Web.Shared;
using System;
using System.Globalization;

namespace SafraAssistenteVirtualInteligente.Web.Intents.Alexa
{
    public class ConsultaSaldoIntent : IIntentResponse
    {
        private readonly ILocaleSpeech _locale;
        private readonly SkillRequest _input;
        private readonly string _acc;
        private readonly string _token;

        public ConsultaSaldoIntent(ILocaleSpeech locale, SkillRequest input)
        {
            _locale = locale;
            _input = input;
            _acc = _input.Session.Attributes["pin"] as String;
            _token = _input.Session.Attributes["Auth"] as String;
        }

        public async System.Threading.Tasks.Task<SkillResponse> ExecuteIntentAsync()
        {

            SkillResponse response = await PermissionValidator.ValidatorAsync(_input, _locale);

            if (response != null)
                return response;

            string result = await HttpSenderApi.Call("open-banking/v1/accounts/" + _acc + "/balances", _token);
            ConsultaSaldoResponseDTO consultaSaldoResponse = JsonConvert.DeserializeObject<ConsultaSaldoResponseDTO>(result);

            string[] arguments = MappingDtoResponseToEchoMessage(consultaSaldoResponse);
            var consultaSaldo = await _locale.Get(LanguageKeys.ConsultaSaldo, arguments);

            return ResponseBuilder.Ask(consultaSaldo, null, _input.Session); ;
        }

        public static string[] MappingDtoResponseToEchoMessage(ConsultaSaldoResponseDTO consultaSaldoResponse) {
             string[] arguments =  {
                consultaSaldoResponse.Data.Balance[0].Amount.amount.ToString("C2", CultureInfo.CurrentCulture),
                consultaSaldoResponse.Data.Balance[0].Amount.amount >= 0? "positivo": "negativo"
            };
            return arguments;
        }
    }
}