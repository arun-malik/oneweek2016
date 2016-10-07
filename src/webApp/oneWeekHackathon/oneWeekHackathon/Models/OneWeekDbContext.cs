namespace oneWeekHackathon.Models
{
    using System.Data.Entity;

    public partial class OneWeekDbContext : DbContext
    {
        public OneWeekDbContext()
            : base("name=OneWeekDbContext")
        {
        }

        public virtual DbSet<business> businesses { get; set; }
        public virtual DbSet<businessPrefernceMapping> businessPrefernceMappings { get; set; }
        public virtual DbSet<customer> customers { get; set; }
        public virtual DbSet<customerPreferenceMapping> customerPreferenceMappings { get; set; }
        public virtual DbSet<location> locations { get; set; }
        public virtual DbSet<locationHistory> locationHistories { get; set; }
        public virtual DbSet<offerFeedback> offerFeedbacks { get; set; }
        public virtual DbSet<offer> offers { get; set; }
        public virtual DbSet<preference> preferences { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<business>()
                .Property(e => e.businessName)
                .IsUnicode(false);

            modelBuilder.Entity<business>()
                .Property(e => e.businessDesc)
                .IsUnicode(false);

            modelBuilder.Entity<business>()
                .HasMany(e => e.businessPrefernceMappings)
                .WithRequired(e => e.business)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<business>()
                .HasMany(e => e.offers)
                .WithRequired(e => e.business)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<customer>()
                .Property(e => e.userTag)
                .IsUnicode(false);

            modelBuilder.Entity<customer>()
                .Property(e => e.emailId)
                .IsUnicode(false);

            modelBuilder.Entity<customer>()
                .HasMany(e => e.locations)
                .WithRequired(e => e.customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<customer>()
                .HasMany(e => e.customerPreferenceMappings)
                .WithRequired(e => e.customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<customer>()
                .HasMany(e => e.offerFeedbacks)
                .WithRequired(e => e.customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<locationHistory>()
                .Property(e => e.action)
                .IsUnicode(false);

            modelBuilder.Entity<offer>()
                .Property(e => e.offerTag)
                .IsUnicode(false);

            modelBuilder.Entity<offer>()
                .Property(e => e.offerDescription)
                .IsUnicode(false);

            modelBuilder.Entity<offer>()
                .HasMany(e => e.offerFeedbacks)
                .WithRequired(e => e.offer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<preference>()
                .Property(e => e.prefernceName)
                .IsUnicode(false);

            modelBuilder.Entity<preference>()
                .HasMany(e => e.businessPrefernceMappings)
                .WithRequired(e => e.preference)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<preference>()
                .HasMany(e => e.customerPreferenceMappings)
                .WithRequired(e => e.preference)
                .WillCascadeOnDelete(false);
        }
    }
}
