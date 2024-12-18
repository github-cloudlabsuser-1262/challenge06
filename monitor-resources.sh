#!/usr/bin/bash

# Function to get CPU usage
get_cpu_usage() {
  top -bn1 | grep "Cpu(s)" | \
  sed "s/.*, *\([0-9.]*\)%* id.*/\1/" | \
  awk '{print 100 - $1"%"}'
}

# Function to get memory usage
get_memory_usage() {
  free -m | awk 'NR==2{printf "Memory Usage: %s/%sMB (%.2f%%)\n", $3,$2,$3*100/$2 }'
}

# Main loop
while true; do
  echo "CPU Usage: $(get_cpu_usage)"
  echo "$(get_memory_usage)"
  sleep 5
done
