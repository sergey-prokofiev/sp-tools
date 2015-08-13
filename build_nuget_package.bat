cd sp-tools
rem ..\.nuget\nuget.exe spec  
..\.nuget\nuget.exe pack sp-tools.csproj -Prop Configuration=Release
copy sp-*.nupkg ..\*.nupkg
del sp-*.nupkg
cd ..