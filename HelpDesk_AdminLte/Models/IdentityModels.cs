using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HelpDesk_AdminLte.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
       
        public virtual ICollection<Requests> Requests { get; set; }

       
        


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("HelpDeskConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();


        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

          

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(e => e.Requests)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.CreatedByUserId);

            modelBuilder.Entity<Departments>()
              .HasMany(e => e.Support)
              .WithOptional(e => e.Departments)
              .HasForeignKey(e => e.DepartamentID);

            modelBuilder.Entity<Companies>()
             .HasMany(e => e.Clients)
             .WithOptional(e => e.Companies)
             .HasForeignKey(e => e.CompanyID);

            modelBuilder.Entity<Priority>()
                .Property(e => e.Name)
                .IsFixedLength();

            modelBuilder.Entity<Requests>()
                .Property(e => e.CreatedByUserId)
                .IsFixedLength();
        }
        public virtual DbSet<Clients> Clients { get; set; }
         public virtual DbSet<Conversation> Conversation { get; set; }
        public virtual DbSet<Messages> Messages { get; set; }
        public virtual DbSet<Companies> Companies { get; set; }
        public virtual DbSet<Departments> Departments { get; set; }
        public virtual DbSet<Priority> Priority { get; set; }
        public virtual DbSet<Requests> Requests { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<Support> Support { get; set; }
        public virtual DbSet<UserInterface> UserInterface { get; set; }
    }
}