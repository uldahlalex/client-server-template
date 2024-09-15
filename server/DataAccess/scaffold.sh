#!/bin/bash
dotnet ef dbcontext scaffold \
  "Server=localhost;Database=testdb;User Id=testuser;Password=testpass;" \
  Npgsql.EntityFrameworkCore.PostgreSQL \
  --output-dir ../Core/Models \
  --context-dir . \
  --context HospitalContext  \
  --no-onconfiguring \
  --force