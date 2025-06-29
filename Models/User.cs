using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDbDemo.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("Id")]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Surname")]
        public string Surname { get; set; }

        [BsonElement("Email")]
        public string Email { get; set; }
    }
}
