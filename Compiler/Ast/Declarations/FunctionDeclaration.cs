using Adamant.Compiler.Ast.Visitors;

namespace Adamant.Compiler.Ast.Declarations
{
	public class FunctionDeclaration : Declaration
	{
		public FunctionDeclaration(AccessModifier access, QualifiedName name)
			: base(access, name)
		{
		}

		public override TReturn Accept<TParam, TReturn>(IDeclarationVisitor<TParam, TReturn> visitor, TParam param)
		{
			return visitor.VisitFunctionDeclaration(this, param);
		}
	}
}
