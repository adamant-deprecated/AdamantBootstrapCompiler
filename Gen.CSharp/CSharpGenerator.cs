using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Adamant.Compiler.Antlr;
using Antlr4.Runtime.Tree;

namespace Adamant.Compiler.Gen.CSharp
{
	public class CSharpGenerator : AdamantParserBaseVisitor<MainFunctions>
	{
		protected readonly CSharpWriter O;
		protected readonly string CurrentClassName;

		public CSharpGenerator(TextWriter output)
		{
			O = new CSharpWriter(output);
		}

		protected CSharpGenerator(CSharpWriter writer)
			: this(writer, null)
		{
		}

		protected CSharpGenerator(CSharpWriter writer, string currentClassName)
		{
			O = writer;
			CurrentClassName = currentClassName;
		}

		#region Formatting
		protected static string Format(IEnumerable<AdamantParser.ModifierContext> modifiers)
		{
			return string.Concat(modifiers.Select(Format));
		}

		protected static string Format(AdamantParser.ModifierContext modifier)
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

		protected static string Format(IEnumerable<AdamantParser.ParameterModifierContext> modifiers)
		{
			return string.Concat(modifiers.Select(Format));
		}

		protected static string Format(AdamantParser.ParameterModifierContext modifier)
		{
			return modifier.GetText() + " ";
		}
		#endregion

		#region Inspection
		protected static bool IsAbstract(AdamantParser.ModifierContext[] modifiers)
		{
			return modifiers.Any(modifier => modifier.Symbol.Type == AdamantLexer.Abstract);
		}

		protected static bool IsAccessModifier(AdamantParser.ModifierContext modifier)
		{
			switch(modifier.Symbol.Type)
			{
				case AdamantLexer.Public:
				case AdamantLexer.Private:
				case AdamantLexer.Protected:
				case AdamantLexer.Package:
					return true;
				default:
					return false;
			}
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
			O.WriteIndentedLine("using אRuntime;");
			O.BlankLine();
			O.WriteIndentedLine("namespace א");
			O.BeginBlock();
			context.Accept(new ConstructorNameGenerator(O));
			context.usingStatement().Select(u => u.Accept(this)).Combine();
			var metaData = context.declaration().Select(d => d.Accept(this)).Combine();
			O.EndBlock();
			return metaData.InNamespace("א");
		}

		public override MainFunctions VisitUsingStatement(AdamantParser.UsingStatementContext context)
		{
			O.WriteIndentedLine($"using א.{context.namespaceName().GetText()};");
			return MainFunctions.Empty;
		}

		#region Declarations
		public override MainFunctions VisitClassDeclaration(AdamantParser.ClassDeclarationContext context)
		{
			var className = context.name.GetText();
			context.Accept(new ConstructorGenerator(O, className));
			O.BlankLine();
			O.WriteIndented(Format(context.modifier()) + "partial class " + className);
			context.typeParameterList()?.Accept(this);
			context.baseTypes()?.Accept(this);
			O.WriteLine();
			O.BeginBlock();
			var classGenerator = new CSharpGenerator(O, className);
			context.member().Select(m => m.Accept(classGenerator)).Combine();
			O.EndBlock();
			return MainFunctions.Empty;
		}

		public override MainFunctions VisitNamespaceDeclaration(AdamantParser.NamespaceDeclarationContext context)
		{
			O.BlankLine();
			var namespaceName = context.namespaceName().GetText();
			O.WriteIndentedLine("namespace " + namespaceName);
			O.BeginBlock();
			context.usingStatement().Select(u => u.Accept(this)).Combine();
			var metaData = context.declaration().Select(d => d.Accept(this)).Combine();
			O.EndBlock();
			return metaData.InNamespace(namespaceName);
		}

		public override MainFunctions VisitFunctionDeclaration(AdamantParser.FunctionDeclarationContext context)
		{
			// Container static class
			O.BlankLine();
			O.WriteIndentedLine("public static partial class אFuncs");
			O.BeginBlock();
			// Declaration
			O.WriteIndented(Format(context.modifier()) + "static ");
			context.returnType.Accept(this);
			var functionName = context.name.GetText();
			O.Write(" " + functionName + "(");
			O.WriteList(context.parameterList()._parameters, this);
			O.WriteLine(")");
			// Body
			O.BeginBlock();
			context.methodBody().statement().Select(s => s.Accept(this)).Combine();
			if(context.returnType.GetText() == "void")
				O.WriteIndentedLine("return default(אVoid);");
			O.EndBlock();
			// End Container class
			O.EndBlock();
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
				O.Write(".");
			}
			switch(name)
			{
				case "self":
					name = "dynamic";
					break;
				case "void":
					name = "אVoid";
					break;
				case "any":
					name = "object";
					break;
			}
			O.Write(name);
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
			O.Write("[");
			O.Write(string.Join("", context._dimensions.Select(d => d.Text)));
			O.Write("]");
			return MainFunctions.Empty;
		}

		public override MainFunctions VisitMaybeType(AdamantParser.MaybeTypeContext context)
		{
			O.Write("Maybe<");
			context.typeName().Accept(this);
			O.Write(">");
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
			O.BlankLine();
			// Declaration
			O.WriteIndented(Format(context.modifier()));
			context.returnType.Accept(this);
			var functionName = context.name.GetText();
			O.Write(" " + functionName);
			context.typeArguments()?.Accept(this);
			O.Write("(");
			O.WriteList(context.parameterList()._parameters, this);
			O.Write(")");
			if(!IsAbstract(context.modifier()))
			{
				O.WriteLine();
				// Body
				O.BeginBlock();
				context.methodBody().statement().Select(s => s.Accept(this)).Combine();
				if(context.returnType.GetText() == "void")
					O.WriteIndentedLine("return default(אVoid);");
				O.EndBlock();
			}
			else
				O.WriteLine(";");
			return MainFunctions.Empty;
		}

		public override MainFunctions VisitField(AdamantParser.FieldContext context)
		{
			O.BlankLine();
			O.WriteIndented(Format(context.modifier()));
			context.ownershipType().Accept(this);
			O.Write(" ");
			O.Write(context.identifier().GetText());
			if(context.expression() != null)
			{
				O.Write(" = ");
				context.expression().Accept(this);
			}
			O.WriteLine(";");
			return MainFunctions.Empty;
		}

		public override MainFunctions VisitConstructor(AdamantParser.ConstructorContext context)
		{
			O.BlankLine();
			// Declaration
			O.WriteIndented(Format(context.modifier()));
			O.Write(CurrentClassName);
			O.Write("(");
			var constructorName = context.name?.GetText();
			if(constructorName != null)
			{
				O.Write("אCtorName_");
				O.Write(constructorName);
				O.Write(" אctorName, ");
			}
			O.WriteList(context.parameterList()._parameters, this);
			O.Write(")");
			O.WriteLine();
			// Body
			O.BeginBlock();
			context.methodBody().statement().Select(s => s.Accept(this)).Combine();
			O.EndBlock();
			return MainFunctions.Empty;
		}
		#endregion

		#region Statements
		public override MainFunctions VisitExpressionStatement(AdamantParser.ExpressionStatementContext context)
		{
			O.WriteIndent();
			context.expression().Accept(this);
			O.WriteLine(";");
			return MainFunctions.Empty;
		}

		public override MainFunctions VisitBlockStatement(AdamantParser.BlockStatementContext context)
		{
			O.BeginBlock();
			context.statement().Select(s => s.Accept(this)).Combine();
			O.EndBlock();
			return MainFunctions.Empty;
		}

		public override MainFunctions VisitReturnStatement(AdamantParser.ReturnStatementContext context)
		{
			O.WriteIndented("return ");
			context.expression().Accept(this);
			O.WriteLine(";");
			return MainFunctions.Empty;
		}

		public override MainFunctions VisitVariableDeclarationStatement(AdamantParser.VariableDeclarationStatementContext context)
		{
			O.WriteIndent();
			context.variableDeclaration().Accept(this);
			O.WriteLine(";");
			return MainFunctions.Empty;
		}

		public override MainFunctions VisitIfStatement(AdamantParser.IfStatementContext context)
		{
			O.WriteIndented("if(");
			context.condition.Accept(this);
			O.WriteLine(")");
			context.then.Accept(this);
			if(context.@else != null)
			{
				O.WriteIndentedLine("else");
				context.@else.Accept(this);
			}
			return MainFunctions.Empty;
		}

		public override MainFunctions VisitThrowStatement(AdamantParser.ThrowStatementContext context)
		{
			O.WriteIndented("throw ");
			context.expression().Accept(this);
			O.WriteLine(";");
			return MainFunctions.Empty;
		}
		#endregion

		#region Expressions
		public override MainFunctions VisitCallExpression(AdamantParser.CallExpressionContext context)
		{
			context.expression().Accept(this);
			O.Write("(");
			O.WriteList(context.argumentList().expression(), this);
			O.Write(")");
			return MainFunctions.Empty;
		}

		public override MainFunctions VisitMemberExpression(AdamantParser.MemberExpressionContext context)
		{
			context.expression().Accept(this);
			O.Write("." + context.identifier().GetText());
			return MainFunctions.Empty;
		}

		public override MainFunctions VisitVariableExpression(AdamantParser.VariableExpressionContext context)
		{
			O.Write(context.identifier().GetText());
			return MainFunctions.Empty;
		}

		public override MainFunctions VisitStringLiteralExpression(AdamantParser.StringLiteralExpressionContext context)
		{
			O.Write(context.GetText());
			return MainFunctions.Empty;
		}

		public override MainFunctions VisitArrayAccessExpression(AdamantParser.ArrayAccessExpressionContext context)
		{
			context.expression().Accept(this);
			O.Write("[");
			O.WriteList(context.argumentList().expression(), this);
			O.Write("]");
			return MainFunctions.Empty;
		}

		public override MainFunctions VisitNewExpression(AdamantParser.NewExpressionContext context)
		{


			context.typeName().Accept(this);
			O.Write(".אCtor");
			O.Write("(");
			O.WriteList(context.argumentList().expression(), this);
			O.Write(")");
			return MainFunctions.Empty;
		}

		public override MainFunctions VisitUnaryExpression(AdamantParser.UnaryExpressionContext context)
		{
			var op = context.op.Text;
			switch(op)
			{
				case "not":
					O.Write("!");
					break;
				default:
					O.Write(op);
					break;
			}
			context.expression().Accept(this);
			return MainFunctions.Empty;
		}

		public override MainFunctions VisitNullLiteralExpression(AdamantParser.NullLiteralExpressionContext context)
		{
			O.Write("null");
			return MainFunctions.Empty;
		}

		public override MainFunctions VisitBooleanLiteralExpression(AdamantParser.BooleanLiteralExpressionContext context)
		{
			O.Write(context.BooleanLiteral().GetText());
			return MainFunctions.Empty;
		}

		public override MainFunctions VisitIntLiteralExpression(AdamantParser.IntLiteralExpressionContext context)
		{
			O.Write(context.IntLiteral().GetText());
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
			O.Write($" {op} ");
			context.rvalue.Accept(this);
			return MainFunctions.Empty;
		}

		public override MainFunctions VisitThisExpression(AdamantParser.ThisExpressionContext context)
		{
			O.Write("this");
			return MainFunctions.Empty;
		}

		public override MainFunctions VisitEqualityExpression(AdamantParser.EqualityExpressionContext context)
		{
			context.lhs.Accept(this);
			var op = context.op.Text;
			if(op == "<>")
				op = "!=";
			O.Write($" {op} ");
			context.rhs.Accept(this);
			return MainFunctions.Empty;
		}

		public override MainFunctions VisitIfExpression(AdamantParser.IfExpressionContext context)
		{
			O.Write("(");
			context.condition.Accept(this);
			O.Write(" ? ");
			context.then.Accept(this);
			O.Write(" : ");
			context.@else.Accept(this);
			O.Write(")");
			return MainFunctions.Empty;
		}
		#endregion

		public override MainFunctions VisitVariableDeclaration(AdamantParser.VariableDeclarationContext context)
		{
			if(context.ownershipType() != null)
				context.ownershipType().Accept(this);
			else
				O.Write("var");

			O.Write(" ");
			O.Write(context.identifier().GetText());
			if(context.expression() != null)
			{
				O.Write(" = ");
				context.expression().Accept(this);
			}
			return MainFunctions.Empty;
		}

		public override MainFunctions VisitParameter(AdamantParser.ParameterContext context)
		{
			O.Write(Format(context._modifiers));
			context.type.Accept(this);
			O.Write(" " + context.name.GetText());
			return MainFunctions.Empty;
		}

		public override MainFunctions VisitTypeArguments(AdamantParser.TypeArgumentsContext context)
		{
			O.Write("<");
			O.WriteList(context.ownershipType(), this);
			O.Write(">");
			return MainFunctions.Empty;
		}

		public override MainFunctions VisitBaseTypes(AdamantParser.BaseTypesContext context)
		{
			var hasBaseClass = context.baseType != null;
			var hasInterfaces = context._interfaces.Any();
			if(hasBaseClass || hasInterfaces)
				O.Write(" : ");

			if(hasBaseClass)
				context.baseType.Accept(this);

			if(hasInterfaces)
				O.WriteList(context._interfaces, this);

			return MainFunctions.Empty;
		}

		public override MainFunctions VisitTypeParameterList(AdamantParser.TypeParameterListContext context)
		{
			O.Write("<");
			O.WriteList(context.typeParameter(), this);
			O.Write(">");
			return MainFunctions.Empty;
		}

		public override MainFunctions VisitTypeParameter(AdamantParser.TypeParameterContext context)
		{
			O.Write(context.name.GetText());
			// TODO how to handle lists and base types?
			return MainFunctions.Empty;
		}
	}
}
