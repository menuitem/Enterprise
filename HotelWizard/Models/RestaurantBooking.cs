using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace HotelWizard.Models
{
    public class RestaurantBooking
    {
        public int RestaurantBookingId { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BookingDate { get; set; }
        public TimeSpan BookingTime { get; set; }
        public int TableNumber { get; set; }
        public int NumOfPeople { get; set; }
        public int customerID { get; set; }
        public virtual RestaurantCustomer customer { get; set; }

        public static SelectList getFreeTables(DateTime queryDate)
        {

            ApplicationDbContext db = new ApplicationDbContext();
            //search for available tables for the given date
            string query = "SELECT * FROM RestaurantBookings where BookingDate = @p0";
            System.Data.Entity.Infrastructure.DbRawSqlQuery<RestaurantBooking> data = db.RestaurantBookings.SqlQuery(query, queryDate);

            //create a new list, representing all tables up to the total number of tables.
            //the total number of tables is stored as an attribute in the ConfigSingleton class
            int totalNumberOfRooms = ConfigSingleton.Instance.numRooms;
            List<int> mylist = new List<int>();
            for (int i = 0; i < totalNumberOfRooms; i++)
            {
                mylist.Add(i);
            }
            

            //now remove the table numbers that are already booked
            foreach (RestaurantBooking rb in data.ToList())
            {
                mylist.Remove(rb.TableNumber);
            }

            return new SelectList(mylist);

        }
    }
}