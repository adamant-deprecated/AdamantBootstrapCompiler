using Adamant.Compiler.Ast.Visitors;

namespace Adamant.Compiler.Ast.Declarations
{
	public class GlobalDeclaration : Declaration
	{
		public GlobalDeclaration(AccessModifier access, QualifiedName name, Node initExpression)
			: base(access, name)
		{
		}

		public override TReturn Accept<TParam, TReturn>(IDeclarationVisitor<TParam, TReturn> visitor, TParam param)
		{
			return visitor.VisitGlobalDeclaration(this, param);
		}
	}
}
