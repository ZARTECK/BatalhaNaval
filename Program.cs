using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho_Avaliacao
{
    internal class Program
    {
        static void Main(string[] args)
        {

            int sizetabuleiro = 40;
            char[,] board1 = new char[sizetabuleiro, sizetabuleiro];
            char[,] board2 = new char[sizetabuleiro, sizetabuleiro];


            int dificuldade = 0;
            

            while (dificuldade != 1 || dificuldade != 2 || dificuldade != 3)
            {
                try
                {
                    Console.WriteLine("Dificuldade de jogo? (1 - Facil [10x10] | 2 - medio [15x15] | 3 - dificil [20x20]");
                    dificuldade = int.Parse(Console.ReadLine());
                    if (dificuldade == 1 || dificuldade == 2 || dificuldade == 3) 
                    {
                        break;
                    }
                    else if(dificuldade >= 4  || dificuldade < 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Insira uma dificuldade valida!!\n");
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Insira uma dificuldade valida!!\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }

            int loop = 1;
            while (loop == 1)
            {
                switch (dificuldade)
                {
                    case 1:
                        sizetabuleiro = 10;
                        loop = 0;
                        break;

                    case 2:
                        sizetabuleiro = 15;
                        loop = 0;
                        break;

                    case 3:
                        sizetabuleiro = 20;
                        loop = 0;
                        break;
                }
            }


            for (int i = 0; i < sizetabuleiro; i++)
            {
                for (int j = 0; j < sizetabuleiro; j++)
                {
                    board1[i, j] = '.';
                    board2[i, j] = '.';
                }
            }
            // Inserir navios do player 1 na board
            Console.Clear();
            Console.WriteLine($"Jogador 1, Insira os seus navios (APENAS VALORES ENTRE 0 e {sizetabuleiro - 1}):");
            InserirBarcos(board1 , sizetabuleiro);

            // Inserir barcos do player 2 na board
            Console.Clear();
            Console.WriteLine($"Jogador 2, Insira os seus navios (APENAS VALORES ENTRE 0 e {sizetabuleiro - 1}):");
            InserirBarcos(board2 , sizetabuleiro);

            // comeco do jogo
            Console.Clear();
            int currentPlayer = 1;
            int jogadas1 = 0, jogadas2 = 0, jogadasvencedor = 0;
            while (true)
            {
                char[,] currentBoard;
                char[,] opponentBoard;

                // Mudar de vez de jogador
                if (currentPlayer == 1)
                {
                    currentBoard = board1;
                    opponentBoard = board2;
                    jogadas1++;
                }
                else
                {
                    currentBoard = board2;
                    opponentBoard = board1;
                    jogadas2++;
                }

                // Mostrar a board
                Console.WriteLine("É a vez do jogador " + currentPlayer + ":");
                Console.WriteLine("Tabuleiro do jogador atual:");
                Console.Write("   ");
                for (int i = 0; i < sizetabuleiro; i++)
                {
                    Console.Write($"{i} ");
                }
                Console.WriteLine();
                for (int i = 0; i < sizetabuleiro; i++)
                {
                    if(i <= 9)
                    {
                        Console.Write(" " + i + " ");
                    }
                    else
                    {
                        Console.Write(i + " ");
                    }
                        
                    for (int j = 0; j < sizetabuleiro; j++)
                    {
                        if (currentBoard[i, j] == 'S')
                        {
                            Console.Write(" . ");
                        }
                        else
                        {
                            Console.Write(currentBoard[i, j] + " ");
                        }
                    }
                    Console.WriteLine();
                }

                // jogada do jogador

                int guessRow = 0;

                Console.Write("Inserir linha:");
                guessRow = int.Parse(Console.ReadLine());
                
                while (guessRow < 0 || guessRow > sizetabuleiro)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Insira um linha valida! (0 a {sizetabuleiro - 1})");
                    Console.ForegroundColor = ConsoleColor.White;
                    guessRow = int.Parse(Console.ReadLine());
                }
                
                int guessCol= 1;

                Console.Write("Inserir coluna:");
                guessCol = int.Parse(Console.ReadLine());

                while (guessCol < 0 || guessCol > sizetabuleiro)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Insira um coluna valida! (0 a {sizetabuleiro - 1})");
                    Console.ForegroundColor = ConsoleColor.White;
                    guessCol = int.Parse(Console.ReadLine());
                }

                

                Console.Clear();

                // Check se acertou ou nao
                if (opponentBoard[guessRow, guessCol] == 'N')
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nEM CHEIOOOOO!\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    opponentBoard[guessRow, guessCol] = '-';
                }

                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nFALHASTE\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    opponentBoard[guessRow, guessCol] = 'F';
                }

                // Verificar se o jogador afundou todos os navios
                bool gameOver = true;
                for (int i = 0; i < sizetabuleiro; i++)
                {
                    for (int j = 0; j < sizetabuleiro; j++)
                    {
                        if (opponentBoard[i, j] == 'N')
                        {
                            gameOver = false;
                            break;
                        }
                    }
                }
                if (gameOver)
                {
                    // inserir numero de jogadas do vencedor na variavel que vai ser usada para inserir no text file 
                    if (currentPlayer == 1)
                    {
                        jogadasvencedor = jogadas1;
                    }
                    else
                    {
                        jogadasvencedor = jogadas2;
                    }

                    // high scores dos jogadores
                    List<string> highScores = new List<string>();
                    try
                    {
                        using (StreamReader reader = new StreamReader(@"C:\Ficheiros\highscores.txt"))
                        {
                            while (!reader.EndOfStream)
                            {
                                highScores.Add(reader.ReadLine());
                            }
                        }
                    
                    }
                    catch (FileNotFoundException)
                    {
                        
                    }

                    //ler os highscores da lista

                    if (File.Exists("highscores.txt"))
                    {
                        using (StreamReader reader = new StreamReader(@"C:\Ficheiros\highscores.txt"))
                        {
                            string line;
                            while ((line = reader.ReadLine()) != null)
                            {
                                highScores.Add(line);
                            }
                        }
                    }

                    // add dos scores
                    highScores.Add(jogadasvencedor + " " + DateTime.Now.ToString());


                    // sort dos scores na lista
                    highScores.Sort((a, b) =>
                    {
                        int scoreA = int.Parse(a.Substring(0, a.IndexOf(" ")));
                        int scoreB = int.Parse(b.Substring(0, b.IndexOf(" ")));
                        if (scoreB != scoreA)
                        {
                            return scoreB - scoreA;
                        }
                        return DateTime.Parse(a.Substring(a.IndexOf(" ") + 1)).CompareTo(DateTime.Parse(b.Substring(b.IndexOf(" ") + 1)));
                    });

                    // escrever a lista no ficheiro
                    using (StreamWriter writer = new StreamWriter(@"C:\Ficheiros\highscores.txt"))
                    {
                        foreach (string highScore in highScores)
                        {
                            writer.WriteLine(highScore);
                        }
                    }

                    Console.WriteLine($"Jogador {currentPlayer} afundou os todos os navios do adversario em {jogadasvencedor} jogadas! Jogador {currentPlayer} ganha!");
                    Console.ReadKey();
                    break;

                }

                // mudar de jogador
                currentPlayer = (currentPlayer == 1) ? 2 : 1;
            }
        }
      
            static void InserirBarcos(char[,] board , int Sizetabuleiro)
            {
                for (int i = 0; i < 8; i++)
                {
                    
                    Console.Write("Insira a linha do navio " + (i + 1) + ": ");
                    int row = int.Parse(Console.ReadLine());
                    while (row < 0 || row > Sizetabuleiro - 1) 
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"\nInsira um numero valido! (0 a {Sizetabuleiro - 1})\n");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("Insira a linha do navio " + (i + 1) + ": ");
                        row = int.Parse(Console.ReadLine());
                    }

                    
                    Console.Write("Insira a coluna do navio " + (i + 1) + ": ");
                    int col = int.Parse(Console.ReadLine());
                    while (col < 0 || col > Sizetabuleiro - 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"\nInsira um numero valido! (0 a {Sizetabuleiro - 1})\n");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("Insira a coluna do navio " + (i + 1) + ": ");
                        col = int.Parse(Console.ReadLine());
                    }
                    while (board[row, col] == 'N')
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"\nCoordenada ocupada. Tente de novo!\n");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("Insira a linha do navio " + (i + 1) + ": ");
                        row = int.Parse(Console.ReadLine());
                        while (row < 0 || row > Sizetabuleiro - 1)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"\nInsira um numero valido! (0 a {Sizetabuleiro - 1})\n");
                            Console.ForegroundColor = ConsoleColor.White;
                            row = int.Parse(Console.ReadLine());
                        }
                        Console.Write("Insira a coluna do navio " + (i + 1) + ": ");
                        col = int.Parse(Console.ReadLine());
                        while (col < 0 || col > Sizetabuleiro - 1)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"\nInsira um numero valido! (0 a {Sizetabuleiro - 1})\n");
                            Console.ForegroundColor = ConsoleColor.White;
                            col = int.Parse(Console.ReadLine());
                        }
                    }
                    board[row, col] = 'N';
                }
            }
            public char[,] Board { get; set; }
   

    }
}
