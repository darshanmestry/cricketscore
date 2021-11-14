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

        public string TeamName;
        public int TotalScore =0;
        public int TotalWicketsLost =0;
        public int Extras = 0;
        public int PlayerCount = 0;
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
    }
}
