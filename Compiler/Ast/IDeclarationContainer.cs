using System.Collections.Generic;

namespace Adamant.Compiler.Ast
{
	public interface IDeclarationContainer
	{
		IEnumerable<Declaration> Declarations { get; } 
	}
}
