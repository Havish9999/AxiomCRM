using AxiomCRM.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AxiomCRM.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; }

    public DbSet<Lead> Leads { get; set; }

    public DbSet<Opportunity> Opportunities { get; set; }

    public DbSet<FollowUp> FollowUps { get; set; }

    public DbSet<Activity> Activities { get; set; }

    public DbSet<AuditLog> AuditLogs { get; set; }
}