using companyApp.Server.DB_Entities;
using Microsoft.EntityFrameworkCore;

namespace companyApp.Server;
public class ApplicationContext : DbContext
{
    public DbSet<CompanyEntity> Companies { get; set; } = null!;
    public DbSet<AgentEntity> Agents { get; set; } = null!;
    public DbSet<BankEntity> Banks { get; set; } = null!;
    public DbSet<ClientEntity> Clients { get; set; } = null!;

    public DbSet<AgentViewEntity> AgentsView { get; set; } = null!;

    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
        Database.EnsureCreated();   // создаем базу данных при первом обращении
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CompanyEntity>().HasData(
                new CompanyEntity { CompanyId = 1 }
        );
        modelBuilder.Entity<AgentEntity>().HasData(
            new AgentEntity { AgentId = 2 }
        );
        modelBuilder.Entity<AgentViewEntity>(ave =>
        {
            ave.HasNoKey();
            ave.ToView("agent_view");
        });
    }
}

