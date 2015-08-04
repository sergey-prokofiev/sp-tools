cd sp-tools
rem ..\.nuget\nuget.exe spec  - uncomment with a backaup only!
..\.nuget\nuget.exe pack sp-tools.csproj -Prop Configuration=Release
copy sp-*.nupkg ..\*.nupkg
del sp-*.nupkg
cd ..