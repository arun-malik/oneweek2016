using AutoMapper.Attributes;
using oneWeekHackathon.Models;

namespace oneWeekHackathon.ViewModel
{
    [MapsFrom(typeof(offerFeedback))]
    public class OfferFeedbackVideModel
    {
        public int offerFeedbackId { get; set; }

        public int offerId { get; set; }

        public int customerId { get; set; }

        public int review { get; set; }

        public OfferViewModel offer { get; set; }
    }
}