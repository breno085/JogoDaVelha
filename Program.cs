char[,] tabuleiro = new char[3, 3];
int linha;
int coluna;
bool fimJogo = false;
int jogador = 1;
int jogada = 0;
bool emptySpace = true;

//preenchimento da matriz com espaços em branco
for (int l = 0; l < 3; l++)
{
    for (int c = 0; c < 3; c++)
    {
        tabuleiro[l, c] = ' ';
    }
}

do
{
    //impressão do tabuleiro atual
    ImprimirTabuleiro(tabuleiro);
    
    if (jogador == 1)
        Console.WriteLine("JOGADOR 1: ");
    else
        Console.WriteLine("JOGADOR 2: ");
    
    do
    {    
        Console.WriteLine("Seleciona uma linha [1-3]: ");
        linha = Convert.ToInt32(Console.ReadLine()) - 1;
        Console.WriteLine("Seleciona uma coluna [1-3]: ");
        coluna = Convert.ToInt32(Console.ReadLine()) - 1;

        if (tabuleiro[linha, coluna] == 'X' || tabuleiro[linha, coluna] == 'O')
        {    
            Console.WriteLine("Espaço já preenchido! Escolha outra posição!");
            emptySpace = false;
        }
        else
            emptySpace = true;
    } while (!emptySpace);

    jogada++;
    //chamada da função para verificar as consequências da jogda realizada
    fimJogo = ConferirJogada(tabuleiro, linha, coluna, jogador, jogada);

    //troca de jogador
    jogador = (jogador == 1) ? 2: 1;
} while (!fimJogo);

static bool ConferirJogada(char[,] tabuleiro, int linha, int coluna, int jogador, int jogada)
{
    bool trinca = false;

    if (jogador == 1)
        tabuleiro[linha, coluna] = 'X';
    else
        tabuleiro[linha, coluna] = 'O';

    //verificar na mesma linha
    for (int c = 0; c < 3; c++)
    {
        if (tabuleiro[linha, c] != tabuleiro[linha, coluna])
            break;
        if (c == 2)
            trinca = true;
    }

    //verificar na mesma coluna
    if (!trinca)
    {
        for (int l = 0; l < 3; l++)
        {
            if (tabuleiro[l, coluna] != tabuleiro[linha, coluna])
                break;
            if (l == 2)
                trinca = true;
        }
    }

    //verificar na diagonal principal
    if (!trinca)
    {
        if (linha == coluna)
        {
            for (int cont = 0; cont < 3; cont++)
            {
                if (tabuleiro[cont, cont] != tabuleiro[linha, coluna])
                    break;
                if (cont == 2)
                    trinca = true;
            }
        }
    }

    //verificar na diagonal secundária
    if (!trinca)
    {
        if (linha + coluna == 3 - 1)
        {
            for (int cont = 0; cont < 3; cont++)
            {
                if (tabuleiro[cont, 3-cont-1] != tabuleiro[linha, coluna])
                    break;
                if (cont == 2)
                    trinca = true;
            }
        }
    }

    if (trinca)
    {   
        Console.WriteLine();
        ImprimirTabuleiro(tabuleiro);
        Console.WriteLine("\nJOGADOR " + jogador + " VENCEU!");
        return true;
    }
    if (jogada == 9)
    {
        Console.WriteLine();
        ImprimirTabuleiro(tabuleiro);
        Console.WriteLine("\nEMPATE!");
        return true;
    }
    else
    {
        Console.WriteLine("\nPRÓXIMO JOGADOR...\n");
        return false;
    }
}

static void ImprimirTabuleiro(char[,] tabuleiro)
{
    for (int l = 0; l < 3; l++)
    {
        for (int c = 0; c < 3; c++)
        {
            Console.Write(tabuleiro[l, c]);
            if (c < 2)
                Console.Write("|");
        }
        Console.WriteLine();
        if (l < 2)
            Console.Write("-----\n");
    }
}