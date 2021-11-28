using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;

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
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
        
        public static bool IsGoodToteName(string tote)
        {
            Regex regex = new Regex(@"[A-Z]{3}-[0-9]{4}");
            Match match1 = regex.Match(tote);

            if (match1.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
