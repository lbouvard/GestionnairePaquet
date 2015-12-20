using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GestionnairePaquet.Models
{
    // Vous pouvez ajouter des données de profil pour l'utilisateur en ajoutant plus de propriétés à votre classe ApplicationUser ; consultez http://go.microsoft.com/fwlink/?LinkID=317594 pour en savoir davantage.
    public class ApplicationUser : IdentityUser
    {
        //ajout du lien d'un utilisateur avec une société
        public int SocieteId { get; set; }

        //surchage pour affichage des champs en français
        [DisplayName("Nom d'utilisateur")]
        public override string UserName { get; set; }

        [DisplayName("Adresse de messagerie")]
        public override string Email { get; set; }

        [DisplayName("Compte confirmé")]
        public override bool EmailConfirmed{ get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Notez qu'authenticationType doit correspondre à l'élément défini dans CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Ajouter les revendications personnalisées de l’utilisateur ici
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Fichier> Fichiers { get; set; }
        public DbSet<Societe> Societes { get; set; }
        public DbSet<Produit> Produits { get; set; }
        public DbSet<Version> Versions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}