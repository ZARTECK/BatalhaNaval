using System;
using System.Collections.Generic;
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

            int sizetabuleiro = 0;
            char[,] board1 = new char[sizetabuleiro, sizetabuleiro];
            char[,] board2 = new char[sizetabuleiro, sizetabuleiro];
            int dificuldade = 0;

            Console.WriteLine("Dificuldade de jogo? (1 - Facil [10*10] | 2 - medio [15*15] | 3 - dificil [2*20]");
            dificuldade = int.Parse(Console.ReadLine());


            if (dificuldade == 1)
            {
                sizetabuleiro = 10;
            }
            else if (dificuldade == 2)
            {
                sizetabuleiro = 15;
            }
            else if (dificuldade == 3)
            {
                sizetabuleiro = 20;
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
            Console.WriteLine("Jogador 1, Insira os seus navios:");
            InserirBarcos(board1);

            // Inserir barcos do player 2 na board
            Console.WriteLine("Jogador 2, Insira os seus navios:");
            InserirBarcos(board2);

            // comeco do jogo
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
                Console.WriteLine("  0 1 2 3 4 5 6 7 8 9");
                for (int i = 0; i < sizetabuleiro; i++)
                {
                    Console.Write(i + " ");
                    for (int j = 0; j < sizetabuleiro; j++)
                    {
                        if (opponentBoard[i, j] == 'S')
                        {
                            Console.Write(". ");
                        }
                        else
                        {
                            Console.Write(opponentBoard[i, j] + " ");
                        }
                    }
                    Console.WriteLine();
                }

                // Get the player's guess
                Console.Write("Enter row: ");
                int guessRow = int.Parse(Console.ReadLine());
                Console.Write("Enter col: ");
                int guessCol = int.Parse(Console.ReadLine());

                // Check if the player hit a ship
                if (opponentBoard[guessRow, guessCol] == 'S')
                {
                    Console.WriteLine("HIT!");
                    opponentBoard[guessRow, guessCol] = 'H';
                }
                else
                {
                    Console.WriteLine("MISS");
                    opponentBoard[guessRow, guessCol] = 'M';
                }

                // Check if the player sank all the opponent's ships
                bool gameOver = true;
                for (int i = 0; i < sizetabuleiro; i++)
                {
                    for (int j = 0; j < sizetabuleiro; j++)
                    {
                        if (opponentBoard[i, j] == 'S')
                        {
                            gameOver = false;
                            break;
                        }
                    }
                }
                if (gameOver)
                {
                    Console.WriteLine("Player " + currentPlayer + " sank all the opponent's ships! Player " + currentPlayer + " wins!");
                    break;
                }

                // Switch to the other player's turn
                currentPlayer = (currentPlayer == 1) ? 2 : 1;
            }
        }

        public void InserirBarcos(char[,] board)
        {
            for (int i = 0; i < 8; i++)
            {
                Console.Write("Enter row for ship " + (i + 1) + ": ");
                int row = int.Parse(Console.ReadLine());
                Console.Write("Enter col for ship " + (i + 1) + ": ");
                int col = int.Parse(Console.ReadLine());
                board[row, col] = 'S';
            }

        }

        public void tabuleiroSize(string dificuldade, int)
    }
}
