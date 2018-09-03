# *Deprecated* Adamant Bootstrap Compiler

The Adamant compiler is being bootstrapped.  To serve this process, a compiler must first be written in another language. In this case C#. This is that temporary compiler.  It will be thrown away when the compiler is re-written in Adamant.

## Project Status: Alpha *Deprecated*

This project should not be used. The current Adamant compiler is the "[adamant.tools.compiler.bootstrap](https://github.com/adamant/adamant.tools.compiler.bootstrap)" project.

### Download and Use

Clone this git repo and compile using Visual Studio 2015.

## Explanation of this Project

This project attempted to write a Adamant to C# direct translator in C#.  That is a translator without type-checking, borrow checking or significant code transformations.  Indeed it didn't even build a symbol table.  However, this approach was found to be inadequate because there were important language features it just wasn't possible to translate this way (like covariant and contravariant types, type inference etc.).