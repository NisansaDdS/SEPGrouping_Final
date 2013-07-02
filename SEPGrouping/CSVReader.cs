using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SEPGrouping
{
    class CSVReader
    {
        StreamReader sr;
        List<List<String>> data=new List<List<String>>();

        public CSVReader(String name)
        {
            sr = new StreamReader(name+".csv");
            string strline = "";
                       
            while (!sr.EndOfStream)
            {               
                strline = sr.ReadLine();
                String[] lineData = strline.Split(',');
                List<String> lineElements = new List<string>();
                for (int i = 0; i < lineData.Length; i++)
                {
                    lineElements.Add(lineData[i]);
                }
                data.Add(lineElements);
            }
        }

        public void Print()
        {
            for (int i = 0; i < data.Count; i++)
            {
                List<String> lineElements = data[i];
                for (int j = 0; j < lineElements.Count; j++)
                {
                    Console.Write(lineElements[j]);
                }
                Console.Write("\n");
            }
        }

        public List<List<String>> GetData()
        {
            return (data);
        }


    }
}
