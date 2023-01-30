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
            Console.WriteLine("Dificuldade de jogo? ");
            
            // Criar a board
            char[,] board1 = new char[10, 10];
            char[,] board2 = new char[10, 10];
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    board1[i, j] = '.';
                    board2[i, j] = '.';
                }
            }

            // Place player 1's ships on the board
            Console.WriteLine("Player 1, place your ships:");
            PlaceShips(board1);

            // Place player 2's ships on the board
            Console.WriteLine("Player 2, place your ships:");
            PlaceShips(board2);

            // Play the game
            int currentPlayer = 1;
            while (true)
            {
                char[,] currentBoard;
                char[,] opponentBoard;

                // Determine which board and opponent board to use based on the current player
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

                // Display the opponent's board
                Console.WriteLine("Player " + currentPlayer + "'s turn:");
                Console.WriteLine("Opponent's board:");
                Console.WriteLine("  0 1 2 3 4 5 6 7 8 9");
                for (int i = 0; i < 10; i++)
                {
                    Console.Write(i + " ");
                    for (int j = 0; j < 10; j++)
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
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
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

        public void PlaceShips(char[,] board)
        {
            for (int i = 0; i < 3; i++)
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
