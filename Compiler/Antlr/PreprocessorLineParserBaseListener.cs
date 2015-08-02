//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.5.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from PreprocessorLineParser.g4 by ANTLR 4.5.1

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591

namespace Adamant.Compiler.Antlr {

using Antlr4.Runtime.Misc;
using IErrorNode = Antlr4.Runtime.Tree.IErrorNode;
using ITerminalNode = Antlr4.Runtime.Tree.ITerminalNode;
using IToken = Antlr4.Runtime.IToken;
using ParserRuleContext = Antlr4.Runtime.ParserRuleContext;

/// <summary>
/// This class provides an empty implementation of <see cref="IPreprocessorLineParserListener"/>,
/// which can be extended to create a listener which only needs to handle a subset
/// of the available methods.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.5.1")]
[System.CLSCompliant(false)]
public partial class PreprocessorLineParserBaseListener : IPreprocessorLineParserListener {
	/// <summary>
	/// Enter a parse tree produced by <see cref="PreprocessorLineParser.preprocessorLine"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterPreprocessorLine([NotNull] PreprocessorLineParser.PreprocessorLineContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="PreprocessorLineParser.preprocessorLine"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitPreprocessorLine([NotNull] PreprocessorLineParser.PreprocessorLineContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="PreprocessorLineParser.directive"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterDirective([NotNull] PreprocessorLineParser.DirectiveContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="PreprocessorLineParser.directive"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitDirective([NotNull] PreprocessorLineParser.DirectiveContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="PreprocessorLineParser.define"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterDefine([NotNull] PreprocessorLineParser.DefineContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="PreprocessorLineParser.define"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitDefine([NotNull] PreprocessorLineParser.DefineContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="PreprocessorLineParser.undefine"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterUndefine([NotNull] PreprocessorLineParser.UndefineContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="PreprocessorLineParser.undefine"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitUndefine([NotNull] PreprocessorLineParser.UndefineContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="PreprocessorLineParser.ifDecl"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterIfDecl([NotNull] PreprocessorLineParser.IfDeclContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="PreprocessorLineParser.ifDecl"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitIfDecl([NotNull] PreprocessorLineParser.IfDeclContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="PreprocessorLineParser.elseif"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterElseif([NotNull] PreprocessorLineParser.ElseifContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="PreprocessorLineParser.elseif"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitElseif([NotNull] PreprocessorLineParser.ElseifContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="PreprocessorLineParser.elseDecl"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterElseDecl([NotNull] PreprocessorLineParser.ElseDeclContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="PreprocessorLineParser.elseDecl"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitElseDecl([NotNull] PreprocessorLineParser.ElseDeclContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="PreprocessorLineParser.endif"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterEndif([NotNull] PreprocessorLineParser.EndifContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="PreprocessorLineParser.endif"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitEndif([NotNull] PreprocessorLineParser.EndifContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="PreprocessorLineParser.expression"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterExpression([NotNull] PreprocessorLineParser.ExpressionContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="PreprocessorLineParser.expression"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitExpression([NotNull] PreprocessorLineParser.ExpressionContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="PreprocessorLineParser.line"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterLine([NotNull] PreprocessorLineParser.LineContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="PreprocessorLineParser.line"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitLine([NotNull] PreprocessorLineParser.LineContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="PreprocessorLineParser.error"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterError([NotNull] PreprocessorLineParser.ErrorContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="PreprocessorLineParser.error"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitError([NotNull] PreprocessorLineParser.ErrorContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="PreprocessorLineParser.warning"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterWarning([NotNull] PreprocessorLineParser.WarningContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="PreprocessorLineParser.warning"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitWarning([NotNull] PreprocessorLineParser.WarningContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="PreprocessorLineParser.region"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterRegion([NotNull] PreprocessorLineParser.RegionContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="PreprocessorLineParser.region"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitRegion([NotNull] PreprocessorLineParser.RegionContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="PreprocessorLineParser.pragma"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterPragma([NotNull] PreprocessorLineParser.PragmaContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="PreprocessorLineParser.pragma"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitPragma([NotNull] PreprocessorLineParser.PragmaContext context) { }

	/// <inheritdoc/>
	/// <remarks>The default implementation does nothing.</remarks>
	public virtual void EnterEveryRule([NotNull] ParserRuleContext context) { }
	/// <inheritdoc/>
	/// <remarks>The default implementation does nothing.</remarks>
	public virtual void ExitEveryRule([NotNull] ParserRuleContext context) { }
	/// <inheritdoc/>
	/// <remarks>The default implementation does nothing.</remarks>
	public virtual void VisitTerminal([NotNull] ITerminalNode node) { }
	/// <inheritdoc/>
	/// <remarks>The default implementation does nothing.</remarks>
	public virtual void VisitErrorNode([NotNull] IErrorNode node) { }
}
} // namespace Adamant.Compiler.Antlr
