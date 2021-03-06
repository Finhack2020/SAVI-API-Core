﻿using Alexa.NET.LocaleSpeech;
using Alexa.NET.Request;

namespace SafraAssistenteVirtualInteligente.Infrastructure.Languages
{
    public static class LanguageExtension
    {
        public static ILocaleSpeech CreateLocale(this SkillRequest skillRequest, DictionaryLocaleSpeechStore store)
        {
            var localeSpeechFactory = new LocaleSpeechFactory(store);
            var locale = localeSpeechFactory.Create(skillRequest);

            return locale;
        }
    }
}
