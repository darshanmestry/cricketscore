using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketScore
{
    class Bowl
    {
        
        //private string[] STRIKE_RATE_CHANGE_DELIVERIES = { "1", "3" };

         string run;
         bool isWicket;
         bool isvalid;
         

        public Bowl(string input)
        {
            calculateRun(input);
           

        }

        void calculateRun(string input)
        {
            //if (InputValidator.validRunInput.wide.ToString()==input.ToUpper() || InputValidator.validRunInput.noball.ToString() == input.ToUpper())
            if (input.ToUpper() =="WD" ||  input.ToUpper()=="NB")
            {
                isvalid = false;
              
                run = "1";
            }
            else
            {
                isvalid = true;

                this.run = input.ToUpper(); 
                
            }
            
        }

        public bool getIsValid()
        {
            return isvalid;
        }

      

        public string getRun()
        {
            return run;
        }

        
         
    }
}
