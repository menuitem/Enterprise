using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelWizard.Models
{
    public class RestaurantCustomer
    {
        public int ID { get; set; }
        public String FirstName { get; set; }
        public string SecondName { get; set; }
        public String Email { get; set; }
        public String Phone { get; set; }
        public virtual ICollection<RestaurantBooking> Bookings { get; set; }

        //method to find customers by customer name
        public static List<RestaurantCustomer> findByName(String name)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            string query = "SELECT * FROM RestaurantCustomers WHERE SecondName = @p0";
            System.Data.Entity.Infrastructure.DbRawSqlQuery<RestaurantCustomer> data = db.RestaurantCustomers.SqlQuery(query, name);
            List<RestaurantCustomer> customers = data.ToList();
            return customers;
        }

        //method to find customers by table booking date
        public static List<RestaurantCustomer> findByDate(DateTime BookingDate)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            string query = "SELECT * FROM RestaurantCustomers WHERE ID in (SELECT customerID FROM RestaurantBookings where BookingDate = @p0)";
            System.Data.Entity.Infrastructure.DbRawSqlQuery<RestaurantCustomer> data = db.RestaurantCustomers.SqlQuery(query, BookingDate);
            List<RestaurantCustomer> customers = data.ToList();
            return customers;
        }
    }
}