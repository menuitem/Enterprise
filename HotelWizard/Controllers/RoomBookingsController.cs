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
         

        // GET: /RoomBookings/
        public async Task<ActionResult> Index()
        {
            var roombookings = db.RoomBookings.Include(r => r.customer);
            return View(await roombookings.ToListAsync());
        }

        // GET: /RoomBookings/Details/5
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

        public ActionResult CheckAvailability(int? id){


            return RedirectToAction("Details", "ReservationCustomers", new { id = id });
        }

        // GET: /RoomBookings/Create
        public ActionResult Create(int? id)
        {
            //string query = "SELECT * FROM Rooms";
            //System.Data.Entity.Infrastructure.DbRawSqlQuery<Room> data = db.Rooms.SqlQuery(query);
            //List<int> mylist = new List<int>();

            //foreach (Room e in data.ToList())
            //{
            //    mylist.Add(e.RoomId);
            //}     

            //DateTime start = new DateTime(2014, 04, 19);
            //DateTime end = new DateTime(2014, 04, 21);
            //SelectList test = Room.getFreeRooms(start, end);
            //System.Diagnostics.Debug.WriteLine(test.DataTextField);


            //ViewBag.roomID = Room.getFreeRooms();
            //ViewBag.roomID = Room.getEmptyRooms();
            ViewBag.customerID = id;
            //System.Diagnostics.Debug.WriteLine("here...................");
    
            return View();
        }

        // POST: /RoomBookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="RoomBookingId,checkin,checkout,specialRate,RoomId,numPeople,isDepositPaid,isCheckedIn,customerID")] RoomBooking roombooking)
        {
            
            if (ModelState.IsValid)
            {
                db.RoomBookings.Add(roombooking);
                await db.SaveChangesAsync();
                return RedirectToAction("Details", "ReservationCustomers", new { id = roombooking.customerID });
            }


            string query = "SELECT * FROM Rooms";
            System.Data.Entity.Infrastructure.DbRawSqlQuery<Room> data = db.Rooms.SqlQuery(query);
            List<int> mylist = new List<int>();

            foreach (Room e in data.ToList())
            {
                mylist.Add(e.RoomId);
            }

            ViewBag.roomID = new SelectList(mylist);
            
            ViewBag.customerID = roombooking.customerID;
            
            return View(roombooking);
        }

        // GET: /RoomBookings/Edit/5
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

            //ViewBag.roomID = Room.getAllRooms();
            //ViewBag.customerID = new SelectList(db.RoomCustomers, "ID", "name", roombooking.customerID);
            return View(roombooking);
        }

        // POST: /RoomBookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "RoomBookingId,checkin,checkout,specialRate,numPeople,isDepositPaid,isCheckedIn,customerID")] RoomBooking roombooking)
        {
            
            if (ModelState.IsValid)
            {
                db.Entry(roombooking).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Details", "ReservationCustomers", new { id = roombooking.customerID });
            }
            //ViewBag.customerID = new SelectList(db.RoomCustomers, "ID", "name", roombooking.customerID);
            return View(roombooking);
        }

        // GET: /RoomBookings/Delete/5
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
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            RoomBooking roombooking = await db.RoomBookings.FindAsync(id);
            //save customer id before deleting, for redirect
            var customerid = roombooking.customerID;
            db.RoomBookings.Remove(roombooking);
            await db.SaveChangesAsync();
            return RedirectToAction("Details", "ReservationCustomers", new { id = roombooking.customerID });
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
