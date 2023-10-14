using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

public static class MongoDbBuilder
{
    public static void Build()
    {
        //examplse for id as string
        // try
        // {
        //     BsonMemberMap SetStringId<T>(BsonClassMap<T> map) => map.IdMemberMap.SetSerializer(new StringSerializer(BsonType.ObjectId)).SetIdGenerator(StringObjectIdGenerator.Instance);

        //     BsonClassMap.RegisterClassMap<Profile>(map =>
        //     {
        //         map.AutoMap();
        //         SetStringId(map);

        //     });
        // }
        // catch (ArgumentException ex)
        // {
        //     if (!ex.Message.ToLower().Contains("an item with the same key has already been added"))
        //         throw ex;
        // }
    }
}