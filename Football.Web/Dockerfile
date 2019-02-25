FROM microsoft/dotnet:2.2-sdk
WORKDIR /app
COPY . .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet Football.Web.dll

#FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
#WORKDIR /app
#EXPOSE 80
#EXPOSE 443
#
#FROM microsoft/dotnet:2.2-sdk AS build
#WORKDIR /src
#COPY ["Football.Web/Football.Web.csproj", "Football.Web/"]
#RUN dotnet restore "Football.Web/Football.Web.csproj"
#COPY . .
#WORKDIR "/src/Football.Web"
#RUN dotnet build "Football.Web.csproj" -c Debug -o /app
#
#FROM build AS publish
#RUN dotnet publish "Football.Web.csproj" -c Debug -o /app
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app .
#ENTRYPOINT ["dotnet", "Football.Web.dll"]

#FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
#WORKDIR /app
#EXPOSE 51309
#EXPOSE 44393
#
#FROM microsoft/dotnet:2.2-sdk AS build
#WORKDIR /src
#COPY ["Football/Football.csproj", "Football/"]
#RUN dotnet restore "Football/Football.csproj"
#COPY . .
#WORKDIR "/src/Football"
#RUN dotnet build "Football.csproj" -c Debug -o /app
#
#FROM build AS publish
#RUN dotnet publish "Football.csproj" -c Debug -o /app
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app .
#ENTRYPOINT ["dotnet", "Football.dll"]
#
#

##FROM microsoft/dotnet:sdk AS build-env
##WORKDIR /app
##
### Copy csproj and restore as distinct layers
##COPY *.csproj ./
##RUN dotnet restore
##
### Copy everything else and build
##COPY . ./
##RUN dotnet publish -c Debug -o out
##
### Build runtime image
##FROM microsoft/dotnet:aspnetcore-runtime
##WORKDIR /app
##COPY --from=build-env /app/out .
##ENTRYPOINT ["dotnet", "Football.dll"]