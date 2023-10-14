using EFCore.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCore;

public interface IRepository<TId>
{
    OverallDbContext Context { get; }
    Task<TEntity> GetByIdAsync<TEntity>(TId Id) where TEntity : EntityBase<TId>;
    Task Update(EntityBase<TId> entity, bool withSave = true);
    Task<int> AddAsync(EntityBase<TId> entity, bool withSave = true);
    Task Delete<TEntity>(TId id, bool withSave = true) where TEntity : EntityBase<TId>; 
    IQueryable<TEntity> Set<TEntity>() where TEntity : EntityBase<TId>;
}

public class RepositoryIntIdImpl : IRepository<int>
{
    public OverallDbContext Context {  get; private set; }  

    public RepositoryIntIdImpl(OverallDbContext context)
    {
        Context = context;
    }
    public async Task<TEntity> GetByIdAsync<TEntity>(int id) 
        where TEntity : EntityBase<int>
    {
        var entity = await Context.FindAsync<TEntity>(id) ?? throw new NotfoundAppException($"id  invalid {id}");

        return entity;
    }

    
    public IQueryable<TEntity> Set<TEntity>()
        where TEntity : EntityBase<int> 
        => Context.Set<TEntity>()
        .Where(i => !i.IsDeleted);

    public async Task Update(EntityBase<int> entity, bool withSave = true)
    {
        entity.LastUpdatedDate = DateTime.UtcNow;
        Context.Entry(entity).State = EntityState.Modified;
        Context.Update(entity);

        if(withSave)
            await Context.SaveChangesAsync();
    }
    public async Task<int> AddAsync(EntityBase<int> entity, bool withSave = true)
    {
        entity.CreatedDate = DateTime.UtcNow;
        await Context.AddAsync(entity);
        if(withSave)
            await Context.SaveChangesAsync();
        return entity.Id;
    }
    public async Task Delete(EntityBase<int> entity, bool withSave = true)
    {
        entity.DeletedDate = DateTime.UtcNow;
        entity.IsDeleted = true;
        Context.Entry(entity).State = EntityState.Modified;
        Context.Update(entity);
        if(withSave)
            await Context.SaveChangesAsync();
    }

    public async Task Delete<TEntity>(int id, bool withSave = true) where TEntity : EntityBase<int>
    {
        var user = await GetByIdAsync<TEntity>(id);
        await Delete(user);
        if(withSave)
            await Context.SaveChangesAsync();
    }
}