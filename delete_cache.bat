rem WARNING dont run on another path, will delete files and folders
rem this batch file MUST copied & executed inside browser CACHE folder.. 
rem the purpose of this batch file is to clean all cached files and keep only the cookies
rem WARNING dont run on another path, will delete files and folders
rem written by GPT 

@echo off
setlocal enabledelayedexpansion

rem Define the names of folders to keep
set "keepFolders=databases Local Storage Network Session Storage shared_proto_db"

rem Loop through each first-level folder in the current directory
for /d %%F in (*) do (
    if exist "%%F" (
        rem Loop through each second-level folder in the first-level folder
        for /d %%G in ("%%F\*") do (
            set "deleteFolder=true"
            rem Check if the folder name is in the keepFolders list
            for %%H in (%keepFolders%) do (
                if /i "%%~nxG"=="%%H" (
                    set "deleteFolder=false"
                )
            )
            rem If the folder is not in the keep list, delete it
            if "!deleteFolder!"=="true" (
                echo Deleting folder: %%G
                rd /s /q "%%G"
            )
        )
    )
)

endlocal