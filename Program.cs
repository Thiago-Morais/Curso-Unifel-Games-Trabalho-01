// Trabalho #1 - Terminal Kahoot

// Desenvolva um jogo de perguntas e respostas
// [x] O jogo deve ter 10 perguntas, cada pergunta com 4 opções onde 1 é a correta.
// [x] O jogador deve informar qual a opção correta para a alternativa. Se ele acertar, é incrementado uma pontuação a ele, se errar é informado que errou e logo em seguida mostrado qual seria a correta.
// [x] No final do jogo, mostre a quantidade de pontos que o jogador fez.
// Seja criativo na elaboração do programa.
// [ ] Desafio extra, tente colocar um timer de 1 segundo entre uma pergunta e outra.
// Acesse bit.ly/cursogamesunifel, entre na pasta CSharp e abra o bloco de notas do trabalho correspondente, lá estará as perguntas.

List<GeneralQuestion> questions = new List<GeneralQuestion>
{
    new GeneralQuestion(1, "Qual comando utilizado para escrever no terminal?\n a. Console.WriteLine \n b. Console.ReadLine \n c. Console.LineWrite \n d. Console.LineRead", "a"),
    new GeneralQuestion(2, "O C# é uma linguagem de qual categoria?\n a. Estruturada \n b. Orientada a Objetos \n c. Script \n d. Marcação", "b"),
    new GeneralQuestion(3, "O C# tem como pai qual outra linguagem?\n a. Java \n b. Python \n c. HTML \n d. C", "d"),
    new GeneralQuestion(4, "Qual comando para criar comentários no C#?\n a. // \n b. \\\\ \n c. || \n d. !--", "a"),
    new GeneralQuestion(5, "Dentre os tipos de variáveis abaixo, qual consome 1 bit?\n a. int \n b. char \n c. bool \n d. string", "c"),
    new GeneralQuestion(6, "Qual operador corresponde ao resto da divisão?\n a. # \n b. $ \n c. & \n d. %", "d"),
    new GeneralQuestion(7, "Qual comando utilizamos para converter para inteiro?\n a. int.Parse \n b. int.Convert \n c. Convert.Parse \n d. Convert.Int", "a"),
    new GeneralQuestion(8, "Qual símbolo usamos para escrever o valor da variável no terminal?\n a. % \n b. $ \n c. & \n d. #", "b"),
    new GeneralQuestion(9, "Qual dos nomes de variáveis abaixo é válido?\n a. 123var \n b. nome variavel \n c. !nome! \n d. Variavel_123_Ex", "d"),
    new GeneralQuestion(10, "Qual comando para gerar números aleatórios?\n a. Console.Random \n b. Math.Random \n c. Random \n d. Random.Round", "c"),
};


const int pointsPerQuestion = 10;
int correctQuestions = 0;
HashSet<string> validInput = new HashSet<string>() { "a", "b", "c", "d" };
const string welcomeText =
@"Bem vindo ao Terminal Kahoot!

Teremos um questionário de perguntas e respostas.
Digite a, b, c ou d para responder.";

{
    questions.Sort((x, y) => x.Index.CompareTo(y.Index));

    PrintPage(welcomeText);

    for (int i = 0; i < questions.Count; i++)
    {
        string? input = "";

        // Ask question and handles invalid input
        bool invalidAnswer = false;
        do
        {
            AskQuestion(questions[i]);
            input = GatherAnswer();

            invalidAnswer = !IsInputValid(input);
            if (invalidAnswer)
            {
                Console.WriteLine("\nResposta inválida! Tente Novamente.");
                WaitForAnyKey();
            }
        }
        while (invalidAnswer);

        ProcessAnswer(questions[i], input);
    }
    LogPoints();

}

bool IsInputValid(string? input) => input != null && validInput.Contains(input);

void AskQuestion(GeneralQuestion generalQuestion)
{
    Console.Clear();

    var text = string.Format("{0}) {1}", generalQuestion.Index, generalQuestion.Text);
    Console.WriteLine(text);
}

string? GatherAnswer()
{
    Console.Write("Digite a, b, c ou d: ");
    return Console.ReadLine();
}

void ProcessAnswer(GeneralQuestion generalQuestion, string? input)
{
    string text;
    if (input == generalQuestion.Answer)
    {
        text = $"\nResposta correta! 😁🎉";
        correctQuestions++;
    }
    else
    {
        text =
@$"
Incorreto 😓

A resposta correta é: '{generalQuestion.Answer}'";
    }

    Console.WriteLine(text);
    WaitForAnyKey();
}

void LogPoints()
{
    var totalPoints = GetTotalPoints();
    var text =
@$"Parabéns! Você concluiu o questionário. 

A sua pontuação foi de {totalPoints} ponto{HandlePlural(totalPoints)} de {GetMaximumPoints()}.

Você acertou {correctQuestions} de {questions.Count} pergunta{HandlePlural(questions.Count)} e recebeu {pointsPerQuestion} ponto{HandlePlural(pointsPerQuestion)} por pergunta correta.";
    PrintPage(text);
}
int GetTotalPoints() => correctQuestions * pointsPerQuestion;
int GetMaximumPoints() => questions.Count * pointsPerQuestion;
string HandlePlural(int count) => count == 1 ? "" : "s";

static void PrintPage(string text)
{
    Console.Clear();
    Console.WriteLine(text);
    WaitForAnyKey();
}

static void WaitForAnyKey()
{
    Console.Write("\nAperte qualquer botão para continuar.");
    Console.ReadKey();
}