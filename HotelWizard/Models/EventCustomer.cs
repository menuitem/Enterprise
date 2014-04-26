using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelWizard.Models
{
    public class EventCustomer
    {
        public int ID { get; set; }
        public String FirstName { get; set; }
        public string SecondName { get; set; }
        public String Email { get; set; }
        public String Phone { get; set; }
        public virtual ICollection<EventBooking> Bookings { get; set; }

        //method to find customers by customer name
        public static List<EventCustomer> findByName(String name)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            string query = "SELECT * FROM EventCustomers WHERE SecondName = @p0";
            System.Data.Entity.Infrastructure.DbRawSqlQuery<EventCustomer> data = db.EventCustomers.SqlQuery(query, name);
            List<EventCustomer> customers = data.ToList();
            return customers;
        }

        //method to find customers by table booking date
        public static List<EventCustomer> findByDate(DateTime BookingDate)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            string query = "SELECT * FROM EventCustomers WHERE ID in (SELECT customerID FROM EventBookings where BookingDate = @p0)";
            System.Data.Entity.Infrastructure.DbRawSqlQuery<EventCustomer> data = db.EventCustomers.SqlQuery(query, BookingDate);
            List<EventCustomer> customers = data.ToList();
            return customers;
        }
    }
}