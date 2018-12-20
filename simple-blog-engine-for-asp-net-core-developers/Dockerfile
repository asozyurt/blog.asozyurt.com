FROM microsoft/dotnet:2.1-aspnetcore-runtime-nanoserver-1803 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk-nanoserver-1803 AS build
WORKDIR /src
COPY ["simple-blog-engine-for-asp-net-core-developers/blog.asozyurt.com.csproj", "simple-asp-net-core-blog-engine/"]
RUN dotnet restore "simple-blog-engine-for-asp-net-core-developers/blog.asozyurt.com.csproj"
COPY . .
WORKDIR "/src/simple-blog-engine-for-asp-net-core-developers"
RUN dotnet build "blog.asozyurt.com.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "blog.asozyurt.com.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "blog.asozyurt.com.dll"]