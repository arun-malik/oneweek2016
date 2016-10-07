using AutoMapper.Attributes;
using oneWeekHackathon.Models;

namespace oneWeekHackathon.ViewModel
{
    [MapsFrom(typeof(customerPreferenceMapping))]
    public class CustomerPreferenceViewModel
    {
        public int cusotmerPrefernceId { get; set; }

        //public int customerId { get; set; }

        //public int preferenceId { get; set; }

        public PreferenceViewModel preference { get; set; }
    }
}