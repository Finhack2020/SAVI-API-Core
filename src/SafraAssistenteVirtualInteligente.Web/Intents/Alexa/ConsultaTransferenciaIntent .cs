using Alexa.NET;
using Alexa.NET.LocaleSpeech;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
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
    public class ConsultaTransferenciaIntent : IIntentResponse
    {
        private readonly ILocaleSpeech _locale;
        private readonly SkillRequest _input;
        private readonly string _acc;
        private readonly string _token;

        public ConsultaTransferenciaIntent(ILocaleSpeech locale, SkillRequest input)
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

            ConsultarTransferenciaRequestDTO consultarTransferenciaRequestDTO = MappingIntentDtoRequest(_input);
            var jsonData = JsonConvert.SerializeObject(consultarTransferenciaRequestDTO);



            string result = await HttpSenderApi.Call("accounts/v1/accounts/" + _acc + "/transfers", _token, jsonData);
            ConsultarTransferenciaResponseDTO consultaTransferenciaResponse = JsonConvert.DeserializeObject<ConsultarTransferenciaResponseDTO>(result);

            var consultaExtrato = await _locale.Get(LanguageKeys.Transferencia, null);

            return ResponseBuilder.Ask(consultaExtrato, null, _input.Session);
        }

        public static ConsultarTransferenciaRequestDTO MappingIntentDtoRequest(SkillRequest _input)
        {
            Request request = _input.Request;
            ConsultarTransferenciaRequestDTO consultarTransferenciaRequestDTO = new ConsultarTransferenciaRequestDTO();

            if (request is IntentRequest intentRequest)
            {
                consultarTransferenciaRequestDTO.amount = new ConsultarTransferenciaRequestDTO.Amount();
                consultarTransferenciaRequestDTO.amount.amount = intentRequest.Intent.Slots["amount"].Value;
                consultarTransferenciaRequestDTO.amount.Currency = "BRL";

                consultarTransferenciaRequestDTO.destinyAccount = new ConsultarTransferenciaRequestDTO.DestinyAccount();
                consultarTransferenciaRequestDTO.destinyAccount.Bank = intentRequest.Intent.Slots["Bank"].Value; ;
                consultarTransferenciaRequestDTO.destinyAccount.Agency = intentRequest.Intent.Slots["Agency"].Value;
                consultarTransferenciaRequestDTO.destinyAccount.Cpf = intentRequest.Intent.Slots["Cpf"].Value; 
                consultarTransferenciaRequestDTO.destinyAccount.Name = intentRequest.Intent.Slots["Name"].Value;
                consultarTransferenciaRequestDTO.destinyAccount.Goal = intentRequest.Intent.Slots["Goal"].Value;

                consultarTransferenciaRequestDTO.Type = intentRequest.Intent.Slots["Type"].Value;
                consultarTransferenciaRequestDTO.TransactionInformation = intentRequest.Intent.Slots["TransactionInformation"].Value;
            }

            return consultarTransferenciaRequestDTO;
        }
    }
}