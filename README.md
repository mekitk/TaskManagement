
## 🛠️ Kullanılan Teknolojiler

### 🔧 Back-End (API)
- ASP.NET Core Web API (.NET 8)
- MongoDB
- JWT Authentication
- Role-based Authorization
- CORS, Logging, Middleware


---

## 🏗️ Mimari Yapı ve Proje Dizini

### 🔹 Back-End (ASP.NET Core Web API)

/Controllers // Auth, Projects, Tasks
/Routes // API yönlendirmeleri
/Services // Business logic
/Models // DTO'lar ve MongoDB şemaları
/Middlewares // JWT doğrulama, rol kontrolü
/Helpers // Token işlemleri, log yazımı



### 🔹 Front-End (Next.js)



---

## ✨ Ekstra Özellikler

- 🔐 JWT ile kullanıcı kimlik doğrulama
- 👥 Admin, Manager, Developer rolleri ile yetkilendirme
- ✅ Görev yönetimi: Durum ve öncelik kontrolü
- 📦 Modular servis ve middleware mimarisi

- ```txt
Kullanıcı → Frontend (Next.js)
            ↓ 
         Backend API (.NET)
            ↓
          MongoDB
