# API en .Net por Fernando Mejia

## Pasos

### Tener instalador Entity Framework
dotnet tool install --global dotnet-ef

### Cambiar la ruta de la base de datos
Esta ruta se encuentra en el archivo appsettings.json :: ConectionStrings :: cnColegio

## Crear la base
Hay dos formas de crear la base:
1. Con una petición get a ​/api​/DataBase​/CreateDatabase
2. Con EntityFramework Migrations usando el comando en la terminal: 
dotnet ef database update

## Login 
Por defecto hay dos usuarios ya creados que se pueden usar para realizar el login y acceder a las funcionalidades del api
1. Fer / admin
2. John / john

## Postman
Primero es necesario importar la colección de la carpeta "Postman"
Segundo es necesario crear dos variables que pueden ser globales
  1. path  -- Es la ruta donde se levanta el servidor Ej: https://localhost:7013
  2. token  -- Es la variable para authenticación 
