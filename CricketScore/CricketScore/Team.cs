using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketScore
{
    class Team
    {
        public List<Player> PlayerList = new List<Player>();

         string TeamName;
         int TotalScore =0;
         int TotalWicketsLost =0;
         int Extras = 0;
         int PlayerCount = 0;

        public static int batter1Index = 0;
        public static int batter2Index = 1;


        public Team(string TeamName, List<string> PlayerNames)
        {
            this.TeamName = TeamName;

            foreach (string pname in PlayerNames)
            {
                Player p_obj = new Player(pname);
                PlayerList.Add(p_obj);
            }

            PlayerCount = PlayerList.Count;
        }
        public bool isAllOut()
        {
            return (PlayerCount-1) == TotalWicketsLost;
        }

        public string isTeamBatting2ndWinning(Team team1, Team team2)
        {
            string result = string.Empty;
            if (team2.TotalScore > team1.TotalScore)
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


        public bool isStrikeChange(string ball)
        {
            int run;

            int.TryParse(ball, out run);

            return run % 2 == 0 ? false : true;
        }

        

        public void toggleStrike(Team obj)
        {
            obj.PlayerList.ElementAt(batter1Index).setIsOnStrike(!obj.PlayerList.ElementAt(batter1Index).getIsOnStrike()); //= !obj.PlayerList.ElementAt(batter1Index).isOnStrike;
            obj.PlayerList.ElementAt(batter2Index).setIsOnStrike(!obj.PlayerList.ElementAt(batter2Index).getIsOnStrike()); // = !obj.PlayerList.ElementAt(batter2Index).isOnStrike;
        }



        public int getNextBatterIndex()
        {
            return (Math.Max(batter1Index, batter2Index) + 1);
        }

        public void incTotalScore(int val)
        {
            TotalScore += val;
        }

        public int getTotalScore()
        {
            return TotalScore;
        }

        public int getTotalWicketLost()
        {
            return TotalWicketsLost;
        }

        public int getExtras()
        {
            return Extras;
        }

        public string getTeamName()
        {
            return TeamName;
        }

        public void incExtra(int val)
        {
            Extras += val;
        }

        public void incTotalWicketsLost()
        {
            TotalWicketsLost++;
        }
    }
}
