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
    }
}