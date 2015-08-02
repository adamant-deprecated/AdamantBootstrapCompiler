lexer grammar AdamantPreprocessor;

import AdamantLexer;

channels { Preprocessor }

// Note:
// * Doesn't enforce a preprocessor directive must not have anything on the line before it except whitespace
// * Doesn't enforce that there should be nothing on the line after it except a single line comment (comment is not captured in directive)

// -------------------------
// Preprocessor Directives
// -------------------------

// Declaration Directives
PP_Define
	: '#define' Whitespace PP_ConditionalSymbol PP_RestOfLine { PreprocessorDefine(); } -> channel(Preprocessor)
	;

PP_Undefine
	: '#undefine' Whitespace PP_ConditionalSymbol PP_RestOfLine { PreprocessorUndefine(); } -> channel(Preprocessor)
	;

// Conditional Directives
PP_If
	: '#if' Whitespace PP_Expression PP_RestOfLine { PreprocessorIf(); } -> channel(Preprocessor)
	;

PP_Elseif
	:'#elseif' Whitespace PP_Expression PP_RestOfLine { PreprocessorElseIf(); } -> channel(Preprocessor)
	;

PP_Else
	: '#else' PP_RestOfLine { PreprocessorElse(); } -> channel(Preprocessor)
	;

PP_Endif
	: '#endif' PP_RestOfLine { PreprocessorEndif(); }-> channel(Preprocessor)
	;

// Line directive
PP_Line
	: '#line' Whitespace LineIndicator PP_RestOfLine -> channel(Preprocessor)
	;

 // Diagnostic Directives
PP_Error
	: '#error' Whitespace PP_Message -> channel(Preprocessor)
	;

PP_Warning
	: '#warning' Whitespace PP_Message -> channel(Preprocessor)
	;

// Region Directives
PP_StartRegion
	: '#region' Whitespace PP_Message -> channel(Preprocessor)
	;

PP_EndRegion
	: '#endregion' Whitespace PP_Message -> channel(Preprocessor)
	;

PP_Invalid
	: '#' InputChar* -> channel(Preprocessor)
	;
// -------------------------
// Fragments used by Preprocessor Directives
// -------------------------

fragment PP_ConditionalSymbol
	: Identifier
	// TODO keyword except true/false
	;

fragment PP_RestOfLine
	: (PP_RestOfLineChar | '/'PP_RestOfLineChar)* // TODO this doesn't match a single / at end of line
	;

fragment PP_RestOfLineChar
	// The character class here is any char except newline or '/'
	: ~[/\u000D\u000A\u0085\u2028\u2029]
	;

fragment PP_Expression
	: PP_ExpressionPrimary Whitespace? (Whitespace? ('||'|'&&'|'=='|'!=') Whitespace? PP_ExpressionPrimary)*
	| '!' Whitespace? PP_Expression
	;

fragment PP_ExpressionPrimary
	: PP_ConditionalSymbol
	| BooleanLiteral
	| '(' Whitespace? PP_Expression Whitespace? ')'
	;

fragment LineIndicator
	: DecimalDigit+ (Whitespace FileName)?
	| 'default'
	| 'hidden'
	;

fragment FileName
	: '"' FileNameChar+ '"'
	;

// Any input_character Except "
fragment FileNameChar
	: ~["]
	;

fragment PP_Message
	: InputChar*
	;