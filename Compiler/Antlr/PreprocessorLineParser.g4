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
	: Define conditionalSymbol
	;

undefine
	: Undefine conditionalSymbol
	;

conditionalSymbol
	: ConditionalSymbol
	| LineMode
	| WarningAction
	;

// Conditional Directives
ifDecl // can't use if
	: If expression
	;

elseif
	: ElseIf expression
	;

elseDecl // can't use else
	: Else
	;

endif
	: EndIf
	;

expression
	: '!' expression							#Not
	| expression '&&' expression				#And
	| expression '||' expression				#Or
	| expression op=('=='|'!=') expression		#Equality
	| '(' expression ')'						#Grouping
	| conditionalSymbol							#Variable
	| BooleanLiteral							#BooleanValue
	;

// Line Directive
line
	: Line LineMode | (Number FileName?)
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
	: Region Message?							#RegionBegin
	| EndRegion Message?						#RegionEnd
	;

// Pragma Directives
pragma
	: PragmaWarning WarningAction (warningNumbers+=Number (',' warningNumbers+=Number)*)?	#PragmaWarning
	| Pragma Message?										#PragmaUnknown
	;
