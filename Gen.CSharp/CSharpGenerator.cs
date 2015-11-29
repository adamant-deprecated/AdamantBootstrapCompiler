using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Adamant.Compiler.Antlr;
using Adamant.Compiler.Ast;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace Adamant.Compiler.Gen.CSharp
{
	public class CSharpGenerator : AdamantParserBaseVisitor<MainFunctions>
	{
		private readonly CSharpWriter o;
		private readonly string currentClassName;

		public CSharpGenerator(TextWriter output)
		{
			o = new CSharpWriter(output);
		}

		private CSharpGenerator(CSharpWriter writer, string currentClassName)
		{
			o = writer;
			this.currentClassName = currentClassName;
		}

		#region Formatting
		private static string Format(IEnumerable<AdamantParser.ModifierContext> modifiers)
		{
			return string.Concat(modifiers.Select(Format));
		}

		private static string Format(AdamantParser.ModifierContext modifier)
		{
			var text = modifier.GetText();
			switch(text)
			{
				case "package":
					return "internal ";
				default:
					return text + " ";
			}
		}

		private static string Format(IEnumerable<AdamantParser.ParameterModifierContext> modifiers)
		{
			return string.Concat(modifiers.Select(Format));
		}

		private static string Format(AdamantParser.ParameterModifierContext modifier)
		{
			return modifier.GetText() + " ";
		}
		#endregion

		#region Inspection
		private static bool IsAbstract(AdamantParser.ModifierContext[] modifiers)
		{
			return modifiers.Any(modifier => modifier.Symbol.Type == AdamantLexer.Abstract);
		}
		#endregion

		#region Generic Visit Methods
		public override MainFunctions Visit(IParseTree tree)
		{
			throw new NotSupportedException("Generic visit methods should not be called.");
		}

		public override MainFunctions VisitChildren(IRuleNode node)
		{
			throw new NotSupportedException("Generic visit methods should not be called.");
		}

		public override MainFunctions VisitTerminal(ITerminalNode node)
		{
			throw new NotSupportedException("Generic visit methods should not be called.");
		}

		public override MainFunctions VisitErrorNode(IErrorNode node)
		{
			throw new NotSupportedException("Generic visit methods should not be called.");
		}
		#endregion

		public override MainFunctions VisitCompilationUnit(AdamantParser.CompilationUnitContext context)
		{
			o.WriteIndentedLine("using אRuntime;");
			o.BlankLine();
			o.WriteIndentedLine("namespace א");
			o.BeginBlock();
			context.usingStatement().Select(u => u.Accept(this)).Combine();
			var metaData = context.declaration().Select(d => d.Accept(this)).Combine();
			o.EndBlock();
			return metaData.InNamespace("א");
		}

		public override MainFunctions VisitUsingStatement(AdamantParser.UsingStatementContext context)
		{
			o.WriteIndentedLine($"using א.{context.namespaceName().GetText()};");
			return MainFunctions.Empty;
		}

		#region Declarations
		public override MainFunctions VisitClassDeclaration(AdamantParser.ClassDeclarationContext context)
		{
			o.BlankLine();
			var className = context.name.GetText();
			o.WriteIndented(Format(context.modifier()) + "class " + className);
			context.typeParameterList()?.Accept(this);
			context.baseTypes()?.Accept(this);
			o.WriteLine();
			o.BeginBlock();
			var classGenerator = new CSharpGenerator(o, className);
			context.member().Select(m => m.Accept(classGenerator)).Combine();
			o.EndBlock();
			return MainFunctions.Empty;
		}

		public override MainFunctions VisitNamespaceDeclaration(AdamantParser.NamespaceDeclarationContext context)
		{
			o.BlankLine();
			var namespaceName = context.namespaceName().GetText();
			o.WriteIndentedLine("namespace " + namespaceName);
			o.BeginBlock();
			context.usingStatement().Select(u => u.Accept(this)).Combine();
			var metaData = context.declaration().Select(d => d.Accept(this)).Combine();
			o.EndBlock();
			return metaData.InNamespace(namespaceName);
		}

		public override MainFunctions VisitFunctionDeclaration(AdamantParser.FunctionDeclarationContext context)
		{
			// Container static class
			o.BlankLine();
			o.WriteIndentedLine("public static partial class אFuncs");
			o.BeginBlock();
			// Declaration
			o.WriteIndented(Format(context.modifier()) + "static ");
			context.returnType.Accept(this);
			var functionName = context.name.GetText();
			o.Write(" " + functionName + "(");
			o.WriteList(context.parameterList()._parameters, this);
			o.WriteLine(")");
			// Body
			o.BeginBlock();
			context.methodBody().statement().Select(s => s.Accept(this)).Combine();
			o.EndBlock();
			// End Container class
			o.EndBlock();
			return functionName == "Main" ? new MainFunctions("אFuncs.Main", context.returnType.GetText()) : MainFunctions.Empty;
		}
		#endregion

		#region Types
		public override MainFunctions VisitImplicitType(AdamantParser.ImplicitTypeContext context)
		{
			return context.plainType().Accept(this);
		}

		public override MainFunctions VisitNamedType(AdamantParser.NamedTypeContext context)
		{
			return context.typeName().Accept(this);
		}

		public override MainFunctions VisitTypeName(AdamantParser.TypeNameContext context)
		{
			var name = context.identifier().GetText();
			if(context.outerType != null)
			{
				context.outerType.Accept(this);
				o.Write(".");
			}
			o.Write(name == "self" ? "dynamic" : name);
			context.typeArguments()?.Accept(this);
			return MainFunctions.Empty;
		}

		public override MainFunctions VisitMutableType(AdamantParser.MutableTypeContext context)
		{
			return context.plainType().Accept(this);
		}

		public override MainFunctions VisitArraySliceType(AdamantParser.ArraySliceTypeContext context)
		{
			context.elementType.Accept(this);
			o.Write("[");
			o.Write(string.Join("", context._dimensions.Select(d => d.Text)));
			o.Write("]");
			return MainFunctions.Empty;
		}

		public override MainFunctions VisitMaybeType(AdamantParser.MaybeTypeContext context)
		{
			o.Write("Maybe<");
			context.typeName().Accept(this);
			o.Write(">");
			return MainFunctions.Empty;
		}

		public override MainFunctions VisitOwnedType(AdamantParser.OwnedTypeContext context)
		{
			return context.plainType().Accept(this);
		}
		#endregion

		#region Members
		public override MainFunctions VisitMethod(AdamantParser.MethodContext context)
		{
			o.BlankLine();
			// Declaration
			o.WriteIndented(Format(context.modifier()) + " ");
			context.returnType.Accept(this);
			var functionName = context.name.GetText();
			o.Write(" " + functionName);
			context.typeArguments()?.Accept(this);
			o.Write("(");
			o.WriteList(context.parameterList()._parameters, this);
			o.Write(")");
			if(!IsAbstract(context.modifier()))
			{
				o.WriteLine();
				// Body
				o.BeginBlock();
				context.methodBody().statement().Select(s => s.Accept(this)).Combine();
				o.EndBlock();
			}
			else
				o.WriteLine(";");
			return MainFunctions.Empty;
		}

		public override MainFunctions VisitField(AdamantParser.FieldContext context)
		{
			o.BlankLine();
			o.WriteIndented(Format(context.modifier()));
			context.ownershipType().Accept(this);
			o.Write(" ");
			o.Write(context.identifier().GetText());
			if(context.expression() != null)
			{
				o.Write(" = ");
				context.expression().Accept(this);
			}
			o.WriteLine(";");
			return MainFunctions.Empty;
		}

		public override MainFunctions VisitConstructor(AdamantParser.ConstructorContext context)
		{
			o.BlankLine();
			// Declaration
			o.WriteIndented(Format(context.modifier()));
			o.Write(currentClassName);
			o.Write("(");
			o.WriteList(context.parameterList()._parameters, this);
			o.Write(")");
			o.WriteLine();
			// Body
			o.BeginBlock();
			context.methodBody().statement().Select(s => s.Accept(this)).Combine();
			o.EndBlock();
			return MainFunctions.Empty;
		}
		#endregion

		#region Statements
		public override MainFunctions VisitExpressionStatement(AdamantParser.ExpressionStatementContext context)
		{
			o.WriteIndent();
			context.expression().Accept(this);
			o.WriteLine(";");
			return MainFunctions.Empty;
		}

		public override MainFunctions VisitBlockStatement(AdamantParser.BlockStatementContext context)
		{
			o.BeginBlock();
			context.statement().Select(s => s.Accept(this)).Combine();
			o.EndBlock();
			return MainFunctions.Empty;
		}

		public override MainFunctions VisitReturnStatement(AdamantParser.ReturnStatementContext context)
		{
			o.WriteIndented("return ");
			context.expression().Accept(this);
			o.WriteLine(";");
			return MainFunctions.Empty;
		}

		public override MainFunctions VisitVariableDeclarationStatement(AdamantParser.VariableDeclarationStatementContext context)
		{
			o.WriteIndent();
			context.variableDeclaration().Accept(this);
			o.WriteLine(";");
			return MainFunctions.Empty;
		}

		public override MainFunctions VisitIfStatement(AdamantParser.IfStatementContext context)
		{
			o.WriteIndented("if(");
			context.condition.Accept(this);
			o.WriteLine(")");
			context.then.Accept(this);
			if(context.@else != null)
			{
				o.WriteIndentedLine("else");
				context.@else.Accept(this);
			}
			return MainFunctions.Empty;
		}

		public override MainFunctions VisitThrowStatement(AdamantParser.ThrowStatementContext context)
		{
			o.WriteIndented("throw ");
			context.expression().Accept(this);
			o.WriteLine(";");
			return MainFunctions.Empty;
		}
		#endregion

		#region Expressions
		public override MainFunctions VisitCallExpression(AdamantParser.CallExpressionContext context)
		{
			context.expression().Accept(this);
			o.Write("(");
			o.WriteList(context.argumentList().expression(), this);
			o.Write(")");
			return MainFunctions.Empty;
		}

		public override MainFunctions VisitMemberExpression(AdamantParser.MemberExpressionContext context)
		{
			context.expression().Accept(this);
			o.Write("." + context.identifier().GetText());
			return MainFunctions.Empty;
		}

		public override MainFunctions VisitVariableExpression(AdamantParser.VariableExpressionContext context)
		{
			o.Write(context.identifier().GetText());
			return MainFunctions.Empty;
		}

		public override MainFunctions VisitStringLiteralExpression(AdamantParser.StringLiteralExpressionContext context)
		{
			o.Write(context.GetText());
			return MainFunctions.Empty;
		}

		public override MainFunctions VisitArrayAccessExpression(AdamantParser.ArrayAccessExpressionContext context)
		{
			context.expression().Accept(this);
			o.Write("[");
			o.WriteList(context.argumentList().expression(), this);
			o.Write("]");
			return MainFunctions.Empty;
		}

		public override MainFunctions VisitNewExpression(AdamantParser.NewExpressionContext context)
		{
			o.Write("new ");
			context.typeName().Accept(this);
			// TODO handle named constructors
			o.Write("(");
			o.WriteList(context.argumentList().expression(), this);
			o.Write(")");
			return MainFunctions.Empty;
		}

		public override MainFunctions VisitUnaryExpression(AdamantParser.UnaryExpressionContext context)
		{
			var op = context.op.Text;
			switch(op)
			{
				case "not":
					o.Write("!");
					break;
				default:
					o.Write(op);
					break;
			}
			context.expression().Accept(this);
			return MainFunctions.Empty;
		}

		public override MainFunctions VisitNullLiteralExpression(AdamantParser.NullLiteralExpressionContext context)
		{
			o.Write("null");
			return MainFunctions.Empty;
		}

		public override MainFunctions VisitBooleanLiteralExpression(AdamantParser.BooleanLiteralExpressionContext context)
		{
			o.Write(context.BooleanLiteral().GetText());
			return MainFunctions.Empty;
		}

		public override MainFunctions VisitIntLiteralExpression(AdamantParser.IntLiteralExpressionContext context)
		{
			o.Write(context.IntLiteral().GetText());
			return MainFunctions.Empty;
		}

		public override MainFunctions VisitAssignmentExpression(AdamantParser.AssignmentExpressionContext context)
		{
			context.lvalue.Accept(this);
			var op = context.op.Text;
			switch(op)
			{
				case "and=":
					op = "&&=";
					break;
				case "or=":
					op = "||=";
					break;
				case "xor=":
					op = "^=";
					break;
			}
			o.Write($" {op} ");
			context.rvalue.Accept(this);
			return MainFunctions.Empty;
		}

		public override MainFunctions VisitThisExpression(AdamantParser.ThisExpressionContext context)
		{
			o.Write("this");
			return MainFunctions.Empty;
		}

		public override MainFunctions VisitEqualityExpression(AdamantParser.EqualityExpressionContext context)
		{
			context.lhs.Accept(this);
			var op = context.op.Text;
			if(op == "<>")
				op = "!=";
			o.Write($" {op} ");
			context.rhs.Accept(this);
			return MainFunctions.Empty;
		}

		public override MainFunctions VisitIfExpression(AdamantParser.IfExpressionContext context)
		{
			o.Write("(");
			context.condition.Accept(this);
			o.Write(" ? ");
			context.then.Accept(this);
			o.Write(" : ");
			context.@else.Accept(this);
			o.Write(")");
			return MainFunctions.Empty;
		}
		#endregion

		public override MainFunctions VisitVariableDeclaration(AdamantParser.VariableDeclarationContext context)
		{
			if(context.ownershipType() != null)
				context.ownershipType().Accept(this);
			else
				o.Write("var");

			o.Write(" ");
			o.Write(context.identifier().GetText());
			if(context.expression() != null)
			{
				o.Write(" = ");
				context.expression().Accept(this);
			}
			return MainFunctions.Empty;
		}

		public override MainFunctions VisitParameter(AdamantParser.ParameterContext context)
		{
			o.Write(Format(context._modifiers));
			context.type.Accept(this);
			o.Write(" " + context.name.GetText());
			return MainFunctions.Empty;
		}

		public override MainFunctions VisitTypeArguments(AdamantParser.TypeArgumentsContext context)
		{
			o.Write("<");
			o.WriteList(context.ownershipType(), this);
			o.Write(">");
			return MainFunctions.Empty;
		}

		public override MainFunctions VisitBaseTypes(AdamantParser.BaseTypesContext context)
		{
			var hasBaseClass = context.baseType != null;
			var hasInterfaces = context._interfaces.Any();
			if(hasBaseClass || hasInterfaces)
				o.Write(" : ");

			if(hasBaseClass)
				context.baseType.Accept(this);

			if(hasInterfaces)
				o.WriteList(context._interfaces, this);

			return MainFunctions.Empty;
		}

		public override MainFunctions VisitTypeParameterList(AdamantParser.TypeParameterListContext context)
		{
			o.Write("<");
			o.WriteList(context.typeParameter(), this);
			o.Write(">");
			return MainFunctions.Empty;
		}

		public override MainFunctions VisitTypeParameter(AdamantParser.TypeParameterContext context)
		{
			o.Write(context.name.GetText());
			// TODO how to handle lists and base types?
			return MainFunctions.Empty;
		}
	}
}
