using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketScore
{
    class Program
    {

        

        

        static void Main(string[] args)
        {

            Match obj = new Match();
            obj.startMatch();


            Console.WriteLine("Press Any key to Exit");
            Console.ReadLine();
        }

    }
}
