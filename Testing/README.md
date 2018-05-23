# Testing in Q\#

## Introduction ##

The Quantum Development Kit comes with two different simulators. These are the __full state simulator__ and the __trace simulator__.

The [full state simulator](https://docs.microsoft.com/en-us/quantum/quantum-fullstate-simulator?view=qsharp-preview) is a full state quantum simulator that can be used to run and debug quantum algorithms using up to 30 qubits locally (depending on hardware constraints) or 40+ qubits using Microsoft's Azure cloud service.

The [trace simulator](https://docs.microsoft.com/en-us/quantum/quantum-computer-trace-simulator-1?view=qsharp-preview) executes a quantum program without actually simulating the state of the quantum computer. For this reason, the trace simulator can locally execute quantum programs that use many more than 40 qubits. It is generally used for two main purposes:

- Debugging classical code that is part of a quantum program.
- Estimating the resources required to run a given instance of a quantum program on a quantum computer.

The trace simulator has the following features:

- The [depth counter](https://docs.microsoft.com/en-us/quantum/quantum-computer-trace-simulator-depth-counter?view=qsharp-preview) gathers counts of the depth of every operation in the program (depth represents the number of time steps required to complete the program).
- The [distinct inputs checker](https://docs.microsoft.com/en-us/quantum/quantum-computer-trace-simulator-distinct-inputs-checker?view=qsharp-preview) can be used to check for bugs e.g. using the same qubit as control and target in a CNOT gate.
- The [invalidated qubits use checker](https://docs.microsoft.com/en-us/quantum/quantum-computer-trace-simulator-invalidated-qubits-use-checker?view=qsharp-preview) can be used to check for bugs e.g. usage of the qubits of a mutable Qubit[] after the qubits have been released.
- The [primitive operations counter](https://docs.microsoft.com/en-us/quantum/quantum-computer-trace-simulator-primitive-operations-counter?view=qsharp-preview) counts the number of primitive executions used by every operation invoked in a quantum program.
- The [width counter](https://docs.microsoft.com/en-us/quantum/quantum-computer-trace-simulator-width-counter?view=qsharp-preview) counts the number of qubits allocated and borrowed by each operation.

The solution provided here provides a framework for running tests against both simulators, which is always recommended for debug and optimisation purposes.

## Setup ##

The purpose of this project is to teach you about testing in Q# using both the full state and trace simulators. For generic instructions, please refer to the official documentation [here](https://docs.microsoft.com/en-us/quantum/quantum-techniques-testinganddebugging?view=qsharp-preview) and [here](https://docs.microsoft.com/en-us/quantum/libraries/testing?view=qsharp-preview).

To set up a new Q# project with unit tests and open it in VS Code, run the following commands from your favourite terminal:

```bash
mkdir Testing
cd .\Testing\
dotnet new sln --name Tutorial
dotnet new console -lang q# -o Tutorial
dotnet new xunit -lang q# -o TutorialTests
dotnet sln add .\Tutorial\Tutorial.csproj
dotnet sln add .\TutorialTests\TutorialTests.csproj
dotnet restore
dotnet build .\Tutorial.sln
code .
```

>You may need to change backslashes to forwardslashes, depending on your environment. The Bash commands will run in PowerShell on Windows.

This will do the following:

1. Create a Visual Studio solution file (.sln)
2. Add new Q# project 'Tutorial'
3. Add new XUnit test project 'TutorialTests'
4. Add the Q# and test projects to the solution (to simplify management)
5. Restore Nuget packages
6. Build the solution
7. Open the parent folder in VS Code

>To open in Visual Studio 2017 instead, omit the last line and simply double click on the `.sln` file in File Explorer.

Once the solution is open in VS Code, you will need to perform some further setup steps:

1. Install the [vscode-solution-explorer](https://marketplace.visualstudio.com/items?itemName=fernandoescolar.vscode-solution-explorer) extension and reload the VS Code UI when prompted once the install is complete.
2. Navigate to the Solution Explorer view now found at the bottom of the navigation pane on the far left of the VS Code window.
3. Expand the TutorialTests project by clicking on the arrow, then expand the references list.
4. Right click on __projects__, found under references. Select 'Add reference' and a reference to the Tutorials project should automatically be created. This will allow us to run tests against the code in the Tutorials project.

>If using Visual Studio 2017, simply navigate to the Solution Explorer pane and add the project reference in (almost) the same way as described above (starting from step 3) for VS Code.

In order to save test output to an XML file for further analysis, you must run tests using the `dotnet xunit` command rather than `dotnet test`. This will require some setup, as described below. 

>If you are using Visual Studio 2017 rather than Visual Studio Code, this is unnecessary as you can use the __Test Explorer__ pane to both run the tests and view the output.

1. Find out your installed .NET Core version by checking the appropriate install location:
    - Windows: `C:\Program Files\dotnet\shared\Microsoft.NETCore.App`
    - Mac: `/usr/local/share/dotnet/shared/Microsoft.NETCore.App` !!!THIS MAY BE INCORRECT - PLEASE CORRECT ME IF I AM WRONG (no Mac to test)!!!
2. Modify the TutorialTests.csproj file accordingly (add the following snippet to the `<PropertyGroup>` element at the start of the file):
    - `<RuntimeFrameworkVersion>2.x.x</RuntimeFrameworkVersion>`
3. In order to save test output as an XML file, run tests (from the TutorialTests directory) with the following command (replacing `testOutput` with a filename of your choice):
    - `dotnet xunit -xml testOutput.xml`

## Extension ##

Once you are happy with the testing framework, setup of new test projects and analysis of the results using the sample code provided, feel free to modify the code and see how this affects the various parameters tracked by the trace simulator.

Some ideas:

- Try passing the same qubit as both the control and target of a CNOT gate. First run this code against the full state simulator and then use the trace simulator.
- Add test projects (and tests!) for all the previous challenges in the workshop.
- Clone the [Microsoft Quantum repo](https://github.com/Microsoft/Quantum) from GitHub and explore the tests found [here](https://github.com/Microsoft/Quantum/tree/master/Samples/UnitTesting).