using System;
using System.Drawing;
using System.Runtime.Serialization;
using Console = Colorful.Console;

namespace Four_in_a_row
{
    class Board
    {

        //Struct of the game pieces
        public struct Tile
        {
            public int Value { get; set; }

            public bool IsDirty { get; set; }

            public Tile(int value = 0, bool isDirty = true) : this()
            {
                Value = value;
                IsDirty = isDirty;
            }
        }

        public Tile[,] GameBoard { get; set; }
        public int rowLength { get; }
        public int colLength { get; }

        public Board(int rows, int columns)
        {
            GameBoard = new Tile[rows, columns];

            //Set each position in 2D array to a new Tile
            for (int y = 0; y < columns; y++)
            {
                for (int x = 0; x < rows; x++)
                {
                    GameBoard[x, y] = new Tile(0, true);
                }
            }

            rowLength = GameBoard.GetLength(0);
            colLength = GameBoard.GetLength(1);
        }

        public void PrintBoard()
        {
            //REQUIRES THE CONSOLE TO BE USING THE FONT: "NSIMSUM"

            Console.Clear();

            Console.OutputEncoding = System.Text.Encoding.UTF8;

            //Numbers under board
            string colNumbers = "❶❷❸❹❺❻❼❽❾❿⓫⓬⓭⓮⓯⓰⓱⓲⓳⓴";
            string getColNumbers = colNumbers.Substring(0, colLength);


            //Loop through the rows and columns and print each to the console
            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    if(GameBoard[i,j].IsDirty)
                    {
                        if (GameBoard[i, j].Value == 0)
                        {
                            Console.SetCursorPosition(j * 2, i);
                            Console.Write("●", Color.White);
                        }
                        else if (GameBoard[i, j].Value == 1)
                        {
                            Console.SetCursorPosition(j * 2, i);
                            Console.Write("●", Color.CadetBlue);
                        }
                        else
                        {
                            Console.SetCursorPosition(j * 2, i);
                            Console.Write("●", Color.PaleVioletRed);
                        }
                    }
                }
                Console.Write("\n");
            }
            Console.WriteLine(getColNumbers);
        }

        //Clear text under boardboard so it doesn't print ontop of current text
        public void CleanText()
        {
            Console.SetCursorPosition(0, rowLength + 1);

            int lines = Console.WindowHeight - rowLength;

            for (int i = 0; i <= lines; i++)
            {
                Console.SetCursorPosition(0, i + (rowLength + 1));
                Console.Write("\r" + new string(' ', Console.BufferWidth) + "\r");
            }
            Console.SetCursorPosition(0, 0);
            Console.SetCursorPosition(0, rowLength + 1);

        }


        public void UpdateBoard()
        {
            //Column numbers under board
            string colNumbers = "❶❷❸❹❺❻❼❽❾❿⓫⓬⓭⓮⓯⓰⓱⓲⓳⓴";
            string getColNumbers = colNumbers.Substring(0, colLength);


            //If piece has been updated to a new player, set cursor position and update
            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    if (GameBoard[i, j].IsDirty)
                    {
                        if (GameBoard[i, j].Value == 0)
                        {
                            Console.SetCursorPosition(j * 2, i);
                            Console.Write("●", Color.White);
                        }
                        else if (GameBoard[i, j].Value == 1)
                        {
                            Console.SetCursorPosition(j * 2, i);
                            Console.Write("●", Color.CadetBlue);
                        }
                        else
                        {
                            Console.SetCursorPosition(j * 2, i);
                            Console.Write("●", Color.PaleVioletRed);
                        }
                    }
                }
            }
            Console.Write("\n");
            Console.WriteLine(getColNumbers);
        }
    }
}
