using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using oneWeekHackathon.Models;

namespace oneWeekHackathon.Controllers.Web
{
    public class businessesController : Controller
    {
        private OneWeekDbContext db = new OneWeekDbContext();

        // GET: businesses
        public async Task<ActionResult> Index()
        {
            return View(await db.businesses.ToListAsync());
        }

        // GET: businesses/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            business business = await db.businesses.FindAsync(id);
            if (business == null)
            {
                return HttpNotFound();
            }
            return View(business);
        }

        // GET: businesses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: businesses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "businessId,businessName,businessDesc,location,modifiedDateTime,createdDateTime")] business business)
        {
            if (ModelState.IsValid)
            {
                db.businesses.Add(business);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(business);
        }

        // GET: businesses/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            business business = await db.businesses.FindAsync(id);
            if (business == null)
            {
                return HttpNotFound();
            }
            return View(business);
        }

        // POST: businesses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "businessId,businessName,businessDesc,location,modifiedDateTime,createdDateTime")] business business)
        {
            if (ModelState.IsValid)
            {
                db.Entry(business).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(business);
        }

        // GET: businesses/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            business business = await db.businesses.FindAsync(id);
            if (business == null)
            {
                return HttpNotFound();
            }
            return View(business);
        }

        // POST: businesses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            business business = await db.businesses.FindAsync(id);
            db.businesses.Remove(business);
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
