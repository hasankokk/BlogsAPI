# 📝 BlogsAPI

Bu proje, ASP.NET Core ile geliştirilmiş bir blog yönetim sistemine ait Web API uygulamasıdır. Kimlik doğrulama, kullanıcı rolleri, blog gönderileri üzerinde işlemler yapmanıza olanak tanır. Katmanlı mimariyle yazılmıştır ve gelişime açıktır.

---

## 🚀 Özellikler

- 🔐 ASP.NET Core Identity ile kimlik doğrulama ve kullanıcı yönetimi
- 🧑‍💻 Rol bazlı yetkilendirme (Admin / Yazar)
- 📰 Gönderi CRUD işlemleri
- 🧵 Entity Framework Core (Code-First)
- 🧭 Scalar UI ile API dokümantasyonu
- 🧱 Katmanlı mimari (Clean Architecture yaklaşımı)

---

## ⚙️ Teknolojiler

- ASP.NET Core 9
- Entity Framework Core
- ASP.NET Core Identity
- FluentValidation
- Mapster
- Scalar UI
- SQLite

---

## 🚀 Projeyi Çalıştırma

### 1. Repo'yu Klonla

```bash
git clone https://github.com/hasankokk/BlogsAPI.git
cd BlogsAPI
```

### 2. Veritabanı Ayarları

`appsettings.json` dosyasındaki bağlantı cümlesini kendi SQL bilgilerinle güncelle.

```bash
dotnet ef migrations add <MigrationName>
dotnet ef database update
```

### 3. Uygulamayı Başlat

```bash
dotnet run --project BlogsAPI.API
```

Tarayıcıda aç:  

---

## 📄 API Dokümantasyonu

Aşağıdaki yollarla erişebilirsin:
- Scalar: `/scalar`
