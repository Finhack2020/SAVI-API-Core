using Alexa.NET.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SafraAssistenteVirtualInteligente.Web.Extensions
{
    public static class RepromptBuilder
    {
        public static Reprompt Create(string text) => Create(new PlainTextOutputSpeech { Text = text });

        public static Reprompt Create(IOutputSpeech speech) => new Reprompt { OutputSpeech = speech };
    }
}