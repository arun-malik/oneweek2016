using AutoMapper.Attributes;
using oneWeekHackathon.Models;
using System;
using System.Data.Entity.Spatial;

namespace oneWeekHackathon.ViewModel
{
    [MapsFrom(typeof(business))]
    public class BusinessViewModel
    {
        public int businessId { get; set; }
        public string businessName { get; set; }

        public string businessDesc { get; set; }

        public DbGeography location { get; set; }

        public DateTime? modifiedDateTime { get; set; }

        public DateTime? createdDateTime { get; set; }



    }
}