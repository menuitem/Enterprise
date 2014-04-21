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
    public class RestaurantBookingController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /RestaurantBooking/
        public async Task<ActionResult> Index()
        {
            var restaurantbookings = db.RestaurantBookings.Include(r => r.customer);
            return View(await restaurantbookings.ToListAsync());
        }

        // GET: /RestaurantBooking/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RestaurantBooking restaurantbooking = await db.RestaurantBookings.FindAsync(id);
            if (restaurantbooking == null)
            {
                return HttpNotFound();
            }
            return View(restaurantbooking);
        }

        // GET: /RestaurantBooking/Create
        public ActionResult Create()
        {
            ViewBag.customerID = new SelectList(db.RestaurantCustomers, "ID", "FirstName");
            return View();
        }

        // POST: /RestaurantBooking/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="RestaurantBookingId,BookingDate,BookingTime,NumOfPeople,customerID")] RestaurantBooking restaurantbooking)
        {
            if (ModelState.IsValid)
            {
                db.RestaurantBookings.Add(restaurantbooking);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.customerID = new SelectList(db.RestaurantCustomers, "ID", "FirstName", restaurantbooking.customerID);
            return View(restaurantbooking);
        }

        // GET: /RestaurantBooking/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RestaurantBooking restaurantbooking = await db.RestaurantBookings.FindAsync(id);
            if (restaurantbooking == null)
            {
                return HttpNotFound();
            }
            ViewBag.customerID = new SelectList(db.RestaurantCustomers, "ID", "FirstName", restaurantbooking.customerID);
            return View(restaurantbooking);
        }

        // POST: /RestaurantBooking/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="RestaurantBookingId,BookingDate,BookingTime,NumOfPeople,customerID")] RestaurantBooking restaurantbooking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(restaurantbooking).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.customerID = new SelectList(db.RestaurantCustomers, "ID", "FirstName", restaurantbooking.customerID);
            return View(restaurantbooking);
        }

        // GET: /RestaurantBooking/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RestaurantBooking restaurantbooking = await db.RestaurantBookings.FindAsync(id);
            if (restaurantbooking == null)
            {
                return HttpNotFound();
            }
            return View(restaurantbooking);
        }

        // POST: /RestaurantBooking/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            RestaurantBooking restaurantbooking = await db.RestaurantBookings.FindAsync(id);
            db.RestaurantBookings.Remove(restaurantbooking);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // POST: /RestaurantBookings/Availability
        public async Task<ActionResult> Availability(DateTime BookingDate)
        {
            //validate dates are not in the past, and checkout is not before checkin
            //if (!RestaurantBooking.validateDates(BookingDate)
            //{
            //    ViewBag.errorMsg = "Dates are before today, or checkout is before checkin";
            //    return View("../ReservationCustomers/Index");
            //}
            //call a method to get a list of available rooms
            SelectList freeTables = RestaurantBooking.getFreeTables(BookingDate);
            if (freeTables == null)
            {
                ViewBag.errorMsg = "No Available Tables. Please try again";
                return View("../RestaurantCustomers/Index");
            }

            //add the select list of available tables and the date to the ViewBag
            ViewBag.BookingDate = BookingDate;
            ViewBag.freeTables = freeTables;
            return View();
        }
    }
}
