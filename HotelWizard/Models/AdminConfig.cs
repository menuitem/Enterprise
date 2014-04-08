using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelWizard.Models
{
    public class AdminConfig
    {
        public int AdminConfigId { get; set; }
        public int numRooms { get; set; }
        public int numTables { get; set; }
    }
}