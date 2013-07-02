using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SEPGrouping
{
    class TimeSlotMatrixManager
    {
        int slotCount;
        List<String> evaluatorVector = new List<string>();       
        ProjectHandler pc = ProjectHandler.getInstance();
        List<List<Boolean>> evaluatorTimeSlotMatrix = new List<List<Boolean>>();       
        private static TimeSlotMatrixManager instance = new TimeSlotMatrixManager();
        List<Int32> categoryLength = new List<int>();

        public List<String> EvaluatorVector
        {
            get { return evaluatorVector; }           
        }


        public List<List<Boolean>> EvaluatorTimeSlotMatrix
        {
            get { return evaluatorTimeSlotMatrix; }           
        }

        public List<Int32> CategoryLength
        {
            get { return categoryLength; }           
        }

        public static TimeSlotMatrixManager getInstance()
        {
            return (instance);
        }

        public Boolean GetExternalEvaluaterPossibility(int category, String evaluator)
        {
            int evalIndex = evaluatorVector.IndexOf(evaluator);
            return (evaluatorTimeSlotMatrix[category][evalIndex]);
        }


        public void Print()
        {
            String line = "Name\t";
            for (int i = 0; i < evaluatorTimeSlotMatrix.Count; i++)
            {
                line += i+" ";
            }
            Console.WriteLine(line);

            for (int i = 0; i < evaluatorTimeSlotMatrix[0].Count; i++)
            {
                line = evaluatorVector[i] + "\t";
                for (int j = 0; j < evaluatorTimeSlotMatrix.Count; j++)
                {
                    line += evaluatorTimeSlotMatrix[j][i] + "\t";
                }
                Console.WriteLine(line);
            }


        }



        public void SetData(List<List<String>> data)
        {
            List<String> internalEvaluatorVector = pc.InternalEvaluatorVector;            
            slotCount = data[0].Count - 1;
            int patternCount = 1;
            
            for (int j = 1; j < data[0].Count; j++)
            {                              
                    List<Boolean> externalEvalTimeSlotVector = new List<bool>();
                    Boolean addPattern = false;
                    

                    for (int i = 2; i < data.Count; i++)
                    {
                        evaluatorVector.Add(data[i][0]);
                        if (Int32.Parse(data[i][j]) == 0)
                        {
                            externalEvalTimeSlotVector.Add(false);
                        }
                        else
                        {
                            externalEvalTimeSlotVector.Add(true);
                        }

                        


                    }


                    if (j == (data[0].Count - 1))
                    {
                        addPattern = true;
                    }

                    Boolean addLine = false;
                    if (evaluatorTimeSlotMatrix.Count > 0)
                    {
                        for (int i = 0; i < externalEvalTimeSlotVector.Count; i++)
                        {
                            if (evaluatorTimeSlotMatrix[evaluatorTimeSlotMatrix.Count - 1][i] != externalEvalTimeSlotVector[i])
                            {
                                addPattern = true;
                                addLine =true;
                                break;
                            }
                        }
                    }
                    else
                    {
                        addLine = true;
                    }

                   

                    if (addLine)
                    {                       
                        //BooleanPrinter.GetInstance().PrintLine(externalEvalTimeSlotVector);                       
                        evaluatorTimeSlotMatrix.Add(externalEvalTimeSlotVector);
                    }
                    else
                    {
                        patternCount++;
                    }

                    
                    if (addPattern)
                    {                        
                            categoryLength.Add(patternCount); 
                            patternCount = 1;
                           
                    }


            
            }     

        }






    }
}
