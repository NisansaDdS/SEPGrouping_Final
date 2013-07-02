using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SEPGrouping
{
    class BooleanPrinter
    {
        static BooleanPrinter instance = new BooleanPrinter();

        public static BooleanPrinter GetInstance()
        {
            return (instance);
        }

       public String GetBooleanString(Boolean candidate)
        {
            if (candidate)
            {
                return ("1");
            }
            else
            {
                return ("0");
            }
        }

       public void PrintLine(List<Boolean> candidateLine)
       {
           for (int i = 0; i < candidateLine.Count; i++)
           {
               Console.Write(GetBooleanString(candidateLine[i])+" ");
           }
           Console.Write("\n");
       }

       public void PrintMatrix(List<List<Boolean>> candidateMatrix)
       {
           for (int i = 0; i < candidateMatrix.Count; i++)
           {
               PrintLine(candidateMatrix[i]);
           }
       }


    }
}
