ARG POSTGRES_VERSION=15

############ Base
FROM postgres:${POSTGRES_VERSION}-alpine as base

ENV POSTGRES_PASSWORD=SuperSecret

############ Build context
FROM base as build_context

############ Build
FROM build_context as build

############ Production
FROM build as production
ENV DB_USER=postgres
HEALTHCHECK --interval=1m --timeout=30s --retries=3 --start-period=5s CMD pg_isready -U $DB_USER || exit 1

############ Manager
FROM dpage/pgadmin4:latest as manager

ENV PGADMIN_DEFAULT_EMAIL=admin@nowhere.invalid.com
ENV PGADMIN_DEFAULT_PASSWORD=SuperSecret
ENV PGADMIN_DISABLE_POSTFIX=1
ENV PGADMIN_CONFIG_ENABLE_PSQL=True
