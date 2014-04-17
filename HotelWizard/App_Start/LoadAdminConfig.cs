using HotelWizard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;

namespace HotelWizard.App_Start
{
    public class LoadAdminConfig
    {      
        public static void loadData(){
            //call a method to get the admin configuration details from the database
           
            //create a config singleton instance
           ConfigSingleton config = ConfigSingleton.Instance;
            //assign attributers of the singleton instance from the values
            //that have been retrieved from the database
           
           System.Diagnostics.Debug.WriteLine(config.numRooms);
           System.Diagnostics.Debug.WriteLine(config.numTables);
        }           
    }
}