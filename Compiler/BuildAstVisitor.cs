using System;
using System.Collections.Generic;
using System.Linq;
using Adamant.Compiler.Antlr;
using Adamant.Compiler.Ast;
using Adamant.Compiler.Ast.Declarations;
using Adamant.Compiler.Ast.Expressions;
using Adamant.Compiler.Ast.Members;
using Antlr4.Runtime.Tree;

namespace Adamant.Compiler
{
	public class BuildAstVisitor : AdamantParserBaseVisitor<Node>
	{
		private readonly UsingContext usingContext;
		private readonly QualifiedName currentNamespace;

		public BuildAstVisitor()
		{
			usingContext = new UsingContext();
			currentNamespace = QualifiedName.None;
		}

		public BuildAstVisitor(UsingContext usingContext, QualifiedName currentNamespace)
		{
			this.usingContext = usingContext;
			this.currentNamespace = currentNamespace;
		}

		public override Node Visit(IParseTree tree)
		{
			throw new NotSupportedException("Generic visit methods should not be called.");
		}

		public override Node VisitChildren(IRuleNode node)
		{
			throw new NotSupportedException("Generic visit methods should not be called.");
		}

		public override Node VisitTerminal(ITerminalNode node)
		{
			throw new NotSupportedException("Generic visit methods should not be called.");
		}

		public override Node VisitErrorNode(IErrorNode node)
		{
			throw new NotSupportedException("Generic visit methods should not be called.");
		}

		public override Node VisitCompilationUnit(AdamantParser.CompilationUnitContext context)
		{
			// TODO global attributes
			var newContext = new UsingContext(usingContext, GetNamespaces(context.usingStatement()));
			var visitor = new BuildAstVisitor(newContext, currentNamespace);
			var declarations = context.declaration().Select(d => (IDeclarationContainer)d.Accept(visitor));
			return new Assemblage(declarations);
		}

		#region Declarations
		public override Node VisitNamespaceDeclaration(AdamantParser.NamespaceDeclarationContext context)
		{
			var namespaceName = context.namespaceName().GetText();
			var newContext = new UsingContext(usingContext, GetNamespaces(context.usingStatement()));
			var visitor = new BuildAstVisitor(newContext, currentNamespace.Append(namespaceName));
			var declarations = context.declaration().Select(d => (IDeclarationContainer)d.Accept(visitor));
			return new Assemblage(declarations);
		}

		public override Node VisitClassDeclaration(AdamantParser.ClassDeclarationContext context)
		{
			// TODO Attributes
			// TODO what about immutable for classes?
			var accessModifier = GetAccessModifier(context.modifier());
			var isPartial = Has(context.modifier(), AdamantLexer.Partial);
			var safety = GetSafety(context.modifier());
			var isAbstract = Has(context.modifier(), AdamantLexer.Abstract);
			var isSealed = Has(context.modifier(), AdamantLexer.Sealed);
			var name = context.name.GetText();
			var fullName = currentNamespace.Append(name);
			// TODO base types
			// TODO type parameter constraints
			var members = context.member().Select(m => (Member)m.Accept(this));
			return new ClassDeclaration(accessModifier, isPartial, safety, isSealed, isAbstract, fullName, members);
		}

		public override Node VisitGlobalDeclaration(AdamantParser.GlobalDeclarationContext context)
		{
			var accessModifier = GetAccessModifier(context.modifier());
			var name = context.name.GetText();
			var fullName = currentNamespace.Append(name);
			var initExpression = context.expression()?.Accept(this);
			return new GlobalDeclaration(accessModifier, fullName, initExpression);
		}

		#endregion

		#region Members
		public override Node VisitField(AdamantParser.FieldContext context)
		{
			var accessModifier = GetAccessModifier(context.modifier());
			var isLet = context.kind.Type == AdamantLexer.Let;
			var isMutable = !isLet || context.mutable != null;
			var name = new Name(context.identifier().GetText());
			var type = context.type()?.Accept(this);
			var initExpression = (Expression)context.expression()?.Accept(this);
			return new Field(accessModifier, !isLet, isMutable, name, initExpression);
		}
		#endregion

		#region Expressions
		public override Node VisitNewObjectExpression(AdamantParser.NewObjectExpressionContext context)
		{
			var baseTypes = context.baseTypes();
			var baseClass = baseTypes?.baseType;
			var interfaces = baseTypes?._interfaces;
			var members = context.member().Select(m => (Member)m.Accept(this));
			return new NewObjectExpression(members);
		}

		public override Node VisitIntLiteralExpression(AdamantParser.IntLiteralExpressionContext context)
		{
			return new LiteralExpression();
		}

		#endregion

		private static IEnumerable<QualifiedName> GetNamespaces(AdamantParser.UsingStatementContext[] contexts)
		{
			return contexts.Select(s => new QualifiedName(s.namespaceName().GetText()));
		}

		private static bool Has(AdamantParser.ModifierContext[] modifiers, int desiredModifier)
		{
			return modifiers.Any(modifier => modifier.Symbol.Type == desiredModifier);
		}

		private static AccessModifier GetAccessModifier(AdamantParser.ModifierContext[] modifiers)
		{
			// Return the first access modifier
			foreach(var modifier in modifiers)
				switch(modifier.Symbol.Type)
				{
					case AdamantLexer.Public:
						return AccessModifier.Public;
					case AdamantLexer.Protected:
						return AccessModifier.Protected;
					case AdamantLexer.Package:
						return AccessModifier.Package;
					case AdamantLexer.Private:
						return AccessModifier.Private;
				}
			// If we don't find an acces modifier
			return AccessModifier.Private;
		}

		private static Safety GetSafety(AdamantParser.ModifierContext[] modifiers)
		{
			foreach(var modifier in modifiers)
				switch(modifier.Symbol.Type)
				{
					case AdamantLexer.Safe:
						return Safety.ExplicitSafe;
					case AdamantLexer.Unsafe:
						return Safety.Unsafe;
				}

			return Safety.ImplicitSafe;
		}
	}
}
