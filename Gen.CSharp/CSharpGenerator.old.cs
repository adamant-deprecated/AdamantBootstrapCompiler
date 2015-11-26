using System;
using System.IO;
using System.Text;
using Adamant.Compiler.Ast;
using Adamant.Compiler.Ast.Declarations;
using Adamant.Compiler.Ast.Types;
using Adamant.Compiler.Ast.Visitors;

namespace Adamant.Compiler.Gen.CSharp
{
	public class CSharpGenerator_old : IDeclarationVisitor<Void, Void>, ITypeVisitor<Void, Void>
	{
		private readonly TextWriter output;
		private readonly StringBuilder indent = new StringBuilder();
		private bool firstElement;

		public CSharpGenerator_old(TextWriter output)
		{
			this.output = output;
		}

		public void GenerateFor(Assemblage assemblage)
		{
			output.WriteLine(indent + "using אRuntime;");
			foreach(var declaration in assemblage.Declarations)
				declaration.Accept(this, Void.Value);
		}

		#region Formatting
		private void Indent()
		{
			indent.Append('\t');
			firstElement = true;
		}

		private void Outdent()
		{
			if(indent.Length == 0)
				throw new InvalidOperationException("Can't outdent top level");

			indent.Length = indent.Length - 1;
		}

		private void BlankLine()
		{
			if(firstElement)
				firstElement = false;
			else
				output.WriteLine();
		}

		private void BeginBlock()
		{
			output.WriteLine(indent + "{");
			Indent();
		}

		private void EndBlock()
		{
			Outdent();
			output.WriteLine(indent + "}");
		}

		private bool BeginNamespace(QualifiedName name)
		{
			var namespaceName = name.Namespace();

			if(namespaceName.Equals(QualifiedName.None))
				return false;

			BlankLine();
			output.WriteLine(indent + "namespace " + namespaceName);
			BeginBlock();
			return true;
		}

		private void EndNamespace(bool namespaceOpen)
		{
			if(namespaceOpen)
				EndBlock();
		}

		private static string Format(AccessModifier accessModifier)
		{
			return accessModifier.ToString().ToLowerInvariant();
		}
		#endregion

		#region Declaration Visitor
		Void IDeclarationVisitor<Void, Void>.VisitClassDeclaration(ClassDeclaration declaration, Void param)
		{
			throw new NotImplementedException();
		}

		Void IDeclarationVisitor<Void, Void>.VisitFunctionDeclaration(FunctionDeclaration declaration, Void param)
		{
			var ns = BeginNamespace(declaration.Name);
			// Container static class
			BlankLine();
			output.WriteLine(indent + "public static partial class אFuncs");
			BeginBlock();
			// Declaration
			output.Write(indent + Format(declaration.Access) + " static ");
			declaration.ReturnType.Accept(this, Void.Value);
			output.Write(" " + declaration.Name.Name() + "(");
			var firstParam = true;
			foreach(var parameter in declaration.Parameters)
			{
				if(firstParam)
					firstParam = false;
				else
					output.Write(", ");

				parameter.Type.Accept(this, Void.Value);
				output.Write(" " + parameter.Name);
			}
			output.WriteLine(")");
			// Body
			BeginBlock();
			EndBlock();
			// End Container class
			EndBlock();
			EndNamespace(ns);
			return Void.Value;
		}

		Void IDeclarationVisitor<Void, Void>.VisitGlobalDeclaration(GlobalDeclaration declaration, Void param)
		{
			throw new System.NotImplementedException();
		}
		#endregion

		#region Type Visitor
		Void ITypeVisitor<Void, Void>.VisitInferredType(InferredType type, Void param)
		{
			throw new NotImplementedException();
		}

		Void ITypeVisitor<Void, Void>.VisitOwnershipType(OwnershipType type, Void param)
		{
			if(type.IsReference)
				output.Write("ref ");
			return type.Type.Accept(this, Void.Value);
		}

		Void ITypeVisitor<Void, Void>.VisitTypeName(TypeName type, Void param)
		{
			output.Write(type.Name);
			return Void.Value;
		}

		Void ITypeVisitor<Void, Void>.VisitArraySliceType(ArraySliceType type, Void param)
		{
			type.ElementType.Accept(this, Void.Value);
			output.Write("[");
			output.Write(new string(',', type.Dimensions - 1));
			output.Write("]");
			return Void.Value;
		}
		#endregion
	}
}
