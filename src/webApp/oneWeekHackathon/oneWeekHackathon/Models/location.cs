namespace oneWeekHackathon.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("location")]
    public partial class location
    {
        public int locationId { get; set; }

        public int customerId { get; set; }

        [Column("location")]
        [Required]
        public DbGeography geoLocation { get; set; }
        public DateTime modifiedDateTime { get; set; }

        public virtual customer customer { get; set; }
    }
}
