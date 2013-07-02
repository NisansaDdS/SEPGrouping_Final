using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SEPGrouping
{
    class Manager
    {
        public Manager()
        {
            CSVReader techReader = new CSVReader("ExternalEvaluatorChoices");
            ExternalEvaluatorMatrixManager eemm = ExternalEvaluatorMatrixManager.getInstance();
            eemm.setData(techReader.GetData());
           // eemm.Print();
          //  Console.WriteLine();
           
            
            //test 1
            //List<String> eval = new List<string>();
            //eval.Add("Danaja Maldeniya");
           // eval.Add("Kathiravelu Pradeeban");
           // List<String> tech = new List<string>();
           // tech.Add("java");
           // tech.Add("HTML/CSS/DOM");
           // Console.WriteLine(eem.CalculateValueOfChoice(eval, tech));  //3

            
            CSVReader markSheetReader = new CSVReader("Mark_sheet_allocations");
            ProjectHandler ph = ProjectHandler.getInstance();
            ph.SetData(markSheetReader.GetData());
            //ph.Print();
           
            //test 2
            //if(eemm.TechnologyVector.Count==ph.TechnologyVector.Count)
            //{
            //    HashSet<String> testerSet=new HashSet<string>();
            //    for (int i = 0; i < eemm.TechnologyVector.Count; i++)
            //    {
            //        testerSet.Add(eemm.TechnologyVector[i]);
            //        testerSet.Add(ph.TechnologyVector[i]);
            //    }
            //    if(testerSet.Count==eemm.TechnologyVector.Count)
            //    {
            //        Console.WriteLine("Elements match");
            //    }
            //    else
            //    {
            //        Console.WriteLine("Elements mismatch");
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("Size mismatch");
            //}         
            
            //Test 3
            //List<String> panel = new List<string>();
            //panel.Add("Shehan");
            //panel.Add("P.P Wijegunawardena");
            //panel.Add("Suhothayan Sriskandarajah");
            //Console.WriteLine("Value " + ph.EvaluateProjectAgainstPanel(2, panel));

            CSVReader timeslotReader = new CSVReader("EvaluatorTimeSlotMatrix_1");
            TimeSlotMatrixManager tsmm = TimeSlotMatrixManager.getInstance();
            tsmm.SetData(timeslotReader.GetData());

            int population = 2000;
            List<AssignmentAlgoAdapter> alocationList = new List<AssignmentAlgoAdapter>();
            for (int i = 0; i < population; i++)
            {
                Console.WriteLine(i + "/" + population);
                AssignmentAlgoAdapter aaa = new AssignmentAlgoAdapter();
                alocationList.Add(aaa);
                //Thread.Sleep(100);
            }


            ///Results anlyser.//////////////////////////////////
            List<List<String>> data = new List<List<string>>();
            List<String> keyLines = new List<string>();
            List<String> line;
            for (int i = 0; i < alocationList.Count; i++)
            {
                line = new List<string>();
                keyLines.Add(i + "");
                line.Add(alocationList[i].OverAllValue+"");
                data.Add(line);
            }
            FileWriter valueWriter = new FileWriter("results1.csv");
            List<String> title = new List<string>();
            title.Add("Index");
            title.Add("Value");  
            valueWriter.writeMatrix(title, keyLines, data);
            ///Results aanlyser.//////////////////////////////////


            alocationList.Sort(AssignmentAlgoAdapter.CompareAllocationByOverallValue);
            for (int i = 0; i < alocationList.Count; i++)
            {
                Console.WriteLine(i + " " + alocationList[i].OverAllValue);
            }
            alocationList[0].PrintToFile();

            ///Results anlyser.//////////////////////////////////
            data = new List<List<string>>();
            keyLines = new List<string>();            
            int key = 550;
            int count = 0;


            for (int i = 0; i < alocationList.Count; i++)
            {
                if (alocationList[i].OverAllValue > key)
                {
                    count++;
                }
                else
                {
                    line = new List<string>();
                    keyLines.Add(key + "");
                    key -= 1;
                    line.Add(count + "");
                    count = 0;
                    data.Add(line);
                }  
            }
            valueWriter = new FileWriter("results2.csv");
            title = new List<string>();
            title.Add("Index");
            title.Add("Value");
            valueWriter.writeMatrix(title, keyLines, data);
            ///Results aanlyser.//////////////////////////////////




            Console.ReadLine();
        }




    }
}
