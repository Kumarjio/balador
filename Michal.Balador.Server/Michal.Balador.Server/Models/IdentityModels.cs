//using System.Security.Claims;
//using System.Threading.Tasks;
//using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.EntityFramework;
//using Microsoft.AspNet.Identity.Owin;
//using System.Data.Entity;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using Michal.Balador.Server.Models;
//namespace lior.api.Models
//{
//    public class UserRole : IdentityUserRole<string>
//    {
//        [Key]
//        [Column(Order = 1)]
//        public override string RoleId
//        {
//            get
//            {
//                return base.RoleId;
//            }

//            set
//            {
//                base.RoleId = value;
//            }
//        }
//        [Key]
//        [Column(Order = 2)]
//        public override string UserId
//        {
//            get
//            {
//                return base.UserId;
//            }

//            set
//            {
//                base.UserId = value;
//            }
//        }
//        ////
//        //// Summary:
//        ////     RoleId for the role
//        //public virtual TKey RoleId { get; set; }
//        ////
//        //// Summary:
//        ////     UserId for the user that is in the role
//        //public virtual TKey UserId { get; set; }
//    }
//    public class ApplicationRole
//    {
//        [Key]
//        public string Id { get; set; }
//        public string Name { get; set; }
//    }
   
//    public class ApplicationUser : IdentityUser
//    {
       
//        public string Discriminator { get; set; }

//        [Required]
//        public string NickName { get; set; }

//        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager,
//            string authenticationType)
//        {
//            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
//            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
//            // Add custom user claims here
//            return userIdentity;
//        }
//    }

//    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
//    {
//        public ApplicationDbContext()
//            : base("MS_SqlStoreConnectionString", throwIfV1Schema: false)
//        {
//        }
       
//        public static ApplicationDbContext Create()
//        {
//            return new ApplicationDbContext();
//        }
//        public DbSet<Client> Client { get; set; }
//        public DbSet<RefreshToken> RefreshToken { get; set; }
//        public DbSet<ClientMessage> ClientMessages { get; set; }


//        protected override void OnModelCreating(DbModelBuilder modelBuilder)
//        {
//            base.OnModelCreating(modelBuilder);
//            modelBuilder.Entity<ApplicationUser>().ToTable("User");
//            modelBuilder.Entity<ApplicationRole>().ToTable("Role");
            
//            modelBuilder.Entity<UserRole>().ToTable("UserRole");
//            modelBuilder.Entity<IdentityUserClaim>().ToTable("userclaim");
//        }

//    }
//}