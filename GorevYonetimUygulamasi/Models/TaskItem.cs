using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using GorevYonetimUygulamasi.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Görev_Yönetim_Uygulaması.Models
{
    public class TaskItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [Required]
        public string Title { get; set; }               // Zorunlu
        [Required]
        public string Description { get; set; }        // Zorunlu

        [BsonRepresentation(BsonType.ObjectId)]
        public string CreatedBy { get; set; }            // Kullanıcı ID'si olarak BsonRepresentation eklenebilir, diğer string alanlarla uyum için
        [Required]
        public TaskStatusEnum Status { get; set; }            // "pending", "in-progress", "completed" gibi sabitler enum ile tanımlanabilir
        [Required]
        public TaskPriorityEnum Priority { get; set; }             // "low", "medium", "high" için enum önerilir

        [BsonRepresentation(BsonType.ObjectId)]
        public string AssignedTo { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string ProjectId { get; set; }

        public DateTime CreatedAt { get; set; }          // Görev oluşturulma zamanı eklenebilir

        public DateTime? DueDate { get; set; }           // İstersen son teslim tarihi gibi bir alan ekleyebilirsin
    }
}
