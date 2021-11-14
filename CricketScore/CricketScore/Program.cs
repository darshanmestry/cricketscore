using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketScore
{
    class Program
    {

        HelperMethods helper;
        Team t1_obj;
        Team t2_obj;


        string result = string.Empty;
        string zero = "0";
        string one = "1";
        string two = "2";
        string three = "3";
        string four = "4";
        string six = "6";
        string wide = "WD";
        string noball = "NB";
        string wicket = "W";


        int NoOfPlayers = 0;
        int NoOfOvers = 0;
        int batter1Index = 0;
        int batter2Index = 1;

        bool isSecondInningStarted = false;

        List<string> validRunInput = new List<string>();

        static void Main(string[] args)
        {

            Program _obj = new Program();
            _obj.startMatch();

            Console.WriteLine("Press Any key to Exit");
            Console.ReadLine();
        }

        void startMatch()
        {

            helper = new HelperMethods();

            init();

            Console.WriteLine("Enter Batting Order for Team 1:");
            List<string> playerNames = helper.getBattingOrder(NoOfPlayers);
            t1_obj = innings(t1_obj, playerNames, "Team_1");


            batter1Index = 0;
            batter2Index = 1;
            playerNames.Clear();

            isSecondInningStarted = true;

            Console.WriteLine("Enter Batting Order for Team 2:");
            playerNames = helper.getBattingOrder(NoOfPlayers);
            t2_obj = innings(t2_obj, playerNames, "Team_2");

            if (string.IsNullOrEmpty(result) && t1_obj.TotalScore == t2_obj.TotalScore)
            {
                result = "Match Tied";
            }

            Console.WriteLine("");
            Console.WriteLine("Result: " + result);
        }



        void init()
        {
            validRunInput.Add(zero);
            validRunInput.Add(one);
            validRunInput.Add(two);
            validRunInput.Add(three);
            validRunInput.Add(four);
            validRunInput.Add(six);
            validRunInput.Add(wide);
            validRunInput.Add(noball);
            validRunInput.Add(wicket);

            Console.WriteLine("Enter No of Players Foreach Team:");
            NoOfPlayers = helper.getNumericValue();

            Console.WriteLine("Enter No of Overs:");
            NoOfOvers = helper.getNumericValue();
        }

        Team innings(Team TeamObj, List<string> playerNames, string TeamName)
        {
            TeamObj = new Team(TeamName, playerNames);

            // Default player at index 0 to be on strike
            TeamObj.PlayerList.ElementAt(batter1Index).isOnStrike = true;

            for (int i = 1; i <= NoOfOvers; i++)
            {
                // keeps record of balls bowled
                int cnt = 1;
                Console.WriteLine("Over No " + 1);
                while (cnt <= 6)
                {

                    string runTemp = string.Empty;
                    Console.WriteLine("     Enter Run for Ball no " + cnt);

                    runTemp = helper.getValidRunInput(validRunInput);


                    // keep prompting user untill valid ball is bowled
                    while (helper.isNoBall(runTemp, noball) || helper.isWide(runTemp, wide))
                    {
                        TeamObj.TotalScore += 1;
                        TeamObj.Extras++;
                        if (isSecondInningStarted)
                        {
                            result = helper.isTeamBatting2ndWinning(t1_obj, TeamObj);
                            break;
                        }

                        Console.WriteLine("     Enter Run for Ball no " + cnt);
                        runTemp = helper.getValidRunInput(validRunInput);
                    }

                    //this condition will be true when team batting 2nd will win by getting Nb or Wd henc skip updating team sheet
                    if (runTemp.ToUpper() == wide || runTemp.ToUpper() == noball)
                    {
                        //need to decrement ball count, As we team has won 
                        cnt--;
                    }
                    else
                    {

                        UpdateTeamSheet(TeamObj, runTemp);
                    }

                    //this condition will be hit when team batting 2nd will win by getting Nb or Wd
                    if (!string.IsNullOrEmpty(result))
                        break;


                    //this condition will be hit when team batting 2nd will win by getting scoring runs
                    if (isSecondInningStarted && TeamObj.TotalScore > t1_obj.TotalScore)
                    {
                        result = helper.isTeamBatting2ndWinning(t1_obj, TeamObj);
                        break;
                    }

                    if (TeamObj.isAllOut())
                    {
                        //this condition will be hit when team batting 1st will win by getting team2 all out
                        if (isSecondInningStarted)
                        {
                            result = helper.isTeamBatting1stWinning(t1_obj, TeamObj);
                        }
                        break;
                    }


                    cnt++;

                    //toggle strike for Over completion
                    if (cnt > 6)
                    {
                        helper.toggleStrike(TeamObj, batter1Index, batter2Index);
                    }
                }

                string oversCompleted = (cnt >= 6) ? i.ToString() : (i - 1).ToString() + "." + (cnt);


                //Print Scoreboard
                helper.printScoreCard(TeamObj, oversCompleted, batter1Index, batter2Index);

                if (TeamObj.isAllOut())
                {
                    //this condition will be hit when team batting 1st will win by getting team2 all out
                    if (isSecondInningStarted)
                    {
                        result = helper.isTeamBatting1stWinning(t1_obj, TeamObj);
                    }
                    break;
                }

                // Need to exit from Overs Loop, When Team batting 2nd would be winning with some overs to spare.
                if (isSecondInningStarted && !string.IsNullOrEmpty(result))
                    break;
            }

            // Team batting first Winning by completing their overs and restricting team 2
            if (isSecondInningStarted && string.IsNullOrEmpty(result))
            {
                result = helper.isTeamBatting1stWinning(t1_obj, TeamObj);
            }

            return TeamObj;
        }

        void UpdateTeamSheet(Team obj, string runTemp)
        {

            int currentBatterIndex = (obj.PlayerList.ElementAt(batter1Index).isOnStrike == true) ? batter1Index : batter2Index;

            if (obj.PlayerList.ElementAt(currentBatterIndex).isOnStrike == true)
            {
                //Increment balls faced
                obj.PlayerList.ElementAt(currentBatterIndex).BallsFaced++;

                //Wicket fell
                if (helper.isWicket(runTemp, wicket))
                {
                    obj.PlayerList.ElementAt(currentBatterIndex).isOnStrike = false;
                    obj.PlayerList.ElementAt(currentBatterIndex).isOut = true;
                    obj.PlayerList.ElementAt(currentBatterIndex).strikeRate = helper.getStrikeRate(obj.PlayerList.ElementAt(currentBatterIndex).RunsScored, obj.PlayerList.ElementAt(currentBatterIndex).BallsFaced);
                    obj.TotalWicketsLost++;

                    //get next batter
                    if (currentBatterIndex == batter1Index)
                    {
                        batter1Index = helper.getNextBatterIndex(batter1Index, batter2Index);
                        currentBatterIndex = batter1Index;
                    }
                    else if (currentBatterIndex == batter2Index)
                    {
                        batter2Index = helper.getNextBatterIndex(batter1Index, batter2Index);
                        currentBatterIndex = batter2Index;
                    }

                    //check is team all out before set next batter on strike
                    if (!obj.isAllOut())
                        obj.PlayerList.ElementAt(currentBatterIndex).isOnStrike = true;
                }
                else
                {
                    //Increment runs scored by player
                    obj.PlayerList.ElementAt(currentBatterIndex).RunsScored += int.Parse(runTemp);

                    //Increment Total Team score
                    obj.TotalScore += int.Parse(runTemp);

                    //Increment Total six counter
                    if (helper.isSix(runTemp, six))
                        obj.PlayerList.ElementAt(currentBatterIndex).SixCount++;

                    //Increment Total four counter
                    if (helper.isFour(runTemp, four))
                        obj.PlayerList.ElementAt(currentBatterIndex).FourCount++;

                    //Calculate strike rate
                    obj.PlayerList.ElementAt(currentBatterIndex).strikeRate = helper.getStrikeRate(obj.PlayerList.ElementAt(currentBatterIndex).RunsScored, obj.PlayerList.ElementAt(currentBatterIndex).BallsFaced);
                }

            }
            
            //Check if Strike needs change 
            if (helper.isStrikeChange(runTemp))
            {
                helper.toggleStrike(obj, batter1Index, batter2Index);
            }
        }
    }
}
