using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelWizard.Models
{
    //this class holds some static methods for date validation, that are used when checking for availability.
    public class Dates
    {
        public static Boolean validateDates(DateTime queryDate)
        {
            //verify the date is not before today's date
            if (DateTime.Compare(queryDate, System.DateTime.Now) < 0 )
            {
                return false;
            }
            return true;
        }

        public static Boolean validateDates(DateTime checkin, DateTime checkout)
        {
            //verify neither checkin or checkout are before today's date
            if ((DateTime.Compare(checkin, System.DateTime.Now) < 0) || (DateTime.Compare(checkout, System.DateTime.Now) < 0))
            {
                return false;
            }

            //verify that checkout is at least 1 day later than checkin
            if ((checkout - checkin).Days < 1)
            {
                return false;
            }
            return true;
        }
    }
}