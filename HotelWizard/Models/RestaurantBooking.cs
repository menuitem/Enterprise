using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HotelWizard.Models
{
    public class RestaurantBooking
    {
        public int RestaurantBookingId { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BookingDate { get; set; }
        public TimeSpan BookingTime { get; set; }
        public int NumOfPeople { get; set; }
        public int customerID { get; set; }
        public virtual RestaurantCustomer customer { get; set; }
    }
}