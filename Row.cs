using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace wltw_task
{
    public class Row
    {
        // Each row holds the Origin Year and a list of row elements
        private int originYear;
        private List<RowElement> rowElements;

        // New rows are created (in triangle) with a new list to populate 
        public Row(int originYear)
        {
            this.originYear = originYear;
            this.rowElements = new List<RowElement>();
        }

        public int getOriginYear()
        {
            return this.originYear;
        }

        public void setOriginYear(int year)
        {
            originYear = year;
        }

        // Returns the highest development year in the row (last element in sorted list)
        public int getMaxDevYears()
        {
            return rowElements[rowElements.Count - 1].getDevYear();
        }

        // Returns total number of elements in the row
        public int getSize()
        {
            return rowElements.Count;
        }

        // Returns element at a given index in the row
        public RowElement getRowElement(int index)
        {
            return rowElements[index];
        }

        // Finds if a given index in the row exists
        public bool existsRowElement(int index)
        {
            return (rowElements.Count > index);
        }

        // Creates a new Row Element, and adds this to sorted list of row elements (provided it is unique)
        public void addElement(int devYear, float value)
        {
            RowElement elem = new RowElement(devYear, value);
            if (!rowElements.Exists(e => e.getDevYear() == elem.getDevYear()))
            {
                rowElements.Add(elem);
                // After a new element is added, the list is re-sorted in ascending order of Dev Years
                rowElements.Sort((x, y) => x.getDevYear().CompareTo(y.getDevYear()));
            }
        }

        // Converts incremental row data -> cummulative row data
        public void convert()
        {
            // Initialises new list of Row Elements
            List<RowElement> cRowElements = new List<RowElement>();
            float prev = 0;
            float next = 0;
            
            // For each element in row, clone with a new cummulated value (adding the previous value)
            // Add this cloned element to the new sorted "cummulative" list of row elements
            for (int i = 0; i < rowElements.Count; i++)
            {
                next = rowElements[i].getValue() + prev;
                RowElement elem = new RowElement(rowElements[i].getDevYear(), next);
                cRowElements.Add(elem);
                rowElements.Sort((x, y) => x.getDevYear().CompareTo(y.getDevYear()));
                prev = next;
            }
            // Replace old "incremental" list with new "cummulative" list
            rowElements = cRowElements;
        }
    }
}
