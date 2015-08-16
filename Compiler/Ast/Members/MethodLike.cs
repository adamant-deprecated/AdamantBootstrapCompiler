using System.Collections.Generic;
using System.Linq;
using Adamant.Compiler.Ast.Declarations;

namespace Adamant.Compiler.Ast.Members
{
	public abstract class MethodLike : Member
	{
		private readonly IList<Parameter> parameters;
		private readonly IList<Statement> body;

		protected MethodLike(AccessModifier access, IEnumerable<Parameter> parameters, IEnumerable<Statement> body)
			: base(access)
		{
			this.parameters = parameters.ToList();
			this.body = body.ToList();
		}

		public IEnumerable<Parameter> Parameters => parameters;
		public IEnumerable<Statement> Body => body;
	}
}
