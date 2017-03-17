using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wltw_task
{
    public class Factory
    {
        // Splits the line of entry data into an array of fields
        // Verifies and validates this data
        private static string[] verify(string line)
        {
            string[] items = line.Split(',');
            float checkFloat;
            int checkInt;

            // Ignores the first line of headings
            if (items[0].Equals("Product"))
            {
                return null;
            }

            // Ignores the line if there exists incorrect number of parameters
            else if (items.Length != 4)
            {
                return null;
            }

            // Ignores the line if there are any empty data fields
            else if (items[0].Equals(" ") || items[1].Equals(" ") || items[2].Equals(" ") || items[3].Equals(" "))
            {
                return null;
            }

            // Ignores if incorrect data type (not parseable into float)
            else if (!float.TryParse(items[3], out checkFloat))
            {
                return null;
            }

            // Ignores if incorrect data type (not parseable into integer)
            else if (!int.TryParse(items[1], out checkInt) || !int.TryParse(items[2], out checkInt))
            {
                return null;
            }

            // Returns list of verified items
            return items;
        }

        // On input, verify the data and add this new entry to data stores
        public static void input(string line)
        {
            string[] items = verify(line);
            if (items==null)
            {
                return;
            }
            TriangleManager.setValues(items[0], int.Parse(items[1]), int.Parse(items[2]), float.Parse(items[3]));
        }

        // On output, add padding and convert to Cummulative Triangle table
        // Return formatted string of all cumulative data values stored
        public static string output()
        {
            string outputText = "";
            int earliestOriginYear = 10000;
            int originYear;
            int maxDevYears = 0;
            int devYear = 0;

            // For each Triangle table in Triangle Manager
            for (int i = 0; i < Program.mgr.getSize(); i++)
            {
                // Compare to find earliest Origin Year
                originYear = Program.mgr.getTriangle(i).getRowAtIndex(0).getOriginYear();
                if (originYear < earliestOriginYear)
                {
                    earliestOriginYear = originYear;
                }

                // Compare to find largest number of Development Years
                devYear = Program.mgr.getTriangle(i).getRowAtIndex(0).getMaxDevYears();
                if (devYear > maxDevYears)
                {
                    maxDevYears = devYear;
                }
            }

            // Add Origin Year and Dev Year to output text
            outputText += earliestOriginYear + ", " + maxDevYears;

            // For each Triangle table in Triangle Manager
            for (int i = 0; i < Program.mgr.getSize(); i++)
            {
                // Add new line
                outputText += "\r\n";
                
                // Add padding of 0's where there exists empty row elements to form "triangle"
                Program.mgr.getTriangle(i).addPadding(earliestOriginYear, maxDevYears);

                // Convert to cummulative triangle data
                Program.mgr.getTriangle(i).convertTriangle();

                // Add each Triangle table's data to output text
                outputText += (Program.mgr.getTriangle(i).printTriangle());
            }
            return outputText;
        }
    }
}
