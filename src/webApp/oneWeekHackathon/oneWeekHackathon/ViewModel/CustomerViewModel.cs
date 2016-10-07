using AutoMapper.Attributes;
using oneWeekHackathon.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace oneWeekHackathon.ViewModel
{
    [MapsFrom(typeof(customer))]
    public class CustomerViewModel
    {
        public int customerId { get; set; }
        public string userTag { get; set; }
        public string emailId { get; set; }
        public long? mobileNumber { get; set; }
        public DateTime? modifiedDateTime { get; set; }
        public DateTime? createdDateTime { get; set; }

        [DisplayName("customerPrefernces")]
        public ICollection<CustomerPreferenceViewModel> customerPreferenceMappings { get; set; }

        //public IEnumerable<PreferenceViewModel> preferences
        //{
        //    get
        //    {
        //        if (this.customerPreferenceMappings != null)
        //        {
        //            return this.customerPreferenceMappings.Select(x => x.preference);
        //        }
        //        return null;
        //    }
        //}
        //public List<LocationViewModel> locations { get; set; }
    }
}