using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SafraAssistenteVirtualInteligente.Infrastructure.Languages
{
    public class PT_BR
    {
        public static Dictionary<string, object> Pt { get; private set; }
        public const string Language = "pt";

        public static Dictionary<string, object> GetLanguage() {
            Pt = new Dictionary<string, object>
            {
                [LanguageKeys.Rotina] = "Inciando sua rotina do Banco safra!Identiquei uma transferência para você. Transferência {0} no dia {1} de {2}, no valor de R$ {3}. <break time =\'1s\'/>Não identifiquei Vencimentos ou Lançamento Futuros.<break time=\'1s\'/>Humm... Tenho Ótimas notícias sobre seus investimentos. A sua compra de 150 mil lotes de Debênture PETRO26 realizada ontem foi efetivada e hoje rendeu R$ 29,07 . AH, quase esqueci, seu saldo está {4} em R$ {5}",
                [LanguageKeys.Response] = "Ok, anotei aqui sua preferência. Iniciando <lang xml:lang='en-US'>PodCast</lang> sobre fundos multimercado do banco safra.",
                [LanguageKeys.ConsultaSaldo] = "Seu saldo está {1} em {0}.",
                [LanguageKeys.Transferencia] = "Sua transferencia foi feita com sucesso!",
                [LanguageKeys.IndicacaoConteudo] = "Como você assistiu o nosso morning call sobre fundos cambiais penso que você gostaria de ouvir um <lang xml:lang='en-US'>PodCast</lang> nosso sobre fundos multimercado?<break time='2s'/>",
                [LanguageKeys.ConsultaExtrato] = "Hum... Só um segundo.Achei! Você tem apenas {0} transação recentes.Transação numero {1} {2} com o valor de R$ {3} no Cartão de {4}o efetuada no dia {5} no(banco){6}. Seu saldo está {8} em {9}.",
                [LanguageKeys.PinInvalido] = "Não foi possível prosseguir com o Pin informado. Por favor, volte assim que tiver um pin Válido. Bye bye!",
                [LanguageKeys.Pinvalido] = "Exelente, Se precisar diga, \"Alexa, me Ajude\", para eu te falar as opções de navegação ou você pode me dizer agora o que deseja.",
                [LanguageKeys.Error] = "Erro no sistema, por favor entre em contato com nossa central de atendimento.",
            };
            return Pt;
        }
    }
}
