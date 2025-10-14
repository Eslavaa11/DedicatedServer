Player Host

Sistema multijugador Host–Cliente hecho con Netcode for GameObjects (NGO).
Un jugador actúa como Host (servidor + cliente) y el resto se conecta como Clientes.
Incluye movimiento sincronizado, spawns separados, color por propietario y panel del host para expulsar (Kick) jugadores.

Nota: El sistema de pausa global fue descartado para esta entrega. Se priorizó estabilidad y flujo Host/Clientes.

Demo (video)

(Agrega aquí tu enlace a video) mostrando:

Host + 2 clientes conectándose

Movimiento sincronizado

Kick desde el HostPanel

Requisitos

Unity 6 (6000.2+ recomendado)

Netcode for GameObjects 2.5.x

Plataforma de destino: Windows (Standalone)

(LAN) Sin Relay: conexión directa por IP (localhost o IP de la red)

Estructura / Scripts principales

QuickStartHUD.cs – UI mínima (OnGUI) con botones Start Host / Client / Server y logs de conexión.

PlayerMovementHost.cs – Movimiento local del owner (WASD/Arrows) y replicación con NetworkTransform.

ColorByOwner.cs – Cambia el color del jugador según OwnerClientId (identificación visual).

SpawnByClientId.cs – Asigna posición inicial separada por OwnerClientId (evita amontonamiento).

NetworkLobby.cs – Registra OnClientConnected/Disconnected y mantiene un diccionario clientId → nombre.

HostPanel.cs – Panel sólo para Host: lista de jugadores conectados y botón Kick por jugador.

BootWindowSize.cs – Fuerza arranque en ventana y tamaño inicial (ej. 1280×720).

(Opcional) AppBootstrap – Objetos utilitarios (DontDestroyOnLoad, etc.).

Prefab del Player

NetworkObject (obligatorio)

NetworkTransform (interpolación ON; Position/Rotation)

PlayerMovementHost, ColorByOwner, SpawnByClientId

NetworkManager (objeto “Networking”)

Transporte: UnityTransport (UDP, por defecto 127.0.0.1:7777)

Default Player Prefab: Player

Network Prefabs List: Player registrado

Run In Background: ✓

Enable Scene Management: ✓

Tick Rate: 30

Cómo ejecutar (localhost)

Opción rápida (Editor + Build)

Abre la escena: Scenes/PlayerHost_Base (y guárdala).

En Networking/NetworkManager, confirma los ajustes de arriba.

Pulsa Play en el Editor → Start Host (QuickStartHUD).

Compila un build: File → Build Profiles… → Build (agrega la escena a Scenes In Build si no está).

Abre el ejecutable y pulsa Start Client.

¡Listo! Host (Editor) y Cliente (Build) conectados. Repite el paso 5 para un tercer jugador.

Opción LAN (otro PC)

En el Host, inicia Start Host.

En cada cliente, establece la IP del Host en QuickStartHUD (si tu HUD no expone el campo, deja 127.0.0.1 y compila una versión que lo exponga o hardcodea UnityTransport.SetConnectionData(ip, port) antes de StartClient()).

Start Client.

Si usas firewall/antivirus, permite el puerto UDP 7777.

Diseño / UX

Ventana redimensionable: Player Settings → Resolution and Presentation → Windowed + Resizable Window ✓.

Overlay y HUD minimalistas para pruebas (OnGUI).

Cubos de colores por propietario para identificar jugadores.

Spawns escalonados para que cada jugador aparezca separado.

Funcionalidades implementadas

Arquitectura Host–Cliente (el host también juega).

Conexiones simultáneas 3+ jugadores (ajustable por Max Connections en UnityTransport).

Movimiento del owner con replicación por NetworkTransform (fluido/interpolado).

Join/Leave sin reiniciar (manejo de callbacks y lista de jugadores).

HostPanel: visualización de jugadores y Kick (desconexión limpia).

Calidad de vida: Run In Background, ventana Windowed, tamaño inicial controlado.

Instrucciones de Build

Build Profiles / Build Settings

Plataforma: Windows (Active)

Scenes In Build: deja sólo la escena que usas (p. ej. Scenes/PlayerHost_Base). Evita duplicados con el mismo nombre en distinta ruta.

Build a carpeta nueva (ej. Build_1.0/) para no mezclar binarios.

Distribuye ese ejecutable a tus testers/jugadores.

⚠️ Importante: Host y clientes deben usar la misma build. Si cambias scripts, prefabs o escenas, reconstruye y usa la nueva versión en todos.

Problemas conocidos / Limitaciones

Pausa global: deshabilitada en esta versión por simplicidad/estabilidad.

Sin Relay (Internet): sólo localhost/LAN. Para Internet usa Unity Relay o port-forwarding del 7777 (y abrir firewall).

Escenas duplicadas en Build: si aparecen errores como:

Scene Hash ... does not exist in the HashToBuildIndex table

NetworkPrefab hash was not found / Failed to spawn NetworkObject
Asegúrate de que solo la escena correcta esté en Scenes In Build y recompila.

Compatibilidad de versiones: si host/cliente usan builds distintos, pueden fallar la conexión o la sincronía.

Guía de pruebas sugeridas

Host (Editor) + 2 clientes (builds): verificar que todos ven 3 cubos y se mueven fluidamente.

Conectar/desconectar clientes en tiempo real (join/leave).

Usar Kick desde el HostPanel y comprobar que el cliente expulsado se desconecta sin colgar el host.

Cambiar el tamaño de la ventana (resizable) y confirmar que el input y red siguen bien.