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
				childSymbolTable.Using(@using.namespaceName().GetText());

			var visitor = new BuildAstVisitor(childSymbolTable, currentNamespace);
			var declarations = context.namespaceMemberDeclaration().Select(d => (Declaration)d.Accept(visitor));
			return new Assemblage(declarations);
		}

		public override Node VisitIdentifier(AdamantParser.IdentifierContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitNamespaceName(AdamantParser.NamespaceNameContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitNamespaceMemberDeclaration(AdamantParser.NamespaceMemberDeclarationContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitNamespaceDeclaration(AdamantParser.NamespaceDeclarationContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitTypeDeclaration(AdamantParser.TypeDeclarationContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitGlobalDeclaration(AdamantParser.GlobalDeclarationContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitAttribute(AdamantParser.AttributeContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitModifier(AdamantParser.ModifierContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitTypeParameterList(AdamantParser.TypeParameterListContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitTypeParameter(AdamantParser.TypeParameterContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitTypeBase(AdamantParser.TypeBaseContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitTypeName(AdamantParser.TypeNameContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitTypeArguments(AdamantParser.TypeArgumentsContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitType(AdamantParser.TypeContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitFuncTypeParameterList(AdamantParser.FuncTypeParameterListContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitFuncTypeParameter(AdamantParser.FuncTypeParameterContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitConstExpression(AdamantParser.ConstExpressionContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitTypeParameterConstraintClause(AdamantParser.TypeParameterConstraintClauseContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitConstructorConstraint(AdamantParser.ConstructorConstraintContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitTypeConstraint(AdamantParser.TypeConstraintContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitTypeListParameterConstraint(AdamantParser.TypeListParameterConstraintContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitTypeMember(AdamantParser.TypeMemberContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitParameterList(AdamantParser.ParameterListContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitParameter(AdamantParser.ParameterContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitParameterModifier(AdamantParser.ParameterModifierContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitConstructor(AdamantParser.ConstructorContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitConstructorInitializer(AdamantParser.ConstructorInitializerContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitArgumentList(AdamantParser.ArgumentListContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitDestructor(AdamantParser.DestructorContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitMethod(AdamantParser.MethodContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitOperatorOverload(AdamantParser.OperatorOverloadContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitConversionMethod(AdamantParser.ConversionMethodContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitProperty(AdamantParser.PropertyContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitMethodBody(AdamantParser.MethodBodyContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitOverloadableOperator(AdamantParser.OverloadableOperatorContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitField(AdamantParser.FieldContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitVariableDeclarationStatement(AdamantParser.VariableDeclarationStatementContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitLetDeclarationStatement(AdamantParser.LetDeclarationStatementContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitUnsafeBlockStatement(AdamantParser.UnsafeBlockStatementContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitBlock(AdamantParser.BlockContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitEmptyStatement(AdamantParser.EmptyStatementContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitExpressionStatement(AdamantParser.ExpressionStatementContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitReturnStatement(AdamantParser.ReturnStatementContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitThrowStatement(AdamantParser.ThrowStatementContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitIfStatement(AdamantParser.IfStatementContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitForStatement(AdamantParser.ForStatementContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitForeachStatement(AdamantParser.ForeachStatementContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitDeleteStatement(AdamantParser.DeleteStatementContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitVariableDeclaration(AdamantParser.VariableDeclarationContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitLetDeclaration(AdamantParser.LetDeclarationContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitNullCheckExpression(AdamantParser.NullCheckExpressionContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitStringLiteralExpression(AdamantParser.StringLiteralExpressionContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitXorExpression(AdamantParser.XorExpressionContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitUnaryExpression(AdamantParser.UnaryExpressionContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitOrExpression(AdamantParser.OrExpressionContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitIntLiteralExpression(AdamantParser.IntLiteralExpressionContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitThisExpression(AdamantParser.ThisExpressionContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitAndExpression(AdamantParser.AndExpressionContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitAssignmentExpression(AdamantParser.AssignmentExpressionContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitBooleanLiteralExpression(AdamantParser.BooleanLiteralExpressionContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitEqualityExpression(AdamantParser.EqualityExpressionContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitMultiplicativeExpression(AdamantParser.MultiplicativeExpressionContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitCallExpression(AdamantParser.CallExpressionContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitNullLiteralExpression(AdamantParser.NullLiteralExpressionContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitAdditiveExpression(AdamantParser.AdditiveExpressionContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitIfExpression(AdamantParser.IfExpressionContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitPointerMemberExpression(AdamantParser.PointerMemberExpressionContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitNewExpression(AdamantParser.NewExpressionContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitUninitializedExpression(AdamantParser.UninitializedExpressionContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitVariableExpression(AdamantParser.VariableExpressionContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitShiftExpression(AdamantParser.ShiftExpressionContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitMemberExpression(AdamantParser.MemberExpressionContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitComparativeExpression(AdamantParser.ComparativeExpressionContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitCoalesceExpression(AdamantParser.CoalesceExpressionContext context)
		{
			throw new NotImplementedException();
		}

		public override Node VisitArrayAccessExpression(AdamantParser.ArrayAccessExpressionContext context)
		{
			throw new NotImplementedException();
		}
	}
}
