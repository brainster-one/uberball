@echo off
Assets\MSBuild.exe .\Sources\Uberball.sln


set "exclude=.log .dll .gitkeep .html .exe .xap clientaccesspolicy.xml" 
for /f "tokens=*" %%i in ( 
'dir /b binaries ^| findstr /l /v "%exclude%"' 
) do ( 
	del /q binaries\%%i
	rd /q binaries\%%i
) 
