using Adamant.Compiler.Ast.Types;

namespace Adamant.Compiler.Ast.Visitors
{
	public interface ITypeVisitor<in TParam, out TReturn>
	{
		TReturn VisitInferredType(InferredType type, TParam param);
		TReturn VisitOwnershipType(OwnershipType type, TParam param);
		TReturn VisitTypeName(TypeName type, TParam param);
	}
}
