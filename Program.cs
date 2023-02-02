using System;
using System.Collections.Generic;
using System.Data;
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

            //perguntar a dificuldade
            Console.WriteLine("Dificuldade de jogo? (1 - Facil [10x10] | 2 - medio [15x15] | 3 - dificil [20x20]");
            dificuldade = int.Parse(Console.ReadLine());
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
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Insira uma dificuldade valida!!\n");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Dificuldade de jogo? (1 - Facil [10x10] | 2 - medio [15x15] | 3 - dificil [20x20]");
                        dificuldade = int.Parse(Console.ReadLine());
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
            Console.WriteLine($"Jogador 1, Insira os seus navios (APENAS VALORES ENTRE 0 e {sizetabuleiro - 1}):");
            InserirBarcos(board1 , sizetabuleiro);

            // Inserir barcos do player 2 na board
            Console.WriteLine($"Jogador 2, Insira os seus navios (APENAS VALORES ENTRE 0 e {sizetabuleiro - 1}):");
            InserirBarcos(board2 , sizetabuleiro);

            // comeco do jogo
            Console.Clear();
            int currentPlayer = 1;
            while (true)
            {
                char[,] currentBoard;
                char[,] opponentBoard;

                // Mudar de vez de jogador
                if (currentPlayer == 1)
                {
                    currentBoard = board1;
                    opponentBoard = board2;
                }
                else
                {
                    currentBoard = board2;
                    opponentBoard = board1;
                }

                // Mostrar a board
                Console.WriteLine("É a vez do jogador " + currentPlayer + ":");
                Console.WriteLine("Tabuleiro do adversario:");
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
                        if (opponentBoard[i, j] == 'S')
                        {
                            Console.Write(" . ");
                        }
                        else
                        {
                            Console.Write(opponentBoard[i, j] + " ");
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

                // Check se acertou ou nao
                if (opponentBoard[guessRow, guessCol] == 'N')
                {
                    Console.WriteLine("EM CHEIOOOOO!");
                    Console.ForegroundColor = ConsoleColor.Green;
                    opponentBoard[guessRow, guessCol] = '-';
                    Console.ForegroundColor = ConsoleColor.White;
                }

                else
                {
                    Console.WriteLine("FALHASTE");
                    Console.ForegroundColor = ConsoleColor.Red;
                    opponentBoard[guessRow, guessCol] = 'F';
                    Console.ForegroundColor = ConsoleColor.White;
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
                    Console.WriteLine("Jogador " + currentPlayer + " Afundou os todos os navios do adversario! Jogador " + currentPlayer + " ganha!");
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
                    Console.Write("Insira a linha do navio: " + (i + 1) + ": ");
                    int row = int.Parse(Console.ReadLine());
                    while (row < 0 || row > Sizetabuleiro) 
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Insira um numero valido! (0 a {Sizetabuleiro - 1})");
                        Console.ForegroundColor = ConsoleColor.White;
                        row = int.Parse(Console.ReadLine());
                    }
                    Console.Write("Insira a coluna do navio " + (i + 1) + ": ");
                    int col = int.Parse(Console.ReadLine());
                    board[row, col] = 'N';

                    while (col < 0 || col > Sizetabuleiro)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Insira um numero valido! (0 a {Sizetabuleiro - 1})");
                        Console.ForegroundColor = ConsoleColor.White;
                        col = int.Parse(Console.ReadLine());
                    }
                }
            }
            public char[,] Board { get; set; }
   

    }
}
