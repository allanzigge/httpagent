# Can be run with either:
# ".\run.ps1" 
# ".\run.ps1 -Port 7777" or whatever port youd like 

# optional argument
param ([int]$Port = 5050)

# url doc: https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-10.0
Start-Process dotnet "run --project MySite --urls http://localhost:$Port" -NoNewWindow

Write-Host "App has started, port: $Port"