using System;

namespace oneWeekHackathon.ViewModel
{
    public class locationCreateUpdateViewModel
    {
        public int locationId { get; set; }
        public int customerId { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public DateTime modifiedDateTime { get; set; }
    }
}