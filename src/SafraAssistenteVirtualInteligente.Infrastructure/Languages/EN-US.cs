using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SafraAssistenteVirtualInteligente.Infrastructure.Languages
{
    public class EN_US
    {
        public static Dictionary<string, object> En { get; private set; }
        public const string Language = "en";

        public static Dictionary<string, object> GetLanguage()
        {
            En = new Dictionary<string, object>
            {
                [LanguageKeys.Welcome] = "Welcome to the Banco Safra!",
                [LanguageKeys.WelcomeReprompt] = "You can ask help if you need instructions on how to interact with the skill",
            };
            return En;
        }
    }
}
