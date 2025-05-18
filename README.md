# WindowsServiceSampleNetCore

publish service:
dotnet publish -c Release -r win-x64 --self-contained true -o C:\MeuServicoWindows\publish

create service:
sc create MeuServicoWindows binPath= "C:\MeuServicoWindows\publish\SeuProjeto.exe" start= auto DisplayName= "Meu Serviço Windows com .NET"

delete service:
sc delete MeuServicoWindows
