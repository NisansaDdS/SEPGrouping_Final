using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SEPGrouping
{
    class ProjectHandler
    {
        List<String> indexNumberVector = new List<string>();        
        List<String> nameVector = new List<string>();
        List<Int32> midMarksVector = new List<Int32>();
        List<String> internalEvaluatorVector = new List<string>();     
        List<Boolean> technologyAvailabilityVector = new List<bool>();
        List<List<Int32>> internalEvaluatorAssignabilityMatrix = new List<List<Int32>>();
        List<String> technologyVector = new List<string>();   
        List<List<Boolean>> projectTechnologyMatrix = new List<List<Boolean>>();
        ExternalEvaluatorMatrixManager eemm = ExternalEvaluatorMatrixManager.getInstance();
        static ProjectHandler instance = new ProjectHandler();

        public int EvaluateProjectAgainstPanel(int projectIndex, List<String> internalEvaluators, List<String> externalEvaluators)
        {
            int value = 1;
            //List<String> externalEvaluators = new List<string>();
            for (int i = 0; i < internalEvaluators.Count; i++)
            {
                int inernalEvalIndex = internalEvaluatorVector.IndexOf(internalEvaluators[i]);
                value *= internalEvaluatorAssignabilityMatrix[projectIndex][inernalEvalIndex];

                //breaking condition
                if (value == 0)
                {
                    return (value);
                }

            }

            //List<String> externalEvaluators = new List<string>();
            //for (int i = 0; i < panel.Count; i++)
            //{
            //    if (internalEvaluatorVector.Contains(panel[i]))
            //    {
            //        int inernalEvalIndex = internalEvaluatorVector.IndexOf(panel[i]);
            //        value *= internalEvaluatorAssignabilityMatrix[projectIndex][inernalEvalIndex];

            //        //breaking condition
            //        if (value == 0)
            //        {
            //            return (value);
            //        }

            //    }
            //    else
            //    {
            //        externalEvaluators.Add(panel[i]);
            //    }

            //}

                int externalValue = 1;
                List<Boolean> allProjectTechnologyList = projectTechnologyMatrix[projectIndex];
                List<String> projectTechnologyList = new List<string>();
                for (int i = 0; i < allProjectTechnologyList.Count; i++)
                {
                    if (allProjectTechnologyList[i])
                    {
                        projectTechnologyList.Add(technologyVector[i]);
                    }
                }
                externalValue += eemm.CalculateValueOfChoice(externalEvaluators, projectTechnologyList);

               

                value *= externalValue;

            
            
            
            return (value);
        }


        public void Print()
        {
            PrintStudentData();
            Console.WriteLine();
            PrintInternalMatrix();
            Console.WriteLine();
            PrintTechMatrix();
        }


        public void PrintStudentData()
        {
            String line = "";

            //Title
            line += "Index\tName\tMid marks";            
            Console.WriteLine(line);

            //Data
            for (int i = 0; i < indexNumberVector.Count; i++)
            {
                line = indexNumberVector[i] + "\t" + nameVector[i] + "\t" + midMarksVector[i];               
                Console.WriteLine(line);
            }
        }
        
        public void PrintInternalMatrix()
        {
            String line = "";

            //Title
            line += "Index ";
            for (int i = 0; i < internalEvaluatorVector.Count; i++)
            {
                line += internalEvaluatorVector[i] + " ";
            }
            Console.WriteLine(line);

            //Data
            for (int i = 0; i < indexNumberVector.Count; i++)
            {
                line = indexNumberVector[i]+"\t\t";

                for (int j = 0; j < internalEvaluatorAssignabilityMatrix[i].Count; j++)
                {
                    line += internalEvaluatorAssignabilityMatrix[i][j]+"\t";
                }
                Console.WriteLine(line);
            }
        }

        public void PrintTechMatrix()
        {
            String line = "";

            //Title
            line += "Index ";
            for (int i = 0; i < technologyVector.Count; i++)
            {
                line += technologyVector[i] + " ";
            }
            Console.WriteLine(line);

            //Data
            for (int i = 0; i < indexNumberVector.Count; i++)
            {
                line = indexNumberVector[i] + " ";

                for (int j = 0; j < projectTechnologyMatrix[i].Count; j++)
                {
                    line += projectTechnologyMatrix[i][j] + " ";
                }
                Console.WriteLine(line);
            }
        }

        private ProjectHandler()
        {

        }

        public static ProjectHandler getInstance()
        {
            return (instance);
        }


        public void SetData(List<List<String>> data)
        {           
            for (int i = 0; i < data.Count; i++)
            {
                List<String> dataLine = data[i];
                if (i == 0)
                {
                    ReadTitles(dataLine);
                }
                else
                {
                    ReadProject(dataLine);
                }

            }
        }
        
        private void ReadTitles(List<String> dataLine)
        {
            int i = 3;
            for (; !dataLine[i].Equals("|"); i++)
            {
                internalEvaluatorVector.Add(dataLine[i]);
            }
            i++;
            int offset = i; 
            for (; i < dataLine.Count; i++)
            {
                Boolean currentTechStatus = eemm.IsTechnologyValid(dataLine[i]);
                technologyAvailabilityVector.Add(currentTechStatus);
                if (currentTechStatus)
                {
                    technologyVector.Add(dataLine[i]);                    
                }
            }            
        }
        
        private void ReadProject(List<String> dataLine)
        {
            indexNumberVector.Add(dataLine[0]);
            nameVector.Add(dataLine[1]);
            midMarksVector.Add(Int32.Parse(dataLine[2]));
            int index = 3;

            List<Int32> internalEvaluatorAssignabilityVector = new List<int>();
            for (; !dataLine[index].Equals("|"); index++)
            {
                internalEvaluatorAssignabilityVector.Add(Int32.Parse(dataLine[index]));
            }
            internalEvaluatorAssignabilityMatrix.Add(internalEvaluatorAssignabilityVector);
            index++;
            
            int offset = index;
            List<Boolean> projectTechnologyVector = new List<Boolean>();
            for (; index < dataLine.Count; index++)
            {
                if (technologyAvailabilityVector[index-offset])
                {
                    if (Int32.Parse(dataLine[index]) == 0)
                    {
                        projectTechnologyVector.Add(false);
                    }
                    else
                    {
                        projectTechnologyVector.Add(true);
                    }                    
                }
            }            
            projectTechnologyMatrix.Add(projectTechnologyVector);
        }




        public List<String> TechnologyVector
        {
            get { return technologyVector; }
        }

        public List<String> InternalEvaluatorVector
        {
            get { return internalEvaluatorVector; }           
        }


        public List<String> IndexNumberVector
        {
            get { return indexNumberVector; }          
        }

    }
}
