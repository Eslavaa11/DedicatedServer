FROM ubuntu:22.04

# Librerías mínimas para Unity headless
RUN apt-get update && apt-get install -y \
    libglib2.0-0 libgssapi-krb5-2 libicu70 libssl3 \
    libstdc++6 libxi6 libxrender1 libxcursor1 libxrandr2 libxinerama1 \
    ca-certificates && rm -rf /var/lib/apt/lists/*

WORKDIR /app

# ⬇️ Copia TODO el build de tu servidor: Builds/PlayerServer
# (Usar barras '/' porque el Dockerfile usa sintaxis tipo Linux)
COPY Builds/PlayerServer/ ./

# Asegura permisos de ejecución
RUN chmod +x ./*.x86_64 || true

EXPOSE 7777/udp

# Ejecuta el primer binario .x86_64 que encuentre en modo headless
CMD bash -lc 'BIN=$(ls *.x86_64 | head -n1); echo "Starting $BIN"; exec ./"$BIN" -batchmode -nographics -logfile /dev/stdout -dedicated -port 7777'
