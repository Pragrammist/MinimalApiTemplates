using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class EntityBase
{
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

    public bool IsDeleted { get; set; }

    [BsonIgnore]
    public ObjectId ParsedObjectId => ObjectId.Parse(Id);

}