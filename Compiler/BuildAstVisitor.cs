using System;
using System.Linq;
using Adamant.Compiler.Antlr;
using Adamant.Compiler.Ast;
using Adamant.Compiler.Symbols;
using Antlr4.Runtime.Tree;

namespace Adamant.Compiler
{
	public class BuildAstVisitor : AdamantParserBaseVisitor<Node>
	{
		private readonly ChildSymbolTable symbolTable;
		private readonly QualifiedName currentNamespace;

		public BuildAstVisitor()
		{
			currentNamespace = QualifiedName.None;
		}

		public BuildAstVisitor(ChildSymbolTable symbolTable, QualifiedName currentNamespace)
		{
			this.symbolTable = symbolTable;
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
			var childSymbolTable = SymbolTable.NewRoot();
			foreach(var @using in context.usingStatement())
				childSymbolTable.Using(new QualifiedName(@using.namespaceName().GetText()));

			var visitor = new BuildAstVisitor(childSymbolTable, currentNamespace);
			var declarations = context.declaration().Select(d => (Declaration)d.Accept(visitor));
			return new Assemblage(declarations);
		}

		public override Node VisitGlobalDeclaration(AdamantParser.GlobalDeclarationContext context)
		{
			var name = context.identifier().GetText();
			return new GlobalDeclaration(GetAccessModifier(context.modifier()), currentNamespace.Append(name));
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
	}
}
