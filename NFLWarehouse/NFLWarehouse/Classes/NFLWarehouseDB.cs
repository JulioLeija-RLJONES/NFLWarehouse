using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFLWarehouse.Classes
{
    class NFLWarehouseDB : SqlHelper 
    {
        public NFLWarehouseDB(): base("nfl")
        {

        }

    }
}
