using System;
using System.Collections.Generic;
using System.Linq;
using Adamant.Compiler.Antlr;

namespace Adamant.Compiler
{
	/// <summary>
	/// This visitor enforces syntax rules not enforced by the grammar
	/// </summary>
	public class SyntaxCheckVisitor : AdamantParserBaseVisitor<Void>
	{
		public override Void VisitShiftExpression(AdamantParser.ShiftExpressionContext context)
		{
			var op1 = context._ops[0];
			var op2 = context._ops[1];
			if(op1.StopIndex != op2.StartIndex - 1)
				throw new NotImplementedException(); // TODO report error on op2 as invalid expression
			return base.VisitShiftExpression(context);
		}

		private static readonly IList<int?> AccessModifier = new List<int?>() { AdamantLexer.Public, AdamantLexer.Private, AdamantLexer.Protected, AdamantLexer.Package };
		private static readonly IList<IList<int?>> MethodModifiers = new List<IList<int?>>() { AccessModifier };

		public override Void VisitMethod(AdamantParser.MethodContext context)
		{
			ValidateModifiers(context.modifier(), MethodModifiers);
			return base.VisitMethod(context);
		}

		private static void ValidateModifiers(AdamantParser.ModifierContext[] modifiers, IList<IList<int?>> expected)
		{
			var adjustedModifiers = modifiers.ToList();
			// TODO remove duplicates and report errors
			foreach(var expectedModifier in expected)
			{
				var modifier = modifiers[0];
				if(expectedModifier.Contains(modifier.Symbol.Type))
				{
					// It is one of the values we expected.
					adjustedModifiers.RemoveAt(0);
				}
				// TODO if they are optional, we are ok, otherwise report error

				// TODO search the rest of the modifiers to see if we find any, remove and report errors
			}
		}
	}
}
