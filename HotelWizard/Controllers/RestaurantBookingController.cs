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
        [Authorize(Roles = "Restaurant")]
        public async Task<ActionResult> Index()
        {
            var restaurantbookings = db.RestaurantBookings.Include(r => r.customer);
            return View(await restaurantbookings.ToListAsync());
        }

        // GET: /RestaurantBooking/Details/5
        [Authorize(Roles = "Restaurant")]
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
        [Authorize(Roles = "Restaurant")]
        public ActionResult Create(int? id)
        {
            ViewBag.customerID = id;
            return View();
        }

        // POST: /RestaurantBooking/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Restaurant")]
        public async Task<ActionResult> Create([Bind(Include = "RestaurantBookingId,BookingDate,BookingTime,NumOfPeople,customerID,TableNumber")] RestaurantBooking restaurantbooking)
        {
            if (ModelState.IsValid)
            {
                db.RestaurantBookings.Add(restaurantbooking);
                await db.SaveChangesAsync();
                return RedirectToAction("Details", "RestaurantCustomer", new { id = restaurantbooking.customerID });
            }

            return View(restaurantbooking);
        }

        // GET: /RestaurantBooking/Edit/5
        [Authorize(Roles = "Restaurant")]
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
       
            return View(restaurantbooking);
        }

        // POST: /RestaurantBooking/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Restaurant")]
        public async Task<ActionResult> Edit([Bind(Include = "RestaurantBookingId,BookingDate,BookingTime,NumOfPeople,customerID,TableNumber")] RestaurantBooking restaurantbooking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(restaurantbooking).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Details", "RestaurantCustomer", new { id = restaurantbooking.customerID });
            }
            //ViewBag.customerID = new SelectList(db.RestaurantCustomers, "ID", "FirstName", restaurantbooking.customerID);
            return View(restaurantbooking);
        }

        // GET: /RestaurantBooking/Delete/5
        [Authorize(Roles = "Restaurant")]
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
        [Authorize(Roles = "Restaurant")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            RestaurantBooking restaurantbooking = await db.RestaurantBookings.FindAsync(id);
            //save customer id before deleting the booking
            int customerID = restaurantbooking.customerID;
            db.RestaurantBookings.Remove(restaurantbooking);
            await db.SaveChangesAsync();

            return RedirectToAction("Details", "RestaurantCustomer", new { id = customerID });
            //return RedirectToAction("Index");
        }

        [Authorize(Roles = "Restaurant")]
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // POST: /RestaurantBookings/Availability
        [Authorize(Roles = "Restaurant")]
        public async Task<ActionResult> Availability(DateTime BookingDate)
        {
            //validate query date is not in the past
            if (!Dates.validateDates(BookingDate))
            {
                ViewBag.errorMsg = "Date is before today";
                return View("../RestaurantCustomer/Index");
            }
            //call a method to get a list of available rooms
            SelectList freeTables = RestaurantBooking.getFreeTables(BookingDate);
            if (freeTables == null)
            {
                ViewBag.errorMsg = "No Available Tables. Please try again";
                return View("../RestaurantCustomers/Index");
            }

            //add the select list of available tables and the date to the ViewBag
            ViewBag.BookingDate = BookingDate;
            ViewBag.TableNum = freeTables;
            return View();
        }

        
    }
}
