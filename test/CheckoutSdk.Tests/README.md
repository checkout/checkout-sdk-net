# NSpec

This template creates a NSpec test project that runs as a console application. Shouldy is used for assertions and Moq for mocking.

For more information on using nspec please see the [project website](http://nspec.org).

## Installation

```
dotnet new -i ./NSpec/ 
```

## Usage

```
dotnet new cko-nspec
```

## Project contents

- `Program.cs` - Discovers the tests in the current assembly and executes the NSpec test runner
- `AtmTests.cs` - Sample specs for an ATM demonstrating the use of both Shouldy and Moq.

## Running tests

The console application has a number of command line switches that enable configuration of the NSpec test runner. To view the help instructions for the application:

```
dotnet run help
```

To run all tests in the project:

```
dotnet run
```

To run a specific test class (note this is the name of the *class* not the file, though these will often have the same value):

```
dotnet run <test class name>
```

To run all tests with the specified tags (comma-separated):

```
dotnet run --tags <tags>
```

To override the default console formatter to use an XUnit formatter supported by Team City:

```
dotnet run --formatter XUnit
```

The default output filename is {AssemblyName}-results.xml. You can override this by providing the `results-file` switch:

```
dotnet run --formatter XUnit --results-file results.xml
```

All of the above flags can be combined. The following example runs all tests tagged with "banking" and "withdrawal" in the "describe_atm" test class and uses the XUnit formatter with a custom output filename:

```
dotnet run describe_atm --tags banking,withdrawal --formatter XUnit --results-file results.xml
```
