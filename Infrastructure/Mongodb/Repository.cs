using MongoDB.Driver;

public interface IRepository<TEntity> where TEntity : EntityBase
{
    IMongoCollection<TEntity> Collection { get; }
    Task<TEntity> Insert(TEntity entity);
    Task Update(TEntity entity);
    Task  Delete(string id);
    IQueryable<TEntity> Set();
    Task<TEntity> GetById(string id);
}

public class Repository<TEntity> : IRepository<TEntity>
    where TEntity: EntityBase
{
    FilterDefinition<TEntity> NotDeletedEntityExpr (TEntity ent){
        return NotDeletedEntityExpr(ent.Id);
    }

    FilterDefinition<TEntity> NotDeletedEntityExpr (string id){
        System.Linq.Expressions.Expression<Func<TEntity, bool>> expr = e => e.Id == id && !e.IsDeleted;
        return expr;
    }

    public IMongoCollection<TEntity> Collection { get; }

    public Repository(IMongoCollection<TEntity> collection)
    {
        Collection = collection;
    }
    public async Task<TEntity> Insert(TEntity entity)
    {
        await Collection.InsertOneAsync(entity);
        return entity;
    }
    public async Task Update(TEntity entity)
    {
        var isUpdated = (await Collection
            .ReplaceOneAsync(NotDeletedEntityExpr(entity), entity))
            .MatchedCount > 0;
        if(!isUpdated)
            throw new NotfoundAppException("entity not found");

    }
    public async Task Delete(string id)
    {
        var setDelete = Builders<TEntity>.Update.Set(t => t.IsDeleted, true);
        var document = await Collection.FindOneAndUpdateAsync(e => e.Id == id, setDelete);

        if(document is null)
            throw new NotfoundAppException("id not found");
    }
    public IQueryable<TEntity> Set()
    {
        return Collection
            .AsQueryable().Where(e => !e.IsDeleted);
            
    }


    public async Task<TEntity> GetById(string id)
    {
        var res = await Collection.Find(NotDeletedEntityExpr(id)).FirstOrDefaultAsync() ?? throw new NotfoundAppException("id is null");
        return res;
    }
    

    
}