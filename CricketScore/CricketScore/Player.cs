using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketScore
{
    class Player
    {
		public string Name;

		public int RunsScored =0;
		public int BallsFaced =0;

		public int SixCount =0;
		public int FourCount =0;

		public bool isOnStrike = false;
		public bool isOut = false;

		public double strikeRate = 0.0f;

		public Player(string Name)
		{
			this.Name = Name; 
		}


	}
}
