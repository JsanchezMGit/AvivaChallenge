# Aviva Challenge

Solucion a: https://avivacredito.atlassian.net/wiki/external/ODFhZTlkMjYxNDhmNGM4ZmE3MjI3ZjdiNzhhZjc4ZmU

## 📂 Estructura

```text
repo-root/
│
├── BE/ # Proyecto Backend (.NET API)
│ ├── Payment/
| | ├── Payment.Adapters/
| | ├── Payment.Application/
| | ├── Payment.Enterprice/
| | ├── Payment.ExternalServices/
| | ├── Payment.Mappers/
| | ├── Payment.WebApi/
│ ├── Products/
| | ├── Products.WebApi/
└── README.md
```

# 🧩 Payment.WebApi (.NET Minimal API)

Este proyecto es una API REST construida con **.NET 9 y Minimal API**, diseñada para exponer metodos para administrar registros de ordenes de productos. Utiliza una base de datos **en memoria**, autenticación por **API Key (`X-Api-Key`)**, y expone documentación mediante **Swagger UI**.

📁 La solución se encuentra dentro de la carpeta `BE/Payment`.

---

## 🚀 Requisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0) instalado
- CLI o editor como Visual Studio / VS Code

---

## 🧪 Tecnologías usadas

- .NET 9
- Minimal API
- Entity Framework Core (InMemory)
- Swagger / OpenAPI
- Middleware personalizado (`X-Api-Key`)


---

## ⚙️ Cómo compilar y ejecutar

### Desde la terminal

🔐 Autenticación por API Key (vía secrets.json)
Todos los endpoints requieren el header:

X-Api-Key: tu_clave_supersecreta

La clave se lee desde la configuración y se almacena en el sistema de secretos de usuario (secrets.json) durante el desarrollo local.

✅ Cómo configurar la API Key localmente

1. Abre una terminal y navega a la carpeta del proyecto:

```bash
cd BE/Payment/Payment.WebApi
```

2. Ejecuta el siguiente comando para inicializar los secretos (si no lo has hecho antes):
```bash
dotnet user-secrets init --project Payment.WebApi
```

3. Agrega la clave ApiKey con el valor que desees:
```bash
dotnet user-secrets set "ApiKey" "72P4gUoC0E+MD9o9xhp9fQ==" --project Payment.WebApi
```

Esto almacena la clave en un archivo seguro fuera del repositorio, en:
```bash
%APPDATA%\Microsoft\UserSecrets\ on Windows  
~/.microsoft/usersecrets/ on Linux/macOS
```

El middleware personalizado leerá automáticamente este valor desde la configuración, este valor es el que debe ser enviado en el header (`X-Api-Key`)

3. Restaura los paquetes:
```bash
dotnet restore
```

3. Ejecuta la API:
```bash
dotnet run --profile http
```

🔒 Seguridad
- No almacenes la clave en appsettings.json en entornos compartidos.
- secrets.json no se sube al control de versiones (está pensado para uso local y seguro).
- En producción, esta clave debería configurarse mediante variables de entorno o un gestor de secretos (Azure Key Vault, AWS Secrets Manager, etc.).

📘 Documentación (Swagger)
Una vez corriendo el proyecto, abre en tu navegador:

http://localhost:5228/swagger

📦 Endpoints disponibles

```text
| Método  | Ruta             | Descripción                             | Autenticación |
| ------- | ---------------- | --------------------------------------- | ------------- |
| GET     | `/orders`        | Devuelve lista de ordenes.              | Sí (API Key)  |
| GET     | `/orders/{id}`   | Devuelve la orden dado su id.           | Sí (API Key)  |
| POST    | `/orders`        | Guarda una orden.                       | Sí (API Key)  |
| DELETE  | `/orders/{id}`   | Cancela una orden dado su id.           | Sí (API Key)  |
| PATCH   | `/orders/{id}`   | Marca como pagada una orden dado su id. | Sí (API Key)  |
```

🧹 Limpieza / Build
```bash
dotnet clean
dotnet build
```

# 🧩 Products.WebApi (.NET Minimal API)

Este proyecto es una API REST construida con **.NET 9 y Minimal API**, diseñada para exponer un listado de productos. Utiliza una base de datos **en memoria**, autenticación por **API Key (`X-Api-Key`)**, y expone documentación mediante **Swagger UI**.

📁 La solución se encuentra dentro de la carpeta `BE/Products`.

---

## 🚀 Requisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0) instalado
- CLI o editor como Visual Studio / VS Code

---

## 🧪 Tecnologías usadas

- .NET 9
- Minimal API
- Entity Framework Core (InMemory)
- Swagger / OpenAPI
- Middleware personalizado (`X-Api-Key`)


---

## ⚙️ Cómo compilar y ejecutar

### Desde la terminal

🔐 Autenticación por API Key (vía secrets.json)
Todos los endpoints requieren el header:

X-Api-Key: tu_clave_supersecreta

La clave se lee desde la configuración y se almacena en el sistema de secretos de usuario (secrets.json) durante el desarrollo local.

✅ Cómo configurar la API Key localmente

1. Abre una terminal y navega a la carpeta del proyecto:

```bash
cd BE/Products/Products.WebApi
```

2. Ejecuta el siguiente comando para inicializar los secretos (si no lo has hecho antes):
```bash
dotnet user-secrets init --project Products.WebApi
```

3. Agrega la clave ApiKey con el valor que desees:
```bash
dotnet user-secrets set "ApiKey" "72P4gUoC0E+MD9o9xhp9fQ==" --project Products.WebApi
```

Esto almacena la clave en un archivo seguro fuera del repositorio, en:
```bash
%APPDATA%\Microsoft\UserSecrets\ on Windows  
~/.microsoft/usersecrets/ on Linux/macOS
```

El middleware personalizado leerá automáticamente este valor desde la configuración, este valor es el que debe ser enviado en el header (`X-Api-Key`)

3. Restaura los paquetes:
```bash
dotnet restore
```

3. Ejecuta la API:
```bash
dotnet run --profile http
```

🔒 Seguridad
- No almacenes la clave en appsettings.json en entornos compartidos.
- secrets.json no se sube al control de versiones (está pensado para uso local y seguro).
- En producción, esta clave debería configurarse mediante variables de entorno o un gestor de secretos (Azure Key Vault, AWS Secrets Manager, etc.).

📘 Documentación (Swagger)
Una vez corriendo el proyecto, abre en tu navegador:

http://localhost:5045/swagger

📦 Endpoints disponibles

```text
| Método | Ruta        | Descripción                 | Autenticación |
| ------ | ----------- | --------------------------- | ------------- |
| GET    | `/products` | Devuelve lista de productos | Sí (API Key)  |
```

🧹 Limpieza / Build
```bash
dotnet clean
dotnet build
```

📄 Licencia
Este proyecto es solo de referencia / evaluación técnica. Licencia libre.