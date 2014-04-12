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
    public class ReservationCustomersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /ReservationCustomers/
        public async Task<ActionResult> Index()
        {
            return View(await db.RoomCustomers.ToListAsync());
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
        public ActionResult Create()
        {
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
                return RedirectToAction("Details/"+roomcustomer.ID);
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

        // GET: /Customer/Search
        public async Task<ActionResult> Search()
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
            //Customer customer = await db.Customers.FindAsync(id);
            string query = "SELECT * FROM RoomCustomers WHERE name = @p0";
            System.Data.Entity.Infrastructure.DbRawSqlQuery<RoomCustomer> data = db.RoomCustomers.SqlQuery(query, name);
            List<RoomCustomer> customers = data.ToList();

            if (customers == null)
            {
                return HttpNotFound();
            }

            foreach (var cust in customers)
            {
                foreach (var booking in cust.Bookings)
                {
                    booking.getCosts();
                }
            }

            return View(customers);
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
