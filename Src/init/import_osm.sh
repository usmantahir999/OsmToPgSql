#!/bin/bash
set -euo pipefail

LOG_FILE="/tmp/osm_import.log"
PBF_FILE="/osm/denmark-latest.osm.pbf"
DB_NAME="RoadNetworkDb"
DB_USER="guest"
DB_HOST="/var/run/postgresql"
IMPORT_FLAG="/osm/.import_done"

echo "$(date) ⏳ --------- Starting OSM import script ---------" | tee -a "$LOG_FILE"

# Skip import if already done
if [ -f "$IMPORT_FLAG" ]; then
  echo "$(date) ⚠️  Import already done. Skipping import." | tee -a "$LOG_FILE"
  exit 0
fi

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

# Mark import as complete
touch "$IMPORT_FLAG"
echo "$(date) ✅ OSM import completed successfully. Flag written to $IMPORT_FLAG" | tee -a "$LOG_FILE"
