using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public int numPeople { get; set; }
        public Boolean isDepositPaid { get; set; }
        public Boolean isCheckedIn { get; set; }
        public Boolean isCheckedOut { get; set; }
        public int customerID { get; set; }
        public virtual RoomCustomer customer { get; set; }
        public int roomID { get; set; }
        
        public virtual Room room { get; set; }

        public RoomCosts costs { get; set; }

        public RoomBooking()
        {
            this.getCosts();
        }
        public void getCosts()
        {
            int rate = 0;
            if (room != null)
            {
                rate = room.roomRate;
            }
            if (specialRate != 0)
            {
                rate = specialRate;
            }
            
            System.TimeSpan numNights = this.checkout - this.checkin;
            this.costs = new RoomCosts(rate, numNights.Days, this.isDepositPaid);
        }

        
    }
}