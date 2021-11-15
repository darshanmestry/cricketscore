using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketScore
{
    class InputValidator
    {
        string zero = "0";
        string one = "1";
        string two = "2";
        string three = "3";
        string four = "4";
        string six = "6";
        string wide = "WD";
        string noball = "NB";
        string wicket = "W";

        public static List<string> validRunInput = new List<string>() { "0","1","2","3","4","6","NB","WD","W"};

        public static string getValidRunInput()
        {
            string runInput;
            while (true)
            {
                runInput = Console.ReadLine();

                if (validRunInput.Contains(runInput.ToUpper()))
                    break;
                else
                {
                    Console.WriteLine("     Invalid Run Input. Enter one of the following values {0, 1, 2, 3, 4, 6, WD, NB, W}");
                }
            }
            return runInput;
        }

        public static List<string> getBattingOrder(int NoOfPlayers)
        {
            List<string> playerNames = new List<string>();
            for (int i = 1; i <= NoOfPlayers; i++)
            {
                Console.WriteLine("     Enter Name for Player No " + i);
                playerNames.Add(Console.ReadLine());
            }

            return playerNames;
        }

        public static int getNumericValue()
        {
            int no;
            while (true)
            {
                try
                {
                    no = int.Parse(Console.ReadLine());
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine("     Invalid Input. Please Enter Numeric Value");
                }
            }

            return no;
        }



    }
}
