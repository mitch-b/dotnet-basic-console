# Basic .NET 7 Console App

Quickly clone this repository to get a .NET 7 console app which:

1. Has dependency injection usage examples ([\[FromServices\]](./ConsoleApp/Services/DemoService.cs))
1. Docker container support
1. devcontainer configured - only need container runtime, not dotnet locally installed (TODO)
1. Has GitHub actions to build a container (TODO)
1. Has optional [authentication/authorization](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis/security?view=aspnetcore-7.0) with Azure AD (TODO)

## Build & Run Docker container

```bash
cd BasicConsole/
docker build -t dotnet-basic-console:latest .
docker run --rm dotnet-basic-console:latest
```

## Renaming from template:

_zsh_ :

```bash
# TODO
```

_PowerShell_ :

```powershell
# TODO
```

_Bash_ :

```bash
# TODO
```