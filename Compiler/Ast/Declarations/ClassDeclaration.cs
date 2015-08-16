using System.Collections.Generic;
using System.Linq;

namespace Adamant.Compiler.Ast.Declarations
{
	public class ClassDeclaration : Declaration
	{
		private readonly IList<Member> members;

		public ClassDeclaration(
			AccessModifier access,
			bool isPartial, 
			Safety safety, 
			bool isSealed,
			bool isAbstract,
			QualifiedName name,
			IEnumerable<Member> members)
			: base(access, name)
		{
			
			IsPartial = isPartial;
			Safety = safety;
			IsSealed = isSealed;
			IsAbstract = isAbstract;
			this.members = members.ToList();
		}

		public bool IsPartial { get; }
		public Safety Safety { get; }
		public bool IsSealed { get; }
		public bool IsAbstract { get; }

		public IEnumerable<Node> Members => members;
	}
}
