using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using GorevYonetimUygulamasi.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Görev_Yönetim_Uygulaması.Models
{
    public class Project
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonIgnore]
        public string Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string OwnerId { get; set; }
        [Required]
        public ProjectStatusEnum Status { get; set; }
        public List<string> MemberIds { get; set; } = new();
    }
}
