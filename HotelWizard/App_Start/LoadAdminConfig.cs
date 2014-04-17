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
                       
            //create a config singleton instance
           ConfigSingleton config = ConfigSingleton.Instance;
                  
           System.Diagnostics.Debug.WriteLine(config.numRooms);
           System.Diagnostics.Debug.WriteLine(config.numTables);
        }           
    }
}