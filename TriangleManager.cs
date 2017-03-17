using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wltw_task
{
    public class TriangleManager
    {
        // Stores a dictionary of all Triangle tables
        // Each entry has a name (key) and the Triangle table (value)
        Dictionary<string, Triangle> triangles;

        // Initialises new manager store of triangles
        public TriangleManager()
        {
            this.triangles = new Dictionary<string, Triangle>();
        }

        // Returns this dictionary of <k,v> triangles
        public Dictionary<string, Triangle> getStore()
        {
            return triangles;
        }

        // Returns total number of Triangle tables in Triangle Manager
        public int getSize()
        {
            return triangles.Count;

        }

        // Creates a new Triangle and adds <k,v> entry to Triangle Manager store
        public void addTriangle(string name)
        {
            Triangle t = new Triangle(name);
            triangles.Add(name, t);
        }

        // Retrieves, or creates and returns a Triangle based on name (key)
        public Triangle getTriangle(string name)
        {
            if (!triangles.ContainsKey(name))
            {
                addTriangle(name);
            }
            return triangles[name];
        }

        // Returns a Triangle table based on index position in the dictionary 
        public Triangle getTriangle(int index)
        {
            return triangles[triangles.ElementAt(index).Key];
        }

        // Static method which gets/adds Triangle table, then gets/adds Row into table, then adds the Row Element
        // This is essentially based on the input data in text file (called by the Factory)
        public static void setValues(string product, int originYear, int devYear, float incrValue)
        {
            Triangle t = Program.mgr.getTriangle(product);
            Row r = t.getRow(originYear);
            r.addElement((devYear-originYear+1), incrValue);
        }

    }
}
