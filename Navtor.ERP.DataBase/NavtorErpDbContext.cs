using Microsoft.EntityFrameworkCore;
using Navtor.ERP.DataBase.Models;

namespace Navtor.ERP.DataBase;

/// <summary>
/// Represents the database context for the Patch API application.
/// </summary>
public sealed class NavtorErpDbContext : DbContext
{
    public DbSet<Vessel> Vessels { get; set; } = null!;

    public DbSet<Product> Products { get; set; } = null!;

    public DbSet<ProductGroup> ProductGroups { get; set; } = null!;


    /// <summary>
    /// Initializes a new instance of the <see cref="NavtorErpDbContext"/> class with default options.
    /// </summary>
    public NavtorErpDbContext() : base(SqlLiteDetails)
    {
        Database.Migrate();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NavtorErpDbContext"/> class with the specified <paramref name="testDbContextOptions"/>.
    /// </summary>
    /// <param name="testDbContextOptions">The <see cref="DbContextOptions"/> to be used for testing.</param>
    public NavtorErpDbContext(DbContextOptions testDbContextOptions) : base(testDbContextOptions)
    {
    }

    private static DbContextOptions SqlLiteDetails
        => new DbContextOptionsBuilder<NavtorErpDbContext>()
            .UseSqlite("Filename=NavtorErp.db")
            .Options;

    public DbSet<TEntity> Table<TEntity>() where TEntity : class
        => Set<TEntity>();
}
