# Employment priority

## Features
 
Mod that allows setting employment priority on workplaces. Umemployed workers will fill up higher priority workplaces first.

If you need a workplace to have a worker immediately, you can click the 'employ now' button. This will grab workers from the lowest priority workplaces first.

This mod also works correctly with saved games and the priority assignment tool.

## Building

Follow these steps if you want to build the mod.

### Requirements

- Visual Studio, but the dotnet CLI tools may work just fine
- NET Standard 2.1 developer tools, whatever those are

### Method

I first recommend installing the mod via r2modman. This will show you the folder structure as well as getting DLL dependencies. 

1. Run a NuGet restore, you may need to add BepInEx sources to Visual Studio, see https://github.com/BepInEx/BepInEx.NuGetUpload.Service/wiki#how-to-download-game-assembly-packages
2. Build the mod
3. Copy `EmploymemtPriority.dll` from the `obj` folder into `resources`, or make a new folder for everything
4. Install that folder into your BepInEx plugins by a method of your choosing
