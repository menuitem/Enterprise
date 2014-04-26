using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace HotelWizard.Models
{
    public class EventBooking
    {
        public int EventBookingId { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BookingDate { get; set; }
        public TimeSpan BookingTime { get; set; }
        public string EventType { get; set; }
        public int NumOfPeople { get; set; }
        public int customerID { get; set; }
        public virtual EventCustomer customer { get; set; }

        public static Boolean isFunctionRoomFree(DateTime queryDate)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            //search for available tables for the given date
            string query = "SELECT * FROM EventBookings where BookingDate = @p0";
            System.Data.Entity.Infrastructure.DbRawSqlQuery<EventBooking> data = db.EventBookings.SqlQuery(query, queryDate);
            List<EventBooking> mylist = data.ToList();
            System.Diagnostics.Debug.WriteLine("here....");
            System.Diagnostics.Debug.WriteLine(mylist.Count);
            //if the above list has a value, then the function room is
            //not free for the query date, and we will return false
            if (mylist.Count != 0)
            {
                return false;
            }
            return true;
        }

    }
}