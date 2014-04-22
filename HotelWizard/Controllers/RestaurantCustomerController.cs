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
    public class RestaurantCustomerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /RestaurantCustomer/
        public async Task<ActionResult> Index()
        {
            return View();
        }

        // GET: /RestaurantCustomer/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RestaurantCustomer restaurantcustomer = await db.RestaurantCustomers.FindAsync(id);
            if (restaurantcustomer == null)
            {
                return HttpNotFound();
            }
            return View(restaurantcustomer);
        }

        public async Task<ActionResult> CreateNew(DateTime BookingDate, int TableNum)
        {
            //add selected dates and room number to the session, so they can 
            //be accessed later during the creation of the reservation.
            //This is because creating a reservation also means creating a 
            //customer, and this is done over more than one view/screen.
            //Therefore storing them in the session means they can be accessed by both views.
            Session.Add("BookingDate", BookingDate);
            Session.Add("TableNum", TableNum);
            
            return Redirect("/RestaurantCustomer/Create");
        }

        // GET: /RestaurantCustomer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /RestaurantCustomer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="ID,FirstName,SecondName,Email,Phone")] RestaurantCustomer restaurantcustomer)
        {
            if (ModelState.IsValid)
            {
                db.RestaurantCustomers.Add(restaurantcustomer);
                await db.SaveChangesAsync();
                //redirect to the create restaurant booking form
                return RedirectToAction("Create/" + restaurantcustomer.ID, "RestaurantBooking");
            }

            return View(restaurantcustomer);
        }

        // GET: /RestaurantCustomer/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RestaurantCustomer restaurantcustomer = await db.RestaurantCustomers.FindAsync(id);
            if (restaurantcustomer == null)
            {
                return HttpNotFound();
            }
            return View(restaurantcustomer);
        }

        // POST: /RestaurantCustomer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="ID,FirstName,SecondName,Email,Phone")] RestaurantCustomer restaurantcustomer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(restaurantcustomer).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Details", "RestaurantCustomer", new { id = restaurantcustomer.ID });
            }
            return View(restaurantcustomer);
        }

        // GET: /RestaurantCustomer/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RestaurantCustomer restaurantcustomer = await db.RestaurantCustomers.FindAsync(id);
            if (restaurantcustomer == null)
            {
                return HttpNotFound();
            }
            return View(restaurantcustomer);
        }

        // POST: /RestaurantCustomer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            RestaurantCustomer restaurantcustomer = await db.RestaurantCustomers.FindAsync(id);
            db.RestaurantCustomers.Remove(restaurantcustomer);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        // GET: /RestaurantCustomer/ResultsByName
        public async Task<ActionResult> ResultsByName(string name)
        {
            if (name == null)
            {
                ViewBag.errorNoName = "Sorry, you didn't enter a name!";
                return View("Index");
            }

            //call method to search for list of customers by customer name
            List<RestaurantCustomer> customers = RestaurantCustomer.findByName(name);

            if (customers.Count == 0)
            {
                ViewBag.errorNoName = "Sorry, no customers with that name!";
                return View("Index");
            }

            ViewBag.SecondName = name;
            return View("Results", customers);
        }

        // GET: /RestaurantCustomer/ResultsByDate
        public async Task<ActionResult> ResultsByDate(DateTime BookingDate)
        {
            //call method to search for list of customers by customer name
            List<RestaurantCustomer> customers = RestaurantCustomer.findByDate(BookingDate);

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
