using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.VisualBasic.FileIO;
Dictionary<string, string> dialogs = new Dictionary<string, string>();
Dictionary<string, string> otherDialogs = new Dictionary<string, string>();
dialogs.Add("pedido", "Qual é o problema com o pedido?");
dialogs.Add("atraso", "Certo, o pedido está em atraso...\nHá alguma ocorrência de atraso com esse número de pedido?");
dialogs.Add("falta", "Hum... houve falta nesse pedido. Certo, esse produto foi faturado na nota fiscal?");
dialogs.Add("resposta atraso sim 1", "Verifique as tarefas desta ocorrência e veja se alguma delas está dentro do prazo ou se estão todas encerradas. Se estiverem, encaminhe o caso para o suporte e aguarde a resposta.");
dialogs.Add("resposta atraso nao 1", "Verifique com o cliente se ele ainda deseja receber o pedido.\nSe sim, abra uma ocorrência de atraso na entrega, descrevendo que o cliente deseja receber o pedido. Informe o SLA e o número de protocolo ao cliente.\nTabulação \nPrmeira categoria:Transporte \nSegunda categoria:Atraso na entrega \nMotivo:Pedido não entregue");
dialogs.Add("resposta falta sim", "Abra uma ocorrência de falta de produto:\nCategoria: Pós-compra\nSubcategoria: Falta\nMotivo: Produto faturado.");
dialogs.Add("resposta falta nao", "Então o produto não consta na nota fiscal? Ou ele foi cortado?");
dialogs.Add("resposta nao consta", "Se o produto não consta na nota fiscal, então ele não foi pedido. Não é possível abrir uma ocorrência de falta de algo que não foi comprado.");
dialogs.Add("resposta cortado", "Certo. Abra uma ocorrência de falta de produto:\nCategoria: Pós-compra\nSubcategoria: Falta\nMotivo: Produto não faturado.");
dialogs.Add("Cadastro", "Cliente está com problema no cadastro? Qual a mensagem de erro?");
dialogs.Add("Cadastro, CPF já cadastrado", "Quando ocorre esse erro Você tem quem reprocessar no GPP \nCaso não apareceça em 5 minutos \nPrencha esse formulário \nMatricula: \nCPF: \n Nome Completo: \nData de nascimento: \n Cep: \nEndereço completo: \nE-mail: \nTelefone de contato:");
dialogs.Add("help", "Voce pode me perguntar sobre Pedidos, sobre o que fazer quando tem Atraso, se esta em Falta ou ate mesmo sobre o cadastro do cliente, caso deseje finalizar nossa conversa digite 'sair' para encerrar.");
otherDialogs.Add("erro", "Desculpe, não entendi. Pode reformular?");
otherDialogs.Add("farewells", "Até mais! Volte sempre que precisar. 😊");
otherDialogs.Add("welcome", "Olá! Meu nome é Rey. Com o que o cliente está tendo problema?");
string RemoveAccents(string text)
{
    var normalizedString = text.Normalize(NormalizationForm.FormD);
    var stringBuilder = new StringBuilder();

    foreach (var c in normalizedString.EnumerateRunes())
    {
        var unicodeCategory = Rune.GetUnicodeCategory(c);
        if (unicodeCategory != UnicodeCategory.NonSpacingMark)
        {
            stringBuilder.Append(c);
        }
    }
    return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
}
string RemovePunctuation(string texto)
{
    var stringBuilder = new StringBuilder();
    foreach (char c in texto)
    {
        if(!char.IsPunctuation(c))
        {
            stringBuilder.Append(c);
        }
    }
    return stringBuilder.ToString();
}
string NormalizedText(string text)
{
    text = text.ToLower();
    text = RemovePunctuation(text);
    text = RemoveAccents(text);
    return text;
}
void ConsoleResponse(string texto)
{
    Console.WriteLine("ChatBot: "+texto);
}
void ChatBot()
{
    ConsoleResponse(otherDialogs["welcome"]);
    string lastResponse = string.Empty;
    string response = string.Empty;
    while(true)
    {
        Console.Write("Você: ");
        string userInput = Console.ReadLine()!;
        string normalizedString = NormalizedText(userInput);
        if (normalizedString == "sair")
        {
            ConsoleResponse(otherDialogs["farewells"]);
            break;
        }else if (lastResponse == dialogs["atraso"])
        {
            if (normalizedString == "sim")
            {
                response = dialogs["resposta atraso sim"];
            }else if (normalizedString == "nao")
            {
                response = dialogs["resposta atraso nao"];
            }else
            {
                response = otherDialogs["erro"];
            }
        }else if (lastResponse == dialogs["falta"])
        {
             if (normalizedString == "sim")
            {
                response = dialogs["resposta falta sim"];
            }else if (normalizedString == "nao")
            {
                response = dialogs["resposta falta nao"];
            }else
            {
                response = otherDialogs["erro"];
            }
        }else if (lastResponse == dialogs["resposta falta nao"])
        {
            if (normalizedString == "sim")
            {
                response = dialogs["resposta nao consta"];
            }else if (normalizedString == "nao")
            {
                response = dialogs["resposta cortado"];
            }else
            {
                response = otherDialogs["erro"];
            }
        }else
        {
            foreach (string keyword in dialogs.Keys)
            {
                if (keyword == normalizedString)
                {
                    response = dialogs[keyword];
                    break;
                }else
                {
                    response = otherDialogs["erro"];
                }
            }
        }
        ConsoleResponse(response);
        lastResponse = response;
    }
}
ChatBot();