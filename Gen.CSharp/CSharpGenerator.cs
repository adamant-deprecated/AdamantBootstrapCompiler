using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Adamant.Compiler.Antlr;
using Adamant.Compiler.Ast;
using Antlr4.Runtime.Tree;

namespace Adamant.Compiler.Gen.CSharp
{
	public class CSharpGenerator : AdamantParserBaseVisitor<GenMetaData>
	{
		private readonly TextWriter output;
		private readonly StringBuilder indent = new StringBuilder();
		private bool firstElement;

		public CSharpGenerator(TextWriter output)
		{
			this.output = output;
		}

		#region Formatting
		private void Indent()
		{
			indent.Append('\t');
			firstElement = true;
		}

		private void Outdent()
		{
			if(indent.Length == 0)
				throw new InvalidOperationException("Can't outdent top level");

			indent.Length = indent.Length - 1;
		}

		private void BlankLine()
		{
			if(firstElement)
				firstElement = false;
			else
				output.WriteLine();
		}

		private void BeginBlock()
		{
			output.WriteLine(indent + "{");
			Indent();
		}

		private void EndBlock()
		{
			Outdent();
			output.WriteLine(indent + "}");
		}
		#endregion

		#region Generic Visit Methods
		public override GenMetaData Visit(IParseTree tree)
		{
			throw new NotSupportedException("Generic visit methods should not be called.");
		}

		public override GenMetaData VisitChildren(IRuleNode node)
		{
			throw new NotSupportedException("Generic visit methods should not be called.");
		}

		public override GenMetaData VisitTerminal(ITerminalNode node)
		{
			throw new NotSupportedException("Generic visit methods should not be called.");
		}

		public override GenMetaData VisitErrorNode(IErrorNode node)
		{
			throw new NotSupportedException("Generic visit methods should not be called.");
		}
		#endregion

		public override GenMetaData VisitCompilationUnit(AdamantParser.CompilationUnitContext context)
		{
			output.WriteLine(indent + "using אRuntime;");
			BlankLine();
			output.WriteLine(indent + "namespace א");
			BeginBlock();
			context.usingStatement().Select(u => u.Accept(this)).Combine();
			var metaData = context.declaration().Select(d => d.Accept(this)).Combine();
			EndBlock();
			return metaData.InNamespace("א");
		}

		public override GenMetaData VisitUsingStatement(AdamantParser.UsingStatementContext context)
		{
			firstElement = false;
            output.WriteLine(indent + $"using א.{context.namespaceName().GetText()};");
			return GenMetaData.Empty;
		}

		#region Declarations
		public override GenMetaData VisitNamespaceDeclaration(AdamantParser.NamespaceDeclarationContext context)
		{
			BlankLine();
			var namespaceName = context.namespaceName().GetText();
			output.WriteLine(indent + "namespace " + namespaceName);
			BeginBlock();
			context.usingStatement().Select(u => u.Accept(this)).Combine();
			var metaData = context.declaration().Select(d => d.Accept(this)).Combine();
			EndBlock();
			return metaData.InNamespace(namespaceName);
		}

		public override GenMetaData VisitFunctionDeclaration(AdamantParser.FunctionDeclarationContext context)
		{
			// Container static class
			BlankLine();
			output.WriteLine(indent + "public static partial class אFuncs");
			BeginBlock();
			// Declaration
			output.Write(indent + string.Join(" ", context.modifier().Select(m => m.GetText())) + " static ");
			context.returnType.Accept(this);
			var functionName = context.name.GetText();
			output.Write(" " + functionName + "(");
			var firstParam = true;
			foreach(var parameter in context.parameterList()._parameters)
			{
				if(firstParam)
					firstParam = false;
				else
					output.Write(", ");

				parameter.Accept(this);
			}
			output.WriteLine(")");
			// Body
			BeginBlock();
			context.methodBody().statement().Select(s => s.Accept(this)).Combine();
			EndBlock();
			// End Container class
			EndBlock();
			return functionName == "Main" ? new GenMetaData("אFuncs.Main") : GenMetaData.Empty;
		}

		public override GenMetaData VisitParameter(AdamantParser.ParameterContext context)
		{
			context.type.Accept(this);
			output.Write(" " + context.name.GetText());
			return GenMetaData.Empty;
		}
		#endregion

		#region Types

		public override GenMetaData VisitImplicitType(AdamantParser.ImplicitTypeContext context)
		{
			return context.plainType().Accept(this);
		}

		public override GenMetaData VisitNamedType(AdamantParser.NamedTypeContext context)
		{
			return context.typeName().Accept(this);
		}

		public override GenMetaData VisitTypeName(AdamantParser.TypeNameContext context)
		{
			if(context.outerType != null)
			{
				context.outerType.Accept(this);
				output.Write(".");
			}
			output.Write(context.identifier().GetText());
			// TODO type args
			return GenMetaData.Empty;
		}

		public override GenMetaData VisitMutableType(AdamantParser.MutableTypeContext context)
		{
			return context.plainType().Accept(this);
		}

		public override GenMetaData VisitArraySliceType(AdamantParser.ArraySliceTypeContext context)
		{
			context.elementType.Accept(this);
			output.Write("[");
			output.Write(string.Join("", context._dimensions.Select(d => d.Text)));
			output.Write("]");
			return GenMetaData.Empty;
		}
		#endregion

		#region Statements
		public override GenMetaData VisitExpressionStatement(AdamantParser.ExpressionStatementContext context)
		{
			output.Write(indent);
			context.expression().Accept(this);
			output.WriteLine(";");
			return GenMetaData.Empty;
		}
		#endregion

		#region Expressions

		public override GenMetaData VisitCallExpression(AdamantParser.CallExpressionContext context)
		{
			context.expression().Accept(this);
			output.Write("(");
			var firstParam = true;
			foreach(var argument in context.argumentList().expression())
			{
				if(firstParam)
					firstParam = false;
				else
					output.Write(", ");

				argument.Accept(this);
			}
			output.Write(")");
			return GenMetaData.Empty;
		}

		public override GenMetaData VisitMemberExpression(AdamantParser.MemberExpressionContext context)
		{
			context.expression().Accept(this);
			output.Write("." + context.identifier().GetText());
			return GenMetaData.Empty;
		}

		public override GenMetaData VisitVariableExpression(AdamantParser.VariableExpressionContext context)
		{
			output.Write(context.identifier().GetText());
			return GenMetaData.Empty;
		}

		public override GenMetaData VisitStringLiteralExpression(AdamantParser.StringLiteralExpressionContext context)
		{
			output.Write(context.GetText());
			return GenMetaData.Empty;
		}
		#endregion
	}
}
