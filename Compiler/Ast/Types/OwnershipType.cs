namespace Adamant.Compiler.Ast.Types
{
	public class OwnershipType : TypeNode
	{
		public OwnershipType(bool isReference, Ownership ownership, TypeNode type)
		{
			IsReference = isReference;
			Ownership = ownership;
			Type = type;
		}

		public bool IsReference { get; }
		public Ownership Ownership { get; }
		public TypeNode Type { get; }
	}
}
