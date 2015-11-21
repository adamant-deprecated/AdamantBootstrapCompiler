using System.IO;
using Adamant.Compiler.Ast;
using Adamant.Compiler.Ast.Declarations;
using Adamant.Compiler.Ast.Visitors;

namespace Adamant.Compiler.Gen.CSharp
{
	public class CSharpGenerator : IDeclarationVisitor<Void, Void>
	{
		private readonly TextWriter output;

		public CSharpGenerator(TextWriter output)
		{
			this.output = output;
		}

		public void GenerateFor(Assemblage assemblage)
		{
			foreach(var declaration in assemblage.Declarations)
				declaration.Accept(this, Void.Value);
		}

		#region Declaration Visitor
		Void IDeclarationVisitor<Void, Void>.VisitClassDeclaration(ClassDeclaration declaration, Void param)
		{
			throw new System.NotImplementedException();
		}

		Void IDeclarationVisitor<Void, Void>.VisitFunctionDeclaration(FunctionDeclaration declaration, Void param)
		{
			throw new System.NotImplementedException();
		}

		Void IDeclarationVisitor<Void, Void>.VisitGlobalDeclaration(GlobalDeclaration declaration, Void param)
		{
			throw new System.NotImplementedException();
		}
		#endregion
	}
}
