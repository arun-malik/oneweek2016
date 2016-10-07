using AutoMapper.Attributes;
using oneWeekHackathon.Models;
using System;
using System.Data.Entity.Spatial;

namespace oneWeekHackathon.ViewModel
{
    [MapsFrom(typeof(location))]
    public class LocationViewModel
    {
        public int locationId { get; set; }
        public int customerId { get; set; }
        public DbGeography geoLocation { get; set; }
        public DateTime modifiedDateTime { get; set; }
    }
}