using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using GorevYonetimUygulamasi.Models.Enums;

namespace Görev_Yönetim_Uygulaması.Models
{
    public class TaskLog
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string TaskId { get; set; }           // Görevin ID'si

        public TaskStatusEnum Status { get; set; }            // Yeni durum bilgisi, enum olabilir

        public DateTime ChangedAt { get; set; }       // Değişiklik zamanı

        [BsonRepresentation(BsonType.ObjectId)]
        public string ChangedBy { get; set; }         // Durumu değiştiren kullanıcı ID’si
    }
}
