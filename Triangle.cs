using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wltw_task
{
    public class Triangle
    {
        // Each Triangle table has a name (e.g. "Comp") and a dictionary of <k,v> rows
        // Each entry in the dictionary stores a Row of elements (value) and its Origin Year (key)
        private string name;
        private SortedDictionary<int, Row> rows;

        // Creates a triangle with a new dictionary of rows
        public Triangle(string name)
        {
            this.name = name;
            this.rows = new SortedDictionary<int, Row>();
        }

        public string getName()
        {
            return this.name;
        }

        // Returns total number of rows in the Triangle table
        public int getSize()
        {
            return rows.Count;
        }

        // Creates a new Row entry and stores in the dictionary
        public void addRow(int year)
        {
            Row row = new Row(year);
            rows.Add(year, row);
        }

        // Returns a Row based on Origin Year (key)
        // If the row does not exist, add this row to dictionary
        public Row getRow(int year)
        {
           if (!rows.ContainsKey(year))
           {
                addRow(year);
           }
            return rows[year];
        }

        // Returns a Row based on index position in the rows dictionary
        public Row getRowAtIndex(int index)
        {
            return rows[rows.ElementAt(index).Key];
        }

        // Adds padding to all rows in the Triangle such that it forms an actual "triangle"
        // A major assumption here is that all tables are triangles
        public void addPadding(int startYear, int devYears)
        {
            // For each set row, retrieve/add the row to Triangle
            for (int i = devYears; i > 0; i--)
            {
                Row row = getRow(startYear);
                row.setOriginYear(startYear);

                // For each Dev Year in row, add padding (i.e. 0) when element does not exist
                for (int j = 1; j <= i; j++)
                {
                    if (!row.existsRowElement(j-1))
                    {
                        row.addElement(j, 0);
                    }
                    else if (row.getRowElement(j-1).getDevYear() != j)
                    {
                        row.addElement(j, 0);
                    }
                }
                startYear++;
            }
        }

        // Converts all rows in Triangle from incremental data -> cummulative data
        public void convertTriangle()
        {
            for (int j = 0; j < getSize(); j++)
            {
               getRowAtIndex(j).convert();
            }
        }

        // Returns output string of all values within all rows in the Triangle table 
        public string printTriangle()
        {
            // Gets number of elements in first row to find max number of Development Years
            int devYears = getRowAtIndex(0).getSize();

            // Simple calculation to find total number of elements (e.g. 4+3+2+1)
            int totalSize = (devYears * (devYears + 1)) / 2;

            // Creates array to store all elements
            float[] list = new float[totalSize];
            int listCounter = 0;

            // For each Row in the Triangle
            for (int i = 0; i < rows.Count; i++)
            {
                Row row = getRowAtIndex(i);

                // For each Row Element in Row, add its value to the array
                for (int j = 0; j < row.getSize(); j++)
                {
                    list[listCounter] = row.getRowElement(j).getValue();
                    listCounter++;
                }
            }
            // Joins array into a comma-separated string, leading with the table name
            return (name + ", " + String.Join(", ", list));
        }
    }
}
