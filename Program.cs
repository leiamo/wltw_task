using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace wltw_task
{
    public class Program
    {
        // Global manager to store all triangles
        public static TriangleManager mgr;

        static void Main(string[] args)
        {
            // Location of text files - Change this if necessary
            string readfileName = @"c:input.txt";
            string writefileName = @"c:output.txt";
            
            // Create new manager
            mgr = new TriangleManager();

            // Read text file incremental data input
            using (StreamReader sr = new StreamReader(readfileName))
            {
                string[] item = new string[4];
                string line;
                // For each line, read and store data
                while ((line = sr.ReadLine()) != null)
                {
                    Factory.input(line);
                }
                sr.Close();
            }

            // Write to text file with converted cummulative data
            using (StreamWriter sw = new StreamWriter(writefileName))
            {
                string text = Factory.output();
                sw.Write(text);
                sw.Close();
            }
        }
    }
}