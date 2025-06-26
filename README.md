# Juan Mario Rojas

Aplicación de escritorio con autenticación y búsqueda de películas, compuesta por:

- **Backend**: API REST construida con ASP.NET Core
- **Frontend**: Aplicación de escritorio desarrollada con WPF (.NET)

---

## Tecnologías usadas

### Backend

- **ASP.NET Core 8** – Framework principal para la API
- **Entity Framework Core** – ORM para acceso a datos
- **SQLite** – Base de datos local para desarrollo
- **JWT (JSON Web Tokens)** – Autenticación basada en tokens
- **OMDb API** – Fuente externa para búsqueda de películas
- **Swagger** – Generación automática de documentación

### Frontend

- **WPF (.NET 8)** – Interfaz de escritorio
- **MVVM** – Patrón de arquitectura para separar lógica de presentación
- **HttpClient** – Consumo de endpoints desde la app

---

## Requisitos previos

- Tener instalado el [SDK de .NET 8](https://dotnet.microsoft.com/download)
- Tener instalada la CLI de Entity Framework Core (`dotnet ef`)
- Contar con una clave de API válida de [OMDb API](http://www.omdbapi.com/apikey.aspx)

---

## Pasos para ejecutar la solución

```bash
# 1. Clonar el repositorio
git clone https://github.com/Demuniano/movie-app-test.git
cd movie-app-test

# 2. Configurar el backend
cd backend/MovieApp.API

# Crear el archivo .env con el siguiente contenido (puedes usar .env.example como base)
# --- .env ---
SQLITE_DB=Data Source=movieapp.db
JWT_SECRET=tu_clave_secreta
OMDB_API_KEY=tu_clave_omdb
# ------------

# Restaurar paquetes y aplicar migraciones
dotnet restore
dotnet ef database update
```

# Ejecutar la API
```bash
cd backend/MovieApp.API
dotnet run
```
La API estará disponible en: http://localhost:5271
La documentación Swagger estará en: http://localhost:5271/swagger

# 3. Ejecutar el frontend WPF
```bash
cd frontend/MovieApp.WPF
dotnet run
```
## Explicación de las decisiones tomadas

- Se optó por una arquitectura basada en capas (**Controladores → Servicios → Repositorios**) para mantener una separación clara de responsabilidades y facilitar el mantenimiento del código.

- Se eligió **Entity Framework Core** como ORM para simplificar el acceso a datos, y **SQLite** por ser una opción ligera y fácil de configurar.

- Para la autenticación, se implementó **JWT (JSON Web Tokens)**, lo que permite un sistema seguro y sin estado.

- El frontend se construyó en **WPF (.NET)** utilizando el patrón **MVVM**, lo que facilita una separación lógica adecuada de las funcionalidades.


---

## Posibles mejoras futuras

- Implementar roles de usuario y control de acceso basado en permisos.
- Completar los métodos CRUD para el manejo de usuarios.
- Añadir paginación, filtros y ordenamiento en los resultados de búsqueda.
- Migrar a una base de datos como **PostgreSQL** en producción.
- Agregar una interfaz de administración para visualizar usuarios y películas guardadas.
- Mejorar el diseño visual y la experiencia de usuario del frontend con animaciones y estilos más personalizados.
- Automatizar el despliegue usando contenedores (**Docker**) y pipelines **CI/CD**.

