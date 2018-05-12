using La27Barberia.DB.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace La27Barberia.DB
{
    public class BarberContext : DbContext
    {
        public BarberContext()
            : base("name=BarberContext")
        {
            this.Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<Barber> Barbers { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();

            var ticket = modelBuilder.Entity<Ticket>();
            ticket.HasRequired(c => c.Client)
                .WithMany(t => t.Tickets)
                .HasForeignKey(t => t.ClientId);

            ticket.HasRequired(c => c.Barber)
                .WithMany(t => t.Tickets)
                .HasForeignKey(t => t.BarberId);
        }
    }
}
