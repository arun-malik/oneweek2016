namespace oneWeekHackathon.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("locationHistory")]
    public partial class locationHistory
    {
        public int locationHistoryId { get; set; }

        public int customerLocationId { get; set; }

        [Required]
        public DbGeography location { get; set; }

        public DateTime? dateTime { get; set; }

        [StringLength(50)]
        public string action { get; set; }
    }
}
