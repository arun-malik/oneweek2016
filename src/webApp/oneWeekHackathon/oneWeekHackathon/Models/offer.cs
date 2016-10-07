using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace oneWeekHackathon.Models
{
    public partial class offer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public offer()
        {
            offerFeedbacks = new HashSet<offerFeedback>();
        }

        public int offerId { get; set; }

        public int businessId { get; set; }

        [Required]
        [StringLength(100)]
        public string offerTag { get; set; }

        [Required]
        [StringLength(500)]
        public string offerDescription { get; set; }

        public DateTime? modifiedDateTime { get; set; }

        public virtual business business { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<offerFeedback> offerFeedbacks { get; set; }
    }
}