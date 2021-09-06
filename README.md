# RecycleRecycleBin
Windows Service that reads the files from the recycle bin and deletes files based on the extension and the amount of time its been there.

### Dependencies:
- Daxxn/CSharpLibraries/JsonReaderLibraryFW

## Install:
- Open the solution in Visual Studio and build as a release.
- Move the executable files to the desired folder. (Needs to be moved. If changes are made to the relese build while the service is running it will crash. Or worse.)
- Run a Command Prompt or Powershell terminal **as Administrator** and navigate to the folder.
- Run `path\to\dir\TrashCleaner.exe install start`.
- Check that the `Recycle Cleaner` service is running in the Windows Service Manager.

## Uninstall:
- Open Windows Service manager and stop the `Recycle Cleaner` service.
- Run a Command Prompt or Powershell terminal **as Administrator** and navigate to the folder.
- Run `path\to\dir\TrashCleaner.exe uninstall`.
- Check that the service is removed from the Windows Service Manager list.
