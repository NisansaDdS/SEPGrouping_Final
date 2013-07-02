using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SEPGrouping
{
    class ExternalEvaluatorMatrixManager
    {
        List<String> technologyVector = new List<string>();       
        List<Int32> frequencyVector = new List<int>();
        List<String> evaluatorVector = new List<string>();       
        List<List<Int32>> matrix = new List<List<Int32>>();
        static ExternalEvaluatorMatrixManager instance = new ExternalEvaluatorMatrixManager();

        public static ExternalEvaluatorMatrixManager getInstance()
        {
            return(instance);
        }

        private ExternalEvaluatorMatrixManager()
        {
        }

        public void setData(List<List<String>> data)
        {
            for (int i = 0; i < data.Count; i++)
            {
                List<String> dataLine = data[i];
                if (i == 0)
                {
                    evaluatorVector.AddRange(dataLine.GetRange(2, dataLine.Count - 3));
                    //for (int j = 0; j <  evaluatorVector.Count; j++)
                    //{
                    //    Console.WriteLine(evaluatorVector[j]);
                    //}
                    //Console.WriteLine("aaa");
                }
                else
                {
                    int evalCount = Int32.Parse(dataLine[dataLine.Count - 1]);
                    if (evalCount > 0)
                    {
                        technologyVector.Add(dataLine[0]);
                        frequencyVector.Add(Int32.Parse(dataLine[1]));
                        List<Int32> matrixRow = new List<int>();
                        for (int j = 2; j < dataLine.Count-1; j++)
                        {
                            matrixRow.Add(Int32.Parse(dataLine[j]));
                        }
                        matrix.Add(matrixRow);
                    }
                }
            }
        }

       


        public int CalculateValueOfChoice(List<String> evaluators, List<String> technologies)
        {
            int value = 0;
           
            for (int i = 0; i < technologies.Count; i++)
            {
                int techIndex = technologyVector.IndexOf(technologies[i]);
                
                if (techIndex >= 0)
                {
                    for (int j = 0; j < evaluators.Count; j++)
                    {
                        int evalIndex = evaluatorVector.IndexOf(evaluators[j]);                        
                        if (evalIndex >= 0)
                        {
                            value += matrix[techIndex][evalIndex];                         
                        }
                    }
                }
            }
            return (value);
        }

        public Boolean IsTechnologyValid(String candidateTech)
        {
            return (technologyVector.Contains(candidateTech));
        }


        public List<String> TechnologyVector
        {
            get { return technologyVector; }
        }

        public void Print()
        {
            Console.WriteLine("Evaluators");
            for (int i = 0; i <  evaluatorVector.Count; i++)
            {
                Console.WriteLine(evaluatorVector[i]);
            }
            Console.WriteLine("Passed tech");
            for (int i = 0; i < technologyVector.Count; i++)
            {
                Console.WriteLine(technologyVector[i]);
            }
            Console.WriteLine("Matrix");
            for (int i = 0; i < matrix.Count; i++)
            {
                List<Int32> martixRow = matrix[i];
                String line = "";
                for (int j = 0; j < martixRow.Count; j++)
                {
                    line += martixRow[j] + " ";
                }
                Console.WriteLine(line);
            }
        }

        public List<String> ExternalEvaluatorVector
        {
            get { return evaluatorVector; }           
        }


    }
}
