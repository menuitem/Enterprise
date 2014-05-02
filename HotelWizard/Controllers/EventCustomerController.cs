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
    public class EventCustomerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /EventCustomer/
        [Authorize(Roles = "EventRoom")]
        public async Task<ActionResult> Index()
        {
            return View();
        }

        // GET: /EventCustomer/Details/5
        [Authorize(Roles = "EventRoom")]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventCustomer eventcustomer = await db.EventCustomers.FindAsync(id);
            if (eventcustomer == null)
            {
                return HttpNotFound();
            }
            return View(eventcustomer);
        }

        [Authorize(Roles = "EventRoom")]
        public async Task<ActionResult> CreateNew(DateTime BookingDate)
        {
            //add selected dateto the session, so it can 
            //be accessed later during the creation of the booking.
            //This is because creating a booking also means creating a 
            //customer, and this is done over more than one view/screen.
            //Therefore storing it in the session means they can be accessed by both views.
            Session.Add("BookingDate", BookingDate);
            
            return Redirect("/EventCustomer/Create");
        }

        // GET: /EventCustomer/Create
        [Authorize(Roles = "EventRoom")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: /EventCustomer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="ID,FirstName,SecondName,Email,Phone")] EventCustomer eventcustomer)
        {
            if (ModelState.IsValid)
            {
                db.EventCustomers.Add(eventcustomer);
                await db.SaveChangesAsync();
                //redirect to the create event booking form
                return RedirectToAction("Create/" + eventcustomer.ID, "EventBooking");
            }

            return View(eventcustomer);
        }

        // GET: /EventCustomer/Edit/5
        [Authorize(Roles = "EventRoom")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventCustomer eventcustomer = await db.EventCustomers.FindAsync(id);
            if (eventcustomer == null)
            {
                return HttpNotFound();
            }
            return View(eventcustomer);
        }

        // POST: /EventCustomer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "EventRoom")]
        public async Task<ActionResult> Edit([Bind(Include = "ID,FirstName,SecondName,Email,Phone")] EventCustomer eventcustomer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eventcustomer).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Details", "EventCustomer", new { id = eventcustomer.ID });
            }
            return View(eventcustomer);
        }

        // GET: /EventCustomer/Delete/5
        [Authorize(Roles = "EventRoom")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventCustomer eventcustomer = await db.EventCustomers.FindAsync(id);
            if (eventcustomer == null)
            {
                return HttpNotFound();
            }
            return View(eventcustomer);
        }

        // POST: /EventCustomer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "EventRoom")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            EventCustomer eventcustomer = await db.EventCustomers.FindAsync(id);
            db.EventCustomers.Remove(eventcustomer);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: /EventCustomer/ResultsByName
        [Authorize(Roles = "EventRoom")]
        public async Task<ActionResult> ResultsByName(string name)
        {
            if (name == null)
            {
                ViewBag.errorNoName = "Sorry, you didn't enter a name!";
                return View("Index");
            }

            //call method to search for list of customers by customer name
            List<EventCustomer> customers = EventCustomer.findByName(name);

            if (customers.Count == 0)
            {
                ViewBag.errorNoName = "Sorry, no customers with that name!";
                return View("Index");
            }

            ViewBag.SecondName = name;
            return View("Results", customers);
        }

        // GET: /EventCustomer/ResultsByDate
        [Authorize(Roles = "EventRoom")]
        public async Task<ActionResult> ResultsByDate(DateTime BookingDate)
        {
            //call method to search for list of customers by customer name
            List<EventCustomer> customers = EventCustomer.findByDate(BookingDate);

            if (customers.Count == 0)
            {
                ViewBag.errorNoDate = "Sorry, no bookings for that Date!";
                return View("Index");
            }

            ViewBag.BookingDate = BookingDate;
            return View("Results", customers);
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
