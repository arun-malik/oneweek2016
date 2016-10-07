using AutoMapper;
using oneWeekHackathon.Models;
using oneWeekHackathon.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace oneWeekHackathon.Controllers.Api
{
    public class customerPreferenceMappingsController : BaseController
    {
        private OneWeekDbContext db = new OneWeekDbContext();

        [Route("api/customers/{customerId}/preferences")]
        // GET: api/customerPreferenceMappings
        public async Task<IHttpActionResult> GetcustomerPreferenceMappings(int customerId, int? page = 1, int pageSize = 10, string orderBy = nameof(customerPreferenceMapping.cusotmerPrefernceId), bool ascending = true)
        {
            var customerPreferences = await CreatePagedResults<customerPreferenceMapping, CustomerPreferenceViewModel>
          (db.customerPreferenceMappings.Where(x => x.customerId == customerId), page.Value, pageSize, orderBy, ascending);
            return Ok(customerPreferences);
        }

        //// GET: api/customerPreferenceMappings/5
        //[ResponseType(typeof(customerPreferenceMapping))]
        //public async Task<IHttpActionResult> GetcustomerPreferenceMapping(int id)
        //{
        //    customerPreferenceMapping customerPreferenceMapping = await db.customerPreferenceMappings.FindAsync(id);
        //    if (customerPreferenceMapping == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(customerPreferenceMapping);
        //}

        // PUT: api/customerPreferenceMappings/5
        //[ResponseType(typeof(void))]
        //public async Task<IHttpActionResult> PutcustomerPreferenceMapping(int id, customerPreferenceMapping customerPreferenceMapping)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != customerPreferenceMapping.cusotmerPrefernceId)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(customerPreferenceMapping).State = EntityState.Modified;

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!customerPreferenceMappingExists(id))
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

        // POST: api/customerPreferenceMappings
        // [ResponseType(typeof(customerPreferenceMapping))]
        [Route("api/customers/{customerId}/preferences")]
        [HttpPost]
        public async Task<IHttpActionResult> PostcustomerPreferenceMapping(int customerId, customerPreferenceMapping customerPreferenceMapping)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (customerPreferenceMapping != null && customerPreferenceMapping.customerId == 0)
            {
                customerPreferenceMapping.customerId = customerId;
            }

            if (customerPreferenceMappingExists(customerId, customerPreferenceMapping.preferenceId))
            {
                return BadRequest("Preference Already Exists");
            }


            db.customerPreferenceMappings.Add(customerPreferenceMapping);
            await db.SaveChangesAsync();


            return CreatedAtRoute("DefaultApi", new { controller = "customerPreferences", id = customerPreferenceMapping.cusotmerPrefernceId }, Mapper.Map<customerPreferenceMapping, CustomerPreferenceViewModel>(customerPreferenceMapping));
        }


        [Route("api/customers/{customerId}/allpreferences")]
        [HttpPost]
        public async Task<IHttpActionResult> PostcustomerPreferenceMapping(int customerId, List<customerPreferenceMapping> customerPreferenceMappingList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var prefs = db.customerPreferenceMappings.Where(x => x.customerId == customerId);
            db.customerPreferenceMappings.RemoveRange(prefs);

            await db.SaveChangesAsync();

            if (customerPreferenceMappingList.Count > 0)
            {
                foreach (customerPreferenceMapping row in customerPreferenceMappingList)
                {
                    if (row.customerId == 0)
                    {
                        row.customerId = customerId;
                    }
                }
            }
            db.customerPreferenceMappings.AddRange(customerPreferenceMappingList);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { controller = "customerPreferences" }, Mapper.Map<IEnumerable<customerPreferenceMapping>, IEnumerable<CustomerPreferenceViewModel>>(customerPreferenceMappingList));
        }

        // DELETE: api/customerPreferenceMappings/5  
        [Route("api/customers/{customerId}/preferences/{preferenceId}")]
        public async Task<IHttpActionResult> DeletecustomerPreferenceMapping(int customerId, int preferenceId)
        {
            customerPreferenceMapping customerPreferenceMapping = await db.customerPreferenceMappings.FindAsync(preferenceId);
            if (customerPreferenceMapping == null)
            {
                return NotFound();
            }

            db.customerPreferenceMappings.Remove(customerPreferenceMapping);
            await db.SaveChangesAsync();

            return Ok(Mapper.Map<customerPreferenceMapping, CustomerPreferenceViewModel>(customerPreferenceMapping));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool customerPreferenceMappingExists(int id)
        {
            return db.customerPreferenceMappings.Count(e => e.cusotmerPrefernceId == id) > 0;
        }

        private bool customerPreferenceMappingExists(int customerId, int preferenceId)
        {
            return db.customerPreferenceMappings.Count(e => e.customerId == customerId && e.preferenceId == preferenceId) > 0;
        }
    }
}