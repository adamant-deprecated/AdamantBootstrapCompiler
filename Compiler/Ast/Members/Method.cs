using System.Collections.Generic;
using Adamant.Compiler.Ast.Declarations;

namespace Adamant.Compiler.Ast.Members
{
	public class Method : MethodLike
	{
		public Method(AccessModifier access, IEnumerable<Parameter> parameters, IEnumerable<Statement> body)
			: base(access, parameters, body)
		{
		}
	}
}
