using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Triggers
{
    public class Left : Next
    {
        public override Direction TriggerDirection
        {
            get
            {
                return Direction.Left;
            }
        }
    }
}
