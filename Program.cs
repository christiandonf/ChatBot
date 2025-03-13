Dictionary<string, string> dialogs = new Dictionary<string, string>(){
    {"pedido", "Qual é o problema com o pedido?"},
    {"atraso", "Certo, o pedido está em atraso...\nHá alguma ocorrência de atraso com esse número de pedido?"},
    {"falta", "Hum... houve falta nesse pedido. Certo, esse produto foi faturado na nota fiscal?"},
    {"resposta atraso sim", "Verifique as tarefas desta ocorrência e veja se alguma delas está dentro do prazo ou se estão todas encerradas. Se estiverem, encaminhe o caso para o suporte e aguarde a resposta."},
    {"resposta atraso nao", "Verifique com o cliente se ele ainda deseja receber o pedido.\nSe sim, abra uma ocorrência de atraso na entrega, descrevendo que o cliente deseja receber o pedido. Informe o SLA e o número de protocolo ao cliente.\nTabulação \nPrmeira categoria:Transporte \nSegunda categoria:Atraso na entrega \nMotivo:Pedido não entregue"},
    {"resposta falta sim", "Abra uma ocorrência de falta de produto:\nCategoria: Pós-compra\nSubcategoria: Falta\nMotivo: Produto faturado."},
    {"resposta falta nao", "Então o produto não consta na nota fiscal? Ou ele foi cortado?"},
    {"resposta nao consta", "Se o produto não consta na nota fiscal, então ele não foi pedido. Não é possível abrir uma ocorrência de falta de algo que não foi comprado."},
    {"resposta cortado", "Certo. Abra uma ocorrência de falta de produto:\nCategoria: Pós-compra\nSubcategoria: Falta\nMotivo: Produto não faturado."},
    {"Cadastro", "Cliente está com problema no cadastro? Qual a mensagem de erro?"},
    {"Cadastro, CPF já cadastrado", "Quando ocorre esse erro Você tem quem reprocessar no GPP \nCaso não apareceça em 5 minutos \nPrencha esse formulário \nMatricula: \nCPF: \n Nome Completo: \nData de nascimento: \n Cep: \nEndereço completo: \nE-mail: \nTelefone de contato:"},
    {"help", "Voce pode me perguntar sobre Pedidos, sobre o que fazer quando tem Atraso, se esta em Falta ou ate mesmo sobre o cadastro do cliente, caso deseje finalizar nossa conversa digite 'sair' para encerrar."}
};
Dictionary<string, string> otherDialogs = new Dictionary<string, string>(){
    {"erro", "Desculpe, não entendi. Pode reformular?"},
    {"farewells", "Até mais! Volte sempre que precisar. 😊"},
    {"welcome", "Olá! Meu nome é Rey. Com o que o cliente está tendo problema?"}
};
void ConsoleResponse(string texto)
{
    Console.WriteLine("ChatBot: " + texto);
}
void ChatBot()
{
    ConsoleResponse(otherDialogs["welcome"]);
    string lastResponse = string.Empty;
    string response = string.Empty;
    while (true)
    {
        Console.Write("Você: ");
        string userInput = Console.ReadLine()!;
        if (userInput.Contains("sair", StringComparison.OrdinalIgnoreCase))
        {
            ConsoleResponse(otherDialogs["farewells"]);
            break;
        }
        else if (lastResponse == dialogs["atraso"])
        {
            if (userInput.Contains("sim", StringComparison.OrdinalIgnoreCase))
            {
                response = dialogs["resposta atraso sim"];
            }
            else if (userInput.Contains("nao", StringComparison.OrdinalIgnoreCase))
            {
                response = dialogs["resposta atraso nao"];
            }
            else
            {
                response = otherDialogs["erro"];
            }
        }
        else if (lastResponse == dialogs["falta"])
        {
            if (userInput.Contains("sim", StringComparison.OrdinalIgnoreCase))
            {
                response = dialogs["resposta falta sim"];
            }
            else if (userInput.Contains("nao", StringComparison.OrdinalIgnoreCase))
            {
                response = dialogs["resposta falta nao"];
            }
            else
            {
                response = otherDialogs["erro"];
            }
        }
        else if (lastResponse == dialogs["resposta falta nao"])
        {
            if (userInput.Contains("sim", StringComparison.OrdinalIgnoreCase))
            {
                response = dialogs["resposta nao consta"];
            }
            else if (userInput.Contains("nao", StringComparison.OrdinalIgnoreCase))
            {
                response = dialogs["resposta cortado"];
            }
            else
            {
                response = otherDialogs["erro"];
            }
        }
        else
        {
            foreach (string keyword in dialogs.Keys)
            {
                if (userInput.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                {
                    response = dialogs[keyword];
                    break;
                }
                else
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