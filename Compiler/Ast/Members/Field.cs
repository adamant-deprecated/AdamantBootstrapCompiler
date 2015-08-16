using Adamant.Compiler.Ast.Types;

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
	}
}
