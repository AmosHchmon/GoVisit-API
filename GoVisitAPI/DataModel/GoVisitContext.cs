using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace GoVisitAPI.DataModel
{
    public class GoVisitContext : DbContext
    {
        public GoVisitContext(DbContextOptions<GoVisitContext> options)
            : base(options)
        {
        }

        // DbSets
        public DbSet<PublicOrganization> PublicOrganizations { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<StaffMember> StaffMembers { get; set; }
        public DbSet<Invitee> Invitees { get; set; }
        public DbSet<Calendar> Calendars { get; set; }
        public DbSet<TimeSlot> TimeSlots { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var fk in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}