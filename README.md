## Installation
### QDK Installation Instructions

#### Browse to Reference
1. Browse https://www.microsoft.com/quantum, navigate to "Get Started", and click on the "Learn More" button
1. Click on the "Download for MacOS and Linux" button. You will land on https://docs.microsoft.com/en-us/quantum/quantum-installconfig. Keep this tab open for the rest of the tutorial

#### Open a terminal window
1. Open a Terminal window from `Finder/Go/Utilities`

#### VS Code
1. Install Visual Studio Code for the mac from https://code.visualstudio.com/docs/setup/mac;
1. Copy the downloaded app into `Finder/Go/Applications`
1. Open VS Code and pin it to tray
1. In VS Code, `Command-Shift-P/"shell"/"Shell Command: Install 'code' command in PATH'"`
1. Run `code ~` and Visual Studio Code will come up. Shut down Visual Studio Code.

#### .NET Core SDK
1. Install Microsoft .NET Core SDK from https://www.microsoft.com/net/learn/get-started/macos 
1. Run `dotnet` and ensure it fails 

#### QDK Extension
1. Install Microsoft Quantum Development Kit for Visual Studio Code from https://marketplace.visualstudio.com/items?itemName=quantum.quantum-devkit-vscode
1. Run `dotnet new -i "Microsoft.Quantum.ProjectTemplates::0.2-*"` and ensure that Q# appears in the list of installed templates. Ignore warnings but ensure that one version of the templates was actually installed.

#### Brew and Git
1. Install homebrew if you haven't already from `https://brew.sh`
1. Run `brew install git`

#### Sanity Check
1. Run `mkdir ~/samples`
1. Run `cd ~/samples`
1. Run `git clone https://github.com/Microsoft/Quantum.git`
1. Run `cd Quantum`
1. Run `code .` and this should bring up Visual Studio Code with access to the files
1. Run `cd Samples/Teleportation`. Other samples are present for your future reference
1. Run `dotnet run` and you should get the Teleportation sample running

#### Tutorial Directory
1. Run `mkdir ~/tutorial`
1. Run `cd ~/tutorial`