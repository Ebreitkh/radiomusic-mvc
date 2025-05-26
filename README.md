# 🎵 MusicRadio - Aplicación ASP.NET Core MVC

## 📌 Descripción

MusicRadio es una aplicación web desarrollada en ASP.NET Core MVC para la gestión de canciones y álbumes. Permite registrar, editar y eliminar canciones asociadas a álbumes utilizando procedimientos almacenados en SQL Server.

---

## 🧱 Arquitectura

- **Modelo Vista Controlador (MVC)**
- **Acceso a datos mediante ADO.NET**
- **Procedimientos almacenados SQL**
- **Vistas Razor (.cshtml)**

---

## 🛠️ Tecnologías Utilizadas

| Tecnología | Descripción |
|------------|-------------|
| ASP.NET Core MVC | Framework web |
| ADO.NET | Acceso a base de datos |
| SQL Server | Motor de base de datos |
| Bootstrap | Estilo y diseño |
| C# | Lenguaje de programación |
| HTML/CSS | Frontend básico |

---

## Como ejecutarlo
-**Clona el repositorio:**
git clone https://github.com/Ebreitkh/radiomusic-mvc.git

-**Configura tu cadena de conexión en DbConnectionHelper.cs:**
private static string connectionString = "Server=.;Database=MusicRadioDB;Trusted_Connection=True;";

--**Abre el proyecto en Visual Studio y presiona F5 o ejecuta con:**

dotnet run

--**Asegúrate de que los procedimientos almacenados estén creados en SQL Server:**


