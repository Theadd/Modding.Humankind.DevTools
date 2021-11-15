# Contributing - Getting Started

This guide is only intended for those who want to contribute in the development of this project, since **in order to develop game mods, this steps are not necessary**.

## Building from sources

### Prerequisites

<sub></sub>
* Requires `MSBuild 16` or greater to build, an easy way to get it is to install `Visual Studio 2019` or **newer** if you don't have it already. 
<br/><kbd>Latest `Visual Studio Community Edition` can be downloaded [here](https://visualstudio.microsoft.com/vs/community/).</kbd>

### Clone and build

<sub></sub>
* __Open__ `Developer Command Prompt for VS 20XX` __by pressing the__ <kbd><sub><img src="https://img.icons8.com/ultraviolet/50/000000/windows-10.png" height="14px"/></sub> Win</kbd> __key and start typing its name.__

* __Clone the repo:__

    ```bash
    git clone https://github.com/Theadd/Modding.Humankind.DevTools.git
    ```

* __Open it and restore dotnet local files:__ 

    ```bash
    cd Modding.Humankind.DevTools
    dotnet restore
    ```

* __Now, let's tell to our build task the directory paths in our file system where the game assemblies are located.__

    First, create the local config file by making a copy of the provided one.

    ```bash
    copy Directory.Build.props.default Directory.Build.props
    ```

    Edit `Directory.Build.props` file to at least, put your **Humankind** local installation path in `HUMANKIND_GAME_PATH` tag.
    > If you have used the `Humankind Mod Tools` before and clicked on the <kbd>Decompile Game Scripts</kbd> button at least once, this is all the config you'll need. Otherwise, you should also fill in the `HUMANKIND_REFERENCED_LIBRARIES_PATH` with a single location where **all game DLL** files are accessible *(Or the ones referenced in .csproj)*.

* __That's it! You can build the project in your IDE of choice or by typing the following in the__ `Developer Command Prompt for VS 20XX`:

    ```bash
    msbuild -p:Configuration=Release 
    ```
