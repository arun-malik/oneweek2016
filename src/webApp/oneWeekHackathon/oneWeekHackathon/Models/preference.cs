namespace oneWeekHackathon.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("preference")]
    public partial class preference
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public preference()
        {
            businessPrefernceMappings = new HashSet<businessPrefernceMapping>();
            customerPreferenceMappings = new HashSet<customerPreferenceMapping>();
        }

        public int preferenceId { get; set; }

        [Required]
        [StringLength(100)]
        public string prefernceName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<businessPrefernceMapping> businessPrefernceMappings { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<customerPreferenceMapping> customerPreferenceMappings { get; set; }
    }
}
