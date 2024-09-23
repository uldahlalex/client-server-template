#!/bin/bash
dotnet ef dbcontext scaffold \
  "Server=localhost;Database=Dunder_Mifflin;User Id=testuser;Password=testpass;" \
  Npgsql.EntityFrameworkCore.PostgreSQL \
  --output-dir ./Models \
  --context-dir . \
  --context DunderContext  \
  --no-onconfiguring \
  --force