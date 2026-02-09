# Prueba [readme_prueba_api.md](https://github.com/user-attachments/files/25194074/readme_prueba_api.md)
# 📦 Prueba API

API REST desarrollada en .NET 8 utilizando Clean Architecture, Entity Framework Core, AutoMapper y patrón Generic Repository + Generic Manager.

---

## 🚀 Instalación

### Requisitos
- .NET SDK 8+
- SQL Server
- EF Core CLI
- Visual Studio / VS Code

### Clonar repositorio
```bash
git clone https://github.com/AngelCanales/Prueba.git
cd Prueba
git checkout develop
```

---

## ⚙️ Configuración

Editar `Prueba.Api/appsettings.json`

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=PruebaDb;Trusted_Connection=True;TrustServerCertificate=True"
}
```

---

## 🗄️ Base de datos

### Crear migración inicial
```bash
dotnet ef migrations add InitialCreate \
--context AppDbContext \
--project Prueba.Infrastructure \
--startup-project Prueba.Api \
--verbose
```

---

## ▶️ Ejecución

```bash
dotnet run --project Prueba.Api
```

La API ejecuta automáticamente:
```csharp
context.Database.Migrate();
```

✔️ Crea la BD si no existe  
✔️ Aplica migraciones pendientes automáticamente

---

## 🌐 Endpoints

### Clientes
- GET `/api/clientes`
- GET `/api/clientes/{id}`
- POST `/api/clientes`
- PUT `/api/clientes/{id}`
- DELETE `/api/clientes/{id}`

### Productos
- GET `/api/productos`
- GET `/api/productos/{id}`
- POST `/api/productos`
- PUT `/api/productos/{id}`
- DELETE `/api/productos/{id}`

### Órdenes
- POST `/api/ordenes`
- GET `/api/ordenes`

---

## 🧠 Decisiones técnicas

- Clean Architecture
- GenericRepository + GenericManager
- Managers por entidad
- DTOs por operación (Create, Update, Read)
- Validaciones en Managers
- OperationResult<T> como estándar de respuesta
- AutoMapper
- EF Core + Migraciones automáticas
- Separación estricta de capas

---

## 🏗️ Arquitectura

```
Api (Controllers)
   ↓
Managers (Application)
   ↓
Repositories (Infrastructure)
   ↓
DbContext (EF Core)
   ↓
SQL Server
```

---

## 📬 Postman

Colección:
https://.postman.co/workspace/Personal-Workspace~b92d576b-f8a4-46a7-9f64-78c94f284bae/collection/5921873-3d9bc5ab-389d-4276-a03e-2542473949d2?action=share&creator=5921873

---

## 📄 Licencia
Proyecto educativo / técnico

---

✍️ Autor: Angel Canales

