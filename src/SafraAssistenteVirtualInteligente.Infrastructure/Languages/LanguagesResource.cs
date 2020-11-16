using Alexa.NET.LocaleSpeech;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SafraAssistenteVirtualInteligente.Infrastructure.Languages
{
    public class LanguagesResource
    {
        public static DictionaryLocaleSpeechStore SetupLanguageResources() {

            var store = new DictionaryLocaleSpeechStore();
            store.AddLanguage(EN_US.Language, EN_US.GetLanguage());
            store.AddLanguage(PT_BR.Language,PT_BR.GetLanguage());
            return store;

        }
    }
}
