using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SEPGrouping
{
    class PanelSlot
    {
        List<String> internalEvaluators = new List<string>();
        List<String> externaEvaluators = new List<string>();
        int category;       
        int remainigVacancies;
        int slotNumber;
        int templateNumber;

       
        public void Print()
        {
            Print(true);
        }

        public void Print(Boolean length)
        {
            if (length)
            {
                Console.WriteLine("Category " + category);
                Console.WriteLine(".................Internal.................");
                for (int i = 0; i < internalEvaluators.Count; i++)
                {
                    Console.WriteLine(internalEvaluators[i]);
                }
                Console.WriteLine(".................External.................");
                for (int i = 0; i < externaEvaluators.Count; i++)
                {
                    Console.WriteLine(externaEvaluators[i]);
                }
            }
            else
            {
                string line = ToString();
                Console.WriteLine(line);
            }
        }

        public override string ToString()
        {
            string line = category + " | " + slotNumber + " | ";
            line += GetInternalEvaluatorString();
            line += GetExternalEvaluatorString();
            return line;
        }

        public string GetExternalEvaluatorString()
        {
            string line="";
            for (int i = 0; i < externaEvaluators.Count; i++)
            {
                line += externaEvaluators[i] + " . ";
            }
            return line;
        }

        public string GetInternalEvaluatorString()
        {
            string line="";
            for (int i = 0; i < internalEvaluators.Count; i++)
            {
                line += internalEvaluators[i] + " . ";
            }
            return line;
        }

        public String RemoveInternalEvaluator()
        {
            String intEval = internalEvaluators[internalEvaluators.Count - 1];
            internalEvaluators.Remove(intEval);
            return (intEval);
        }

        Random r = new Random();

        public String RemoveExternalEvaluator(Boolean withReplacement)
        {
            String extEval = externaEvaluators[r.Next(0,externaEvaluators.Count)];
            externaEvaluators.Remove(extEval);
            if (withReplacement)
            {                
                remainigVacancies++;
            }
            return (extEval);
        }

        public PanelSlot(int categ, int remainigVac)
        {            
            category = categ;
            remainigVacancies = remainigVac;
        }

        public PanelSlot(List<String> internalEvaluators,int categ, int remainigVac)
            : this(categ, remainigVac)
        {
            this.internalEvaluators.AddRange(internalEvaluators);
        }

        public PanelSlot(PanelSlot cloningParent) : this(cloningParent,cloningParent.category)
        {

        }

        public PanelSlot(PanelSlot cloningParent,int categ)
            : this(cloningParent.internalEvaluators, categ, cloningParent.remainigVacancies)
        {
            for (int i = 0; i < cloningParent.externaEvaluators.Count; i++)
            {
                AddExternalEvaluator(cloningParent.externaEvaluators[i]);
                remainigVacancies++; //to make it neutral again
            }
            
        }

        public void AddInternalEvaluator(string name)
        {
            internalEvaluators.Add(name);
        }

        public void AddExternalEvaluator(string name)
        {
            externaEvaluators.Add(name);
            remainigVacancies--;
        }


        public int RemainigVacancies
        {
            get { return remainigVacancies; }
            set { remainigVacancies = value; }
        }

        public List<String> InternalEvaluators
        {
            get { return internalEvaluators; }           
        }

        public List<String> ExternaEvaluators
        {
            get { return externaEvaluators; }           
        }

        public int SlotNumber
        {
            get { return slotNumber; }
            set { slotNumber = value; }
        }

        public int Category
        {
            get { return category; }           
        }

        public int TemplateNumber
        {
            get { return templateNumber; }
            set { templateNumber = value; }
        }

        public static int CompareSlotbySlotNumber(PanelSlot s1, PanelSlot s2)
        {
            if (s1.SlotNumber  < s2.SlotNumber )
                return -1;
            else if (s1.SlotNumber  == s2.SlotNumber)
                return 0;
            else
                return 1;
        }


    }
}
