dotnet tool install --global dotnet-sonarscanner
dotnet sonarscanner begin /k:"ToDo" /d:sonar.host.url="http://localhost:9000"  /d:sonar.login="e96230782d0acba45f2fcd74543370d2d29ec36c" /d:sonar.cs.opencover.reportsPaths="%CD%\opencover.xml"
dotnet build
dotnet sonarscanner end /d:sonar.login="e96230782d0acba45f2fcd74543370d2d29ec36c"
pause