namespace Adamant.Compiler.Ast.Members
{
	public class Field : Member
	{
		public Field(AccessModifier access, bool isMutableReference, bool isMutableValue, Name name, Expression initExpression)
			: base(access)
		{
			IsMutableReference = isMutableReference;
			IsMutableValue = isMutableValue;
			Name = name;
			InitExpression = initExpression;
		}

		public bool IsMutableReference { get; }
		public bool IsMutableValue { get; }
		public Name Name { get; }
		public Expression InitExpression { get; set; }
	}
}
