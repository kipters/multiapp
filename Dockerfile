FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS restore
COPY multiapp.sln /app/multiapp.sln
COPY src /app/src
RUN dotnet restore \
    --runtime linux-musl-arm64 \
    /app/multiapp.sln

FROM restore AS build
ARG APP_NAME
WORKDIR /app/src/${APP_NAME}
RUN dotnet publish \
    --configuration Release \
    --runtime linux-musl-arm64 \
    --no-restore \
    --output /dist
RUN mv /dist/${APP_NAME} /dist/app

FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine
COPY --from=build /dist /dist
ENTRYPOINT [ "/dist/app" ]
