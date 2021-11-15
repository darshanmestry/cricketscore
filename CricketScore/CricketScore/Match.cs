using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketScore
{
    class Match
    {
       


        public static string result = string.Empty;

        public static Innings[] innings_obj;

        int NoOfPlayers = 0;
        int NoOfOvers = 0;

        public static bool isMatchOver = false;
        public static bool isSecondInningStarted = false;



        public Match()
        {

            innings_obj = new Innings[2];

            
        }

        public void startMatch()
        {


            init();

            Console.WriteLine("Enter Batting Order for Team 1:");
            List<string> playerNames = InputValidator.getBattingOrder(NoOfPlayers);


            innings_obj[0] = new Innings(playerNames, "Team_1",NoOfOvers);
            innings_obj[0].Buildinnings();


            playerNames.Clear();
            isSecondInningStarted = true;
            Console.WriteLine("Enter Batting Order for Team 2:");
            playerNames = InputValidator.getBattingOrder(NoOfPlayers);

            innings_obj[1] = new Innings(playerNames, "Team_2", NoOfOvers);
            innings_obj[1].Buildinnings();

            if (innings_obj[0].getTeamObj().getTotalScore() > innings_obj[1].getTeamObj().getTotalScore())
            {
                result = innings_obj[0].getTeamObj().getTeamName() + " Won by " + (innings_obj[0].getTeamObj().getTotalScore() - innings_obj[1].getTeamObj().getTotalScore()) + " runs";
            }
            else if (innings_obj[0].getTeamObj().getTotalScore() < innings_obj[1].getTeamObj().getTotalScore())
            {
                result = innings_obj[1].getTeamObj().getTeamName() + " Won by " + ((innings_obj[0].getTeamObj().PlayerList.Count - innings_obj[1].getTeamObj().getTotalWicketLost())-1) + " wickets";
            }
            else
                result = "Match tied";

            Console.WriteLine("");
            Console.WriteLine("Result: " + result);
        }

        void init()
        {
            
            Console.WriteLine("Enter No of Players Foreach Team:");
            NoOfPlayers = InputValidator.getNumericValue();

            Console.WriteLine("Enter No of Overs:");
            NoOfOvers = InputValidator.getNumericValue();
        }

       

        //void UpdateTeamSheet(Team obj, string runTemp)
        //{

        //    int currentBatterIndex = (obj.PlayerList.ElementAt(batter1Index).isOnStrike == true) ? batter1Index : batter2Index;

        //    if (obj.PlayerList.ElementAt(currentBatterIndex).isOnStrike == true)
        //    {
        //        //Increment balls faced
        //        obj.PlayerList.ElementAt(currentBatterIndex).BallsFaced++;

        //        //Wicket fell
        //        if (helper.isWicket(runTemp, wicket))
        //        {
        //            obj.PlayerList.ElementAt(currentBatterIndex).isOnStrike = false;
        //            obj.PlayerList.ElementAt(currentBatterIndex).isOut = true;
        //            obj.PlayerList.ElementAt(currentBatterIndex).strikeRate = helper.getStrikeRate(obj.PlayerList.ElementAt(currentBatterIndex).RunsScored, obj.PlayerList.ElementAt(currentBatterIndex).BallsFaced);
        //            obj.TotalWicketsLost++;

        //            //get next batter
        //            if (currentBatterIndex == batter1Index)
        //            {
        //                batter1Index = helper.getNextBatterIndex(batter1Index, batter2Index);
        //                currentBatterIndex = batter1Index;
        //            }
        //            else if (currentBatterIndex == batter2Index)
        //            {
        //                batter2Index = helper.getNextBatterIndex(batter1Index, batter2Index);
        //                currentBatterIndex = batter2Index;
        //            }

        //            //check is team all out before set next batter on strike
        //            if (!obj.isAllOut())
        //                obj.PlayerList.ElementAt(currentBatterIndex).isOnStrike = true;
        //        }
        //        else
        //        {
        //            //Increment runs scored by player
        //            obj.PlayerList.ElementAt(currentBatterIndex).RunsScored += int.Parse(runTemp);

        //            //Increment Total Team score
        //            obj.TotalScore += int.Parse(runTemp);

        //            //Increment Total six counter
        //            if (helper.isSix(runTemp, six))
        //                obj.PlayerList.ElementAt(currentBatterIndex).SixCount++;

        //            //Increment Total four counter
        //            if (helper.isFour(runTemp, four))
        //                obj.PlayerList.ElementAt(currentBatterIndex).FourCount++;

        //            //Calculate strike rate
        //            obj.PlayerList.ElementAt(currentBatterIndex).strikeRate = helper.getStrikeRate(obj.PlayerList.ElementAt(currentBatterIndex).RunsScored, obj.PlayerList.ElementAt(currentBatterIndex).BallsFaced);
        //        }

        //    }

        //    //Check if Strike needs change 
        //    if (helper.isStrikeChange(runTemp))
        //    {
        //        helper.toggleStrike(obj, batter1Index, batter2Index);
        //    }
        //}

        public Innings[] getInnings()
        {
            return innings_obj;
        }
    }
}
