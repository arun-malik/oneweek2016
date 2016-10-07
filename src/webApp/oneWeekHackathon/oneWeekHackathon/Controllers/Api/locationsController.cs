using AutoMapper;
using oneWeekHackathon.Helpers;
using oneWeekHackathon.Models;
using oneWeekHackathon.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace oneWeekHackathon.Controllers.Api
{
    public class locationsController : BaseController
    {
        OneWeekDbContext db = new OneWeekDbContext();


        // GET: api/customers/{customerId}/locations
        [Route("api/customers/{customerId}/locations")]
        public IHttpActionResult GetCustomersLocation(int customerId)
        {

            var location = db.locations.Where(x => x.customerId == customerId).FirstOrDefault<location>();

            if (location == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<location, LocationViewModel>(location));

        }



        [Route("api/customers/{customerId}/refresh")]
        [HttpPost]
        public IHttpActionResult PostRefreshData(int customerId, locationCreateUpdateViewModel location)
        {
            var point = ControllerUtil.CreatePoint(location.latitude, location.longitude);
            var region = point.Buffer(20);
            var businessLocationList = db.businesses.Where(x => SqlSpatialFunctions.Filter(x.location, region) == true).ToList<business>();

            if (businessLocationList == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<IEnumerable<business>, IEnumerable<BusinessViewModel>>(businessLocationList));

        }

        //// GET: api/locations
        //public IQueryable<location> Getlocations()
        //{

        //    return db.locations;

        //    //var point = DbGeography.FromText(string.Format("POINT ({0} {1})", longitude, latitude), 4326);
        //    //var q1 = from f in context.Facilities
        //    //         let distance = f.Geocode.Distance(jobsite)
        //    //         where distance < 500 * 1609.344
        //    //         orderby distance
        //    //         select f;
        //    //return q1.FirstOrDefault();
        //}

        //// GET: api/locations/5
        //[ResponseType(typeof(location))]
        //public async Task<IHttpActionResult> Getlocation(int id)
        //{
        //    location location = await db.locations.FindAsync(id);
        //    if (location == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(location);
        //}

        // PUT: api/locations/5
        [Route("api/customers/{customerId}/locations/{locationId}")]
        [ResponseType(typeof(void))]
        [HttpPut]
        public async Task<IHttpActionResult> Putlocation(int customerId, int locationId, locationCreateUpdateViewModel location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (locationId != location.locationId || locationId == 0)
            {
                return BadRequest("Location Id cannot be 0");
            }

            if (customerId == 0)
            {
                return BadRequest("Customer Id canot be 0");
            }

            var updatedLocation = db.locations.Find(location.locationId);
            updatedLocation.geoLocation = ControllerUtil.CreatePoint(location.latitude, location.longitude);
            updatedLocation.modifiedDateTime = location.modifiedDateTime;
            db.Entry(updatedLocation).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!locationExists(locationId))
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

        // POST: api/locations
        [Route("api/customers/{customerId}/locations")]
        [ResponseType(typeof(location))]
        public async Task<IHttpActionResult> Postlocation(int customerId, locationCreateUpdateViewModel location)
        {
            location createUpdateLocation = null;

            if (location != null)
            {
                location.modifiedDateTime = location.modifiedDateTime == null ? DateTime.Now : location.modifiedDateTime;
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (customerId == 0 || customerId != location.customerId)
            {
                return BadRequest("Customer Id in URL and postdata is not same or it is 0");
            }

            createUpdateLocation = db.locations.Where(x => x.customerId == customerId).FirstOrDefault<location>();

            if (createUpdateLocation != null)
            {
                createUpdateLocation.geoLocation = ControllerUtil.CreatePoint(location.latitude, location.longitude);
                createUpdateLocation.modifiedDateTime = location.modifiedDateTime;
                db.Entry(createUpdateLocation).State = EntityState.Modified;
            }
            else
            {
                createUpdateLocation = new location();
                createUpdateLocation.geoLocation = ControllerUtil.CreatePoint(location.latitude, location.longitude);
                createUpdateLocation.customerId = location.customerId;
                createUpdateLocation.modifiedDateTime = location.modifiedDateTime;
                db.locations.Add(createUpdateLocation);
            }

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (locationExists(createUpdateLocation.locationId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            location.locationId = createUpdateLocation.locationId;

            return CreatedAtRoute("DefaultApi", new { controller = "locations", id = createUpdateLocation.locationId }, location);
        }

        //// DELETE: api/locations/5
        //[ResponseType(typeof(location))]
        //public async Task<IHttpActionResult> Deletelocation(int id)
        //{
        //    location location = await db.locations.FindAsync(id);
        //    if (location == null)
        //    {
        //        return NotFound();
        //    }

        //    db.locations.Remove(location);
        //    await db.SaveChangesAsync();

        //    return Ok(location);
        //}




        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool locationExists(int id)
        {
            return db.locations.Count(e => e.locationId == id) > 0;
        }
    }
}