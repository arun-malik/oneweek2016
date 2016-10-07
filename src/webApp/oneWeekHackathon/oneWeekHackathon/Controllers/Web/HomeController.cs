using oneWeekHackathon.Helpers;
using oneWeekHackathon.Models;
using System.Dynamic;
using System.Linq;
using System.Web.Mvc;

namespace oneWeekHackathon.Controllers.Web
{
    public class HomeController : Controller
    {

        OneWeekDbContext db = new OneWeekDbContext();
        public ActionResult Index(int businessId = 0, int offerId = 0)
        {
            ViewBag.showData = false;

            var businessList = db.businesses.ToList<business>();
            dynamic homeData = new ExpandoObject();


            if (businessId != 0)
            {
                ViewBag.showData = true;
                homeData.businessSelectList = new SelectList(businessList, "businessId", "businessName", businessId);

                homeData.offerSelectList = new SelectList(db.offers.Where(x => x.businessId == businessId), "offerId", "offerTag", offerId);


                business selectedBusiness = businessList.Where(x => x.businessId == businessId).FirstOrDefault<business>();

                var point = ControllerUtil.CreatePoint((double)selectedBusiness.location.Latitude, (double)selectedBusiness.location.Longitude);
                // var test = db.locations.Select(x => x.geoLocation.Distance(point) <= 500).Count();
                var nearbyUsersCount = db.locations.Where(x => x.geoLocation.Distance(point) <= 500).Count();
                homeData.nearbyUsersCount = nearbyUsersCount;


            }
            else
            {
                if (businessList.Count > 0)
                {
                    homeData.businessSelectList = new SelectList(businessList, "businessId", "businessName");
                }
            }




            return View(homeData);
        }

        public string CreateOffers(string offerName, string businessName)
        {

            try
            {
                if (!string.IsNullOrEmpty(businessName) && !string.IsNullOrEmpty(offerName))
                {
                    var offer = db.offers.Where(x => x.offerTag == offerName).FirstOrDefault<offer>();
                    var selectedBusiness = db.businesses.Where(x => x.businessName == businessName).FirstOrDefault<business>();

                    var point = ControllerUtil.CreatePoint((double)selectedBusiness.location.Latitude, (double)selectedBusiness.location.Longitude);
                    var nearByCustomers = db.locations.Where(x => x.geoLocation.Distance(point) <= 500).ToList<location>();

                    foreach (var cus in nearByCustomers)
                    {
                        offerFeedback createOffer = new offerFeedback() { customerId = cus.customerId, isActive = 0, offerId = offer.offerId, review = 0 };
                        db.offerFeedbacks.Add(createOffer);
                        db.SaveChanges();

                    }
                    db.offerFeedbacks.Create();
                }
                else
                {
                    return "error sending offer";
                }
            }
            catch
            {
                return "failed";

            }
            return "offers sent";
        }
    }
}