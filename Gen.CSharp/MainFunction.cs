using Adamant.Compiler.Ast;

namespace Adamant.Compiler.Gen.CSharp
{
	public class MainFunction
	{
		public MainFunction(QualifiedName name, string returnType)
		{
			Name = name;
			ReturnType = returnType;
		}

		public QualifiedName Name { get; }
		public string ReturnType { get; }

		public MainFunction InNamespace(string namespaceName)
		{
			return new MainFunction(Name.Prepend(namespaceName), ReturnType);
		}
	}
}
