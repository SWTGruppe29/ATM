using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Interfaces;

namespace ATM.Classes
{
    public class SeparationCondition : ICondition
    {
        private int verticalSeparationCondition;
        private int horizontalSeparationCondition;

        public SeparationCondition(int v, int h)
        {
            verticalSeparationCondition = v;
            horizontalSeparationCondition = h;
        }

        public int getVerticalSeparationCondition()
        {
            return verticalSeparationCondition;
        }

        public int getHorizontalSeparationCondition()
        {
            return horizontalSeparationCondition;
        }
    }
}
