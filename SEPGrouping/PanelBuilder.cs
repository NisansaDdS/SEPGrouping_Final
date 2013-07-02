using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SEPGrouping
{
    class PanelBuilder
    {
        List<PanelSlot> panelMotiffs = new List<PanelSlot>();
        List<PanelSlot> panels = new List<PanelSlot>();        
        TimeSlotMatrixManager tsmm = TimeSlotMatrixManager.getInstance();
        List<String> externalevaluatorVector = new List<string>();
        Random r = new Random();
        List<PanelSlot> panelGeneration0;
        List<PanelSlot> panelGeneration1;
        List<PanelSlot> panelGeneration2;

        public PanelBuilder()
        {

            CreatePanelMotiff(new string[] { "P.P Wijegunawardena", "Asanka" }, 0, 1);
            CreatePanelMotiff(new string[] { "N.H.N.D de silva", "Thilak" }, 0, 1);
            CreatePanelMotiff(new string[] { "Sachini" }, 0, 2);
            CreatePanelMotiff(new string[] { "Shehan" }, 0, 1);
            CreatePanelMotiff(new string[] { "Shahani" }, 0, 1);

            List<Int32> categoryLength = new List<int>();
            categoryLength.AddRange(tsmm.CategoryLength);            
            externalevaluatorVector.AddRange(ExternalEvaluatorMatrixManager.getInstance().ExternalEvaluatorVector);
            int category = 0;

            panelGeneration0 = new List<PanelSlot>();
            BuildGeneration0(category, panelGeneration0); 
            buildPanelSlots(panelGeneration0, categoryLength[category],true);            
            
            category++;
            
            panelGeneration1 = new List<PanelSlot>();
            BuildGeneration1(category, panelGeneration0, panelGeneration1);
            buildPanelSlots(panelGeneration1, categoryLength[category], false);

            category++;

            panelGeneration2 = new List<PanelSlot>();
            BuildGeneration2(category, panelGeneration1, panelGeneration2);
            buildPanelSlots(panelGeneration2, categoryLength[category], false);  
                     
        }

        private void buildPanelSlots(List<PanelSlot> panelList, int categoryLength,Boolean firstGen)
        {
            for (int i = 0; i < panelList.Count; i++)
            {
                int limit = categoryLength;
                
                if (firstGen)
                {
                    limit -= 2;
                    if (i > 2)
                    {
                        limit++;
                    }
                }

                for (int j = 0; j < limit; j++)
                {
                    PanelSlot panelInstance = new PanelSlot(panelList[i]);
                    panelInstance.SlotNumber = j;
                    panelInstance.TemplateNumber = i;
                    panels.Add(panelInstance);
                }
            }
        }

        private void BuildGeneration2(int category, List<PanelSlot> panelGeneration1, List<PanelSlot> panelGeneration2)
        {
            panelGeneration2.Add(TransferExternalEvaluators(panelGeneration1[5], panelGeneration1[1], category));
            panelGeneration2.Add(TransferExternalEvaluators(panelGeneration1[2], panelGeneration1[4], category));
            panelGeneration2.Add(new PanelSlot(panelGeneration1[3], category));
            panelGeneration2.Add(new PanelSlot(panelGeneration1[0], category));
            panelGeneration2.Add(new PanelSlot(panelGeneration1[6], category));
        }

        private void BuildGeneration1(int category, List<PanelSlot> panelGeneration0, List<PanelSlot> panelGeneration1)
        {
            panelGeneration1.AddRange(CreateSplitPanels(panelGeneration0[0], panelGeneration0[3], category));
            panelGeneration1.AddRange(CreateSplitPanels(panelGeneration0[1], panelGeneration0[4], category));
            panelGeneration1.Add(panelGeneration0[2]);
            for (int i = 0; i < panelGeneration1.Count; i++)
            {
                AddExternalEvaluatorToPanel(category, panelGeneration1[i]);
            }
        }

        private void BuildGeneration0(int category, List<PanelSlot> panelGeneration0)
        {
            for (int i = 0; i < panelMotiffs.Count; i++)
            {
                PanelSlot panel = new PanelSlot(panelMotiffs[i]);
                AddExternalEvaluatorToPanel(category, panel);
                panelGeneration0.Add(panel);
            }
        }
        
        private void AddExternalEvaluatorToPanel(int category, PanelSlot panel)
        {          
            while(panel.RemainigVacancies>0)
            {
                String eval;
                do
                {
                    eval = externalevaluatorVector[r.Next(0, externalevaluatorVector.Count)];
                } while (!tsmm.GetExternalEvaluaterPossibility(category, eval));
                externalevaluatorVector.Remove(eval);
                panel.AddExternalEvaluator(eval);
            }
        }

        private PanelSlot TransferExternalEvaluators(PanelSlot externalEvalDorner, PanelSlot externalEvalreceiver, int category)
        {
            PanelSlot resultantPanel = new PanelSlot(externalEvalreceiver, category);
            List<String> externallEvaluators = externalEvalDorner.ExternaEvaluators;
            for (int i = 0; i < externallEvaluators.Count; i++)
            {
                resultantPanel.AddExternalEvaluator(externallEvaluators[i]);
            }
            return (resultantPanel);
        }
        
        private List<PanelSlot> CreateSplitPanels(PanelSlot internalEvalDorner, PanelSlot externalEvalDorner,int category)
        {
            List<PanelSlot> newPanels = new List<PanelSlot>();
            PanelSlot seniorPanel = new PanelSlot(externalEvalDorner,category);
            PanelSlot juniorPanel = new PanelSlot(internalEvalDorner, category);
            String externalEvaluator = seniorPanel.RemoveExternalEvaluator(true);
            newPanels.Add(seniorPanel);
            String internalEvaluator = juniorPanel.RemoveInternalEvaluator();
            newPanels.Add(juniorPanel);
            PanelSlot splitPanel = new PanelSlot(category, 0);
            splitPanel.AddInternalEvaluator(internalEvaluator);
            splitPanel.AddExternalEvaluator(externalEvaluator);
            newPanels.Add(splitPanel);
            return (newPanels);
        }  


        private void CreatePanelMotiff(string[] names, int categ, int remainigVac)
        {
            PanelSlot panelMotiff = new PanelSlot(categ,remainigVac);
            for (int i = 0; i < names.Length; i++)
            {
                panelMotiff.AddInternalEvaluator(names[i]);
            }
            panelMotiffs.Add(panelMotiff);
        }


        internal List<PanelSlot> Panels
        {
            get { return panels; }          
        }

    }
}
