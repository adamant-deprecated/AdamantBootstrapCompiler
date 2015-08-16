using System.Collections.Generic;
using Adamant.Compiler.Ast.Declarations;

namespace Adamant.Compiler.Ast.Members
{
	public class Constructor : MethodLike
	{
		public Constructor(AccessModifier access, Name name, IEnumerable<Parameter> parameters, IEnumerable<Statement> body) 
			: base(access, parameters,body)
		{
			Name = name;
		}

		public Name Name { get; }
	}
}
