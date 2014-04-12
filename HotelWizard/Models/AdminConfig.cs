using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelWizard.Models
{
    public class AdminConfig
    // I will be happy to call this model HotelWizardConfig as it is about Hotel configuartion and not admin configuration LS
    {
        public int AdminConfigId { get; set; }
        public string HotelName { get; set; }
        public string HotelAddress { get; set; }
        public int NumOfRooms { get; set; }
        public int NumOfTables { get; set; }
    }
}