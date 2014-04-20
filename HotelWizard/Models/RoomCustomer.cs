using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelWizard.Models
{
    public class RoomCustomer
    {
        public int ID { get; set; }
        public String name { get; set; }
        public string firstname { get; set; }
        public String email { get; set; }
        public String phone { get; set; }
        public String address { get; set; }
        public String nationality { get; set; }
        public virtual ICollection<RoomBooking> Bookings { get; set; }

        //method to find customers by customer name
        public static List<RoomCustomer> findByName(String name){
            ApplicationDbContext db = new ApplicationDbContext();
            string query = "SELECT * FROM RoomCustomers where name = @p0"; // and ID in (SELECT customerID from RoomBookings rb, RoomCustomers rc WHERE rc.ID = rb.customerID)";
            System.Data.Entity.Infrastructure.DbRawSqlQuery<RoomCustomer> data = db.RoomCustomers.SqlQuery(query, name);
            List<RoomCustomer> customers = data.ToList();
            return customers;
        }
    }
}