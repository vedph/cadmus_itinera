@echo off
echo BUILD Cadmus Itinera Packages
del .\Cadmus.Itinera.Parts\bin\Debug\*.snupkg
del .\Cadmus.Itinera.Parts\bin\Debug\*.nupkg

del .\Cadmus.Itinera.Services\bin\Debug\*.snupkg
del .\Cadmus.Itinera.Services\bin\Debug\*.nupkg

del .\Cadmus.Seed.Itinera.Parts\bin\Debug\*.snupkg
del .\Cadmus.Seed.Itinera.Parts\bin\Debug\*.nupkg

cd .\Cadmus.Itinera.Parts
dotnet pack -c Debug -p:IncludeSymbols=true -p:SymbolPackageFormat=snupkg
cd..

cd .\Cadmus.Itinera.Services
dotnet pack -c Debug -p:IncludeSymbols=true -p:SymbolPackageFormat=snupkg
cd..

cd .\Cadmus.Seed.Itinera.Parts
dotnet pack -c Debug -p:IncludeSymbols=true -p:SymbolPackageFormat=snupkg
cd..

pause
