using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HotelWizard.Models;

namespace HotelWizard.Controllers
{
    public class RoomBookingsController : Controller
    {
              
        private ApplicationDbContext db = new ApplicationDbContext();
         

        //// GET: /RoomBookings/
        //public async Task<ActionResult> Index()
        //{
        //    var roombookings = db.RoomBookings.Include(r => r.customer);
        //    return View(await roombookings.ToListAsync());
        //}

        //// GET: /RoomBookings/Details/5
        [Authorize(Roles = "Reception")]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomBooking roombooking = await db.RoomBookings.FindAsync(id);
            if (roombooking == null)
            {
                return HttpNotFound();
            }
            roombooking.getCosts();
            return View(roombooking);
        }
        //public ActionResult CheckAvailability(int? id){


        //    return RedirectToAction("Details", "ReservationCustomers", new { id = id });
        //}

        // GET: /RoomBookings/Create
        [Authorize(Roles = "Reception")]
        public ActionResult Create(int? id)
        {
            //send the customer id to the create form in the ViewBag
            ViewBag.customerID = id;
     
            return View();
        }

        // POST: /RoomBookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Reception")]
        public async Task<ActionResult> Create([Bind(Include = "RoomBookingId,checkin,checkout,specialRate,RoomId,numPeople,isDepositPaid,isCheckedIn,isCheckedOut,customerID")] RoomBooking roombooking)
        {
            
            if (ModelState.IsValid)
            {
                db.RoomBookings.Add(roombooking);
                await db.SaveChangesAsync();
                //redirect to the customer details for this reservation
                return RedirectToAction("Details", "ReservationCustomers", new { id = roombooking.customerID });
            }


            //string query = "SELECT * FROM Rooms";
            //System.Data.Entity.Infrastructure.DbRawSqlQuery<Room> data = db.Rooms.SqlQuery(query);
            //List<int> mylist = new List<int>();

            //foreach (Room e in data.ToList())
            //{
            //    mylist.Add(e.RoomId);
            //}
  
            //send the customer id back to the create form in the ViewBag
            ViewBag.customerID = roombooking.customerID;           
            return View(roombooking);
        }

        // GET: /RoomBookings/Edit/5
        [Authorize(Roles = "Reception")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomBooking roombooking = await db.RoomBookings.FindAsync(id);
            if (roombooking == null)
            {
                return HttpNotFound();
            }

            return View(roombooking);
        }

        // POST: /RoomBookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Reception")]
        public async Task<ActionResult> Edit([Bind(Include = "RoomBookingId,roomID,checkin,checkout,specialRate,numPeople,isDepositPaid,isCheckedIn,isCheckedOut,customerID")] RoomBooking roombooking)
        {
            
            if (ModelState.IsValid)
            {
                db.Entry(roombooking).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Details", "ReservationCustomers", new { id = roombooking.customerID });
            }
            //send the customer id back to the edit form in the ViewBag
            ViewBag.customerID = roombooking.customerID; 
            return View(roombooking);
        }

        // GET: /RoomBookings/Delete/5
        [Authorize(Roles = "Reception")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomBooking roombooking = await db.RoomBookings.FindAsync(id);
            if (roombooking == null)
            {
                return HttpNotFound();
            }
            return View(roombooking);
        }

        // POST: /RoomBookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Reception")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            RoomBooking roombooking = await db.RoomBookings.FindAsync(id);
            //save customer id before deleting, for redirect
            var customerid = roombooking.customerID;
            db.RoomBookings.Remove(roombooking);
            await db.SaveChangesAsync();
            return RedirectToAction("Details", "ReservationCustomers", new { id = roombooking.customerID });
        }

        // POST: /RoomBookings/Availability
        [Authorize(Roles = "Reception")]
        public async Task<ActionResult> Availability(DateTime checkin, DateTime checkout)
        {
            //validate dates are not in the past, and checkout is not before checkin
            if (!RoomBooking.validateDates(checkin, checkout))
            {
                ViewBag.errorMsg = "Dates are before today, or checkout is before checkin";
                return View("../ReservationCustomers/Index");
            }
            //call a method to get a list of available rooms
            SelectList freeRooms = Room.getFreeRooms(checkin, checkout);
            if (freeRooms == null)
            {
                ViewBag.errorMsg = "No Available Rooms. Please try again";
                return View("../ReservationCustomers/Index");
            }

            //add the select list of available rooms and dates to the ViewBag
            ViewBag.checkin = checkin;
            ViewBag.checkout = checkout;
            ViewBag.roomID = freeRooms;
            return View();
        }

        //Add and extra room booking to existing customer
        [Authorize(Roles = "Reception")]
        public async Task<ActionResult> AddExtraRoom(int? customerID)
        {
            ViewBag.customerID = customerID;
            System.Diagnostics.Debug.WriteLine(customerID);
            return View();
        }

        // POST: /RoomBookings/AvailabilityExtraRoom
        [Authorize(Roles = "Reception")]
        public async Task<ActionResult> AvailabilityExtraRoom(DateTime checkin, DateTime checkout, int customerID)
        {
            //validate dates are not in the past, and checkout is not before checkin
            if (!RoomBooking.validateDates(checkin, checkout))
            {
                ViewBag.errorMsg = "Dates are before today, or checkout is before checkin";
                ViewBag.customerID = customerID;
                return View("AddExtraRoom");
            }

            //call a method to get a list of available rooms
            SelectList freeRooms = Room.getFreeRooms(checkin, checkout);
            if (freeRooms == null)
            {
                ViewBag.errorMsg = "No Available Rooms. Please try again";
                ViewBag.customerID = customerID;
                return View("AddExtraRoom");
            }

            //add the select list of available rooms and dates to the ViewBag
            ViewBag.checkin = checkin;
            ViewBag.checkout = checkout;
            ViewBag.roomID = freeRooms;
            ViewBag.customerID = customerID;

            return View();
        
        }

        [Authorize(Roles = "Reception")]
        public async Task<ActionResult> CreateExtra(DateTime checkin, DateTime checkout, int roomId, int customerID)
        {
            //add selected dates and room number to the session, so they can 
            //be accessed later during the creation of the reservation.
            //This is because creating a reservation also means creating a 
            //customer, and this is done over more than one view/screen.
            //Therefore storing them in the session means they can be accessed by both views.
            Session.Add("checkin", checkin);
            Session.Add("checkout", checkout);
            Session.Add("roomNum", roomId);

            return RedirectToAction("Create", "RoomBookings", new { id = customerID });
            //return RedirectToAction("Create", "RoomBookings", customerID);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
