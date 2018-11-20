using System;
using System.Drawing;
using System.Runtime.Serialization;
using Console = Colorful.Console;


namespace Four_in_a_row
{
    class Program
    {
        static void Main(string[] args)
        {
            Start();
        }

        public static void Start()
        {
            int Rows = 6;
            int Columns = 7;
            string PlayerOne;
            string PlayerTwo;
            string input;
            int temp;

            Console.WriteAscii("CONNECT FOUR", Color.White);

            Console.Write("Enter player one's name: ", Color.White);
            PlayerOne = Console.ReadLine();

            Console.Write("Enter player two's name: ", Color.White);
            PlayerTwo = Console.ReadLine();

            bool Valid = false;
            while (!Valid)
            {

                Console.Write("Enter number of columns (nothing for default): ", Color.White);

                input = Console.ReadLine();
                //Is input a number?
                if (int.TryParse(input, out temp))
                {
                    //is input under 20?
                    if (temp <= 20)
                    {
                        Columns = temp;
                        Valid = true;
                    }
                    else
                    {
                        Console.WriteLine("Sorry the number of columns has to be 20 or less", Color.White);
                    }
                } else
                {
                    Valid = true;
                }
            }

            Console.Write("Enter number of rows (nothing for default): ", Color.White);

            input = Console.ReadLine();

            //Is input a number?
            if (int.TryParse(input, out temp))
            {
                Rows = temp;
            }


            //Default player names if empty
            if (PlayerOne == "")
            {
                PlayerOne = "Player one";
            }
            if (PlayerTwo == "")
            {
                PlayerTwo = "Player two";
            }

            GameLogic Game = new GameLogic(Rows, Columns, PlayerOne, PlayerTwo);

            Game.RunGame();
        }
    }
}