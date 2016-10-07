namespace oneWeekHackathon.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("businessPrefernceMapping")]
    public partial class businessPrefernceMapping
    {
        [Key]
        public int businessPrefId { get; set; }

        public int businessId { get; set; }

        public int preferenceId { get; set; }

        public virtual business business { get; set; }

        public virtual preference preference { get; set; }
    }
}
