#!/bin/bash
set -euo pipefail

LOG_FILE="/tmp/osm_import.log"
PBF_FILE="/osm/denmark-latest.osm.pbf"
DB_NAME="RoadNetworkDb"
DB_USER="guest"
DB_HOST="/var/run/postgresql"

echo "$(date) ⏳ --------- Starting OSM import script ---------" | tee -a "$LOG_FILE"

# Check if the PBF file exists
if [ ! -f "$PBF_FILE" ]; then
  echo "$(date) ❌ PBF file not found at $PBF_FILE. Exiting." | tee -a "$LOG_FILE"
  exit 1
fi

echo "$(date) ✅ PBF file found at $PBF_FILE." | tee -a "$LOG_FILE"

# Wait for PostgreSQL to be ready
echo "$(date) ⏳ Waiting for PostgreSQL to be ready..." | tee -a "$LOG_FILE"
until pg_isready -h "$DB_HOST" -U "$DB_USER" > /dev/null 2>&1; do
  sleep 2
  echo "$(date) ... still waiting for PostgreSQL ..." | tee -a "$LOG_FILE"
done

echo "$(date) ✅ PostgreSQL is ready." | tee -a "$LOG_FILE"

# Create PostGIS extensions if not present
echo "$(date) 🧩 Enabling PostGIS extensions in $DB_NAME..." | tee -a "$LOG_FILE"
psql -h "$DB_HOST" -U "$DB_USER" -d "$DB_NAME" -c "CREATE EXTENSION IF NOT EXISTS postgis;" | tee -a "$LOG_FILE"
psql -h "$DB_HOST" -U "$DB_USER" -d "$DB_NAME" -c "CREATE EXTENSION IF NOT EXISTS postgis_topology;" | tee -a "$LOG_FILE"
psql -h "$DB_HOST" -U "$DB_USER" -d "$DB_NAME" -c "CREATE EXTENSION IF NOT EXISTS hstore;" | tee -a "$LOG_FILE"

# Run osm2pgsql
echo "$(date) 🚀 Starting import with osm2pgsql..." | tee -a "$LOG_FILE"
osm2pgsql --create --slim \
  --hstore \
  --database="$DB_NAME" \
  --username="$DB_USER" \
  --host="$DB_HOST" \
  "$PBF_FILE" 2>&1 | tee -a "$LOG_FILE"

echo "$(date) ✅ OSM import completed successfully." | tee -a "$LOG_FILE"
