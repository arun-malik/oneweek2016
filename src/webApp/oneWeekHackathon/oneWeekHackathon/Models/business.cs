namespace oneWeekHackathon.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("business")]
    public partial class business
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public business()
        {
            businessPrefernceMappings = new HashSet<businessPrefernceMapping>();
        }

        public int businessId { get; set; }

        [Required]
        [StringLength(100)]
        public string businessName { get; set; }

        [StringLength(1000)]
        public string businessDesc { get; set; }

        [Required]
        public DbGeography location { get; set; }

        public DateTime? modifiedDateTime { get; set; }

        [Column(TypeName = "date")]
        public DateTime? createdDateTime { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<businessPrefernceMapping> businessPrefernceMappings { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<offer> offers { get; set; }
    }
}
