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
            //var roomNumber = db.AdminConfig.First().NumOfRooms;
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

        // GET: /RoomBookings/Create
        public ActionResult Create(int? id)
        {
            //ViewBag.customerID = new SelectList(db.RoomCustomers, "ID", "name");
            ViewBag.customerID = id;
            return View();
        }

        // POST: /RoomBookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="RoomBookingId,checkin,checkout,specialRate,roomRate,roomType,roomNum,numPeople,isDepositPaid,isCheckedIn,customerID")] RoomBooking roombooking)
        {
 
            if (ModelState.IsValid)
            {
                db.RoomBookings.Add(roombooking);
                await db.SaveChangesAsync();
                return RedirectToAction("Details", "ReservationCustomers", new { id = roombooking.customerID });
            }

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
            ViewBag.customerID = new SelectList(db.RoomCustomers, "ID", "name", roombooking.customerID);
            return View(roombooking);
        }

        // POST: /RoomBookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="RoomBookingId,checkin,checkout,specialRate,roomRate,roomType,roomNum,numPeople,isDepositPaid,isCheckedIn,customerID")] RoomBooking roombooking)
        {
            
            if (ModelState.IsValid)
            {
                db.Entry(roombooking).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Details", "ReservationCustomers", new { id = roombooking.customerID });
            }
            ViewBag.customerID = new SelectList(db.RoomCustomers, "ID", "name", roombooking.customerID);
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
