
using AutoMapper.Attributes;
using oneWeekHackathon.Models;

namespace oneWeekHackathon.ViewModel
{
    [MapsFrom(typeof(offer))]
    public class OfferViewModel
    {
        public string offerTag { get; set; }
        public string offerDescription { get; set; }
    }
}