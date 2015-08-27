using Adamant.Compiler.Ast.Types;
using Adamant.Compiler.Ast.Visitors;

namespace Adamant.Compiler.Ast.Declarations
{
	public class GlobalDeclaration : Declaration
	{
		public GlobalDeclaration(AccessModifier access, bool isMutableReference, QualifiedName name, OwnershipType type, Expression initExpression)
			: base(access, name)
		{
			IsMutableReference = isMutableReference;
			Type = type;
			InitExpression = initExpression;
		}

		public bool IsMutableReference { get; }
		public OwnershipType Type { get; }
		public Expression InitExpression { get; }

		public override TReturn Accept<TParam, TReturn>(IDeclarationVisitor<TParam, TReturn> visitor, TParam param)
		{
			return visitor.VisitGlobalDeclaration(this, param);
		}
	}
}
