using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyPlanner_JohnathanBeal.Class
{
    public static class NullCheckUtility
    {
        public static bool IsNotNull(object checkThis)
        {
            bool notNull = !object.ReferenceEquals(checkThis, null);
            return notNull;
        }
        

    }
}
