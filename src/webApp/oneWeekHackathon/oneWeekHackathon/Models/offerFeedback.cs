namespace oneWeekHackathon.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("offerFeedback")]
    public partial class offerFeedback
    {
        public int offerFeedbackId { get; set; }

        public int offerId { get; set; }

        public int customerId { get; set; }

        public int review { get; set; }

        public int isActive { get; set; }

        public virtual customer customer { get; set; }

        public virtual offer offer { get; set; }
    }
}