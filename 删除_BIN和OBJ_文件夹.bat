@echo off
@echo Deleting all (bin)And(obj) folders...
for /d /r . %%d in (bin,obj) do @if exist "%%d" rd /s/q "%%d"

@echo.
@echo all (bin)And(obj) folders had deleted...
@echo.
@echo.
pause > nul