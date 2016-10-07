using oneWeekHackathon.Models;
using oneWeekHackathon.ViewModel;
using System.Threading.Tasks;
using System.Web.Http;

namespace oneWeekHackathon.Controllers.Api
{
    public class preferencesController : BaseController
    {
        private OneWeekDbContext db = new OneWeekDbContext();

        // GET: api/preferences
        public async Task<IHttpActionResult> Getpreferences(int? page = 1, int pageSize = 10, string orderBy = nameof(preference.preferenceId), bool ascending = true)
        {
            var prefernces = await CreatePagedResults<preference, PreferenceViewModel>
          (db.preferences, page.Value, pageSize, orderBy, ascending);
            return Ok(prefernces);
        }

        //// GET: api/preferences/5
        //[ResponseType(typeof(preference))]
        //public async Task<IHttpActionResult> Getpreference(int id)
        //{
        //    preference preference = await db.preferences.FindAsync(id);
        //    if (preference == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(preference);
        //}

        //// PUT: api/preferences/5
        //[ResponseType(typeof(void))]
        //public async Task<IHttpActionResult> Putpreference(int id, preference preference)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != preference.preferenceId)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(preference).State = EntityState.Modified;

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!preferenceExists(id))
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

        //// POST: api/preferences
        //[ResponseType(typeof(preference))]
        //public async Task<IHttpActionResult> Postpreference(preference preference)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.preferences.Add(preference);

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (preferenceExists(preference.preferenceId))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtRoute("DefaultApi", new { id = preference.preferenceId }, preference);
        //}

        //// DELETE: api/preferences/5
        //[ResponseType(typeof(preference))]
        //public async Task<IHttpActionResult> Deletepreference(int id)
        //{
        //    preference preference = await db.preferences.FindAsync(id);
        //    if (preference == null)
        //    {
        //        return NotFound();
        //    }

        //    db.preferences.Remove(preference);
        //    await db.SaveChangesAsync();

        //    return Ok(preference);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //private bool preferenceExists(int id)
        //{
        //    return db.preferences.Count(e => e.preferenceId == id) > 0;
        //}
    }
}