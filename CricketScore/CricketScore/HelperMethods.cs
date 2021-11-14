using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketScore
{
    class HelperMethods
    {
        public int getNumericValue()
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
        
        public List<string> getBattingOrder(int NoOfPlayers)
        {
            List<string> playerNames = new List<string>();
            for (int i = 1; i <= NoOfPlayers; i++)
            {
                Console.WriteLine("     Enter Name for Player No " + i);
                playerNames.Add(Console.ReadLine());
            }

            return playerNames;
        }

        public string getValidRunInput(List<string> validRunInput)
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

        public bool isStrikeChange(string ball)
        {
            int run;

            int.TryParse(ball, out run);

            return run % 2 == 0 ? false : true;
        }

        public bool isWicket(string runInput,string wicket)
        {
            return runInput.ToUpper() == wicket;
        }

        public bool isFour(string runInput,string four)
        {
            return runInput.ToUpper() == four;
        }

        public bool isSix(string runInput,string six)
        {
            return runInput.ToUpper() == six;
        }

        public bool isWide(string runInput,string wide)
        {
            return runInput.ToUpper() == wide;
        }

        public bool isNoBall(string runInput,string noball)
        {
            return runInput.ToUpper() == noball;
        }
    
        public int getNextBatterIndex(int batter1Index,int batter2Index)
        {
            return (Math.Max(batter1Index, batter2Index) + 1);
        }
    
        public void toggleStrike(Team obj,int batter1Index, int batter2Index)
        {
            obj.PlayerList.ElementAt(batter1Index).isOnStrike = !obj.PlayerList.ElementAt(batter1Index).isOnStrike;
            obj.PlayerList.ElementAt(batter2Index).isOnStrike = !obj.PlayerList.ElementAt(batter2Index).isOnStrike;
        }

        public void printScoreCard(Team obj,string overNo,int batter1Index, int batter2Index)
        {
            Console.WriteLine("------------------------------");
            Console.WriteLine("Score Card for " + obj.TeamName);
            Console.WriteLine("------------------------------");
            Console.WriteLine("P_Name\t\tScore\t\t4s\t\t6s\t\tBalls\t\tStrikeRate");

            for (int i = 0; i < obj.PlayerList.Count; i++)
            {
                string p_name = obj.PlayerList.ElementAt(i).Name;
                if (i==batter1Index ||i== batter2Index)
                    p_name += "*";

                Console.WriteLine(p_name + "\t\t" + obj.PlayerList.ElementAt(i).RunsScored + "\t\t" +
                                  obj.PlayerList.ElementAt(i).FourCount + "\t\t" + obj.PlayerList.ElementAt(i).SixCount + "\t\t" +
                                  obj.PlayerList.ElementAt(i).BallsFaced+"\t\t"+obj.PlayerList.ElementAt(i).strikeRate.ToString("0.00"));
            }

            Console.WriteLine("Total:" + obj.TotalScore+"/"+obj.TotalWicketsLost+" ,(Extras: "+obj.Extras+" )");

            Console.WriteLine("Over " + overNo);
        }

        public string isTeamBatting2ndWinning(Team team1, Team team2 )
        {
            string result = string.Empty;
            if(team2.TotalScore > team1.TotalScore)
            {
                result = team2.TeamName + " won by " + ((team2.PlayerCount - 1) - (team2.TotalWicketsLost)) + " wickets";
            }

            return result;
        }

        public string isTeamBatting1stWinning(Team team1, Team team2)
        {
            string result = string.Empty;
            if (team1.TotalScore > team2.TotalScore)
            {
                result = team1.TeamName + " won by " + (team1.TotalScore - team2.TotalScore) + " runs";
            }

            return result;
        }
   
       
        public double getStrikeRate(int runsScored,int ballsFaced)
        {
            double res =(double)runsScored / (double)ballsFaced;
            res *= 100;

            return res;
        }
    
    }
}
