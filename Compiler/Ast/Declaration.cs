using System.Collections.Generic;
using Adamant.Compiler.Ast.Visitors;

namespace Adamant.Compiler.Ast
{
	public abstract class Declaration : Node, IDeclarationContainer
	{
		protected Declaration(AccessModifier access, QualifiedName name)
		{
			Name = name;
			Access = access;
		}

		public QualifiedName Name { get; private set; }
		public AccessModifier Access { get; private set; }

		IEnumerable<Declaration> IDeclarationContainer.Declarations => new[] { this };

		public abstract TReturn Accept<TParam, TReturn>(IDeclarationVisitor<TParam, TReturn> visitor, TParam param);
	}
}
