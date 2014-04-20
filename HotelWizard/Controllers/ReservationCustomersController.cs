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
using System.Web.SessionState;

namespace HotelWizard.Controllers
{
    public class ReservationCustomersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /ReservationCustomers/
        public async Task<ActionResult> Index()
        {
            //return View(await db.RoomCustomers.ToListAsync());
            return View();
        }

        // GET: /ReservationCustomers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomCustomer roomcustomer = await db.RoomCustomers.FindAsync(id);
            if (roomcustomer == null)
            {
                return HttpNotFound();
            }
            foreach (var booking in roomcustomer.Bookings)
            {
                booking.getCosts();
            }
            return View(roomcustomer);
        }

        // GET: /ReservationCustomers/Create
        public ActionResult Create()//DateTime checkin, DateTime checkout, int roomId)
        {
            //Session.Add("checkin", checkin);
            //Session.Add("checkout", checkout);
            //Session.Add("roomNum", roomId);

            System.Diagnostics.Debug.WriteLine("....here....");
            return View();
        }

        // POST: /ReservationCustomers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="ID,name,firstname,email,phone,address,nationality")] RoomCustomer roomcustomer)
        {
            if (ModelState.IsValid)
            {
                db.RoomCustomers.Add(roomcustomer);
                await db.SaveChangesAsync();
                //return RedirectToAction("Details/"+roomcustomer.ID);
                return RedirectToAction("Create/"+roomcustomer.ID, "RoomBookings");
            }

            return View(roomcustomer);
        }

        // GET: /ReservationCustomers/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomCustomer roomcustomer = await db.RoomCustomers.FindAsync(id);
            if (roomcustomer == null)
            {
                return HttpNotFound();
            }
            return View(roomcustomer);
        }

        // POST: /ReservationCustomers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="ID,name,firstname,email,phone,address,nationality")] RoomCustomer roomcustomer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(roomcustomer).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(roomcustomer);
        }

        // GET: /ReservationCustomers/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomCustomer roomcustomer = await db.RoomCustomers.FindAsync(id);
            if (roomcustomer == null)
            {
                return HttpNotFound();
            }
            return View(roomcustomer);
        }

        // POST: /ReservationCustomers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            RoomCustomer roomcustomer = await db.RoomCustomers.FindAsync(id);
            db.RoomCustomers.Remove(roomcustomer);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        //// GET: /ReservationCustomer/Index
        //public async Task<ActionResult> Index()
        //{

        //    return View();
        //}

        // GET: /Customer/Search
        public async Task<ActionResult> SearchByRef()
        {

            return View();
        }

        // GET: /ReservationCustomer/Results
        public async Task<ActionResult> Results(string name)
        {
            if (name == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //call method to search for list of customers by customer name
            List<RoomCustomer> customers = RoomCustomer.findByName(name);

            if (customers == null)
            {
                return HttpNotFound();
            }

            //calculate the bookings costs for each booking
            foreach (var cust in customers)
            {
                foreach (var booking in cust.Bookings)
                {
                    booking.getCosts();
                }
            }
            ViewBag.surname = name;
            return View("Results", customers);
        }

        // GET: /ReservationCustomer/Index
        public async Task<ActionResult> ResultsByRef(string id)
        {
            int roombookingId = Convert.ToInt32(id);
       
            //search for booking and customer by booking id
            RoomBooking roombooking = await db.RoomBookings.FindAsync(roombookingId);
            if (roombooking == null)
            {
                return HttpNotFound();
            }

            //redirect to details page for the customer id
            return RedirectToAction("Details/" + roombooking.customerID);
        }

        public async Task<ActionResult> CreateNew(DateTime checkin, DateTime checkout, int roomId)
        {
            //add selected dates and room number to the session, so they can 
            //be accessed later during the creation of the reservation.
            //This is because creating a reservation also means creating a 
            //customer, and this is done over more than one view/screen.
            //Therefore storing them in the session means they can be accessed by both views.
            Session.Add("checkin", checkin);
            Session.Add("checkout", checkout);
            Session.Add("roomNum", roomId);

            return Redirect("/ReservationCustomers/Create");
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
