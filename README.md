# WindowsServiceSampleNetCore

publish service:
dotnet publish -c Release

create service:
sc create MeuServicoWindows binPath= "C:\MeuServicoWindows\publish\SeuProjeto.exe" start= auto DisplayName= "Meu Serviço Windows com .NET"

delete service:
sc delete MeuServicoWindows
