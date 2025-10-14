FROM ubuntu:22.04

RUN apt-get update && apt-get install -y \
    libglib2.0-0 libgssapi-krb5-2 libicu70 libssl3 \
    libstdc++6 libxi6 libxrender1 libxcursor1 libxrandr2 libxinerama1 \
    ca-certificates && rm -rf /var/lib/apt/lists/*

WORKDIR /app
COPY Builds/Server_Linux/ ./
RUN chmod +x ./Jugador.x86_64

EXPOSE 7777/udp
ENTRYPOINT ["./Jugador.x86_64","-batchmode","-nographics","-logfile","/dev/stdout","-dedicated","-port","7777"]
