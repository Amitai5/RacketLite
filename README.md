# RacketLite

[![Project status](https://img.shields.io/badge/status-v3.0%20beta-D97706?style=for-the-badge)](#project-status)
[![C sharp](https://img.shields.io/badge/C%23-239120?style=for-the-badge)](#technology)
[![.NET](https://img.shields.io/badge/.NET-6-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)](#local-development)
[![Windows](https://img.shields.io/badge/platform-Windows-0078D4?style=for-the-badge)](#local-development)
[![MIT License](https://img.shields.io/badge/license-MIT-22C55E?style=for-the-badge)](LICENSE)

> Explore Racket-style expressions through a small interpreter written in C#.

## Language Summary

RacketLite, also called Racket-Lite, is a small [Racket](https://racket-lang.org/) interpreter focused on a subset of the Beginning Student Language ([BSL](https://docs.racket-lang.org/htdp-langs/beginner.html)). It began as a passion project by [Amitai Erfanian](https://www.amitai.tech) in early September 2020.

The current application runs in a Windows console, where you can evaluate nested expressions, experiment with numbers, booleans, and strings, choose branches with `if`, and load expressions from a source file. The parser, expression classes, and value types are implemented in C#.

The wiki links below provide the original language reference. The [supported expressions](#supported-expressions) and [project status](#project-status) sections describe the current source revision.

## Table Of Contents
### [1.0 Syntax](https://github.com/Amitai5/RacketLite/wiki/Syntax)
* [**Interpreter Commands**](https://github.com/Amitai5/RacketLite/wiki/Syntax#interpreter-commands)
* [**Define**](https://github.com/Amitai5/RacketLite/wiki/Syntax#define)
* [**If**](https://github.com/Amitai5/RacketLite/wiki/Syntax#if)
* [**Local**](https://github.com/Amitai5/RacketLite/wiki/Syntax#local)

### [1.1 Numbers](https://github.com/Amitai5/RacketLite/wiki/Numbers)
* [**Number Attributes**](https://github.com/Amitai5/RacketLite/wiki/Numbers#number-attributes)
* [**Number Methods**](https://github.com/Amitai5/RacketLite/wiki/Numbers#number-methods)
* [**Conversion Methods**](https://github.com/Amitai5/RacketLite/wiki/Numbers#conversion-methods)

### [1.2 Booleans](https://github.com/Amitai5/RacketLite/wiki/Booleans)
* [**Boolean Methods**](https://github.com/Amitai5/RacketLite/wiki/Booleans#boolean-methods)
* [**Boolean Conversions**](https://github.com/Amitai5/RacketLite/wiki/Booleans#boolean-conversions)

### 1.3 Lists
### 1.4 Characters

### [1.5 Strings](https://github.com/Amitai5/RacketLite/wiki/Strings)
* [**String Methods**](https://github.com/Amitai5/RacketLite/wiki/Strings#string-methods)

### Project guide

- [Why RacketLite?](#why-racketlite)
- [Current experience](#current-experience)
- [Supported expressions](#supported-expressions)
- [Local development](#local-development)
- [Example session](#example-session)
- [Interpreter commands](#interpreter-commands)
- [Technology](#technology)
- [Repository layout](#repository-layout)
- [Project status](#project-status)
- [License](#license)

## Why RacketLite?

RacketLite makes the steps of interpreting a language visible in a compact codebase. Text becomes typed arguments and expression objects, each expression evaluates its children, and the result is returned as a Racket-style value.

Each operation has its own expression class, while a central registry connects operator names to their parsers. This structure makes it possible to follow a calculation from console input through parsing, type checks, evaluation, and formatted output.

## Current experience

- **Interactive console:** enter an expression at the `>` prompt and see its result immediately.
- **Nested evaluation:** combine operations such as `(* (+ 2 3) 4)`.
- **Typed values:** work with numeric, boolean, and string values, including the `#i` inexact-number prefix.
- **Conditional expressions:** use `if` to evaluate the selected branch of a boolean question.
- **File loading:** read expressions from a local file, including expressions spanning multiple lines.
- **Diagnostics:** parsing and evaluation errors are displayed in red with a leading semicolon. Optional expression-tree output is available through the interpreter's `PrintTree` constructor setting; the default entry point leaves it disabled.

## Supported expressions

The following are representative operators registered in `ExpressionDefinitions.cs`:

| Category | Examples |
| --- | --- |
| Arithmetic | `+`, `-`, `*`, `/`, `abs`, `add1`, `sub1`, `sqr`, `sqrt`, `expt` |
| Numeric helpers | `floor`, `ceiling`, `round`, `min`, `max`, `gcd`, `lcm`, `quotient`, `modulo` |
| Comparisons and predicates | `=`, `<`, `<=`, `>`, `>=`, `number?`, `integer?`, `even?`, `odd?`, `exact?` |
| Booleans | `and`, `or`, `not`, `boolean=?`, `boolean?`, `false?` |
| Strings | `string-append`, `string-copy`, `string-length`, `string-upcase`, `string-downcase`, `string-contains` |
| Conversions | `number->string`, `boolean->string`, `boolean->integer`, `exact->inexact`, `inexact->exact` |
| Control flow | `if` |

Boolean arguments accept `#t`, `#f`, `#true`, `#false`, `true`, and `false`; results print as `#t` or `#f`. Strings use double quotes.

Operator names, argument rules, and numeric behavior can differ from the broader wiki documentation. For example, the current registry uses `string-contains`, while the wiki describes `string-contains?`. Use the registry and expression implementation when checking support for a particular form.

## Local development

### Prerequisites

- Windows: the project explicitly targets `net6.0-windows`, and the help launcher uses `cmd`.
- A .NET SDK capable of building the .NET 6 target.
- The .NET 6 runtime to run the unchanged, framework-dependent application.

The checked-in .NET 6 target is [out of support](https://dotnet.microsoft.com/en-us/platform/support/policy/dotnet-core). Current SDKs may report `NETSDK1138` when building it.

### Build and run

From PowerShell:

```powershell
git clone https://github.com/Amitai5/RacketLite.git
cd RacketLite
dotnet build RacketLite/RacketLite.sln
dotnet run --project RacketLite/RacketLite/RacketLite.csproj --no-build
```

The console displays `Welcome to Racket-Lite v3.0 beta [cs].` and an input prompt. Enter one nonempty expression per prompt, keeping interactive expressions on a single line. Press **Ctrl+C** to stop the interpreter.

The build and example session below were verified on Windows using .NET SDK 10.0.401 and the .NET 6.0.36 runtime. The build completed with the expected .NET 6 support warnings.

## Example session

```text
> (+ 1 2 3)
6
> (* (+ 2 3) 4)
20
> (and #true (not #false))
#t
> (if (> 5 3) "yes" "no")
"yes"
> (string-append "Hello, " "RacketLite!")
"Hello, RacketLite!"
> (string-length "Racket")
6
> (number->string 42)
"42"
> (exact? #i3.5)
#f
> (sqr 8)
64
```

## Interpreter commands

Enter a command on its own line. Command matching is case-insensitive.

| Command | Behavior |
| --- | --- |
| `#help` | Print the wiki address and open it through the Windows shell |
| `#loadfile` or `#ldf` | Prompt for a local filename, read the file, and evaluate its expressions |
| `#clear` or `#cls` | Clear the console and display the welcome message |

For file loading, enter `#loadfile` first, then provide the path at the filename prompt. The filename reader includes Tab completion. The file parser joins lines until parentheses balance and skips lines whose first character is `;` or `#`, including reader directives such as `#lang`. Keep loaded files within the expression subset described above.

## Technology

| Layer | Technology | Responsibility |
| --- | --- | --- |
| Application | C# and .NET 6 for Windows | Console entry point and interpreter loop |
| Parsing | Custom C# parser and operator dictionaries | Recognize expressions, parse arguments, and select implementations |
| Evaluation | Expression classes | Apply arithmetic, boolean, string, conversion, and conditional operations |
| Values | Custom numeric, boolean, and string wrappers | Carry results and format Racket-style output |
| Console tools | `System.Console` and local helpers | Colors, filename entry, Tab completion, and file-loading interaction |
| Build | .NET SDK and MSBuild | Restore framework assets and compile the solution |

The application project declares no external NuGet package references.

## Repository layout

```text
RacketLite/
├── LICENSE
├── README.md
└── RacketLite/
    ├── .editorconfig
    ├── RacketLite.sln
    └── RacketLite/
        ├── Program.cs                 # Console entry point
        ├── RacketInterpreter.cs       # Input loop, commands, file loading, and output
        ├── RacketParsingHelper.cs     # Argument parsing and type validation
        ├── ExpressionDefinitions.cs   # Operator-to-parser registry
        ├── IRacketObject.cs           # Shared evaluation and tree-output contract
        ├── Expressions/               # Arithmetic, boolean, string, and special forms
        ├── ValueTypes/                # Numeric, boolean, and string values
        ├── ConsoleTools/              # Console and filename-input helpers
        ├── Exceptions/                # Interpreter error types
        ├── Assets/                    # Application icon
        └── RacketLite.csproj           # .NET 6 Windows console project
```

## Project status

The interpreter identifies itself as **v3.0 beta**. It implements a subset of Racket-style expression evaluation, with several limits to keep in mind:

- The original table of contents includes `define`, `local`, lists, and characters. Those topics remain in the reference, but corresponding forms or value types are not implemented in the current source.
- Numeric values use C# single-precision `float` storage with exactness and rationality flags. This differs from Racket's full numeric system and exact rational arithmetic.
- Interactive input is line-oriented. Empty input and end-of-input handling are incomplete, so use nonempty expressions and stop with Ctrl+C.
- File parsing uses a lightweight space- and parenthesis-based approach. General Racket module syntax and complete reader compatibility are outside the implemented subset.
- The repository includes no automated test project or continuous-integration workflow. The example session above is a manual smoke check, not a language-conformance test suite.

## License

RacketLite is released under the [MIT License](LICENSE).

## Acknowledgments

RacketLite draws its language inspiration from [Racket](https://racket-lang.org/) and the [Beginning Student Language](https://docs.racket-lang.org/htdp-langs/beginner.html). The repository's wiki preserves the original syntax and operation reference.
