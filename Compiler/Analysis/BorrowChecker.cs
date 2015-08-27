using System;
using Adamant.Compiler.Ast;
using Adamant.Compiler.Ast.Declarations;
using Adamant.Compiler.Ast.Expressions;
using Adamant.Compiler.Ast.Members;
using Adamant.Compiler.Ast.Types;
using Adamant.Compiler.Ast.Visitors;

namespace Adamant.Compiler.Analysis
{
	/// <summary>
	/// This class validates the borrow rules and infers ownership
	/// </summary>
	public class BorrowChecker :
		IDeclarationVisitor<Void, Void>,
		IMemberVisitor<Void, Void>,
		IExpressionVisitor<Void, Ownership>
	{
		public void Check(Assemblage assemblage)
		{
			foreach(var declaration in assemblage.Declarations)
				declaration.Accept(this, Void.Value);
		}

		#region Declaration Visitor
		Void IDeclarationVisitor<Void, Void>.VisitClassDeclaration(ClassDeclaration declaration, Void param)
		{
			foreach(var member in declaration.Members)
			{
				member.Accept(this, Void.Value);
			}
			return Void.Value;
		}

		Void IDeclarationVisitor<Void, Void>.VisitFunctionDeclaration(FunctionDeclaration declaration, Void param)
		{
			throw new System.NotImplementedException();
		}

		Void IDeclarationVisitor<Void, Void>.VisitGlobalDeclaration(GlobalDeclaration declaration, Void param)
		{
			if(declaration.InitExpression == null)
				declaration.Type.DefaultOwnership(Ownership.Owned);
			else
			{
				var expressionOwnership = declaration.InitExpression.Accept(this, Void.Value);
				declaration.Type.AssignValueWithOwnership(expressionOwnership);
			}
			return Void.Value;
		}
		#endregion

		#region Member Visitor
		Void IMemberVisitor<Void, Void>.VisitConstructor(Constructor member, Void param)
		{
			foreach(var parameter in member.Parameters)
			{
				//TODO parameter.Type.DefaultOwnership(Ownership.ImmutableBorrow);
			}

			throw new NotImplementedException();
		}

		Void IMemberVisitor<Void, Void>.VisitField(Field member, Void param)
		{
			if(member.InitExpression == null)
				member.Type.DefaultOwnership(Ownership.Owned);
			else
			{
				var expressionOwnership = member.InitExpression.Accept(this, Void.Value);
				member.Type.AssignValueWithOwnership(expressionOwnership);
			}
			return Void.Value;
		}

		Void IMemberVisitor<Void, Void>.VisitMethod(Method member, Void param)
		{
			throw new System.NotImplementedException();
		}

		Void IMemberVisitor<Void, Void>.VisitProperty(Property member, Void param)
		{
			throw new System.NotImplementedException();
		}
		#endregion

		#region Expression Visitor
		Ownership IExpressionVisitor<Void, Ownership>.VisitLiteral(LiteralExpression expression, Void param)
		{
			return Ownership.ImmutableBorrow;
		}

		Ownership IExpressionVisitor<Void, Ownership>.VisitNewObject(NewObjectExpression expression, Void param)
		{
			return Ownership.Owned;
		}
		#endregion
	}
}
