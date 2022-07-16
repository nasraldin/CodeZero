@ECHO off
cls

ECHO CodeZero by Nasr Aldin - nasr2ldin@gmail.com
ECHO Deleting 'BIN' and 'OBJ' folders
ECHO Version: 1.0.0
ECHO.

ECHO Deleting...
FOR /d /r . %%d in (bin,obj) DO (
	IF EXIST "%%d" (
		ECHO.Deleting: %%d
		rd /s/q "%%d"
	)
)

ECHO.
ECHO.BIN and OBJ folders have been successfully deleted. Press any key to exit.
pause > nul