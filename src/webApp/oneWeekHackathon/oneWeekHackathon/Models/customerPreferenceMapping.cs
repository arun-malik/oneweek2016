namespace oneWeekHackathon.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("customerPreferenceMapping")]
    public partial class customerPreferenceMapping
    {
        [Key]
        public int cusotmerPrefernceId { get; set; }

        public int customerId { get; set; }

        public int preferenceId { get; set; }

        public virtual customer customer { get; set; }

        public virtual preference preference { get; set; }
    }
}
