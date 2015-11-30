using System.Collections.Generic;
using System.Linq;
using Adamant.Compiler.Antlr;

namespace Adamant.Compiler.Gen.CSharp
{
	internal class ConstructorNameGenerator : CSharpGenerator
	{
		private readonly ISet<string> names = new HashSet<string>();

		public ConstructorNameGenerator(CSharpWriter writer)
			: base(writer)
		{
		}

		public override MainFunctions VisitCompilationUnit(AdamantParser.CompilationUnitContext context)
		{
			return context.declaration().Select(d => d.Accept(this)).Combine();
		}

		public override MainFunctions VisitUsingStatement(AdamantParser.UsingStatementContext context)
		{
			return MainFunctions.Empty;
		}

		#region Declarations
		public override MainFunctions VisitClassDeclaration(AdamantParser.ClassDeclarationContext context)
		{
			context.member().Select(m => m.Accept(this)).Combine();
			return MainFunctions.Empty;
		}
		public override MainFunctions VisitNamespaceDeclaration(AdamantParser.NamespaceDeclarationContext context)
		{
			context.declaration().Select(d => d.Accept(this)).Combine();
			return MainFunctions.Empty;
		}
		public override MainFunctions VisitFunctionDeclaration(AdamantParser.FunctionDeclarationContext context)
		{
			return MainFunctions.Empty;
		}
		#endregion

		#region Members
		public override MainFunctions VisitConstructor(AdamantParser.ConstructorContext context)
		{
			var constructorName = context.name?.GetText();
			if(constructorName != null && !names.Contains(constructorName))
			{
				names.Add(constructorName);
				O.BlankLine();
				O.WriteIndentedLine($"internal partial struct אCtorName_{constructorName} {{}}");
			}
			return MainFunctions.Empty;
		}

		public override MainFunctions VisitMethod(AdamantParser.MethodContext context)
		{
			return MainFunctions.Empty;
		}

		public override MainFunctions VisitField(AdamantParser.FieldContext context)
		{
			return MainFunctions.Empty;
		}
		#endregion
	}
}
