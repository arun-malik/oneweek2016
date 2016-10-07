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
    public class customersController : BaseController
    {
        //UnitOfWork unitOfWork = new UnitOfWork();
        OneWeekDbContext db = new OneWeekDbContext();

        // GET: api/customers
        public async Task<IHttpActionResult> Getcustomers(int? page = 1, int pageSize = 10, string orderBy = nameof(customer.customerId), bool ascending = true)
        {
            var customer = await CreatePagedResults<customer, CustomerViewModel>
            (db.customers, page.Value, pageSize, orderBy, ascending);
            return Ok(customer);

        }

        // GET: api/customers/5  
        public async Task<IHttpActionResult> Getcustomer(int id)
        {

            var customer = await db.customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<customer, CustomerViewModel>(customer));

        }

        // PUT: api/customers/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putcustomer(int id, customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer.customerId)
            {
                return BadRequest();
            }

            db.Entry(customer).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!customerExists(id))
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

        // POST: api/customers
        [ResponseType(typeof(customer))]
        public async Task<IHttpActionResult> Postcustomer(customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (customer == null)
            {
                return BadRequest("no body found");
            }

            customer customerCreateOrUpdate = null;

            customerCreateOrUpdate = db.customers.Where(x => x.mobileNumber == customer.mobileNumber).FirstOrDefault<customer>();
            if (customerCreateOrUpdate != null)
            {

            }
            else
            {
                customerCreateOrUpdate = customer;
                db.customers.Add(customer);

                try
                {
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    if (customerExists(customer.customerId))
                    {
                        return Conflict();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return CreatedAtRoute("DefaultApi", new { id = customerCreateOrUpdate.customerId }, Mapper.Map<customer, CustomerViewModel>(customerCreateOrUpdate));
        }

        // DELETE: api/customers/5
        [ResponseType(typeof(customer))]
        public async Task<IHttpActionResult> Deletecustomer(int id)
        {
            customer customer = await db.customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            db.customers.Remove(customer);
            await db.SaveChangesAsync();

            return Ok(Mapper.Map<customer, CustomerViewModel>(customer));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool customerExists(int id)
        {
            return db.customers.Count(e => e.customerId == id) > 0;
        }

        private bool customerExists(string email)
        {
            return db.customers.Count(e => e.emailId == email) > 0;
        }

        //[Route("api/customers/{customerId}/preferences")]
        //[HttpGet]
        //public IHttpActionResult GetCusomterPreferences(int customerId)
        //{

        //    var cb = db.customerPreferenceMappings.Where(x => x.customerId == customerId).AsQueryable();
        //    return Ok(Mapper.Map<IQueryable<customerPreferenceMapping>, IEnumerable<CustomerPreferenceViewModel>>(cb));

        //}
    }
}