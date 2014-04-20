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

            //create a single entry in the database, if it doens't already exist
            //there will only be opportunities for the admin user to modify the enty later
            //but admin user will not have capability of creatin the single entry in database.
            //initialize the attributes by retrieveing data from the database
            try
            {
                AdminConfig adminconfig = AdminConfig.getDetails();
            }
            catch (Exception)
            {

                //no data exists, so create single entry
                AdminConfig.createInitialEntry("sample name", "sample address", 1, 1);
            }
              
            //create a config singleton instance
           ConfigSingleton config = ConfigSingleton.Instance;
                              
        }           
    }
}