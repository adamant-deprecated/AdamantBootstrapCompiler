using System;
using System.Collections.Generic;
using Adamant.Compiler.Antlr;
using Antlr4.Runtime.Tree;

namespace Adamant.Compiler.Preprocessor
{
	// TODO emit errors for space between # and command
	public class PreprocessorVisitor : PreprocessorLineParserBaseVisitor<Void>
	{
		private readonly PreprocessorState state;
		private readonly ParseTreeProperty<bool> expressionValue = new ParseTreeProperty<bool>();

		public PreprocessorVisitor(PreprocessorState state)
		{
			this.state = state;
		}

		#region Declaration Directives
		public override Void VisitDefine(PreprocessorLineParser.DefineContext context)
		{
			if(state.InSkippedSection) return Void.Value;

			var symbol = context.conditionalSymbol().GetText();
			state.Define(symbol);
			return Void.Value;
		}

		public override Void VisitUndefine(PreprocessorLineParser.UndefineContext context)
		{
			if(state.InSkippedSection) return Void.Value;

			var symbol = context.conditionalSymbol().GetText();
			state.Undefine(symbol);
			return Void.Value;
		}
		#endregion

		#region Conditional Directives
		public override Void VisitIfDecl(PreprocessorLineParser.IfDeclContext context)
		{
			base.VisitIfDecl(context);
			state.BeginIf(expressionValue.Get(context.expression()));
			return Void.Value;
		}

		public override Void VisitElseif(PreprocessorLineParser.ElseifContext context)
		{
			base.VisitElseif(context);
			state.BeginElseIf(expressionValue.Get(context.expression()));
			return Void.Value;
		}

		public override Void VisitElseDecl(PreprocessorLineParser.ElseDeclContext context)
		{
			state.BeginElse();
			return Void.Value;
		}

		public override Void VisitEndif(PreprocessorLineParser.EndifContext context)
		{
			state.EndIf();
			return Void.Value;
		}
		#endregion

		#region Expressions
		public override Void VisitNot(PreprocessorLineParser.NotContext context)
		{
			base.VisitNot(context);
			expressionValue.Put(context, !expressionValue.Get(context.expression()));
			return Void.Value;
		}

		public override Void VisitAnd(PreprocessorLineParser.AndContext context)
		{
			base.VisitAnd(context);
			expressionValue.Put(context, expressionValue.Get(context.expression(0)) && expressionValue.Get(context.expression(1)));
			return Void.Value;
		}

		public override Void VisitOr(PreprocessorLineParser.OrContext context)
		{
			base.VisitOr(context);
			expressionValue.Put(context, expressionValue.Get(context.expression(0)) || expressionValue.Get(context.expression(1)));
			return Void.Value;
		}

		public override Void VisitEquality(PreprocessorLineParser.EqualityContext context)
		{
			base.VisitEquality(context);
			var left = expressionValue.Get(context.expression(0));
			var right = expressionValue.Get(context.expression(1));
			var op = context.op.Type;
			bool result;
			switch(op)
			{
				case PreprocessorLineLexer.Equal:
					result = left == right;
					break;
				case PreprocessorLineLexer.NotEqual:
					result = left != right;
					break;
				default:
					throw new Exception("Invalid equality operator");
			}
			expressionValue.Put(context, result);
			return Void.Value;
		}

		public override Void VisitGrouping(PreprocessorLineParser.GroupingContext context)
		{
			base.VisitGrouping(context);
			expressionValue.Put(context, expressionValue.Get(context.expression()));
			return Void.Value;
		}

		public override Void VisitVariable(PreprocessorLineParser.VariableContext context)
		{
			var symbol = context.conditionalSymbol().GetText();
			expressionValue.Put(context, state.IsDefined(symbol));
			return Void.Value;
		}

		public override Void VisitBooleanValue(PreprocessorLineParser.BooleanValueContext context)
		{
			expressionValue.Put(context, bool.Parse(context.BooleanLiteral().GetText()));
			return Void.Value;
		}
		#endregion

		#region Line Directive
		public override Void VisitLine(PreprocessorLineParser.LineContext context)
		{
			if(state.InSkippedSection) return Void.Value;

			throw new NotImplementedException();
		}
		#endregion

		#region Diagnostic Directives
		public override Void VisitError(PreprocessorLineParser.ErrorContext context)
		{
			if(state.InSkippedSection) return Void.Value;

			throw new NotImplementedException();
		}

		public override Void VisitWarning(PreprocessorLineParser.WarningContext context)
		{
			if(state.InSkippedSection) return Void.Value;

			throw new NotImplementedException();
		}
		#endregion

		#region Region Directives
		public override Void VisitRegionBegin(PreprocessorLineParser.RegionBeginContext context)
		{
			state.BeginRegion();
			return Void.Value;
		}

		public override Void VisitRegionEnd(PreprocessorLineParser.RegionEndContext context)
		{
			state.EndRegion();
			return Void.Value;
		}
		#endregion

		#region Pragma Directives
		public override Void VisitPragmaWarning(PreprocessorLineParser.PragmaWarningContext context)
		{
			if(state.InSkippedSection) return Void.Value;

			throw new NotImplementedException();
		}

		public override Void VisitPragmaUnknown(PreprocessorLineParser.PragmaUnknownContext context)
		{
			if(state.InSkippedSection) return Void.Value;

			throw new NotImplementedException();
		}
		#endregion
	}
}
