using oneWeekHackathon.Models;
using oneWeekHackathon.ViewModel;
using System.Threading.Tasks;
using System.Web.Http;

namespace oneWeekHackathon.Controllers.Api
{
    public class businessesController : BaseController
    {
        OneWeekDbContext db = new OneWeekDbContext();
        // GET: api/businesses
        public async Task<IHttpActionResult> Getbusinesses(int? page = null, int pageSize = 10, string orderBy = nameof(business.businessId), bool ascending = true)
        {
            var business = await CreatePagedResults<business, BusinessViewModel>
           (db.businesses, page.Value, pageSize, orderBy, ascending);

            return Ok(business);
        }

        //// GET: api/businesses/5
        //[ResponseType(typeof(business))]
        //public async Task<IHttpActionResult> Getbusiness(int id)
        //{
        //    business business = await db.businesses.FindAsync(id);
        //    if (business == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(business);
        //}

        //// PUT: api/businesses/5
        //[ResponseType(typeof(void))]
        //public async Task<IHttpActionResult> Putbusiness(int id, business business)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != business.businessId)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(business).State = EntityState.Modified;

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!businessExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //// POST: api/businesses
        //[ResponseType(typeof(business))]
        //public async Task<IHttpActionResult> Postbusiness(business business)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.businesses.Add(business);
        //    await db.SaveChangesAsync();

        //    return CreatedAtRoute("DefaultApi", new { id = business.businessId }, business);
        //}

        //// DELETE: api/businesses/5
        //[ResponseType(typeof(business))]
        //public async Task<IHttpActionResult> Deletebusiness(int id)
        //{
        //    business business = await db.businesses.FindAsync(id);
        //    if (business == null)
        //    {
        //        return NotFound();
        //    }

        //    db.businesses.Remove(business);
        //    await db.SaveChangesAsync();

        //    return Ok(business);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //private bool businessExists(int id)
        //{
        //    return db.businesses.Count(e => e.businessId == id) > 0;
        //}

    }
}
