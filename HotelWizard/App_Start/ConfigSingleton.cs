using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelWizard.Models
{
    public class ConfigSingleton
    {
        public int numRooms;
        public int numTables;
        public string hotelName;
        public string hotelAddress;

        private static ConfigSingleton configInstance;
        //constructor is private
        private ConfigSingleton() 
        {
            getDetails();
        }

        public static ConfigSingleton Instance
        {
            get
            {
                //only call the constructor if the instance is null
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

        public void setHotelName(string hotelName)
        {
            this.hotelName = hotelName;
        }

        public void setHotelAddress(string hotelAddress)
        {
            this.hotelAddress = hotelAddress;
        }

        public void getDetails(){
            AdminConfig adminconfig = null;
            //initialize the attributes by retrieveing data from the database
            try
            {
                adminconfig = AdminConfig.getDetails();
            }
            catch (Exception)
            {
                //there has been a problem 
            }
            
            if (adminconfig != null)
            {
                this.numRooms = adminconfig.NumOfRooms;
                this.numTables = adminconfig.NumOfTables;
                this.hotelName = adminconfig.HotelName;
                this.hotelAddress = adminconfig.HotelAddress;
            }
        }        
    }
}