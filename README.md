# SignalR Chat

Aplicacion de chat en tiempo real desarrollada con C# y .NET 10, inspirada en la arquitectura del repositorio de referencia.

## Proyectos

- `SignalR.Hub`: servidor ASP.NET Core que administra las conexiones y las salas del chat.
- `SignalR.Client`: aplicacion web Blazor para conectarse, elegir una sala y enviar mensajes.
- `SignalR.Shared`: contrato compartido para los mensajes.

## Requisitos

- .NET 10 SDK
- Visual Studio 2022 actualizado o Visual Studio Code

## Ejecutar

Abre dos terminales en la carpeta del repositorio.

Terminal 1:

```bash
dotnet run --project SignalR.Hub --urls http://localhost:7001
```

Terminal 2:

```bash
dotnet run --project SignalR.Client --urls http://localhost:5000
```

Abre `http://localhost:5000` en dos ventanas del navegador, escribe nombres distintos y entra a la misma sala para probar el chat.

Las salas disponibles son Desarrollo y Diseno. Los mensajes se transmiten en tiempo real a los usuarios conectados a la sala seleccionada; no se guardan en una base de datos.
