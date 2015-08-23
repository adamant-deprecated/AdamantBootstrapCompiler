using Adamant.Compiler.Ast.Declarations;

namespace Adamant.Compiler.Ast.Visitors
{
	public interface IDeclarationVisitor<in TParam, out TReturn>
	{
		TReturn VisitClassDeclaration(ClassDeclaration declaration, TParam param);
		TReturn VisitFunctionDeclaration(FunctionDeclaration declaration, TParam param);
		TReturn VisitGlobalDeclaration(GlobalDeclaration declaration, TParam param);
	}
}
