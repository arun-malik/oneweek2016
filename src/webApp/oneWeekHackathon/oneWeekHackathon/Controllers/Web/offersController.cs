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
    public class offersController : Controller
    {
        private OneWeekDbContext db = new OneWeekDbContext();

        // GET: offers
        public async Task<ActionResult> Index()
        {
            var offers = db.offers.Include(o => o.business);
            return View(await offers.ToListAsync());
        }

        // GET: offers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            offer offer = await db.offers.FindAsync(id);
            if (offer == null)
            {
                return HttpNotFound();
            }
            return View(offer);
        }

        // GET: offers/Create
        public ActionResult Create()
        {
            ViewBag.businessId = new SelectList(db.businesses, "businessId", "businessName");
            return View();
        }

        // POST: offers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "offerId,businessId,offerTag,offerDescription,modifiedDateTime")] offer offer)
        {
            if (ModelState.IsValid)
            {
                db.offers.Add(offer);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.businessId = new SelectList(db.businesses, "businessId", "businessName", offer.businessId);
            return View(offer);
        }

        // GET: offers/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            offer offer = await db.offers.FindAsync(id);
            if (offer == null)
            {
                return HttpNotFound();
            }
            ViewBag.businessId = new SelectList(db.businesses, "businessId", "businessName", offer.businessId);
            return View(offer);
        }

        // POST: offers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "offerId,businessId,offerTag,offerDescription,modifiedDateTime")] offer offer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(offer).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.businessId = new SelectList(db.businesses, "businessId", "businessName", offer.businessId);
            return View(offer);
        }

        // GET: offers/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            offer offer = await db.offers.FindAsync(id);
            if (offer == null)
            {
                return HttpNotFound();
            }
            return View(offer);
        }

        // POST: offers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            offer offer = await db.offers.FindAsync(id);
            db.offers.Remove(offer);
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
