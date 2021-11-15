using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketScore
{
    class Player
    {
		 string Name;

		 int RunsScored =0;
		 int BallsFaced =0;

		 int SixCount =0;
		 int FourCount =0;

		 bool isOnStrike = false;
		 bool isOut = false;

		 double strikeRate = 0.0f;

		public Player(string Name)
		{
			this.Name = Name; 
		}

		public double CalculateStrikeRate(int runsScored, int ballsFaced)
		{
			double res = (double)runsScored / (double)ballsFaced;
			res *= 100;

			return res;
		}

		public string getPlayerName()
        {
			return Name;
        }
		public int getRunsScored()
        {
			return RunsScored;
        }

		public int getBallsFaced()
        {
			return BallsFaced;
		}
		public int getSixCount()
        {
			return SixCount;
		}

		public int getFourCount()
        {
			return FourCount;
		}

		public bool getIsOnStrike()
        {
			return isOnStrike;
        }

		public bool getIsOut()
        {
			return isOut;
		}

		public double getStrikeRate()
        {
			return strikeRate;
		}

		public void incrementBallsFaced()
        {
			BallsFaced++;
		}

		public void setIsOnStrike(bool val)
        {
			isOnStrike = val;
		}

		public void setIsOut(bool val)
        {
			isOut = true;
        }

		public void setStrikeRate(double val)
        {
			strikeRate = val;
        }

		public void incrementRun(int val)
        {
			RunsScored += val;
        }

		public void incSixCount()
        {
			SixCount++;
        }

		public void incFourCount()
        {
			FourCount++;
        }
	}
}
