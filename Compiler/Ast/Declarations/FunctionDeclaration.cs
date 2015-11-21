using System.Collections.Generic;
using System.Linq;
using Adamant.Compiler.Ast.Visitors;

namespace Adamant.Compiler.Ast.Declarations
{
	public class FunctionDeclaration : Declaration
	{
		private readonly IList<Parameter> parameters;
		private readonly IList<Statement> body;

		public FunctionDeclaration(AccessModifier access, QualifiedName name, IEnumerable<Parameter> parameters, IEnumerable<Statement> body)
			: base(access, name)
		{
			this.parameters = parameters.ToList();
			this.body = body.ToList();
		}

		public IEnumerable<Parameter> Parameters => parameters;
		public IEnumerable<Statement> Body => body;

		public override TReturn Accept<TParam, TReturn>(IDeclarationVisitor<TParam, TReturn> visitor, TParam param)
		{
			return visitor.VisitFunctionDeclaration(this, param);
		}
	}
}
