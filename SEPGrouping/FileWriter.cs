using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SEPGrouping
{
    class FileWriter
    {
        System.IO.StreamWriter file;

        public FileWriter(String name)
        {
            file = new System.IO.StreamWriter(name);
        }

        public void writeLines(List<String> lines)
        {
            for (int i = 0; i < lines.Count; i++)
            {
                file.WriteLine(lines[i]);
            }
            file.Close();
        }

        public void writeMatrix(List<String> horizental, List<String> vertical, List<List<Int32>> matrix)
        {
            List<List<String>> newMatrix = new List<List<string>>();
            for (int i = 0; i < matrix.Count; i++)
            {
                List<String> matrixLine = new List<string>();
                for (int j = 0; j < matrix[i].Count; j++)
                {
                    matrixLine.Add("" + matrix[i][j]);
                }
                newMatrix.Add(matrixLine);
            }
            writeMatrix(horizental, vertical, newMatrix);
        }

        public void writeMatrix(List<String> horizental, List<String> vertical, List<List<String>> matrix)
        {
            List<String> lines = new List<string>();

            //first line
            String line = ","; //there is an empty space at the begining
            for (int i = 0; i <  horizental.Count; i++)
            {
                line += horizental[i] + ",";
            }
            lines.Add(line);

            //rest of the lines
            for (int i = 0; i < vertical.Count; i++)
            {
                line = vertical[i]+",";
                for (int j = 0; j < matrix[i].Count; j++)
                {
                    line += matrix[i][j] + ",";
                }
                lines.Add(line);
            }

            writeLines(lines);
        }

    }
}
