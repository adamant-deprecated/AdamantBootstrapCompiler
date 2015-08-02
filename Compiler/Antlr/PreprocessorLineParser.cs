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
using System;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.5.1")]
[System.CLSCompliant(false)]
public partial class PreprocessorLineParser : Parser {
	public const int
		SingleLineComment=1, Define=2, Undefine=3, If=4, ElseIf=5, Else=6, EndIf=7, 
		Line=8, Error=9, Warning=10, Region=11, EndRegion=12, PragmaWarning=13, 
		Pragma=14, BooleanLiteral=15, ConditionalSymbol=16, And=17, Or=18, Not=19, 
		Equal=20, NotEqual=21, LeftParen=22, RightParen=23, Whitespace=24, NewLine=25, 
		LineMode=26, LineNumber=27, FileName=28, WarningAction=29, WarningNumber=30, 
		Comma=31, Message=32;
	public const int
		RULE_preprocessorLine = 0, RULE_directive = 1, RULE_define = 2, RULE_undefine = 3, 
		RULE_ifDecl = 4, RULE_elseif = 5, RULE_elseDecl = 6, RULE_endif = 7, RULE_expression = 8, 
		RULE_line = 9, RULE_error = 10, RULE_warning = 11, RULE_region = 12, RULE_pragma = 13;
	public static readonly string[] ruleNames = {
		"preprocessorLine", "directive", "define", "undefine", "ifDecl", "elseif", 
		"elseDecl", "endif", "expression", "line", "error", "warning", "region", 
		"pragma"
	};

	private static readonly string[] _LiteralNames = {
		null, null, null, null, null, null, null, null, null, null, null, null, 
		null, null, null, null, null, "'&&'", "'||'", "'!'", "'=='", "'!='", "'('", 
		"')'", null, null, null, null, null, null, null, "','"
	};
	private static readonly string[] _SymbolicNames = {
		null, "SingleLineComment", "Define", "Undefine", "If", "ElseIf", "Else", 
		"EndIf", "Line", "Error", "Warning", "Region", "EndRegion", "PragmaWarning", 
		"Pragma", "BooleanLiteral", "ConditionalSymbol", "And", "Or", "Not", "Equal", 
		"NotEqual", "LeftParen", "RightParen", "Whitespace", "NewLine", "LineMode", 
		"LineNumber", "FileName", "WarningAction", "WarningNumber", "Comma", "Message"
	};
	public static readonly IVocabulary DefaultVocabulary = new Vocabulary(_LiteralNames, _SymbolicNames);

	[NotNull]
	public override IVocabulary Vocabulary
	{
		get
		{
			return DefaultVocabulary;
		}
	}

	public override string GrammarFileName { get { return "PreprocessorLineParser.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string SerializedAtn { get { return _serializedATN; } }

	public PreprocessorLineParser(ITokenStream input)
		: base(input)
	{
		Interpreter = new ParserATNSimulator(this,_ATN);
	}
	public partial class PreprocessorLineContext : ParserRuleContext {
		public DirectiveContext directive() {
			return GetRuleContext<DirectiveContext>(0);
		}
		public ITerminalNode Eof() { return GetToken(PreprocessorLineParser.Eof, 0); }
		public PreprocessorLineContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_preprocessorLine; } }
		public override void EnterRule(IParseTreeListener listener) {
			IPreprocessorLineParserListener typedListener = listener as IPreprocessorLineParserListener;
			if (typedListener != null) typedListener.EnterPreprocessorLine(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IPreprocessorLineParserListener typedListener = listener as IPreprocessorLineParserListener;
			if (typedListener != null) typedListener.ExitPreprocessorLine(this);
		}
	}

	[RuleVersion(0)]
	public PreprocessorLineContext preprocessorLine() {
		PreprocessorLineContext _localctx = new PreprocessorLineContext(Context, State);
		EnterRule(_localctx, 0, RULE_preprocessorLine);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 28; directive();
			State = 29; Match(Eof);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class DirectiveContext : ParserRuleContext {
		public DefineContext define() {
			return GetRuleContext<DefineContext>(0);
		}
		public UndefineContext undefine() {
			return GetRuleContext<UndefineContext>(0);
		}
		public IfDeclContext ifDecl() {
			return GetRuleContext<IfDeclContext>(0);
		}
		public ElseifContext elseif() {
			return GetRuleContext<ElseifContext>(0);
		}
		public ElseDeclContext elseDecl() {
			return GetRuleContext<ElseDeclContext>(0);
		}
		public EndifContext endif() {
			return GetRuleContext<EndifContext>(0);
		}
		public LineContext line() {
			return GetRuleContext<LineContext>(0);
		}
		public ErrorContext error() {
			return GetRuleContext<ErrorContext>(0);
		}
		public WarningContext warning() {
			return GetRuleContext<WarningContext>(0);
		}
		public RegionContext region() {
			return GetRuleContext<RegionContext>(0);
		}
		public PragmaContext pragma() {
			return GetRuleContext<PragmaContext>(0);
		}
		public DirectiveContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_directive; } }
		public override void EnterRule(IParseTreeListener listener) {
			IPreprocessorLineParserListener typedListener = listener as IPreprocessorLineParserListener;
			if (typedListener != null) typedListener.EnterDirective(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IPreprocessorLineParserListener typedListener = listener as IPreprocessorLineParserListener;
			if (typedListener != null) typedListener.ExitDirective(this);
		}
	}

	[RuleVersion(0)]
	public DirectiveContext directive() {
		DirectiveContext _localctx = new DirectiveContext(Context, State);
		EnterRule(_localctx, 2, RULE_directive);
		try {
			State = 42;
			switch (TokenStream.La(1)) {
			case Define:
				EnterOuterAlt(_localctx, 1);
				{
				State = 31; define();
				}
				break;
			case Undefine:
				EnterOuterAlt(_localctx, 2);
				{
				State = 32; undefine();
				}
				break;
			case If:
				EnterOuterAlt(_localctx, 3);
				{
				State = 33; ifDecl();
				}
				break;
			case ElseIf:
				EnterOuterAlt(_localctx, 4);
				{
				State = 34; elseif();
				}
				break;
			case Else:
				EnterOuterAlt(_localctx, 5);
				{
				State = 35; elseDecl();
				}
				break;
			case EndIf:
				EnterOuterAlt(_localctx, 6);
				{
				State = 36; endif();
				}
				break;
			case Line:
			case LineNumber:
				EnterOuterAlt(_localctx, 7);
				{
				State = 37; line();
				}
				break;
			case Error:
				EnterOuterAlt(_localctx, 8);
				{
				State = 38; error();
				}
				break;
			case Warning:
				EnterOuterAlt(_localctx, 9);
				{
				State = 39; warning();
				}
				break;
			case Region:
			case EndRegion:
				EnterOuterAlt(_localctx, 10);
				{
				State = 40; region();
				}
				break;
			case PragmaWarning:
			case Pragma:
				EnterOuterAlt(_localctx, 11);
				{
				State = 41; pragma();
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class DefineContext : ParserRuleContext {
		public ITerminalNode Define() { return GetToken(PreprocessorLineParser.Define, 0); }
		public ITerminalNode ConditionalSymbol() { return GetToken(PreprocessorLineParser.ConditionalSymbol, 0); }
		public DefineContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_define; } }
		public override void EnterRule(IParseTreeListener listener) {
			IPreprocessorLineParserListener typedListener = listener as IPreprocessorLineParserListener;
			if (typedListener != null) typedListener.EnterDefine(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IPreprocessorLineParserListener typedListener = listener as IPreprocessorLineParserListener;
			if (typedListener != null) typedListener.ExitDefine(this);
		}
	}

	[RuleVersion(0)]
	public DefineContext define() {
		DefineContext _localctx = new DefineContext(Context, State);
		EnterRule(_localctx, 4, RULE_define);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 44; Match(Define);
			State = 45; Match(ConditionalSymbol);
			 PreprocessorDefine(); 
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class UndefineContext : ParserRuleContext {
		public ITerminalNode Undefine() { return GetToken(PreprocessorLineParser.Undefine, 0); }
		public ITerminalNode ConditionalSymbol() { return GetToken(PreprocessorLineParser.ConditionalSymbol, 0); }
		public UndefineContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_undefine; } }
		public override void EnterRule(IParseTreeListener listener) {
			IPreprocessorLineParserListener typedListener = listener as IPreprocessorLineParserListener;
			if (typedListener != null) typedListener.EnterUndefine(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IPreprocessorLineParserListener typedListener = listener as IPreprocessorLineParserListener;
			if (typedListener != null) typedListener.ExitUndefine(this);
		}
	}

	[RuleVersion(0)]
	public UndefineContext undefine() {
		UndefineContext _localctx = new UndefineContext(Context, State);
		EnterRule(_localctx, 6, RULE_undefine);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 48; Match(Undefine);
			State = 49; Match(ConditionalSymbol);
			 PreprocessorUndefine(); 
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class IfDeclContext : ParserRuleContext {
		public ITerminalNode If() { return GetToken(PreprocessorLineParser.If, 0); }
		public ExpressionContext expression() {
			return GetRuleContext<ExpressionContext>(0);
		}
		public IfDeclContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_ifDecl; } }
		public override void EnterRule(IParseTreeListener listener) {
			IPreprocessorLineParserListener typedListener = listener as IPreprocessorLineParserListener;
			if (typedListener != null) typedListener.EnterIfDecl(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IPreprocessorLineParserListener typedListener = listener as IPreprocessorLineParserListener;
			if (typedListener != null) typedListener.ExitIfDecl(this);
		}
	}

	[RuleVersion(0)]
	public IfDeclContext ifDecl() {
		IfDeclContext _localctx = new IfDeclContext(Context, State);
		EnterRule(_localctx, 8, RULE_ifDecl);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 52; Match(If);
			State = 53; expression(0);
			 PreprocessorIf(); 
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class ElseifContext : ParserRuleContext {
		public ITerminalNode ElseIf() { return GetToken(PreprocessorLineParser.ElseIf, 0); }
		public ExpressionContext expression() {
			return GetRuleContext<ExpressionContext>(0);
		}
		public ElseifContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_elseif; } }
		public override void EnterRule(IParseTreeListener listener) {
			IPreprocessorLineParserListener typedListener = listener as IPreprocessorLineParserListener;
			if (typedListener != null) typedListener.EnterElseif(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IPreprocessorLineParserListener typedListener = listener as IPreprocessorLineParserListener;
			if (typedListener != null) typedListener.ExitElseif(this);
		}
	}

	[RuleVersion(0)]
	public ElseifContext elseif() {
		ElseifContext _localctx = new ElseifContext(Context, State);
		EnterRule(_localctx, 10, RULE_elseif);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 56; Match(ElseIf);
			State = 57; expression(0);
			 PreprocessorElseIf(); 
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class ElseDeclContext : ParserRuleContext {
		public ITerminalNode Else() { return GetToken(PreprocessorLineParser.Else, 0); }
		public ElseDeclContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_elseDecl; } }
		public override void EnterRule(IParseTreeListener listener) {
			IPreprocessorLineParserListener typedListener = listener as IPreprocessorLineParserListener;
			if (typedListener != null) typedListener.EnterElseDecl(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IPreprocessorLineParserListener typedListener = listener as IPreprocessorLineParserListener;
			if (typedListener != null) typedListener.ExitElseDecl(this);
		}
	}

	[RuleVersion(0)]
	public ElseDeclContext elseDecl() {
		ElseDeclContext _localctx = new ElseDeclContext(Context, State);
		EnterRule(_localctx, 12, RULE_elseDecl);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 60; Match(Else);
			 PreprocessorElse(); 
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class EndifContext : ParserRuleContext {
		public ITerminalNode EndIf() { return GetToken(PreprocessorLineParser.EndIf, 0); }
		public EndifContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_endif; } }
		public override void EnterRule(IParseTreeListener listener) {
			IPreprocessorLineParserListener typedListener = listener as IPreprocessorLineParserListener;
			if (typedListener != null) typedListener.EnterEndif(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IPreprocessorLineParserListener typedListener = listener as IPreprocessorLineParserListener;
			if (typedListener != null) typedListener.ExitEndif(this);
		}
	}

	[RuleVersion(0)]
	public EndifContext endif() {
		EndifContext _localctx = new EndifContext(Context, State);
		EnterRule(_localctx, 14, RULE_endif);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 63; Match(EndIf);
			 PreprocessorEndif(); 
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class ExpressionContext : ParserRuleContext {
		public ExpressionContext[] expression() {
			return GetRuleContexts<ExpressionContext>();
		}
		public ExpressionContext expression(int i) {
			return GetRuleContext<ExpressionContext>(i);
		}
		public ITerminalNode ConditionalSymbol() { return GetToken(PreprocessorLineParser.ConditionalSymbol, 0); }
		public ITerminalNode BooleanLiteral() { return GetToken(PreprocessorLineParser.BooleanLiteral, 0); }
		public ExpressionContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_expression; } }
		public override void EnterRule(IParseTreeListener listener) {
			IPreprocessorLineParserListener typedListener = listener as IPreprocessorLineParserListener;
			if (typedListener != null) typedListener.EnterExpression(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IPreprocessorLineParserListener typedListener = listener as IPreprocessorLineParserListener;
			if (typedListener != null) typedListener.ExitExpression(this);
		}
	}

	[RuleVersion(0)]
	public ExpressionContext expression() {
		return expression(0);
	}

	private ExpressionContext expression(int _p) {
		ParserRuleContext _parentctx = Context;
		int _parentState = State;
		ExpressionContext _localctx = new ExpressionContext(Context, _parentState);
		ExpressionContext _prevctx = _localctx;
		int _startState = 16;
		EnterRecursionRule(_localctx, 16, RULE_expression, _p);
		int _la;
		try {
			int _alt;
			EnterOuterAlt(_localctx, 1);
			{
			State = 75;
			switch (TokenStream.La(1)) {
			case Not:
				{
				State = 67; Match(Not);
				State = 68; expression(7);
				}
				break;
			case LeftParen:
				{
				State = 69; Match(LeftParen);
				State = 70; expression(0);
				State = 71; Match(RightParen);
				}
				break;
			case ConditionalSymbol:
				{
				State = 73; Match(ConditionalSymbol);
				}
				break;
			case BooleanLiteral:
				{
				State = 74; Match(BooleanLiteral);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
			Context.Stop = TokenStream.Lt(-1);
			State = 88;
			ErrorHandler.Sync(this);
			_alt = Interpreter.AdaptivePredict(TokenStream,3,Context);
			while ( _alt!=2 && _alt!=global::Antlr4.Runtime.Atn.ATN.InvalidAltNumber ) {
				if ( _alt==1 ) {
					if ( ParseListeners!=null )
						TriggerExitRuleEvent();
					_prevctx = _localctx;
					{
					State = 86;
					switch ( Interpreter.AdaptivePredict(TokenStream,2,Context) ) {
					case 1:
						{
						_localctx = new ExpressionContext(_parentctx, _parentState);
						PushNewRecursionContext(_localctx, _startState, RULE_expression);
						State = 77;
						if (!(Precpred(Context, 6))) throw new FailedPredicateException(this, "Precpred(Context, 6)");
						State = 78; Match(And);
						State = 79; expression(7);
						}
						break;
					case 2:
						{
						_localctx = new ExpressionContext(_parentctx, _parentState);
						PushNewRecursionContext(_localctx, _startState, RULE_expression);
						State = 80;
						if (!(Precpred(Context, 5))) throw new FailedPredicateException(this, "Precpred(Context, 5)");
						State = 81; Match(Or);
						State = 82; expression(6);
						}
						break;
					case 3:
						{
						_localctx = new ExpressionContext(_parentctx, _parentState);
						PushNewRecursionContext(_localctx, _startState, RULE_expression);
						State = 83;
						if (!(Precpred(Context, 4))) throw new FailedPredicateException(this, "Precpred(Context, 4)");
						State = 84;
						_la = TokenStream.La(1);
						if ( !(_la==Equal || _la==NotEqual) ) {
						ErrorHandler.RecoverInline(this);
						}
						else {
						    Consume();
						}
						State = 85; expression(5);
						}
						break;
					}
					} 
				}
				State = 90;
				ErrorHandler.Sync(this);
				_alt = Interpreter.AdaptivePredict(TokenStream,3,Context);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			UnrollRecursionContexts(_parentctx);
		}
		return _localctx;
	}

	public partial class LineContext : ParserRuleContext {
		public ITerminalNode Line() { return GetToken(PreprocessorLineParser.Line, 0); }
		public ITerminalNode LineMode() { return GetToken(PreprocessorLineParser.LineMode, 0); }
		public ITerminalNode LineNumber() { return GetToken(PreprocessorLineParser.LineNumber, 0); }
		public ITerminalNode FileName() { return GetToken(PreprocessorLineParser.FileName, 0); }
		public LineContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_line; } }
		public override void EnterRule(IParseTreeListener listener) {
			IPreprocessorLineParserListener typedListener = listener as IPreprocessorLineParserListener;
			if (typedListener != null) typedListener.EnterLine(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IPreprocessorLineParserListener typedListener = listener as IPreprocessorLineParserListener;
			if (typedListener != null) typedListener.ExitLine(this);
		}
	}

	[RuleVersion(0)]
	public LineContext line() {
		LineContext _localctx = new LineContext(Context, State);
		EnterRule(_localctx, 18, RULE_line);
		int _la;
		try {
			State = 97;
			switch (TokenStream.La(1)) {
			case Line:
				EnterOuterAlt(_localctx, 1);
				{
				State = 91; Match(Line);
				State = 92; Match(LineMode);
				}
				break;
			case LineNumber:
				EnterOuterAlt(_localctx, 2);
				{
				{
				State = 93; Match(LineNumber);
				State = 95;
				_la = TokenStream.La(1);
				if (_la==FileName) {
					{
					State = 94; Match(FileName);
					}
				}

				}
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class ErrorContext : ParserRuleContext {
		public ITerminalNode Error() { return GetToken(PreprocessorLineParser.Error, 0); }
		public ITerminalNode Message() { return GetToken(PreprocessorLineParser.Message, 0); }
		public ErrorContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_error; } }
		public override void EnterRule(IParseTreeListener listener) {
			IPreprocessorLineParserListener typedListener = listener as IPreprocessorLineParserListener;
			if (typedListener != null) typedListener.EnterError(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IPreprocessorLineParserListener typedListener = listener as IPreprocessorLineParserListener;
			if (typedListener != null) typedListener.ExitError(this);
		}
	}

	[RuleVersion(0)]
	public ErrorContext error() {
		ErrorContext _localctx = new ErrorContext(Context, State);
		EnterRule(_localctx, 20, RULE_error);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 99; Match(Error);
			State = 101;
			_la = TokenStream.La(1);
			if (_la==Message) {
				{
				State = 100; Match(Message);
				}
			}

			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class WarningContext : ParserRuleContext {
		public ITerminalNode Warning() { return GetToken(PreprocessorLineParser.Warning, 0); }
		public ITerminalNode Message() { return GetToken(PreprocessorLineParser.Message, 0); }
		public WarningContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_warning; } }
		public override void EnterRule(IParseTreeListener listener) {
			IPreprocessorLineParserListener typedListener = listener as IPreprocessorLineParserListener;
			if (typedListener != null) typedListener.EnterWarning(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IPreprocessorLineParserListener typedListener = listener as IPreprocessorLineParserListener;
			if (typedListener != null) typedListener.ExitWarning(this);
		}
	}

	[RuleVersion(0)]
	public WarningContext warning() {
		WarningContext _localctx = new WarningContext(Context, State);
		EnterRule(_localctx, 22, RULE_warning);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 103; Match(Warning);
			State = 105;
			_la = TokenStream.La(1);
			if (_la==Message) {
				{
				State = 104; Match(Message);
				}
			}

			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class RegionContext : ParserRuleContext {
		public ITerminalNode Region() { return GetToken(PreprocessorLineParser.Region, 0); }
		public ITerminalNode Message() { return GetToken(PreprocessorLineParser.Message, 0); }
		public ITerminalNode EndRegion() { return GetToken(PreprocessorLineParser.EndRegion, 0); }
		public RegionContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_region; } }
		public override void EnterRule(IParseTreeListener listener) {
			IPreprocessorLineParserListener typedListener = listener as IPreprocessorLineParserListener;
			if (typedListener != null) typedListener.EnterRegion(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IPreprocessorLineParserListener typedListener = listener as IPreprocessorLineParserListener;
			if (typedListener != null) typedListener.ExitRegion(this);
		}
	}

	[RuleVersion(0)]
	public RegionContext region() {
		RegionContext _localctx = new RegionContext(Context, State);
		EnterRule(_localctx, 24, RULE_region);
		int _la;
		try {
			State = 115;
			switch (TokenStream.La(1)) {
			case Region:
				EnterOuterAlt(_localctx, 1);
				{
				State = 107; Match(Region);
				State = 109;
				_la = TokenStream.La(1);
				if (_la==Message) {
					{
					State = 108; Match(Message);
					}
				}

				}
				break;
			case EndRegion:
				EnterOuterAlt(_localctx, 2);
				{
				State = 111; Match(EndRegion);
				State = 113;
				_la = TokenStream.La(1);
				if (_la==Message) {
					{
					State = 112; Match(Message);
					}
				}

				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class PragmaContext : ParserRuleContext {
		public ITerminalNode PragmaWarning() { return GetToken(PreprocessorLineParser.PragmaWarning, 0); }
		public ITerminalNode WarningAction() { return GetToken(PreprocessorLineParser.WarningAction, 0); }
		public ITerminalNode[] WarningNumber() { return GetTokens(PreprocessorLineParser.WarningNumber); }
		public ITerminalNode WarningNumber(int i) {
			return GetToken(PreprocessorLineParser.WarningNumber, i);
		}
		public ITerminalNode Pragma() { return GetToken(PreprocessorLineParser.Pragma, 0); }
		public ITerminalNode Message() { return GetToken(PreprocessorLineParser.Message, 0); }
		public PragmaContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_pragma; } }
		public override void EnterRule(IParseTreeListener listener) {
			IPreprocessorLineParserListener typedListener = listener as IPreprocessorLineParserListener;
			if (typedListener != null) typedListener.EnterPragma(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IPreprocessorLineParserListener typedListener = listener as IPreprocessorLineParserListener;
			if (typedListener != null) typedListener.ExitPragma(this);
		}
	}

	[RuleVersion(0)]
	public PragmaContext pragma() {
		PragmaContext _localctx = new PragmaContext(Context, State);
		EnterRule(_localctx, 26, RULE_pragma);
		int _la;
		try {
			State = 133;
			switch (TokenStream.La(1)) {
			case PragmaWarning:
				EnterOuterAlt(_localctx, 1);
				{
				State = 117; Match(PragmaWarning);
				State = 118; Match(WarningAction);
				State = 127;
				_la = TokenStream.La(1);
				if (_la==WarningNumber) {
					{
					State = 119; Match(WarningNumber);
					State = 124;
					ErrorHandler.Sync(this);
					_la = TokenStream.La(1);
					while (_la==Comma) {
						{
						{
						State = 120; Match(Comma);
						State = 121; Match(WarningNumber);
						}
						}
						State = 126;
						ErrorHandler.Sync(this);
						_la = TokenStream.La(1);
					}
					}
				}

				}
				break;
			case Pragma:
				EnterOuterAlt(_localctx, 2);
				{
				State = 129; Match(Pragma);
				State = 131;
				_la = TokenStream.La(1);
				if (_la==Message) {
					{
					State = 130; Match(Message);
					}
				}

				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public override bool Sempred(RuleContext _localctx, int ruleIndex, int predIndex) {
		switch (ruleIndex) {
		case 8: return expression_sempred((ExpressionContext)_localctx, predIndex);
		}
		return true;
	}
	private bool expression_sempred(ExpressionContext _localctx, int predIndex) {
		switch (predIndex) {
		case 0: return Precpred(Context, 6);
		case 1: return Precpred(Context, 5);
		case 2: return Precpred(Context, 4);
		}
		return true;
	}

	public static readonly string _serializedATN =
		"\x3\x430\xD6D1\x8206\xAD2D\x4417\xAEF1\x8D80\xAADD\x3\"\x8A\x4\x2\t\x2"+
		"\x4\x3\t\x3\x4\x4\t\x4\x4\x5\t\x5\x4\x6\t\x6\x4\a\t\a\x4\b\t\b\x4\t\t"+
		"\t\x4\n\t\n\x4\v\t\v\x4\f\t\f\x4\r\t\r\x4\xE\t\xE\x4\xF\t\xF\x3\x2\x3"+
		"\x2\x3\x2\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3"+
		"\x3\x3\x5\x3-\n\x3\x3\x4\x3\x4\x3\x4\x3\x4\x3\x5\x3\x5\x3\x5\x3\x5\x3"+
		"\x6\x3\x6\x3\x6\x3\x6\x3\a\x3\a\x3\a\x3\a\x3\b\x3\b\x3\b\x3\t\x3\t\x3"+
		"\t\x3\n\x3\n\x3\n\x3\n\x3\n\x3\n\x3\n\x3\n\x3\n\x5\nN\n\n\x3\n\x3\n\x3"+
		"\n\x3\n\x3\n\x3\n\x3\n\x3\n\x3\n\a\nY\n\n\f\n\xE\n\\\v\n\x3\v\x3\v\x3"+
		"\v\x3\v\x5\v\x62\n\v\x5\v\x64\n\v\x3\f\x3\f\x5\fh\n\f\x3\r\x3\r\x5\rl"+
		"\n\r\x3\xE\x3\xE\x5\xEp\n\xE\x3\xE\x3\xE\x5\xEt\n\xE\x5\xEv\n\xE\x3\xF"+
		"\x3\xF\x3\xF\x3\xF\x3\xF\a\xF}\n\xF\f\xF\xE\xF\x80\v\xF\x5\xF\x82\n\xF"+
		"\x3\xF\x3\xF\x5\xF\x86\n\xF\x5\xF\x88\n\xF\x3\xF\x2\x3\x12\x10\x2\x4\x6"+
		"\b\n\f\xE\x10\x12\x14\x16\x18\x1A\x1C\x2\x3\x3\x2\x16\x17\x96\x2\x1E\x3"+
		"\x2\x2\x2\x4,\x3\x2\x2\x2\x6.\x3\x2\x2\x2\b\x32\x3\x2\x2\x2\n\x36\x3\x2"+
		"\x2\x2\f:\x3\x2\x2\x2\xE>\x3\x2\x2\x2\x10\x41\x3\x2\x2\x2\x12M\x3\x2\x2"+
		"\x2\x14\x63\x3\x2\x2\x2\x16\x65\x3\x2\x2\x2\x18i\x3\x2\x2\x2\x1Au\x3\x2"+
		"\x2\x2\x1C\x87\x3\x2\x2\x2\x1E\x1F\x5\x4\x3\x2\x1F \a\x2\x2\x3 \x3\x3"+
		"\x2\x2\x2!-\x5\x6\x4\x2\"-\x5\b\x5\x2#-\x5\n\x6\x2$-\x5\f\a\x2%-\x5\xE"+
		"\b\x2&-\x5\x10\t\x2\'-\x5\x14\v\x2(-\x5\x16\f\x2)-\x5\x18\r\x2*-\x5\x1A"+
		"\xE\x2+-\x5\x1C\xF\x2,!\x3\x2\x2\x2,\"\x3\x2\x2\x2,#\x3\x2\x2\x2,$\x3"+
		"\x2\x2\x2,%\x3\x2\x2\x2,&\x3\x2\x2\x2,\'\x3\x2\x2\x2,(\x3\x2\x2\x2,)\x3"+
		"\x2\x2\x2,*\x3\x2\x2\x2,+\x3\x2\x2\x2-\x5\x3\x2\x2\x2./\a\x4\x2\x2/\x30"+
		"\a\x12\x2\x2\x30\x31\b\x4\x1\x2\x31\a\x3\x2\x2\x2\x32\x33\a\x5\x2\x2\x33"+
		"\x34\a\x12\x2\x2\x34\x35\b\x5\x1\x2\x35\t\x3\x2\x2\x2\x36\x37\a\x6\x2"+
		"\x2\x37\x38\x5\x12\n\x2\x38\x39\b\x6\x1\x2\x39\v\x3\x2\x2\x2:;\a\a\x2"+
		"\x2;<\x5\x12\n\x2<=\b\a\x1\x2=\r\x3\x2\x2\x2>?\a\b\x2\x2?@\b\b\x1\x2@"+
		"\xF\x3\x2\x2\x2\x41\x42\a\t\x2\x2\x42\x43\b\t\x1\x2\x43\x11\x3\x2\x2\x2"+
		"\x44\x45\b\n\x1\x2\x45\x46\a\x15\x2\x2\x46N\x5\x12\n\tGH\a\x18\x2\x2H"+
		"I\x5\x12\n\x2IJ\a\x19\x2\x2JN\x3\x2\x2\x2KN\a\x12\x2\x2LN\a\x11\x2\x2"+
		"M\x44\x3\x2\x2\x2MG\x3\x2\x2\x2MK\x3\x2\x2\x2ML\x3\x2\x2\x2NZ\x3\x2\x2"+
		"\x2OP\f\b\x2\x2PQ\a\x13\x2\x2QY\x5\x12\n\tRS\f\a\x2\x2ST\a\x14\x2\x2T"+
		"Y\x5\x12\n\bUV\f\x6\x2\x2VW\t\x2\x2\x2WY\x5\x12\n\aXO\x3\x2\x2\x2XR\x3"+
		"\x2\x2\x2XU\x3\x2\x2\x2Y\\\x3\x2\x2\x2ZX\x3\x2\x2\x2Z[\x3\x2\x2\x2[\x13"+
		"\x3\x2\x2\x2\\Z\x3\x2\x2\x2]^\a\n\x2\x2^\x64\a\x1C\x2\x2_\x61\a\x1D\x2"+
		"\x2`\x62\a\x1E\x2\x2\x61`\x3\x2\x2\x2\x61\x62\x3\x2\x2\x2\x62\x64\x3\x2"+
		"\x2\x2\x63]\x3\x2\x2\x2\x63_\x3\x2\x2\x2\x64\x15\x3\x2\x2\x2\x65g\a\v"+
		"\x2\x2\x66h\a\"\x2\x2g\x66\x3\x2\x2\x2gh\x3\x2\x2\x2h\x17\x3\x2\x2\x2"+
		"ik\a\f\x2\x2jl\a\"\x2\x2kj\x3\x2\x2\x2kl\x3\x2\x2\x2l\x19\x3\x2\x2\x2"+
		"mo\a\r\x2\x2np\a\"\x2\x2on\x3\x2\x2\x2op\x3\x2\x2\x2pv\x3\x2\x2\x2qs\a"+
		"\xE\x2\x2rt\a\"\x2\x2sr\x3\x2\x2\x2st\x3\x2\x2\x2tv\x3\x2\x2\x2um\x3\x2"+
		"\x2\x2uq\x3\x2\x2\x2v\x1B\x3\x2\x2\x2wx\a\xF\x2\x2x\x81\a\x1F\x2\x2y~"+
		"\a \x2\x2z{\a!\x2\x2{}\a \x2\x2|z\x3\x2\x2\x2}\x80\x3\x2\x2\x2~|\x3\x2"+
		"\x2\x2~\x7F\x3\x2\x2\x2\x7F\x82\x3\x2\x2\x2\x80~\x3\x2\x2\x2\x81y\x3\x2"+
		"\x2\x2\x81\x82\x3\x2\x2\x2\x82\x88\x3\x2\x2\x2\x83\x85\a\x10\x2\x2\x84"+
		"\x86\a\"\x2\x2\x85\x84\x3\x2\x2\x2\x85\x86\x3\x2\x2\x2\x86\x88\x3\x2\x2"+
		"\x2\x87w\x3\x2\x2\x2\x87\x83\x3\x2\x2\x2\x88\x1D\x3\x2\x2\x2\x11,MXZ\x61"+
		"\x63gkosu~\x81\x85\x87";
	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN.ToCharArray());
}
} // namespace Adamant.Compiler.Antlr
