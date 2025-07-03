FROM postgis/postgis:latest

RUN apt-get update && \
    apt-get install -y postgresql-client osm2pgsql && \
    rm -rf /var/lib/apt/lists/*