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

        public static AdminConfig getDetails()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            AdminConfig adminconfig = null;
            //there should only be one row for this table
            try
            {
                adminconfig = db.AdminConfig.First();
                System.Diagnostics.Debug.WriteLine("empty details .........");
                System.Diagnostics.Debug.WriteLine(adminconfig.HotelName);
            }
            catch (Exception)
            {   
                //throw an exception if the tablle has no entry
                throw new System.Exception();
            }
            return adminconfig;
                       
        }

        public static void createInitialEntry(string hotelName, string hotelAddress, int numRooms, int numTables)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            AdminConfig initial = new AdminConfig();
            initial.AdminConfigId = 1;
            initial.HotelName = hotelName;
            initial.HotelAddress = hotelAddress;
            initial.NumOfRooms = numRooms;
            initial.NumOfTables = numTables;
             
            db.AdminConfig.Add(initial);           
            db.SaveChanges();

        }
    }

     
}