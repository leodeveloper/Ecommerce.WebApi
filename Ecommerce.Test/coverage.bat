@echo off

dotnet clean
dotnet build /p:DebugType=Full
dotnet minicover instrument --workdir ../ --assemblies crosssolar.tests/**/bin/**/*.dll --sources crosssolar/**/*.cs --exclude-sources crosssolar/Migrations/**/*.cs --exclude-sources crosssolar/*.cs --exclude-sources crosssolar\Domain\CrossSolarDbContext.cs

dotnet minicover reset --workdir ../

dotnet test --no-build
dotnet minicover uninstrument --workdir ../
dotnet minicover report --workdir ../ --threshold 70