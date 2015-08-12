using System;
using Adamant.Compiler.Antlr;
using Adamant.Compiler.Ast;
using Antlr4.Runtime.Tree;

namespace Adamant.Compiler
{
	public class BuildAstVisitor : IAdamantParserVisitor<Node>
	{
		public Node Visit(IParseTree tree)
		{
			throw new NotImplementedException();
		}

		public Node VisitChildren(IRuleNode node)
		{
			throw new NotImplementedException();
		}

		public Node VisitTerminal(ITerminalNode node)
		{
			throw new NotImplementedException();
		}

		public Node VisitErrorNode(IErrorNode node)
		{
			throw new NotImplementedException();
		}

		public Node VisitCompilationUnit(AdamantParser.CompilationUnitContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitUsingStatement(AdamantParser.UsingStatementContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitIdentifier(AdamantParser.IdentifierContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitNamespaceName(AdamantParser.NamespaceNameContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitNamespaceMemberDeclaration(AdamantParser.NamespaceMemberDeclarationContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitNamespaceDeclaration(AdamantParser.NamespaceDeclarationContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitTypeDeclaration(AdamantParser.TypeDeclarationContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitGlobalDeclaration(AdamantParser.GlobalDeclarationContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitAttribute(AdamantParser.AttributeContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitModifier(AdamantParser.ModifierContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitTypeParameterList(AdamantParser.TypeParameterListContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitTypeParameter(AdamantParser.TypeParameterContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitTypeBase(AdamantParser.TypeBaseContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitTypeName(AdamantParser.TypeNameContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitTypeArguments(AdamantParser.TypeArgumentsContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitType(AdamantParser.TypeContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitFuncTypeParameterList(AdamantParser.FuncTypeParameterListContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitFuncTypeParameter(AdamantParser.FuncTypeParameterContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitConstExpression(AdamantParser.ConstExpressionContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitTypeParameterConstraintClause(AdamantParser.TypeParameterConstraintClauseContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitConstructorConstraint(AdamantParser.ConstructorConstraintContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitTypeConstraint(AdamantParser.TypeConstraintContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitTypeListParameterConstraint(AdamantParser.TypeListParameterConstraintContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitTypeMember(AdamantParser.TypeMemberContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitParameterList(AdamantParser.ParameterListContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitParameter(AdamantParser.ParameterContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitParameterModifier(AdamantParser.ParameterModifierContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitConstructor(AdamantParser.ConstructorContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitConstructorInitializer(AdamantParser.ConstructorInitializerContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitArgumentList(AdamantParser.ArgumentListContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitDestructor(AdamantParser.DestructorContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitMethod(AdamantParser.MethodContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitOperatorOverload(AdamantParser.OperatorOverloadContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitConversionMethod(AdamantParser.ConversionMethodContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitProperty(AdamantParser.PropertyContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitMethodBody(AdamantParser.MethodBodyContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitOverloadableOperator(AdamantParser.OverloadableOperatorContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitField(AdamantParser.FieldContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitVariableDeclarationStatement(AdamantParser.VariableDeclarationStatementContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitLetDeclarationStatement(AdamantParser.LetDeclarationStatementContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitUnsafeBlockStatement(AdamantParser.UnsafeBlockStatementContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitBlock(AdamantParser.BlockContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitEmptyStatement(AdamantParser.EmptyStatementContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitExpressionStatement(AdamantParser.ExpressionStatementContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitReturnStatement(AdamantParser.ReturnStatementContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitThrowStatement(AdamantParser.ThrowStatementContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitIfStatement(AdamantParser.IfStatementContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitForStatement(AdamantParser.ForStatementContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitForeachStatement(AdamantParser.ForeachStatementContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitDeleteStatement(AdamantParser.DeleteStatementContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitVariableDeclaration(AdamantParser.VariableDeclarationContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitLetDeclaration(AdamantParser.LetDeclarationContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitNullCheckExpression(AdamantParser.NullCheckExpressionContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitStringLiteralExpression(AdamantParser.StringLiteralExpressionContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitXorExpression(AdamantParser.XorExpressionContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitUnaryExpression(AdamantParser.UnaryExpressionContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitOrExpression(AdamantParser.OrExpressionContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitIntLiteralExpression(AdamantParser.IntLiteralExpressionContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitThisExpression(AdamantParser.ThisExpressionContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitAndExpression(AdamantParser.AndExpressionContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitAssignmentExpression(AdamantParser.AssignmentExpressionContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitBooleanLiteralExpression(AdamantParser.BooleanLiteralExpressionContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitEqualityExpression(AdamantParser.EqualityExpressionContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitMultiplicativeExpression(AdamantParser.MultiplicativeExpressionContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitCallExpression(AdamantParser.CallExpressionContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitNullLiteralExpression(AdamantParser.NullLiteralExpressionContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitAdditiveExpression(AdamantParser.AdditiveExpressionContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitIfExpression(AdamantParser.IfExpressionContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitPointerMemberExpression(AdamantParser.PointerMemberExpressionContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitNewExpression(AdamantParser.NewExpressionContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitUninitializedExpression(AdamantParser.UninitializedExpressionContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitVariableExpression(AdamantParser.VariableExpressionContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitShiftExpression(AdamantParser.ShiftExpressionContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitMemberExpression(AdamantParser.MemberExpressionContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitComparativeExpression(AdamantParser.ComparativeExpressionContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitCoalesceExpression(AdamantParser.CoalesceExpressionContext context)
		{
			throw new NotImplementedException();
		}

		public Node VisitArrayAccessExpression(AdamantParser.ArrayAccessExpressionContext context)
		{
			throw new NotImplementedException();
		}
	}
}
