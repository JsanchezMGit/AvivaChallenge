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
| | ├── Payment.Data
| | ├── Payment.Enterprice/
| | ├── Payment.ExternalServices/
| | ├── Payment.Mappers/
| | ├── Payment.Models
| | ├── Payment.Presenter
| | ├── Payment.Repository
| | ├── Payment.WebApi/
│ ├── Products/
| | ├── Products.WebApi/
├── FE/ # Proyecto Frontend (React)
│ ├── products-app/
│ | ├── src/
| | | ├── api/           # Configuración de APIs y servicios
| | | ├── components/    # Componentes reutilizables
│ | | | └── ProductList/
| | | ├── hooks/         # Hooks personalizados
| | | ├── store/         # Configuración de Redux
| | | ├── types/         # Definiciones de tipos TypeScript
| | | ├── assets/        # Recursos estáticos
| | | ├── App.tsx        # Componente principal
| | | └── main.tsx       # Punto de entrada
└── README.md
```

# PRODUCTS-APP (React) 🛍️

Una aplicación web moderna para la gestión de órdenes de productos desarrollada con React, TypeScript y Vite. Esta aplicación permite visualizar, actualizar y cancelar órdenes de productos con una interfaz intuitiva y responsive.

📁 La solución se encuentra dentro de la carpeta `FE/products-app`.

## 📋 Características

- **Gestión de Órdenes**: Visualización de órdenes con detalles de productos, tarifas y estado
- **Estados de Órdenes**: Manejo de estados (Pendiente, Pagado, Cancelado)
- **Interfaz Moderna**: UI responsive con iconos de FontAwesome
- **Gestión de Estado**: Redux Toolkit para el manejo del estado global
- **TypeScript**: Tipado estático para mayor robustez del código
- **API Integration**: Comunicación con backend mediante Axios

## 🚀 Tecnologías Utilizadas

### Core
- **React 19.1.0** - Biblioteca principal para la construcción de interfaces
- **TypeScript 5.8.3** - Superset de JavaScript con tipado estático
- **Vite 7.0.4** - Build tool rápido y moderno

### Estado y Datos
- **Redux Toolkit 2.8.2** - Gestión del estado de la aplicación
- **React Redux 9.2.0** - Integración de Redux con React
- **Axios 1.10.0** - Cliente HTTP para comunicación con APIs

### UI/UX
- **FontAwesome 6.7.2** - Biblioteca de iconos
  - `@fortawesome/fontawesome-svg-core`
  - `@fortawesome/free-solid-svg-icons`
  - `@fortawesome/react-fontawesome`

### Desarrollo
- **ESLint 9.30.1** - Linting de código
- **TypeScript ESLint 8.35.1** - Reglas específicas para TypeScript
- **Vite Plugin React 4.6.0** - Plugin de React para Vite

## ⚙️ Configuración del Entorno

### Variables de Entorno

Crea un archivo `.env` en la raíz del proyecto:

```env
VITE_API_KEY=72P4gUoC0E+MD9o9xhp9fQ==
```

### Configuración de la API

La aplicación está configurada para conectarse a un backend en `http://localhost:5045/`. Asegúrate de que el servidor backend esté ejecutándose en este puerto.

## 🛠️ Instalación y Configuración

### Prerrequisitos

- Node.js (versión 16 o superior)
- npm o yarn

### Pasos de Instalación

1. **Clonar el repositorio**:
```bash
git clone https://github.com/JsanchezMGit/AvivaChallenge.git
cd FE/products-app
```

2. **Instalar dependencias**:
```bash
npm install
```

3. **Configurar variables de entorno**:
```bash
cp .env.example .env
# Editar .env con tus valores
```

4. **Iniciar el servidor de desarrollo**:
```bash
npm run dev
```

## 📦 Scripts Disponibles

### Desarrollo
```bash
npm run dev          # Inicia el servidor de desarrollo
npm run preview      # Vista previa de la build de producción
```

### Construcción
```bash
npm run build        # Construye la aplicación para producción
```

### Calidad de Código
```bash
npm run lint         # Ejecuta ESLint para revisar el código
```

## 🏗️ Compilación y Despliegue

### Desarrollo Local

1. **Iniciar en modo desarrollo**:
```bash
npm run dev -- --port 5555
```
La aplicación estará disponible en `http://localhost:5555`

2. **Hot Module Replacement (HMR)**: Los cambios se reflejan automáticamente sin recargar la página

### Compilación para Producción

1. **Generar build optimizada**:
```bash
npm run build
```

2. **Probar build localmente**:
```bash
npm run preview
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
| Método  | Ruta             | Descripción                                     | Autenticación |
| ------- | ---------------- | ----------------------------------------------- | ------------- |
| GET     | `/orders`        | Devuelve lista de ordenes.                      | Sí (API Key)  |
| GET     | `/orders/{id}`   | Devuelve la orden dado su id.                   | Sí (API Key)  |
| POST    | `/orders`        | Guarda una orden.                               | Sí (API Key)  |
| DELETE  | `/orders/{id}`   | Cancela una orden dado su id.                   | Sí (API Key)  |
| PATCH   | `/orders/{id}`   | Marca como pagada una orden dado su id.         | Sí (API Key)  |
| PATCH   | `/sync/orders`   | Sinconiza la BD con los datos de los provedores | Sí (API Key)  |
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