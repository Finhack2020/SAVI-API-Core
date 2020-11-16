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

namespace SafraAssistenteVirtualInteligente.Core.Intents.Alexa
{
    public class ConsultaExtratoIntent : IIntentResponse
    {
        private readonly ILocaleSpeech _locale;
        private readonly SkillRequest _input;
        private readonly string _acc;
        private readonly string _token;

        public ConsultaExtratoIntent(ILocaleSpeech locale, SkillRequest input)
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

            string result = await HttpSenderApi.Call("open-banking/v1/accounts/" + _acc + "/transactions", _token);
            ConsultaExtratoResponseDTO consultaExtratoResponse = JsonConvert.DeserializeObject<ConsultaExtratoResponseDTO>(result);

            string[] arguments = MappingDtoResponseToEchoMessage(consultaExtratoResponse);
            var consultaExtrato = await _locale.Get(LanguageKeys.ConsultaExtrato, arguments);

            return ResponseBuilder.Ask(consultaExtrato, null, _input.Session);
        }

        public static string[] MappingDtoResponseToEchoMessage(ConsultaExtratoResponseDTO consultaExtratoResponse) {
             string[] arguments =  {
                consultaExtratoResponse.Data.Transaction.Count.ToString(),
                consultaExtratoResponse.Data.Transaction[0].TransactionId,
                consultaExtratoResponse.Data.Transaction[0].TransactionInformation,
                consultaExtratoResponse.Data.Transaction[0].Balance.Amount.amount.ToString("C2", CultureInfo.CurrentCulture),
                consultaExtratoResponse.Data.Transaction[0].CreditDebitIndicator,
                consultaExtratoResponse.Data.Transaction[0].ValueDateTime.Day.ToString(),
                consultaExtratoResponse.Data.Transaction[0].ProprietaryBankTransactionCode.Issuer,
                consultaExtratoResponse.Data.Transaction[0].Amount.amount >= 0? "positivo": "negativo",
                consultaExtratoResponse.Data.Transaction[0].Amount.amount.ToString("C2", CultureInfo.CurrentCulture),
            };
            return arguments;
        }
    }
}