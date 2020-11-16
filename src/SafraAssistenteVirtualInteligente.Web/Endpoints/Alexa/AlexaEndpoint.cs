using Ardalis.ApiEndpoints;
using SafraAssistenteVirtualInteligente.Core.Entities;
using SafraAssistenteVirtualInteligente.SharedKernel.Interfaces;
using SafraAssistenteVirtualInteligente.Infrastructure.Languages;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using Alexa.NET;
using Alexa.NET.LocaleSpeech;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System;
using SafraAssistenteVirtualInteligente.Infrastructure.MocksFinHack;
using User = SafraAssistenteVirtualInteligente.Infrastructure.MocksFinHack;
using SafraAssistenteVirtualInteligente.Infrastructure;
using SafraAssistenteVirtualInteligente.Web.Intents;
using SafraAssistenteVirtualInteligente.Core;

namespace SafraAssistenteVirtualInteligente.Web.Endpoints.Alexa
{
    public class AlexaEndpoint : BaseAsyncEndpoint<SkillRequest, IActionResult>
    {
        private readonly ILogger<AlexaEndpoint> _logger;
        private readonly IRepository _repository;


        public AlexaEndpoint(ILogger<AlexaEndpoint> logger, IRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpPost("v1/Alexa")]
        [SwaggerOperation(
            Summary = "Assistente virtual da Amazon",
            Description = "Este endpoint trata do processamento dos intents solicitados pelo cliente, por medida de segurança a documentação desse processo é feita" +
            "de forma sigilosa e interna em um ambiente seguro, não expondo suas informações",
            OperationId = "Alexa.Post",
            Tags = new[] { "AlexaEndpoints" })
        ]
        public override async Task<ActionResult<IActionResult>> HandleAsync(SkillRequest input)
        {

            /// <summary>
            /// Bloco de segurança para verificando e validar o request enviado pela Alexa,
            /// identificando possiveis fraudes.
            /// </summary>
            var req = HttpContext.Request;
            var isValid = await input.ValidateRequestAsync(req, _logger);
            if (!isValid)
            {
              return new BadRequestResult();
            }


            var request = input.Request;
            SkillResponse response = new SkillResponse { Response = new ResponseBody() };

            DictionaryLocaleSpeechStore store = LanguagesResource.SetupLanguageResources();
            ILocaleSpeech locale = input.CreateLocale(store);

            try
            {
                if (input.Session.Attributes == null)
                    input.Session.Attributes = new Dictionary<string, object>();

                if (!(request is IntentRequest intentRequest))
                    return new BadRequestResult();

                string IntentName = intentRequest.Intent.Name;

                if (string.IsNullOrEmpty((String)input.Session.Attributes["Auth"]))
                {
                    //Autenticação
                    string token = await HttpSenderApi.Call();
                    input.Session.Attributes["Auth"] = token;
                }

                if (intentRequest.Intent.Name == "PinUsuarioIntent")
                {
                    Account user = await new LogInPinAlexa(_repository).LogInAlexaAsync(intentRequest.Intent.Slots["pin"].Value);
                    if (user != null)
                    {
                        input.Session.Attributes.Add("pin", user.AccountId);
                        IOutputSpeech message = await locale.Get(LanguageKeys.Pinvalido, null);
                        response = ResponseBuilder.Ask(message, null, input.Session);
                        return new OkObjectResult(response);
                    }
                    else
                    {
                        IOutputSpeech message = await locale.Get(LanguageKeys.PinInvalido, null);
                        response = ResponseBuilder.Ask(message, null, input.Session);
                        return new OkObjectResult(response);
                    }
                }

                if(intentRequest.Intent.Name != "PinUsuarioIntent")
                response = await ExecuteIntentAlexaAsync(IntentName, input, locale);
            }
            catch (Exception ex)
            {
                var message = await locale.Get(LanguageKeys.Error, null);
                response = ResponseBuilder.Tell(message);
                _logger.LogError(ex.Message + " : ", message);
            }

            return new OkObjectResult(response);
        }

        public static async Task<SkillResponse> ExecuteIntentAlexaAsync(string IntentName, SkillRequest input, ILocaleSpeech locale)
        {
            //Acha o Intent correto entre as classes e executa seu processo
            Type repType = Type.GetType("SafraAssistenteVirtualInteligente.Web.Intents.Alexa." + IntentName);
            IIntentResponse intent = Activator.CreateInstance(repType, locale, input) as IIntentResponse;
            return await intent.ExecuteIntentAsync();
        }
    }
}