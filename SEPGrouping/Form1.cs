using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace SEPGrouping
{
    public partial class Form1 : Form
    {
        StreamReader sr = new StreamReader(@"Mark_sheet_allocations.csv");
      List<  String> internalEvaluators=new List<string>();//=new String[]{ "Shehan","P.P Wijegunawardena","N.H.N.D De silva","Sachini","shahani","Thilak","Asanka"};
      Boolean[,] probabilityMatrix = new Boolean[,] {{ true , true, true, true, true, true,true },
                                                     { true , true, true, true, true, true,true },
                                                     { true , true, true, true, true, false,true }};
      int[] humanhrs = new int[] { 6, 6, 6, 6, 6, 4, 6 };
      int[] remainingProjectSlots;
      List<String> technologies = new List<string>();
      Boolean verbose = false;
      List<Student> studentList = new List<Student>();

        public Form1()
        {
            InitializeComponent();

            remainingProjectSlots = new int[humanhrs.Length];
            for (int i = 0; i < humanhrs.Length; i++)
            {
                remainingProjectSlots[i] = humanhrs[i] * 4;
            }


            string strline = "";
            
            int x = 0;
            if (!sr.EndOfStream)
            {
                //read the first line
                strline = sr.ReadLine();
                ReadTitles(strline);

                while (!sr.EndOfStream)
                {
                      x++;
                      strline = sr.ReadLine();
                      ReadStudent(strline);
                }

            }
            sr.Close();


            studentList.Sort(Student.CompareStudentsByIntEvalAssignability);

            for (int i = 0; i < studentList.Count; i++)
            {
                
            }



        }

        private void ReadStudent(string strline)
        {
            String[] _values = strline.Split('|');
            String[] studentEvaluatorData = _values[0].Split(',');
            String[] technologyData = _values[1].Split(',');

            if (studentEvaluatorData[0].Trim().Length > 0)
            {
                Student tempStudent = new Student();
                tempStudent.Number = Int32.Parse(studentEvaluatorData[0].Trim());
                tempStudent.IndexNumber = studentEvaluatorData[1].Trim();
                tempStudent.Name = studentEvaluatorData[2].Trim();
                tempStudent.MidGrade = Int32.Parse(studentEvaluatorData[3].Trim());
                int result;

                for (int i = 4; i < studentEvaluatorData.Length; i++)
                {
                    if (Int32.TryParse(studentEvaluatorData[i], out result))
                    {
                        tempStudent.InternalPossibleVector.Add(result);
                        if (result == 1)
                        {
                            tempStudent.InternalCountUp();
                        }
                    }
                }
                
                for (int i = 0; i < technologyData.Length; i++)
                {
                    if (Int32.TryParse(technologyData[i], out result))
                    {
                        tempStudent.TechnologyVector.Add(result);
                    }
                }

                if (verbose)
                {
                    tempStudent.Print();
                    Console.WriteLine("");
                }

                studentList.Add(tempStudent);
            }
        }

        private void ReadTitles(String strline)
        {
            string[] _values= strline.Split('|');
            String[] tempInternalEvaluators = _values[0].Trim().Split(',');
            String[] tempTechnologies = _values[1].Trim().Split(',');
            for (int i = 4; i < tempInternalEvaluators.Length; i++)
            {
                if (tempInternalEvaluators[i].Trim().Length > 0)
                {
                    internalEvaluators.Add(tempInternalEvaluators[i]);
                    if(verbose){
                        Console.WriteLine(i + " " + tempInternalEvaluators[i]);
                    }
                }
            }
            if (verbose)
            {
                Console.WriteLine("........");
            }
            for (int i = 0; i < tempTechnologies.Length; i++)
            {
                if (tempTechnologies[i].Trim().Length > 0)
                {
                    technologies.Add(tempTechnologies[i]);
                    if (verbose)
                    {

                        Console.WriteLine(i + " " + tempTechnologies[i]);

                    }
                }
            }
        }




    }
}
