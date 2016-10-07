using oneWeekHackathon.Models;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace oneWeekHackathon.Controllers.Web
{
    public class offerFeedbacksController : Controller
    {
        private OneWeekDbContext db = new OneWeekDbContext();

        // GET: offerFeedbacks
        public async Task<ActionResult> Index()
        {
            var offerFeedbacks = db.offerFeedbacks.Include(o => o.customer).Include(o => o.offer);
            return View(await offerFeedbacks.ToListAsync());
        }

        // GET: offerFeedbacks/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            offerFeedback offerFeedback = await db.offerFeedbacks.FindAsync(id);
            if (offerFeedback == null)
            {
                return HttpNotFound();
            }
            return View(offerFeedback);
        }

        // GET: offerFeedbacks/Create
        public ActionResult Create()
        {
            ViewBag.customerId = new SelectList(db.customers, "customerId", "userTag");
            ViewBag.offerId = new SelectList(db.offers, "offerId", "offerTag");
            return View();
        }


        // GET: offerFeedbacks/Create
        public ActionResult SendOffers()
        {
            ViewBag.offerId = new SelectList(db.offers, "offerId", "offerTag");
            return View();
        }

        // POST: offerFeedbacks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "offerFeedbackId,offerId,customerId,review")] offerFeedback offerFeedback)
        {
            if (ModelState.IsValid)
            {
                db.offerFeedbacks.Add(offerFeedback);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.customerId = new SelectList(db.customers, "customerId", "userTag", offerFeedback.customerId);
            ViewBag.offerId = new SelectList(db.offers, "offerId", "offerTag", offerFeedback.offerId);
            return View(offerFeedback);
        }

        // GET: offerFeedbacks/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            offerFeedback offerFeedback = await db.offerFeedbacks.FindAsync(id);
            if (offerFeedback == null)
            {
                return HttpNotFound();
            }
            ViewBag.customerId = new SelectList(db.customers, "customerId", "userTag", offerFeedback.customerId);
            ViewBag.offerId = new SelectList(db.offers, "offerId", "offerTag", offerFeedback.offerId);
            return View(offerFeedback);
        }

        // POST: offerFeedbacks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "offerFeedbackId,offerId,customerId,review")] offerFeedback offerFeedback)
        {
            if (ModelState.IsValid)
            {
                db.Entry(offerFeedback).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.customerId = new SelectList(db.customers, "customerId", "userTag", offerFeedback.customerId);
            ViewBag.offerId = new SelectList(db.offers, "offerId", "offerTag", offerFeedback.offerId);
            return View(offerFeedback);
        }

        // GET: offerFeedbacks/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            offerFeedback offerFeedback = await db.offerFeedbacks.FindAsync(id);
            if (offerFeedback == null)
            {
                return HttpNotFound();
            }
            return View(offerFeedback);
        }

        // POST: offerFeedbacks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            offerFeedback offerFeedback = await db.offerFeedbacks.FindAsync(id);
            db.offerFeedbacks.Remove(offerFeedback);
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
