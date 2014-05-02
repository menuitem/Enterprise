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
    public class EventBookingController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /EventBooking/Create
        [Authorize(Roles = "EventRoom")]
        public ActionResult Create(int? id)
        {
            //send the customer id to the create form in the ViewBag
            ViewBag.customerID = id;
            return View();
        }

        // POST: /EventBooking/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "EventRoom")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="EventBookingId,BookingDate,BookingTime,EventType,NumOfPeople,customerID")] EventBooking eventbooking)
        {
            if (ModelState.IsValid)
            {
                db.EventBookings.Add(eventbooking);
                await db.SaveChangesAsync();
                //redirect to the customer details page for this booking
                return RedirectToAction("Details", "EventCustomer", new { id = eventbooking.customerID });
            }

            //send the customer id back to the create form in the ViewBag
            ViewBag.customerID = eventbooking.customerID;
            return View(eventbooking);
        }

        // GET: /EventBooking/Edit/5
        [Authorize(Roles = "EventRoom")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventBooking eventbooking = await db.EventBookings.FindAsync(id);
            if (eventbooking == null)
            {
                return HttpNotFound();
            }

            //send the customer id to the edit form in the ViewBag
            ViewBag.customerID = id;
            return View(eventbooking);
        }

        // POST: /EventBooking/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "EventRoom")]
        public async Task<ActionResult> Edit([Bind(Include = "EventBookingId,BookingDate,BookingTime,EventType,NumOfPeople,customerID")] EventBooking eventbooking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eventbooking).State = EntityState.Modified;
                await db.SaveChangesAsync();
                //redirect to the customer details page for this booking
                return RedirectToAction("Details", "EventCustomer", new { id = eventbooking.customerID });
            }

            //send the customer id back to the edit form in the ViewBag
            ViewBag.customerID = eventbooking.customerID;
            return View(eventbooking);
        }

        // GET: /EventBooking/Delete/5
        [Authorize(Roles = "EventRoom")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventBooking eventbooking = await db.EventBookings.FindAsync(id);
            if (eventbooking == null)
            {
                return HttpNotFound();
            }
            return View(eventbooking);
        }

        // POST: /EventBooking/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "EventRoom")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            EventBooking eventbooking = await db.EventBookings.FindAsync(id);
            //save customer id before deleting the booking
            int customerID = eventbooking.customerID;
            db.EventBookings.Remove(eventbooking);
            await db.SaveChangesAsync();
            //redirect to the customer details for this bookings
            return RedirectToAction("Details", "EventCustomer", new { id = customerID });
        }

        [Authorize(Roles = "EventRoom")]
        public async Task<ActionResult> Availability(DateTime BookingDate)
        {
            //validate query date is not in the past
            if (!Dates.validateDates(BookingDate))
            {
                ViewBag.errorMsg = "Date is before today";
                return View("../EventCustomer/Index");
            }
            //call a method to get a list of available rooms
            Boolean freeFunctionRoom = EventBooking.isFunctionRoomFree(BookingDate);
            if (!freeFunctionRoom)
            {
                ViewBag.errorMsg = "Event Room already booked for that date. Please try again";
                return View("../EventCustomer/Index");
            }

            //add the date to the ViewBag
            ViewBag.BookingDate = BookingDate;
            return View();
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
