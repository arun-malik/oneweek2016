using AutoMapper.Attributes;
using oneWeekHackathon.Models;

namespace oneWeekHackathon.ViewModel
{
    [MapsFrom(typeof(preference))]
    public class PreferenceViewModel
    {
        public int preferenceId { get; set; }
        public string prefernceName { get; set; }
    }
}