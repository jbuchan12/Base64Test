namespace Navtor.ERP.DataBase.Repositories;

/// <summary>
/// Abstract base class for implementing repository pattern for entity types.
/// </summary>
/// <typeparam name="TEntity">The entity type.</typeparam>
public abstract class Repository<TEntity> where TEntity : class
{
    protected readonly NavtorErpDbContext Context;

    /// <summary>
    /// Initializes a new instance of the <see cref="Repository{TEntity}"/> class.
    /// </summary>
    /// <param name="testContext">The DbContext options. If null, a default PatchApiContext is created.</param>
    protected Repository(NavtorErpDbContext? testContext)
    {
        Context = testContext
                  ?? new NavtorErpDbContext();
    }
    
    public virtual bool Any()
        => Context.Set<TEntity>().Any();

    /// <summary>
    /// Gets a queryable collection of entities of the specified type.
    /// </summary>
    /// <returns>A queryable collection of entities.</returns>
    public virtual IQueryable<TEntity> Get()
        => Context.Set<TEntity>()
            .AsQueryable();

    public virtual TEntity? GetById(Guid id)
        => Context.Set<TEntity>()
            .Find(id);

    /// <summary>
    /// Adds an entity to the database.
    /// </summary>
    /// <param name="entity">The entity to be written to the db.</param>
    /// <returns>Was the record written correctly</returns>
    public virtual bool Add(TEntity entity)
    {
        Context.Add(entity);
        return Context.SaveChanges() > 0;
    }

    /// <summary>
    /// Adds an entity to the database.
    /// </summary>
    /// <param name="entities">The entity to be written to the db.</param>
    /// <returns>Was the record written correctly</returns>
    public virtual bool Add(List<TEntity> entities)
    {
        Context.AddRange(entities);
        return Context.SaveChanges() > 0;
    }

    /// <summary>
    /// Updates the specified entity in the database.
    /// </summary>
    /// <param name="entity">The entity to be updated.</param>
    /// <returns><c>true</c> if the entity was successfully updated; otherwise, <c>false</c>.</returns>
    public virtual bool Update(TEntity entity)
    {
        Context.Update(entity);
        return Context.SaveChanges() > 0;
    }

    public virtual bool Delete(TEntity entity)
    {
        Context.Remove(entity);
        return Context.SaveChanges() > 0;
    }
}