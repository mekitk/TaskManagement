﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Görev_Yönetim_Uygulaması.Models
{
    // Models/User.cs
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("username")]
        public string Username { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("passwordHash")]
        public string PasswordHash { get; set; }

        [BsonElement("role")]
        public string Role { get; set; } // Admin, Manager, Developer
    }
}
