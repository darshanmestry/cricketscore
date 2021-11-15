using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketScore
{
    class Innings
    {
        Team TeamObj;
        
        Over[] overObj;
        public Innings( List<string> playerNames, string TeamName,int NoOfOvers)
        {
           
            this.TeamObj = new Team(TeamName, playerNames);
            Team.batter1Index = 0;
            Team.batter2Index = 1;
            overObj = new Over[NoOfOvers];



        }


        public void Buildinnings()
        {
            TeamObj.PlayerList.ElementAt(Team.batter1Index).setIsOnStrike(true);

            for (int i=0;i<overObj.Count();i++)
            {
                overObj[i] = new Over(i+1, TeamObj);
                //TeamObj.PlayerList.ElementAt(Team.batter1Index).setIsOnStrike(true);
                TeamObj = overObj[i].proceed(TeamObj);

                string tempOverNo = string.Empty;
                if (overObj[i].getBallsCompleted() >= 6)
                    tempOverNo = (i+1).ToString();
                else
                    tempOverNo=i+"."+overObj[i].getBallsCompleted();
                printScoreCard(tempOverNo);

                if (TeamObj.isAllOut())
                   break;

                if (!string.IsNullOrEmpty(Match.result))
                    break;
            }
          
        }

        public void printScoreCard( string overNo)
        {
            Console.WriteLine("------------------------------");
            Console.WriteLine("Score Card for " + TeamObj.getTeamName());
            Console.WriteLine("------------------------------");
            Console.WriteLine("P_Name\t\tScore\t\t4s\t\t6s\t\tBalls\t\tStrikeRate");

            for (int i = 0; i < TeamObj.PlayerList.Count; i++)
            {
                string p_name = TeamObj.PlayerList.ElementAt(i).getPlayerName();
                if (i == Team.batter1Index || i == Team.batter2Index)
                    p_name += "*";

                if (TeamObj.PlayerList.ElementAt(i).getIsOnStrike())
                    p_name += "*";

                Console.WriteLine(p_name + "\t\t" + TeamObj.PlayerList.ElementAt(i).getRunsScored() + "\t\t" +
                                  TeamObj.PlayerList.ElementAt(i).getFourCount() + "\t\t" + TeamObj.PlayerList.ElementAt(i).getSixCount() + "\t\t" +
                                  TeamObj.PlayerList.ElementAt(i).getBallsFaced() + "\t\t" + TeamObj.PlayerList.ElementAt(i).getStrikeRate().ToString("0.00"));
            }

            Console.WriteLine("Total:" + TeamObj.getTotalScore() + "/" + TeamObj.getTotalWicketLost() + " ,(Extras: " + TeamObj.getExtras() + " )");

            Console.WriteLine("Over " + overNo.ToString());
        }

        public Team getTeamObj()
        {
            return TeamObj;
        }
    }
}
