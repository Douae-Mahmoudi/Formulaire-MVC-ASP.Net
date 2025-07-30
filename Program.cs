using Microsoft.EntityFrameworkCore; // Nécessaire pour UseSqlServer et DbContextOptions
using WebApplication3.Data; // Nécessaire pour votre ApplicationDbContext

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// --- DEBUT DE LA CONFIGURATION DE LA BASE DE DONNEES SQL Server avec Entity Framework Core ---
// Récupère la chaîne de connexion nommée "DefaultConnection" depuis appsettings.json.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Enregistre votre ApplicationDbContext dans le conteneur de dépendances d'ASP.NET Core.
// Cela rend ApplicationDbContext disponible pour l'injection dans vos contrôleurs.
// Il est configuré pour utiliser SQL Server avec la chaîne de connexion spécifiée.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
// --- FIN DE LA CONFIGURATION DE LA BASE DE DONNEES ---


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // This serves files from wwwroot

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); // This is your default route.
                                                        // To access your Conge Demande: /Conge/Demande
                                                        // Vous pouvez changer "{controller=Home}/{action=Index}"
                                                        // par "{controller=Conge}/{action=Demande}" si vous voulez
                                                        // que la page Demande soit la page d'accueil par défaut.

app.Run();