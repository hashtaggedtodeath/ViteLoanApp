
using LoanAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LoanAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<LoanApplication> LoanApplications => Set<LoanApplication>();
}
