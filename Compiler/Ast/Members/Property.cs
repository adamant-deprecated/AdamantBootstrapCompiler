using System.Collections.Generic;
using Adamant.Compiler.Ast.Declarations;

namespace Adamant.Compiler.Ast.Members
{
	public class Property : MethodLike
	{
		public Property(AccessModifier access, IEnumerable<Parameter> parameters, IEnumerable<Statement> body)
			: base(access,parameters,body)
		{
		}
	}
}
