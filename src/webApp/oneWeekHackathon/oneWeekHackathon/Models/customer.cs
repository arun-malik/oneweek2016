namespace oneWeekHackathon.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("customer")]
    public partial class customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public customer()
        {
            locations = new HashSet<location>();
            customerPreferenceMappings = new HashSet<customerPreferenceMapping>();
        }

        public int customerId { get; set; }


        [StringLength(50)]
        public string userTag { get; set; }


        [StringLength(100)]
        public string emailId { get; set; }

        [Required]
        public long? mobileNumber { get; set; }

        public DateTime? modifiedDateTime { get; set; }

        public DateTime? createdDateTime { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<location> locations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<customerPreferenceMapping> customerPreferenceMappings { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<offerFeedback> offerFeedbacks { get; set; }

    }
}
