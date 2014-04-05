using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelWizard.Models
{
    public class RoomCosts
    {
        //public int RoomCostsId { get; set; }
        public double totalCost { get; set; }
        public double depositDue { get; set; }
        public double depositPaid { get; set; }
        public double balanceDue { get; set; }

        public RoomCosts()
        {
            this.totalCost = 0;
            this.depositDue = 0;
            this.depositPaid = 0;
            this.balanceDue = 0;
        }
        public RoomCosts(double roomRate, int numNights, Boolean isDepositPaid)
        {
            this.totalCost = roomRate * numNights;
            this.depositDue = totalCost * .3;
            if (isDepositPaid)
            {
                this.depositPaid = this.depositDue;
            };
            this.balanceDue = totalCost - depositPaid;
        }

        public void updateCosts(int depositPaid)
        {
            this.balanceDue = this.totalCost - depositPaid;
        }
    }
}