using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Runtime.Serialization;
using Console = Colorful.Console;

namespace Four_in_a_row
{
    class GameLogic
    {
        private string PlayerOne { get; }

        private string PlayerTwo { get; }

        private Board Board { get; }

        private string CurrentPlayerName;

        private Color CurrentColor;


        private int CurrentPlayer;
        bool GameOver = false;

        //Inital gameboard and player setup
        public GameLogic(int rows, int columns, string playerOne, string playerTwo)
        {
            Board = new Board(rows, columns);
            PlayerOne = playerOne;
            PlayerTwo = playerTwo;
        }

        public void DropPiece()
        {
            int Column = 0;

            //Get player input for column to drop piece in
            bool Valid = false;
            while (!Valid)
            {
                Console.Write("Enter column to drop piece in: ", Color.White);
                string input = Console.ReadLine();

                //Is input a number?
                int temp;
                if (int.TryParse(input, out temp))
                {
                    Column = temp;

                    //Is column number within the board and is the column not full?
                    if (Column - 1 < Board.colLength && Board.GameBoard[0, Column - 1].Value == 0)
                    {
                        Valid = true;
                    }
                    else
                    {
                        Console.WriteLine("Sorry not a valid column", Color.White);
                    }
                }
                else
                {
                    Console.WriteLine("Sorry not a valid column", Color.White);
                }
            }

            //Loop through column until a piece that is white is found
            //Set this piece in gameboard 2D array to CurrentPlayer
            for (int i = Board.rowLength; i > 0; i--)
            {
                if (Board.GameBoard[i - 1, Column - 1].Value == 0)
                {
                    Board.GameBoard[i - 1, Column - 1].Value = CurrentPlayer;

                    //Check if the move is a win
                    if (!CheckWin(i - 1, Column - 1, CurrentPlayer))
                    {
                        break;
                    }
                    else
                    {
                        GameOver = true;
                        Console.Clear();

                        Console.WriteAscii($"{CurrentPlayerName} won!", CurrentColor);

                        //Restart game?
                        Console.WriteLine("Press R to play again", Color.White);
                        string input = Console.ReadLine();
                        if (input.ToLower() == "r")
                        {
                            Console.Clear();
                            Program.Start();
                        }

                    }
                }

            }

        }

        public bool CheckWin(int row, int col, int CurrentPlayer)
        {
            int count;
            int rowMax = Board.rowLength - (row + 1);
            int colMax = Board.colLength - (col + 1);

            //Horizontal
            count = 0;
            for (int i = 1; i <= 3; i++)
            {
                if (i <= colMax)
                {
                    if (Board.GameBoard[row, col + i].Value == CurrentPlayer)
                    {
                        count++;
                    }
                }
            }
            for (int i = 1; i <= 3; i++)
            {
                if (col - i >= 0)
                {
                    if (Board.GameBoard[row, col - i].Value == CurrentPlayer)
                    {
                        count++;
                    }
                }
            }
            if (count >= 3)
            {
                return true;
            }

            //Vertical
            count = 0;
            for (int i = 1; i <= 3; i++)
            {
                if (i <= rowMax)
                {
                    if (Board.GameBoard[row + i, col].Value == CurrentPlayer)
                    {
                        count++;
                    }
                }

            }
            for (int i = 1; i <= 3; i++)
            {
                if (row - i >= 0)
                {
                    if (Board.GameBoard[row - i, col].Value == CurrentPlayer)
                    {
                        count++;
                    }
                }

            }
            if (count >= 3)
            {
                return true;
            }

            //Diagonal /
            count = 0;
            for (int i = 1; i <= 3; i++)
            {
                if (row - i >= 0 && i <= colMax)
                {
                    if (Board.GameBoard[row - i, col + i].Value == CurrentPlayer)
                    {
                        count++;
                    }
                }
            }
            for (int i = 1; i <= 3; i++)
            {
                if (i <= rowMax && col - i >= 0)
                {
                    if (Board.GameBoard[row + i, col - i].Value == CurrentPlayer)
                    {
                        count++;
                    }
                }
            }
            if (count >= 3)
            {
                return true;
            }

            //Diagonal \
            count = 0;
            for (int i = 1; i <= 3; i++)
            {
                if (i <= rowMax && i <= colMax)
                {
                    if (Board.GameBoard[row + i, col + i].Value == CurrentPlayer)
                    {
                        count++;
                    }
                }
            }
            for (int i = 1; i <= 3; i++)
            {
                if (row - i >= 0 && col - i >= 0)
                {
                    if (Board.GameBoard[row - i, col - i].Value == CurrentPlayer)
                    {
                        count++;
                    }
                }
            }
            if (count >= 3)
            {
                return true;
            }

            return false;
        }

        public void RunGame()
        {
            CurrentPlayer = 1;
            CurrentPlayerName = PlayerOne;
            CurrentColor = Color.CadetBlue;
            Board.PrintBoard();

            while (!GameOver)
            {
                Board.UpdateBoard();

                Board.CleanText();

                Console.WriteLineFormatted("It's your turn {0}!", CurrentColor, Color.White, CurrentPlayerName);

                DropPiece();

                //Switch player at the end of turn
                if (CurrentPlayer == 1)
                {
                    CurrentPlayerName = PlayerTwo;
                    CurrentPlayer = 2;
                    CurrentColor = Color.PaleVioletRed;
                }
                else
                {
                    CurrentPlayerName = PlayerOne;
                    CurrentPlayer = 1;
                    CurrentColor = Color.CadetBlue;
                }
            }
        }
    }
}
