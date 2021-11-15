using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketScore
{
    class Over
    {
        private int OverNo;
       
        int counter;
        List<Bowl> bowls;
        Team TeamObj;
        public Over(int OverNo,Team TeamObj)
        {
            this.OverNo = OverNo;
            this.counter = 1;
            this.bowls = new List<Bowl>();
            this.TeamObj = TeamObj;

        }



        public Team proceed(Team TeamObj)
        {
            while (counter <= 6)
            {
                
                string runTemp = string.Empty;
                Console.WriteLine("     Enter Run for Ball no " + counter);

                runTemp = InputValidator.getValidRunInput();

                Bowl bowl_obj = new Bowl(runTemp);
                bowls.Add(bowl_obj);
                // keep prompting user untill valid ball is bowled
                while (!bowl_obj.getIsValid())
                {
                    TeamObj.incTotalScore(1);
                    TeamObj.incExtra(1);
                    //if (isSecondInningStarted)
                    //{
                    //    result = TeamObj.isTeamBatting2ndWinning(t1_obj, TeamObj);
                    //    break;
                    //}

                    Console.WriteLine("     Enter Run for Ball no " + counter);
                    runTemp = InputValidator.getValidRunInput();
                    bowl_obj = new Bowl(runTemp);
                    bowls.Add(bowl_obj);
                }

                TeamObj=UpdateTeamSheet(bowl_obj);

                if (TeamObj.isAllOut())
                    return TeamObj;

                if(Match.isSecondInningStarted && Match.innings_obj[1].getTeamObj().getTotalScore() > Match.innings_obj[0].getTeamObj().getTotalScore())
                {
                    Match.result= TeamObj.isTeamBatting2ndWinning(Match.innings_obj[0].getTeamObj(), TeamObj);
               
                    return TeamObj;
                }
                counter++;

               
            }


            TeamObj.toggleStrike(TeamObj);

            return TeamObj;
        }

        Team UpdateTeamSheet(Bowl bowlObj)
        {

            int currentBatterIndex = (TeamObj.PlayerList.ElementAt(Team.batter1Index).getIsOnStrike() == true) ? Team.batter1Index : Team.batter2Index;

            if (TeamObj.PlayerList.ElementAt(currentBatterIndex).getIsOnStrike() == true)
            {
                //Increment balls faced
                TeamObj.PlayerList.ElementAt(currentBatterIndex).incrementBallsFaced();

                //Wicket fell
                if ("W" == bowlObj.getRun().ToString().ToUpper())
                {
                    TeamObj.PlayerList.ElementAt(currentBatterIndex).setIsOnStrike(false);
                    TeamObj.PlayerList.ElementAt(currentBatterIndex).setIsOut(true);
                    TeamObj.PlayerList.ElementAt(currentBatterIndex).setStrikeRate(
                                                                        TeamObj.PlayerList.ElementAt(currentBatterIndex).
                                                                        CalculateStrikeRate(
                                                                            TeamObj.PlayerList.ElementAt(currentBatterIndex).getRunsScored(),
                                                                            TeamObj.PlayerList.ElementAt(currentBatterIndex).getBallsFaced())
                                                                        ); // helper.getStrikeRate(obj.PlayerList.ElementAt(currentBatterIndex).RunsScored, obj.PlayerList.ElementAt(currentBatterIndex).BallsFaced);
                    TeamObj.incTotalWicketsLost();

                    //get next batter
                    if (currentBatterIndex == Team.batter1Index)
                    {
                        Team.batter1Index = TeamObj.getNextBatterIndex();
                        currentBatterIndex = Team.batter1Index;
                    }
                    else if (currentBatterIndex == Team.batter2Index)
                    {
                        Team.batter2Index = TeamObj.getNextBatterIndex();
                        currentBatterIndex = Team.batter2Index;
                    }

                    //check is team all out before set next batter on strike
                    if (!TeamObj.isAllOut())
                        TeamObj.PlayerList.ElementAt(currentBatterIndex).setIsOnStrike(true);
                }
                else
                {
                    //Increment runs scored by player
                    TeamObj.PlayerList.ElementAt(currentBatterIndex).incrementRun(int.Parse(bowlObj.getRun()));

                    //Increment Total Team score
                    TeamObj.incTotalScore(int.Parse(bowlObj.getRun()));

                    //Increment Total six counter
                    if ("6" == bowlObj.getRun().ToString().ToUpper())
                        TeamObj.PlayerList.ElementAt(currentBatterIndex).incSixCount();

                    //Increment Total four counter
                    if ("4" == bowlObj.getRun().ToString().ToUpper())
                        TeamObj.PlayerList.ElementAt(currentBatterIndex).incFourCount();

                    //Calculate strike rate
                    TeamObj.PlayerList.ElementAt(currentBatterIndex).setStrikeRate(
                        TeamObj.PlayerList.ElementAt(currentBatterIndex).CalculateStrikeRate(TeamObj.PlayerList.ElementAt(currentBatterIndex).getRunsScored(),
                                                                        TeamObj.PlayerList.ElementAt(currentBatterIndex).getBallsFaced()));
                }

            }

            //Check if Strike needs change 
            if (TeamObj.isStrikeChange(bowlObj.getRun().ToString()))
            {
                TeamObj.toggleStrike(TeamObj);
            }

            return TeamObj;
        }
        public List<Bowl> getBowls()
        {
            return bowls;
        }


        public int getBallsCompleted()
        {
            return counter;
        }

    }
}
