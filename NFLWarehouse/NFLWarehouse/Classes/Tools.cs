using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFLWarehouse.Classes
{
    static class Tools
    {
        public static bool isDebugMode()
        {
            if (Debugger.IsAttached)
            {
                // Since there is a debugger attached, assume we are running from the IDE
                return true;
            }
            else
            {
                return false;
                // Assume we aren't running from the IDE
            }
        }
    }
}
