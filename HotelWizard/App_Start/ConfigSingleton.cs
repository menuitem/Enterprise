﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelWizard.Models
{
    public class ConfigSingleton
    {
        public int numRooms;
        public int numTables;
        private static ConfigSingleton configInstance;
        private ConfigSingleton() 
        {
            //initialize the attributes by retrieveing data from the database
            AdminConfig adminconfig = AdminConfig.getDetails();
            if (adminconfig != null)
            {
                this.numRooms = adminconfig.NumOfRooms;
                this.numTables = adminconfig.NumOfTables;
            }
        }

        public static ConfigSingleton Instance
        {
            get
            {
                if(configInstance==null)
                {
                    configInstance = new ConfigSingleton();
                }
                return configInstance;
            }
        }

        public void setNumRooms (int numRooms){
            this.numRooms = numRooms;
        }

        public void setNumTables(int numTables)
        {
            this.numTables = numTables;
        }

        
    }
}