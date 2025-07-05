using companyApp.Server.Models.Entities;
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
        Database.Migrate(); 
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AgentEntity>()
            .HasOne(a => a.Company)
            .WithOne(c => c.Agent)
            .HasForeignKey<AgentEntity>(a => a.AgentId);

        modelBuilder.Entity<AgentEntity>()
            .HasMany(a => a.Banks)
            .WithMany(b => b.Agents)
            .UsingEntity<BankAgentRelationEntity>(
                j => j
                    .HasOne(bar => bar.Bank)
                    .WithMany()
                    .HasForeignKey(bar => bar.BankId),
                j => j
                    .HasOne(bar => bar.Agent)
                    .WithMany()
                    .HasForeignKey(bar => bar.AgentId),
                j =>
                {
                    j.HasKey(t => new { t.BankId, t.AgentId });
                    j.ToTable("bank_agent_relation");
                });

        modelBuilder.Entity<CompanyEntity>()
            .HasOne(c => c.Bank)
            .WithOne(b => b.Company)
            .HasForeignKey<BankEntity>(b => b.BankId);

        modelBuilder.Entity<CompanyEntity>()
            .HasOne(c => c.Client)
            .WithOne(cl => cl.Company)
            .HasForeignKey<ClientEntity>(cl => cl.ClientId);

        modelBuilder.Entity<AgentViewEntity>(ave =>
        {
            ave.HasNoKey();
            ave.ToView("agent_view");
        });
    }
}

