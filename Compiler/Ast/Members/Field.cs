using Adamant.Compiler.Ast.Types;
using Adamant.Compiler.Ast.Visitors;

namespace Adamant.Compiler.Ast.Members
{
	public class Field : Member
	{
		public Field(AccessModifier access, bool isMutableReference, Name name, OwnershipType type, Expression initExpression)
			: base(access)
		{
			IsMutableReference = isMutableReference;
			Name = name;
			Type = type;
			InitExpression = initExpression;
		}

		public bool IsMutableReference { get; }
		public Name Name { get; }
		public OwnershipType Type { get; }
		public Expression InitExpression { get; }

		public override TReturn Accept<TParam, TReturn>(IMemberVisitor<TParam, TReturn> visitor, TParam param)
		{
			return visitor.VisitField(this, param);
		}
	}
}
