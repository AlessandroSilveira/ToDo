%USERPROFILE%\.nuget\packages\opencover\4.7.1221\tools\OpenCover.Console.exe -output:"%CD%\opencover.xml" -register:user -target:"C:\Program Files\dotnet\dotnet.exe" -targetargs:"test C:\Users\alesi\source\repos\ToDo\ToDo.Tests\ToDo.Tests.csproj" -oldstyle
dotnet sonarscanner end /d:sonar.login="e96230782d0acba45f2fcd74543370d2d29ec36c"
pause