using Microsoft.EntityFrameworkCore;
using WebApplication3.Models; // Assurez-vous d'inclure votre namespace Models

namespace WebApplication3.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Constructeur requis pour la configuration du DbContext via l'injection de dépendances
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet pour votre modèle Conge. Cela mappera votre classe Conge à une table 'Conges' dans la base de données.
        public DbSet<Conge> Conges { get; set; } = default!; // 'default!' est pour satisfaire les avertissements de nullabilité

        // Si vous avez d'autres modèles, ajoutez des DbSets pour eux ici :
        // public DbSet<WebApplication3.Models.AutreModele> AutresModeles { get; set; } = default!;

        // Cette méthode peut être utilisée pour configurer des mappings plus avancés si nécessaire
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Exemple : Si vous vouliez renommer la table 'Conges' en 'DemandesDeConge'
            // modelBuilder.Entity<Conge>().ToTable("DemandesDeConge");
        }
    }
}