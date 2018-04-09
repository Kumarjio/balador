using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
//using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Michal.Balador.Server.Models;
using System.Linq;
using System;
using Michal.Balador.Contracts.Dal;
using System.ComponentModel.Composition;

namespace lior.api.Models
{
    [Export(typeof(IUnitOfWork))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IUnitOfWork
    {
        public ApplicationDbContext()
            : base("MS_SqlStoreConnectionString", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public DbSet<Client> Client { get; set; }
        public DbSet<RefreshToken> RefreshToken { get; set; }
        public DbSet<ClientMessage> ClientMessages { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUser>().ToTable("User");
            modelBuilder.Entity<ApplicationRole>().ToTable("Role");

            modelBuilder.Entity<UserRole>().ToTable("UserRole");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("userclaim");
        }
        public IQueryable<T> Get<T>() where T : class
        {
            return Set<T>();
        }

        public bool Remove<T>(T item) where T : class
        {
            try
            {
                Set<T>().Remove(item);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public async Task Commit()
        {
          await base.SaveChangesAsync();
        }

        public void Attach<T>(T obj) where T : class
        {
            Set<T>().Attach(obj);
        }

        public void Add<T>(T obj) where T : class
        {
            Set<T>().Add(obj);
        }
    }
}
