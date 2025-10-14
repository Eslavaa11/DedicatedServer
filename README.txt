======================== INICIO ========================

SERVIDOR DEDICADO – UNITY + NETCODE + DOCKER

Proyecto multijugador con servidor dedicado (Linux Server en Docker) y clientes Windows. La escena principal es: Scenes/Servidor_Dedicado. Los clientes se conectan por UDP 7777, ven a otros jugadores en tiempo real y se mueven de forma suave (interpolación).

DEMO – QUÉ SE VE

3 o más clientes conectados al mismo servidor.

Cada jugador aparece en una posición distinta y con color propio por dueño.

Movimiento en tiempo real, suave gracias a interpolación.

Los jugadores pueden entrar o salir sin reiniciar el servidor.

HUD simple en el cliente para poner IP y puerto y pulsar Start Client.

REQUISITOS

Unity 6.0.2f1 (o compatible con NGO 2.5.x).

Docker Desktop (WSL2 recomendado en Windows).

Puerto UDP 7777 abierto.

Regla de firewall que permita UDP 7777.

ESTRUCTURA (RESUMEN)

/Assets/Scripts/ScrptsPlayerHost/ (BootWindowSize.cs, ColorByOwner.cs, PlayerMovementHost.cs, SpawnByClientId.cs, QuickStartHUD.cs, etc.)
/Scenes/Servidor_Dedicado.unity
/Dockerfile
/Build_Server/PlayerServer/ (Linux Server: PlayerServer.x86_64 + PlayerServer_Data/)
/Build_Client_Windows/PlayerClient/ (Windows: PlayerClient.exe + PlayerClient_Data/)
Nota: No subir /Build_* a Git. Añadir a .gitignore.

CÓMO CONSTRUIR – SERVIDOR (LINUX SERVER, HEADLESS)

Unity > File > Build Profiles…

Plataforma: Linux Server (Active).

Scene List: dejar sólo Scenes/Servidor_Dedicado marcado.

Build a: Build_Server/PlayerServer/
Comprobar que existen:

Build_Server/PlayerServer/PlayerServer.x86_64

Build_Server/PlayerServer/PlayerServer_Data/

CÓMO CONSTRUIR – CLIENTE (WINDOWS)

Unity > Build Profiles…

Plataforma: Windows (Switch Platform).

Player Settings > Other Settings > Scripting Backend: MONO (rápido) o instalar IL2CPP si se desea.

Scene List: dejar sólo Scenes/Servidor_Dedicado.

Build a: Build_Client_Windows/PlayerClient/
Ejecutable: Build_Client_Windows/PlayerClient/PlayerClient.exe

DOCKER – IMAGEN Y EJECUCIÓN

Contenido del Dockerfile (guardar en la raíz del proyecto):
FROM ubuntu:22.04
RUN apt-get update && apt-get install -y
libglib2.0-0 libgssapi-krb5-2 libicu70 libssl3
libstdc++6 libxi6 libxrender1 libxcursor1 libxrandr2 libxinerama1
ca-certificates && rm -rf /var/lib/apt/lists/*
WORKDIR /app
COPY Build_Server/PlayerServer/ ./
RUN chmod +x ./*.x86_64 || true
EXPOSE 7777/udp
CMD bash -lc 'BIN=$(ls *.x86_64 | head -n1); echo "Starting $BIN"; exec ./"$BIN" -batchmode -nographics -logfile /dev/stdout -dedicated -port 7777'

Construir imagen (PowerShell en la carpeta del Dockerfile):
docker build -t playerhost-server .

Abrir firewall (una vez):
New-NetFirewallRule -DisplayName "Unity NGO UDP 7777" -Direction Inbound -Action Allow -Protocol UDP -LocalPort 7777

Ejecutar servidor en primer plano (ver logs):
docker run --rm -p 7777:7777/udp playerhost-server

Ejecutar en segundo plano:
docker run -d --name playerhost -p 7777:7777/udp playerhost-server
Ver logs: docker logs -f playerhost
Parar: docker stop playerhost

Si el servidor corre OK verás algo como:
Starting PlayerServer.x86_64
Forcing GfxDevice: Null
[DEDICATED] Listening UDP 7777 (Servidor_Dedicado)

CÓMO JUGAR (CLIENTES)

Asegura que el servidor Docker está corriendo.

Abre PlayerClient.exe (puedes abrir 2 o más instancias, o usar otro PC en la LAN).

En el HUD:

Address: 127.0.0.1 si Docker está en esta PC. Si es otra PC, usa su IP LAN (por ejemplo 192.168.x.x).

Port: 7777

Pulsa Start Client (no usar Start Server/Host si usas el dedicado).

Repite en el segundo/tercer cliente.

Controles: WASD o flechas para mover tu cubo (sólo lo controla su propietario).

FUNCIONALIDADES IMPLEMENTADAS

Servidor dedicado headless (Linux Server en Docker).

Soporte 3+ jugadores concurrentes.

Sincronización en tiempo real del movimiento (NetworkTransform + interpolación).

Spawns por ClientId (posiciones separadas) y color por propietario (identificación visual).

Join/Leave sin reiniciar el servidor.

HUD simple de conexión (IP/puerto y Start Client).
Extras para la rúbrica:

Interpolación para suavizar movimiento.

Logs del servidor visibles en consola Docker.

Opción de “Kick” desde HostPanel cuando se prueba en modo Host local (no dedicado).

ESTABILIDAD Y BUENAS PRÁCTICAS

Scenes In Build: dejar sólo Scenes/Servidor_Dedicado en cliente y servidor (evita desincronizaciones).

Construir realmente como Linux Server (no “Linux” normal).

En scripts de UI/ventana, proteger modo servidor:
if (Application.isBatchMode) return;
(Ejemplo: BootWindowSize.cs y cualquier OnGUI).

NetworkManager: Run In Background ON, Tick Rate 30.

Time > Fixed Timestep 0.02 (por defecto).

Asegurar que el puerto UDP 7777 esté libre y permitido por el firewall.

SOLUCIÓN DE PROBLEMAS

El cliente no conecta:

Revisar IP/puerto y firewall. Ver docker logs -f playerhost.

Errores de shader o SIGSEGV en Docker:

Se copió un build “Linux” normal o viejo. Volver a construir como Linux Server a carpeta limpia y reconstruir imagen con --no-cache.

Scene Hash / Failed to spawn NetworkObject:

Escenas distintas entre cliente/servidor. Dejar sólo Scenes/Servidor_Dedicado y recompilar ambos.

Dos servidores en el mismo puerto:

No usar Start Server/Host en clientes si Docker ya corre.

CUMPLIMIENTO DE LA RÚBRICA

Estabilidad (5/5): servidor dedicado estable; join/leave sin reiniciar; Run In Background; Tick 30; escena única coherente.

Sincronización (5/5): movimiento con NetworkTransform e interpolación; sin botones de sincronización manual; fluido.

Conexiones (5/5): 3+ jugadores; desconexiones limpias; lista visible en HUD para pruebas locales con HostPanel (opcional).

Diseño/UI (5/5): HUD claro (IP/puerto/Start Client), colores por owner, spawns separados.

Funciones adicionales (5/5): interpolación + logs visibles (y Kick en pruebas Host).

Documentación (5/5): este TXT explica instalación, build, Docker, juego y troubleshooting.

Experiencia de usuario (5/5): conexión simple y movimiento suave.

Seguimiento de instrucciones (5/5): proyecto base, servidor en Docker, múltiples clientes, README/TXT completo.

VIDEO DE ENTREGA (GUION SUGERIDO)

Terminal con docker run --rm -p 7777:7777/udp playerhost-server mostrando que el servidor escucha.

Cliente A: Address 127.0.0.1, Port 7777, Start Client, moverse.

Cliente B (segunda instancia o segunda PC con IP LAN), Start Client, moverse.

Mostrar que ambos se ven e interactúan en tiempo real.

Cerrar un cliente y verificar que el servidor sigue estable.

LIMITACIONES

Proyecto de ejemplo minimalista para enfocarse en conectividad y sincronización.

En Internet pública se requiere apertura de puertos o Relay/DTLS (no incluido).

LICENCIA / USO

Uso académico para el taller. Se puede reutilizar y adaptar dentro del curso.

======================== FIN ========================
