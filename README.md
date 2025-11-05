# 🧱 Laboratorio 1 — Implementación de Monolito con patrón MVC y DAO

Este proyecto implementa un **monolito ASP.NET Core MVC** con el patrón **DAO (Data Access Object)** y **Entity Framework Core**, sobre una base de datos SQL Server llamada `persona_db`.  
Incluye controladores, vistas y endpoints REST para realizar operaciones CRUD sobre las entidades: **Persona**, **Profesion**, **Telefono** y **Estudios**.

---

## 🧩 Stack Tecnológico

- .NET 7
- ASP.NET Core MVC
- Entity Framework Core 7
- SQL Server 2022 Express
- Visual Studio Community 2022
- Swagger 3 (para endpoints REST)

---

## ⚙️ Configuración del Ambiente

dotnet restore

### 1️⃣ Instalaciones requeridas
- [SQL Server 2022 Express](https://www.microsoft.com/es-es/sql-server/sql-server-downloads)
- [SQL Server Management Studio (SSMS) 19](https://learn.microsoft.com/es-es/sql/ssms/download-sql-server-management-studio-ssms)
- [Visual Studio 2022 Community](https://visualstudio.microsoft.com/es/vs/)
  - Carga de trabajo: **Desarrollo ASP.NET y web**
  - Complementos: **Entity Framework Core Tools**, **SQL Server Data Tools**

---

### 2️⃣ Crear la base de datos

1. Abre SQL Server Management Studio (SSMS)
2. Conéctate al servidor local (`localhost\SQLEXPRESS`)
3. Ejecuta el script [`script.sql`](./script.sql)

El script creará la base `persona_db`, sus tablas y datos de prueba.

---

### 3️⃣ Configurar la conexión a la base de datos

### 4️⃣ Restaurar dependencias

Abre una terminal en la carpeta del proyecto y ejecuta:

```
dotnet restore
```

### 5️⃣ Compilación
```
dotnet build
```

O desde Visual Studio:

🔹Selecciona Build → Build Solution (Ctrl+Shift+B)

### 6️⃣ Ejecución (Despliegue Local)
🔹 Opción 1 — Visual Studio

Presiona Ctrl + F5
La app se ejecutará en:
```
http://localhost:xxxx/
```
🔹 Opción 2 — CLI

Desde la terminal:
```
dotnet run
```

Accede en el navegador a:
```
http://localhost:5000/
```
### 7️⃣ Despliegue (Publicación)

Puedes publicar la aplicación en una carpeta ejecutable con:
```
dotnet publish -c Release -o ./publish
```

Esto generará los archivos listos en publish/.
Para correrlos:
```
cd publish
dotnet personapi-dotnet.dll
```
### 8️⃣ (Opcional) Despliegue con Docker

Si deseas ejecutar el proyecto dentro de un contenedor:
```
docker build -t personapi-dotnet .
docker run -d -p 8080:80 personapi-dotnet
```

Luego accede a:
```
http://localhost:8080
```

🧱 Estructura del Proyecto
```
personapi-dotnet/
├── Controllers/
│   ├── PersonasController.cs
│   ├── ProfesionesController.cs
│   ├── EstudiosController.cs
│   └── TelefonosController.cs
│
├── Models/
│   ├── Entities/
│   └── DAO/
│       ├── Interfaces y Repositorios DAO
│
├── Views/
├── appsettings.json
├── Program.cs
├── script.sql
└── README.md
```
### 📄 Script SQL

El archivo script.sql
 contiene:

🔹DDL: creación de tablas (persona, profesion, telefono, estudios)

🔹DML: datos de prueba para cada tabla

### 🧾 Documento de soporte

Informe en PDF incluye:

  1. Portada  
  2. Marco conceptual  
  3. Diseño de la solución (arquitectura MVC + DAO)  
  4. Procedimiento  
  5. Conclusiones y lecciones aprendidas  
  6. Referencias

### 🏷️ Repositorio y TAG

🔹Repositorio GitHub: https://github.com/<TU_USUARIO>/personapi-dotnet

🔹TAG de entrega: v1.0-lab1

Autor:
Juan Nicolas Avella
Pontificia Universidad Javeriana
Curso: Arquitectura de Software
