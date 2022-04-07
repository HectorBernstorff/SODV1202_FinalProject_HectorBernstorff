using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_project
{
    class Connect4
    {
        private const int numColsRows = 9;
        private static char[,] board = new char[numColsRows, numColsRows];
        private static bool[] columnFull = new bool[numColsRows];
        private static char currentPlayerSymbol;
        private static int chosenColumn = 0;
        private static bool done = false;
        private static bool simulation = true; //Change to true for Player VS Computer mode.

        static async Task Main(string[] args)
        {
            Console.WriteLine("Final Project Connect 4\n");
            Console.WriteLine("Each player will choose a column to drop their symbol on each turn.");
            Console.WriteLine("The winner of the game is the first to have 4 discs of their symbol/color in a row. \nWich can be horizontally, vertically, or diagonally. \nHow to play: Choose a column, type a number from 0 to 8 referring to the selected column and then press enter!");
            Console.WriteLine("\nThe first to play will be chosen randomly");
            Console.WriteLine("\nPLAYER (X) and PLAYER (O).");

            if (!simulation)
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
            InitializeBoard();
            displayBoard();

            int determineFirstPLayer = new Random().Next() % 2;

            if (determineFirstPLayer == 0)
            {
                currentPlayerSymbol = 'X';
                Console.WriteLine("PLAYER (X) starts.");
            }
            else
            {
                currentPlayerSymbol = 'O';
                Console.WriteLine("PLAYER (O) starts.");
            }
                do
                {
                    if (currentPlayerSymbol == 'X')
                    {
                        Console.WriteLine("PLAYER (x):");
                    }
                    else
                    {
                        Console.Write("PLAYER (O):\n");
                    }

                    try
                    {
                        if (simulation)
                        {
                            do
                            {
                                if (currentPlayerSymbol == 'X')
                                {
                                    await Task.Delay(1500);
                                    chosenColumn = new Random().Next() % 9;
                                    Console.WriteLine(chosenColumn);
                                }
                                else
                                {
                                    double temp = Double.Parse(Console.ReadLine());
                                    chosenColumn = (int)temp;
                                }
                            }
                            while (done == true);
                        }
                        else
                        {
                            double temp = Double.Parse(Console.ReadLine());
                            chosenColumn = (int)temp;
                        }

                        if (chosenColumn >= 0 && chosenColumn <= 8)
                        {
                            placeInColumn(chosenColumn, currentPlayerSymbol);
                            if (WinConditionMet(currentPlayerSymbol))
                            {
                                Console.WriteLine("Player {0} Won!", currentPlayerSymbol);
                                PlayAgainPrompt();
                        }
                            else if (!columnFull[chosenColumn])
                            {
                                currentPlayerSymbol = (currentPlayerSymbol == 'O' ? 'X' : 'O');
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
                InitializeBoard();
                displayBoard();
                done = false;
            }
            else done = true;
        }

        private static void InitializeBoard()
        {
            for (int i = 0; i < numColsRows; i++)
            {
                columnFull[i] = false;
                for (int j = 0; j < numColsRows; j++)
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
            for (int i = 0; i < numColsRows; i++) if (!columnFull[i]) boardIsFull = false;
            return boardIsFull;
        }
        private static void displayBoard()
        {
            Console.WriteLine("\n\n 012345678");
            for (int i = 1; i < numColsRows; i++)
            {
                Console.Write("|");
                for (int j = 0; j < numColsRows; j++)
                {
                    Console.Write(Connect4.board[i, j]);
                }
                Console.WriteLine();
            }
        }
        private static void placeInColumn(int columnNumber, char symbol)
        {
            int index = numColsRows - 1;
            char cc = Connect4.board[index, columnNumber];
            while ((cc == 'X' || cc == 'O') && index >= 0)
            {
                index--;
                if (index >= 0) cc = Connect4.board[index, columnNumber];
            }
            if (index < 0) columnFull[columnNumber] = true;
            if (!columnFull[columnNumber])
            {
                Connect4.board[index, columnNumber] = symbol;
                displayBoard();
            }
            else
            {
                Console.WriteLine("FULL CLOLUMN!");
            }
        }
    }
}

