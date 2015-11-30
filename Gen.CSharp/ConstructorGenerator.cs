using System.Linq;
using Adamant.Compiler.Antlr;

namespace Adamant.Compiler.Gen.CSharp
{
	internal class ConstructorGenerator : CSharpGenerator
	{
		private int constructorCount;

		public ConstructorGenerator(CSharpWriter writer, string currentClassName)
			: base(writer, currentClassName)
		{
		}

		#region Declarations
		public override MainFunctions VisitClassDeclaration(AdamantParser.ClassDeclarationContext context)
		{
			O.BlankLine();
			O.WriteIndented(Format(context.modifier().Where(IsAccessModifier)) + $"partial class {CurrentClassName}");
			O.WriteLine();
			O.BeginBlock();
			context.member().Select(m => m.Accept(this)).Combine();
			if(constructorCount == 0 && !IsAbstract(context.modifier()))
			{
				// Generate default constructor
				O.WriteIndentedLine($"public static {CurrentClassName} אCtor()");
				O.BeginBlock();
				O.WriteIndentedLine($"return new {CurrentClassName}();");
				O.EndBlock();
			}
			O.EndBlock();
			return MainFunctions.Empty;
		}
		#endregion

		#region Members
		public override MainFunctions VisitConstructor(AdamantParser.ConstructorContext context)
		{
			constructorCount++;

			O.BlankLine();

			var constructorName = context.name?.GetText();
			if(constructorName != null)
			{
				O.WriteIndentedLine($"public partial class {constructorName}");
				O.BeginBlock();
			}

			O.WriteIndented(Format(context.modifier()) + "static ");
			if(context.returnType != null)
				context.returnType.Accept(this);
			else
				O.Write(CurrentClassName);
			O.Write(" אCtor");
			// TODO handle generic class parameters
			O.Write("(");
			O.WriteList(context.parameterList()._parameters, this);
			O.Write(")");
			O.WriteLine();
			// Body
			O.BeginBlock();
			O.WriteIndented("return new ");
			if(context.returnType != null)
				context.returnType.Accept(this); // TODO handle the case where this is a base type
			else
				O.Write(CurrentClassName);

			O.Write("(");
			if(constructorName != null)
			{
				O.Write("default(אCtorName_");
				O.Write(constructorName);
				O.Write("), ");
			}
			O.Write(string.Join(", ", context.parameterList()._parameters.Select(p => p.name.GetText())));
			O.WriteLine(");");
			O.EndBlock();
			if(constructorName != null)
				O.EndBlock();
			return MainFunctions.Empty;
		}

		public override MainFunctions VisitMethod(AdamantParser.MethodContext context)
		{
			return MainFunctions.Empty;
		}

		public override MainFunctions VisitField(AdamantParser.FieldContext context)
		{
			return MainFunctions.Empty;
		}
		#endregion
	}
}
