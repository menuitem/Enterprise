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
    public class AdminConfigController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /AdminConfig/
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Index()
        {
            return View(await db.AdminConfig.ToListAsync());
        }

        // GET: /AdminConfig/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminConfig adminconfig = await db.AdminConfig.FindAsync(id);
            if (adminconfig == null)
            {
                return HttpNotFound();
            }
            return View(adminconfig);
        }

        // GET: /AdminConfig/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: /AdminConfig/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create([Bind(Include="AdminConfigId,HotelName,HotelAddress,NumOfRooms,NumOfTables")] AdminConfig adminconfig)
        {
            if (ModelState.IsValid)
            {
                db.AdminConfig.Add(adminconfig);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(adminconfig);
        }

        // GET: /AdminConfig/Edit/5
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminConfig adminconfig = await db.AdminConfig.FindAsync(id);
            if (adminconfig == null)
            {
                return HttpNotFound();
            }
            return View(adminconfig);
        }

        // POST: /AdminConfig/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit([Bind(Include="AdminConfigId,HotelName,HotelAddress,NumOfRooms,NumOfTables")] AdminConfig adminconfig)
        {
            if (ModelState.IsValid)
            {
                db.Entry(adminconfig).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(adminconfig);
        }

        // GET: /AdminConfig/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminConfig adminconfig = await db.AdminConfig.FindAsync(id);
            if (adminconfig == null)
            {
                return HttpNotFound();
            }
            return View(adminconfig);
        }

        // POST: /AdminConfig/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            AdminConfig adminconfig = await db.AdminConfig.FindAsync(id);
            db.AdminConfig.Remove(adminconfig);
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
    }
}
