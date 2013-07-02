using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SEPGrouping
{
    class AssignmentAlgoAdapter
    {

        int overAllValue;       
        List<PanelSlot> panelSlots;
        ProjectHandler ph = ProjectHandler.getInstance();

        public void PrintToFile()
        {
            PrintToFile(new int[] { }, true);
        }

        private void PrintToFile(int[] assignments ,Boolean fromDict)
        {                        
            List<String> panelKey = new List<string>();
            List<String> keyLines = new List<string>();
             List<List<String>> data = new List<List<string>>();
             List<String> indexNumbers = ph.IndexNumberVector;
             int cuttentTemplateNumber = 0;
             List<String> indexList = new List<string>();
             Boolean addLine = false;
            for (int i = 0; i < panelSlots.Count; i++)
            {
                if (panelSlots[i].TemplateNumber == cuttentTemplateNumber)
                {
                    int index;
                    if (fromDict)
                    {
                        assignedData.TryGetValue(panelSlots[i], out index);
                    }
                    else
                    {
                        index = assignments[i];
                    }

                    if ((index < 0) || (index >= indexNumbers.Count))
                    {
                        //dataLine.Add("nill");
                    }
                    else
                    {
                        indexList.Add(indexNumbers[index]);
                    }

                }
                else
                {
                    addLine = true;
                }


                if ((i == (panelSlots.Count - 1)) || (addLine))
                {
                    keyLines.Add(""+panelSlots[i - 1].Category);
                    List<String> dataLine = new List<string>();                    
                    dataLine.Add(panelSlots[i - 1].GetInternalEvaluatorString());
                    dataLine.Add(panelSlots[i - 1].GetExternalEvaluatorString());
                    dataLine.AddRange(indexList);
                    indexList = new List<string>();
                    ////////////////////////
                    int index;
                    if (fromDict)
                    {
                        assignedData.TryGetValue(panelSlots[i], out index);
                    }
                    else
                    {
                        index = assignments[i];
                    }

                    if ((index < 0) || (index >= indexNumbers.Count))
                    {
                        //dataLine.Add("nill");
                    }
                    else
                    {
                        indexList.Add(indexNumbers[index]);
                    }
                    ////////////////////////////////////////////

                   // Console.WriteLine(cuttentTemplateNumber + " " + panelSlots[i].TemplateNumber);
                    cuttentTemplateNumber = panelSlots[i].TemplateNumber;
                    data.Add(dataLine);
                    addLine =false;
                }
                //String panelName = "Instance " + i;
                //panelKey.Add(panelName);
                //String line = panelName + " -> " + panelSlots[i].ToString();
                //keyLines.Add(line);

                //List<String> dataLine = new List<string>();
                //int index;
                //if (fromDict)
                //{
                //    assignedData.TryGetValue(panelSlots[i], out index);
                //}
                //else
                //{
                //    index = assignments[i];
                //}

                //if ((index < 0) || (index >= indexNumbers.Count))
                //{
                //    dataLine.Add("nill");
                //}
                //else
                //{
                //    dataLine.Add(indexNumbers[index]);
                //}
                //data.Add(dataLine);
            }
            //Write the key
            FileWriter keyWriter = new FileWriter("key.txt");
            keyWriter.writeLines(keyLines);
            //Write the matrix
            FileWriter valueWriter = new FileWriter("value.csv");
            List<String> title = new List<string>();
            title.Add("Assignment");
            title.Add("Internal");
            title.Add("External");


            valueWriter.writeMatrix(title, keyLines, data);             
        }


        public AssignmentAlgoAdapter()
        {

           
            PanelBuilder pb = new PanelBuilder();
            

            List<List<Int32>> valueMatrix = new List<List<int>>();
            List<String> indexNumbers = ph.IndexNumberVector;
            panelSlots = pb.Panels;
            for (int i = 0; i < indexNumbers.Count; i++)
            {
                List<Int32> valueVector = new List<int>();
                List<Int32> catLength=TimeSlotMatrixManager.getInstance().CategoryLength;
                int typeIncrimentor = catLength[catLength.Count-1];
                int oldType = -1;
                for (int j = 0; j < panelSlots.Count; j++)
                {
                    int value =10* ph.EvaluateProjectAgainstPanel(i, panelSlots[j].InternalEvaluators, panelSlots[j].ExternaEvaluators);
                    if (oldType != panelSlots[j].TemplateNumber)
                    {
                        oldType = panelSlots[j].TemplateNumber;
                        typeIncrimentor = catLength[catLength.Count - 1];
                    }
                    else
                    {
                        typeIncrimentor--;
                    }

                    if (value > 0)
                    {
                        value +=  (4 - panelSlots[j].Category) * typeIncrimentor;
                    }
                    

                    valueVector.Add(value);
                }
                valueMatrix.Add(valueVector);
            }

            /*            
            List<String> panelKey = new List<string>();
            List<String> keyLines = new List<string>();
            for (int i = 0; i < panelSlots.Count; i++)
            {
                String panelName = "Instance " + i;
                panelKey.Add(panelName);
                String line = panelName + " -> " + panelSlots[i].ToString();
                keyLines.Add(line);
            }
            //Write the key
            FileWriter keyWriter = new FileWriter("key.txt");
            keyWriter.writeLines(keyLines);
            //Write the matrix
            FileWriter valueWriter = new FileWriter("value.csv");
            valueWriter.writeMatrix(keyLines, indexNumbers, valueMatrix); 
            */



            int[,] costs = new Int32[valueMatrix[0].Count, valueMatrix[0].Count];
            int maxValue = 0;
            for (int i = 0; i < valueMatrix.Count; i++)
            {
                for (int j = 0; j < valueMatrix[i].Count; j++)
                {
                    maxValue=Math.Max( maxValue,valueMatrix[i][j]);
                }
            }

          

            for (int i = 0; i < valueMatrix.Count; i++) //tasks
            {
                for (int j = 0; j < valueMatrix[i].Count; j++) //Agents
                {
                    costs[j,i] =  maxValue-valueMatrix[i][j];
                }
            }
           
            int[] assignments = HungarianAlgorithm.FindAssignments(costs);

          valePrinter(ph, indexNumbers, panelSlots, assignments,false);
           PrintToFile(assignments,false);
         


            assignedData = new Dictionary<PanelSlot, int>();
            for (int i = 0; i < panelSlots.Count; i++)
            {             
                if (assignments[i] < indexNumbers.Count)
                {              
                    assignedData.Add(panelSlots[i], assignments[i]);
                }
                else
                {              
                    assignedData.Add(panelSlots[i], -1);
                }           
            }        
                       

            List<PanelSlot> sortingList = new List<PanelSlot>();
            sortingList.AddRange(panelSlots);
            sortingList.Sort(PanelSlot.CompareSlotbySlotNumber);
           
            for (int i = 0; i < sortingList.Count; i++)
            {
                int currentAssignment=0;
                assignedData.TryGetValue(sortingList[i], out currentAssignment);
                if (currentAssignment < 0)
                {
                    int j = sortingList.Count - 1;
                    for (; j > i; j--)
                    {
                        int invalidationCandidate = -1;
                        assignedData.TryGetValue(sortingList[j], out invalidationCandidate);
                        if (invalidationCandidate >= 0)
                        {
                            int newValue=ph.EvaluateProjectAgainstPanel(invalidationCandidate, sortingList[i].InternalEvaluators, sortingList[i].ExternaEvaluators);
                            if (newValue > 0)
                            {
                                assignedData.Remove(sortingList[i]);
                                assignedData.Add(sortingList[i], invalidationCandidate);
                                assignedData.Remove(sortingList[j]);
                                assignedData.Add(sortingList[j], -1);
                                break;
                            }
                        }
                    }
                    if (j == i)
                    {
                        break;
                    }
                }
            }


           


             valePrinter(ph, indexNumbers, panelSlots, assignments,true);

           //  Console.WriteLine(overAllValue );

            PrintToFile();
            
        }


     
        Dictionary<PanelSlot, Int32> assignedData;
        //int badCount;


        private void valePrinter(ProjectHandler ph, List<String> indexNumbers, List<PanelSlot> panelSlots, int[] assignments, Boolean useDict)
        {
            //badCount=0;
            List<Int32> nillRuns = new List<int>();
            Boolean inNillRun = false;
            int currentNillRunValue = 0;
            for (int i = 0; i < panelSlots.Count; i++)
            {
                int compater;
                if (useDict)
                {
                    assignedData.TryGetValue(panelSlots[i],out compater);
                    if (compater < 0)
                    {
                        compater = indexNumbers.Count;
                    }
                }
                else{
                    compater = assignments[i];
                }



                if (compater < indexNumbers.Count)
                {
                    
                    int assignmentvalue=AssignmentValue(ph, panelSlots, assignments, i, useDict);
                    if (assignmentvalue==0) //suicide
                    {
                        overAllValue = 0;
                        return;
                        //badCount++;                        
                    }

                    overAllValue += assignmentvalue;
                    if (inNillRun)
                    {
                        inNillRun = false;
                        nillRuns.Add(currentNillRunValue);
                        currentNillRunValue = 0;
                    }
                }
                else
                {
                    if (inNillRun)
                    {
                        currentNillRunValue++;
                    }
                    else
                    {
                        inNillRun = true;
                        currentNillRunValue = 1;
                    }
                }

            }
            //overAllValue -= (10 * badCount * badCount);


            if (currentNillRunValue != 0)
            {
                nillRuns.Add(currentNillRunValue);
            }
            int allNillCount = 0;
            for (int i = 0; i < nillRuns.Count; i++)
            {
                allNillCount+=nillRuns[i];
            }          
        }

        private int AssignmentValue(ProjectHandler ph, List<PanelSlot> panelSlots, int[] assignments, int i, Boolean useDict)
        {

            int index;
            if (useDict)
            {
                assignedData.TryGetValue(panelSlots[i], out index);
                if (index < 0)
                {
                    return (0);
                }
            }
            else
            {
                index = assignments[i];
            }
            return ph.EvaluateProjectAgainstPanel(index, panelSlots[i].InternalEvaluators, panelSlots[i].ExternaEvaluators);
           
        }

        public static int CompareAllocationByOverallValue(AssignmentAlgoAdapter a1, AssignmentAlgoAdapter a2)
        {
            if (a1.OverAllValue > a2.OverAllValue)
                return -1;
            else if (a1.OverAllValue == a2.OverAllValue)
                return 0;
            else
                return 1;
        }


        public int OverAllValue
        {
            get { return overAllValue; }
            set { overAllValue = value; }
        }


    }
}
