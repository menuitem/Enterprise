using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotelWizard.Models
{
    public class RoomBooking
    {
        public int RoomBookingId { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime checkin { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime checkout { get; set; }
        public int specialRate { get; set; }
        public int roomRate { get; set; }
        public String roomType { get; set; }
        public int roomNum { get; set; }
        public int numPeople { get; set; }
        public Boolean isDepositPaid { get; set; }
        public Boolean isCheckedIn { get; set; }
        public int customerID { get; set; }
        public virtual RoomCustomer customer { get; set; }

        public RoomCosts costs { get; set; }

        public RoomBooking()
        {
            this.getCosts();
        }
        public void getCosts()
        {
            System.TimeSpan numNights = this.checkout - this.checkin;
            this.costs = new RoomCosts(roomRate, numNights.Days, this.isDepositPaid);
        }
    }
}