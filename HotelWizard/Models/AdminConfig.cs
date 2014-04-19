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
            //there should only be one row for this table
            try
            {
                AdminConfig adminconfig = db.AdminConfig.Find(1);
                return adminconfig;
            }
            catch (Exception)
            {   
                //throw an exception if the tablle has no entry
                throw;
            }
                       
        }

        public static void createInitialEntry(string hotelName, string hotelAddress, int numRooms, int numTables)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            AdminConfig initial = new AdminConfig();
            initial.HotelName = hotelName;
            initial.HotelAddress = hotelAddress;
            initial.NumOfRooms = numRooms;
            initial.NumOfTables = numTables;
            System.Diagnostics.Debug.WriteLine("here............");
            db.SaveChangesAsync();

        }
    }

     
}