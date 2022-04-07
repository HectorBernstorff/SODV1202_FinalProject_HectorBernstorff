using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_project
{
    class Connect4
    {
        private const int row_col = 9;
        private static char[,] board = new char[row_col, row_col];
        private static bool[] column_full = new bool[row_col];
        private static char current_player;
        private static int selected_column = 0;
        private static bool done = false;
        private static bool onePlayer_mode = true; //Change to false for Player VS Playes mode.

        static async Task Main(string[] args)
        {
            Console.WriteLine("Final Project Connect 4\n");
            Console.WriteLine("Each player will choose a column to drop their symbol on each turn.");
            Console.WriteLine("The winner of the game is the first to have 4 discs of their symbol/color in a row. \nWich can be horizontally, vertically, or diagonally. \nHow to play: Choose a column, type a number from 0 to 8 referring to the selected column and then press enter!");
            Console.WriteLine("\nThe first to play will be chosen randomly");
            Console.WriteLine("\nPLAYER (X) and PLAYER (O).");

            if (!onePlayer_mode)
            {
                Console.WriteLine("\n********** Player VS Playes Mode **********\n");
                Console.WriteLine("Before starting the game decide who will be the X and who will be the O.");
                Console.WriteLine("If you have alredy decided press enter:");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("\n********** Player(O) VS Computer(X) Mode **********\n");
                Console.WriteLine("Before starting the game know that on this mode: Player will always be (O) and Computer will always be (X) ");
                Console.WriteLine("If you are ready press enter:");
                Console.ReadLine();
            }
            Board();
            startBoard();

            int determineFirstPLayer = new Random().Next() % 2;

            if (determineFirstPLayer == 0)
            {
                current_player = 'X';
                Console.WriteLine("PLAYER (X) starts.");
            }
            else
            {
                current_player = 'O';
                Console.WriteLine("PLAYER (O) starts.");
            }
                do
                {
                    if (current_player == 'X')
                    {
                        Console.WriteLine("PLAYER (x):");
                    }
                    else
                    {
                        Console.Write("PLAYER (O):\n");
                    }

                    try
                    {
                        if (onePlayer_mode)
                        {
                            do
                            {
                                if (current_player == 'X')
                                {
                                    await Task.Delay(1500);
                                selected_column = new Random().Next() % 9;
                                    Console.WriteLine(selected_column);
                                }
                                else
                                {
                                    double temp = Double.Parse(Console.ReadLine());
                                selected_column = (int)temp;
                                }
                            }
                            while (done == true);
                        }
                        else
                        {
                            double temp = Double.Parse(Console.ReadLine());
                        selected_column = (int)temp;
                        }

                        if (selected_column >= 0 && selected_column <= 8)
                        {
                            placeInColumn(selected_column, current_player);
                            if (WinConditionMet(current_player))
                            {
                                Console.WriteLine("Player {0} Won!", current_player);
                                PlayAgainPrompt();
                        }
                            else if (!column_full[selected_column])
                            {
                            current_player = (current_player == 'O' ? 'X' : 'O');
                            }

                            if (BoardIsFull())
                            {
                                Console.WriteLine("Tie!");
                                PlayAgainPrompt();
                            }
                        }
                        else
                        {
                            Console.WriteLine("CHOOSE BETWEEN 0 AND 8.");
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("CHOOSE BETWEEN 0 AND 8.");
                    }
                } while (!done); 
        } 

        private static void PlayAgainPrompt()
        {
            Console.WriteLine("\nEnter Y to play again or N to exit.");
            if (Console.ReadLine().ToUpper().StartsWith("Y"))
            {
                Board();
                startBoard();
                done = false;
            }
            else done = true;
        }

        private static void Board()
        {
            for (int i = 0; i < row_col; i++)
            {
                column_full[i] = false;
                for (int j = 0; j < row_col; j++)
                    board[i, j] = '#';
            }
        }

        private static bool WinConditionMet(char cps)
        {
            bool winOrNot = false;

            for (int i = 0; i < 9; i++) //Check horizontal winning moves.
            {
                for (int j = 0; j < 6; j++)
                {
                    if (board[i, j] == cps &&
                        board[i, j + 1] == cps &&
                        board[i, j + 2] == cps &&
                        board[i, j + 3] == cps)
                    {
                        Console.WriteLine("4 SYMBOLS IN A ROW DETECTED! Row {0}, Column {1}.", i, j);
                        winOrNot = true;
                    }
                }
            }

            for (int i = 0; i < 6; i++) //Check horizontal winning moves.
            {
                for (int j = 0; j < 9; j++)
                {
                    if (board[i, j] == cps &&
                        board[i + 1, j] == cps &&
                        board[i + 2, j] == cps &&
                        board[i + 3, j] == cps)
                    {
                        Console.WriteLine("4 SYMBOLS IN A ROW DETECTED! Row {0}, Column {1}.", i, j);
                        winOrNot = true;
                    }
                }
            }

            for (int i = 0; i < 6; i++) //Check diagonal down winning moves.
            {
                for (int j = 0; j < 6; j++)
                {
                    if (board[i, j] == cps &&
                        board[i + 1, j + 1] == cps &&
                        board[i + 2, j + 2] == cps &&
                        board[i + 3, j + 3] == cps)
                    {
                        Console.WriteLine("4 SYMBOLS IN A ROW DETECTED! Row {0}, Column {1}.", i, j);
                        winOrNot = true;
                    }
                }
            }

            for (int i = 0; i < 6; i++) //Check diagonal up winning moves.
            {
                for (int j = 8; j < 3; j--)
                {
                    if (board[i, j] == cps &&
                        board[i + 1, j - 1] == cps &&
                        board[i + 2, j - 2] == cps &&
                        board[i + 3, j - 3] == cps)
                    {
                        Console.WriteLine("4 SYMBOLS IN A ROW DETECTED! Row {0}, Column {1}.", i, j);
                        winOrNot = true;
                    }
                }
            }
            return winOrNot;
        }

        private static bool BoardIsFull()
        {
            bool boardIsFull = true;
            for (int i = 0; i < row_col; i++) if (!column_full[i]) boardIsFull = false;
            return boardIsFull;
        }
        private static void startBoard()
        {
            Console.WriteLine("\n\n 012345678");
            for (int i = 1; i < row_col; i++)
            {
                Console.Write("|");
                for (int j = 0; j < row_col; j++)
                {
                    Console.Write(Connect4.board[i, j]);
                }
                Console.WriteLine();
            }
        }
        private static void placeInColumn(int columnNumber, char symbol)
        {
            int index = row_col - 1;
            char cc = Connect4.board[index, columnNumber];
            while ((cc == 'X' || cc == 'O') && index >= 0)
            {
                index--;
                if (index >= 0) cc = Connect4.board[index, columnNumber];
            }
            if (index < 0) column_full[columnNumber] = true;
            if (!column_full[columnNumber])
            {
                Connect4.board[index, columnNumber] = symbol;
                startBoard();
            }
            else
            {
                Console.WriteLine("FULL CLOLUMN!");
            }
        }
    }
}



