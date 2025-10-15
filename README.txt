PROYECTO: Servidor Dedicado Multijugador - Unity + Docker

OBJETIVO
Desarrollar un sistema multijugador funcional con Unity y un servidor dedicado en Docker.
El sistema permite la conexión en tiempo real entre varios jugadores, manteniendo estabilidad,
sincronización y una experiencia de usuario fluida.

----------------------------------------
ESTRUCTURA DEL PROYECTO
----------------------------------------
DedicatedServer/
 ├── Assets/                Recursos y scripts del juego
 ├── Builds/                Contiene los ejecutables del servidor y cliente
 │    ├── PlayerServer/     Build del servidor dedicado
 │    └── Client/           Build del cliente
 ├── Dockerfile             Configuración para ejecutar el servidor en Docker
 ├── FuncionamientoSD.mov   Video demostrativo
 └── README.txt             Documento informativo del proyecto

----------------------------------------
DESCRIPCIÓN DEL FUNCIONAMIENTO
----------------------------------------
SERVIDOR (Docker)
- Compilado en modo headless desde Unity.
- Ejecutado dentro de un contenedor Docker para manejo de conexiones.
- Script principal: ServerBootstrap.cs

Para ejecutar el servidor:
1. cd DedicatedServer
2. docker build -t playerhost-server .
3. docker run -d -p 7777:7777 playerhost-server

CLIENTE
- Utiliza Netcode for GameObjects con Unity Transport.
- Scripts principales:
  PlayerMovementServerAuth.cs, PlayerController.cs, GameManager.cs
- Conexión mediante IP del servidor.
- Sincronización de movimientos en tiempo real.

----------------------------------------
SINCRONIZACIÓN Y ESTABILIDAD
----------------------------------------
- Actualización constante de posición y rotación del jugador.
- Sin botones manuales para sincronizar estados.
- Interpolación de movimiento para suavizar desplazamientos.
- Probado con mínimo 3 jugadores simultáneos.

----------------------------------------
GESTIÓN DE CONEXIONES
----------------------------------------
- Manejo automático de conexión y desconexión de jugadores.
- Registro de actividad en carpeta Logs/.

----------------------------------------
INTERFAZ Y EXPERIENCIA
----------------------------------------
Escena principal: Servidor_Dedicado.unity
Elementos principales:
  Main Camera, Directional Light, Networking, AppBootstrap

----------------------------------------
FUNCIONES ADICIONALES
----------------------------------------
1. Logs de eventos del servidor.
2. Interpolación de movimiento para mejor experiencia.
3. Reconexión básica ante pérdida de conexión.

----------------------------------------
TECNOLOGÍAS
----------------------------------------
- Unity 6 (Netcode for GameObjects)
- C#
- Docker
- Git y GitHub
- Git LFS (manejo de archivos grandes)

----------------------------------------
PRUEBAS REALIZADAS
----------------------------------------
- Conexión entre servidor (Docker) y 2 clientes simultáneos.
- Sincronización estable y sin saltos.
- Sesiones largas sin errores críticos.

----------------------------------------
VIDEO DEMOSTRATIVO
----------------------------------------
Archivo: FuncionamientoSD.mov
Muestra la conexión entre dos dispositivos diferentes.

----------------------------------------
REPOSITORIO
----------------------------------------
https://github.com/Eslavaa11/DedicatedServer

----------------------------------------
NOTA FINAL
----------------------------------------
Este commit soluciona el error previo de subida del video de funcionamiento.
(fix: solución a error de subida del video de funcionamiento)
