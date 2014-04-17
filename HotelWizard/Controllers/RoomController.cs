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
    public class RoomController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
  
        // GET: /Room/
        public async Task<ActionResult> Index()
        {
            return View(await db.Rooms.ToListAsync());
        }

        // GET: /Room/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = await db.Rooms.FindAsync(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }

        // GET: /Room/Create
        public ActionResult Create()
        {
            //the number of rooms is stored in a singleton class called 'config'
            //this singleton class gets it's attirbutes set when application starts.
            //but a check should be done, and if not set then get details from database

            var numRooms = ConfigSingleton.Instance.numRooms;         
            if (numRooms == null)
            {
                ConfigSingleton config = ConfigSingleton.Instance;
                config.getDetails();              
                numRooms = config.numRooms;
            }

            ViewBag.numRooms = numRooms;
            return View();
        }

        // POST: /Room/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="RoomId,roomNum,roomRate,roomType")] Room room)
        {
            if (ModelState.IsValid)
            {
                //can only create rooms up to the no. of rooms in the hotel
                //the number of rooms is stored in a singleton class called 'config'
                //this singleton class gets it's attirbutes set when application starts.
                //but a check should be done, and if not set then get details from database
                var numRooms = ConfigSingleton.Instance.numRooms;
                if (numRooms == null)
                {
                    ConfigSingleton config = ConfigSingleton.Instance;
                    config.getDetails();
                }
                if (room.roomNum > numRooms)
                {
                    ViewBag.errormsg = "you are trying to add a room number that is too large for your hotel!";
                    return View(room);
                }
                
                db.Rooms.Add(room);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(room);
        }

        // GET: /Room/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = await db.Rooms.FindAsync(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }

        // POST: /Room/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="RoomId,roomNum,roomRate,roomType")] Room room)
        {
            if (ModelState.IsValid)
            {
                db.Entry(room).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(room);
        }

        // GET: /Room/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = await db.Rooms.FindAsync(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }

        // POST: /Room/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Room room = await db.Rooms.FindAsync(id);
            db.Rooms.Remove(room);
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
