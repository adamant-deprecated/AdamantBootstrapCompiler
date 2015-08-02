parser grammar PreprocessorLineParser;

options
{
	language=CSharp;
	tokenVocab=PreprocessorLineLexer;
}

/*
	This grammar is applied to individual preprocessor lines recieved from the main lexer in order
	to correctly parse and interpret them
*/

// Note:
// * Doesn't enforce a preprocessor directive must not have anything on the line before it except whitespace
// * Doesn't enforce that there should be nothing on the line after it except a single line comment (comment is not captured in directive)

// Start symbol
preprocessorLine : directive EOF;

directive
	: define
	| undefine
	| ifDecl
	| elseif
	| elseDecl
	| endif
	| line
	| error
	| warning
	| region
	| pragma
	;

// Declaration Directives
define
	: Define ConditionalSymbol { PreprocessorDefine(); }
	;

undefine
	: Undefine ConditionalSymbol { PreprocessorUndefine(); }
	;

// Conditional Directives
ifDecl // can't use if
	: If expression { PreprocessorIf(); }
	;

elseif
	: ElseIf expression { PreprocessorElseIf(); }
	;

elseDecl // can't use else
	: Else { PreprocessorElse(); }
	;

endif
	: EndIf { PreprocessorEndif(); }
	;

expression
	: '!' expression
	| expression '&&' expression
	| expression '||' expression
	| expression ('=='|'!=') expression
	| '(' expression ')'
	| ConditionalSymbol
	| BooleanLiteral
	;

// Line directive
line
	: Line LineMode | (LineNumber FileName?)
	;

// Diagnostic Directives
error
	: Error Message?
	;

warning
	: Warning Message?
	;

// Region Directives
region
	: Region Message?
	| EndRegion Message?
	;

pragma
	: PragmaWarning WarningAction (WarningNumber (',' WarningNumber)*)?
	| Pragma Message?
	;
