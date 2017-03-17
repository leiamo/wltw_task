using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wltw_task
{
    public class RowElement
    {
        // Each row element tuple contains Development Year and Value
        private int devYear;
        private float value;

        public RowElement(int devYear, float value)
        {
            this.devYear = devYear;
            this.value = value;
        }

        public int getDevYear()
        {
            return this.devYear;
        }

        public float getValue()
        {
            return this.value;
        }

        public void setDevYear(int devYear)
        {
            this.devYear = devYear;
        }

        public void setValue(float value)
        {
            this.value = value;
        }
    }
}
