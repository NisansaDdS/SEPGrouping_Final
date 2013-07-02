using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SEPGrouping
{
    class Student
    {
        int number;
        String indexNumber;
        String name;
        List<Int32> internalPossibleVector = new List<int>();       
        List<Int32> technologyVector = new List<int>();
        int midGrade;
        int internalCount = 0;

        
        

       

        public void Print()
        {
            Console.WriteLine("Number "+number);
            Console.WriteLine("Index Number " + indexNumber);
            Console.WriteLine("Name " + name);
            Console.WriteLine("Mid grade " + midGrade);
            Console.WriteLine("Int. Avail. Score " + internalCount);
            string line = "";
            for (int i = 0; i < internalPossibleVector.Count; i++)
            {
                line += internalPossibleVector[i] + " ";
            }
            Console.WriteLine("Internal Possible Vector " + line);
            line = "";
            for (int i = 0; i < technologyVector.Count; i++)
            {
                line += technologyVector[i] + " ";
            }
            Console.WriteLine("Technology Vector " + line);
        }

        public int Number
        {
            get { return number; }
            set { number = value; }
        }

        public String IndexNumber
        {
            get { return indexNumber; }
            set { indexNumber = value; }
        }

        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        public List<Int32> InternalPossibleVector
        {
            get { return internalPossibleVector; }
            set { internalPossibleVector = value; }
        }

        public List<Int32> TechnologyVector
        {
            get { return technologyVector; }
            set { technologyVector = value; }
        }

        public int MidGrade
        {
            get { return midGrade; }
            set { midGrade = value; }
        }

        public int InternalCount
        {
            get { return internalCount; }
            set { internalCount = value; }
        }

        public void InternalCountUp()
        {
            internalCount++;
        }

        public static int CompareStudentsByIntEvalAssignability(Student s1, Student s2)
        {
            if (s1.InternalCount < s2.InternalCount)
                return -1;
            else if (s1.InternalCount == s2.InternalCount)
                return 0;
            else
                return 1;
        }
       
    }
}
