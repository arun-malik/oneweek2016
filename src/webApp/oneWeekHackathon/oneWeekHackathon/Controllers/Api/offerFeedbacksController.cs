using AutoMapper;
using oneWeekHackathon.Models;
using oneWeekHackathon.ViewModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace oneWeekHackathon.Controllers.Api
{
    public class offerFeedbacksController : BaseController
    {
        private OneWeekDbContext db = new OneWeekDbContext();

        [Route("api/customers/{customerId}/offers")]
        // GET: api/offerFeedbacks
        public async Task<IHttpActionResult> GetofferFeedbacks(int customerId, int? page = 1, int pageSize = 10, string orderBy = nameof(offerFeedback.offerFeedbackId), bool ascending = true)
        {
            var offerFeedbacks = await CreatePagedResults<offerFeedback, OfferFeedbackVideModel>
            (db.offerFeedbacks.Where(x => x.customerId == customerId && x.isActive == 0), page.Value, pageSize, orderBy, ascending);

            return Ok(offerFeedbacks);
        }

        [Route("api/customers/{customerId}/offers/{offerFeedbackId}")]
        // GET: api/offerFeedbacks/5
        [ResponseType(typeof(offerFeedback))]
        public IHttpActionResult GetofferFeedback(int customerId, int offerFeedbackId)
        {
            offerFeedback offerFeedback = db.offerFeedbacks.Where(x => x.offerFeedbackId == offerFeedbackId && x.customerId == customerId).FirstOrDefault<offerFeedback>();
            if (offerFeedback == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<offerFeedback, OfferFeedbackVideModel>(offerFeedback));
        }

        [Route("api/customers/{customerId}/offers/{offerId}")]
        // GET: api/customers/{customerId}/offers/{offerId}?review=1
        [ResponseType(typeof(void))]
        [HttpGet]
        public async Task<IHttpActionResult> GetofferFeedback(int customerId, int offerId, int review)
        {
            if (offerId <= 0)
            {
                return BadRequest("offer Id in URL should be greater than 0");
            }

            if (customerId <= 0)
            {
                return BadRequest("customer Id in URL should be greater than 0");
            }

            var offerFeedbacToUpdate = db.offerFeedbacks.Where(x => x.customerId == customerId && x.offerFeedbackId == offerId && x.isActive == 0).First<offerFeedback>();
            offerFeedbacToUpdate.isActive = 1;
            offerFeedbacToUpdate.review = review;

            db.Entry(offerFeedbacToUpdate).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!offerFeedbackExists(offerId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        //// POST: api/offerFeedbacks
        //[ResponseType(typeof(offerFeedback))]
        //public async Task<IHttpActionResult> PostofferFeedback(offerFeedback offerFeedback)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.offerFeedbacks.Add(offerFeedback);
        //    await db.SaveChangesAsync();

        //    return CreatedAtRoute("DefaultApi", new { id = offerFeedback.offerFeedbackId }, offerFeedback);
        //}

        //// DELETE: api/offerFeedbacks/5
        //[ResponseType(typeof(offerFeedback))]
        //public async Task<IHttpActionResult> DeleteofferFeedback(int id)
        //{
        //    offerFeedback offerFeedback = await db.offerFeedbacks.FindAsync(id);
        //    if (offerFeedback == null)
        //    {
        //        return NotFound();
        //    }

        //    db.offerFeedbacks.Remove(offerFeedback);
        //    await db.SaveChangesAsync();

        //    return Ok(offerFeedback);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool offerFeedbackExists(int id)
        {
            return db.offerFeedbacks.Count(e => e.offerFeedbackId == id) > 0;
        }
    }
}