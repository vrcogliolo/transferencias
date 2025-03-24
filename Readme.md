# Local

## Instalar net core 8

https://dotnet.microsoft.com/es-es/download

## Agregar la herramienta dotnet-ef

```console
dotnet tool install --global dotnet-ef
```

## Instalar dependencias
```console
dotnet restore
```

## Aplicar Migraciones
```console
dotnet ef database update
```

## Ejecutar el proyecto

```console
dotnet run
```