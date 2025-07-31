using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace GorevYonetimUygulamasi.Models.Enums
{
    public enum TaskStatusEnum
    {
        Beklemede = 0,     // Pending
        DevamEdiyor = 1,   // InProgress
        Tamamlandı=2     // Completed
    }
}
