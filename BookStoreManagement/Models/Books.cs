using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;    

namespace BookStoreManagement.Models
{
    public class Books
    {
        [BsonId] // Marks this property as the primary key
        [BsonRepresentation(BsonType.ObjectId)] // Allows using string instead of ObjectId
        public string? Id { get; set; }

        [BsonElement("Title")] // Maps to MongoDB field "Title"
        public string Title { get; set; }

        [BsonElement("Author")]
        public string Author { get; set; }

        [BsonElement("Price")]
        public decimal Price { get; set; }
    }
}
