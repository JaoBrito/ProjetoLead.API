using Microsoft.EntityFrameworkCore;
using ProjetoLead.API.Models;

namespace ProjetoLead.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { }
        public DbSet<LeadModel> Leads { get; set; }
    }
}
