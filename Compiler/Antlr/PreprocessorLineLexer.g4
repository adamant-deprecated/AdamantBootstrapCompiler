lexer grammar PreprocessorLineLexer;

import AdamantCommon; // defines Whitespace, Newline and InputChar

options
{
	language=CSharp;
}

SingleLineComment
	: '//' InputChar* -> skip
	;

// Directives
Define : '#' Whitespace? 'define';
Undefine : '#' Whitespace? 'undefine';
If : '#' Whitespace? 'if';
ElseIf : '#' Whitespace? 'elseif';
Else : '#' Whitespace? 'else';
EndIf : '#' Whitespace? 'endif';
Line : '#' Whitespace? 'line' -> mode(LINE);
Error : '#' Whitespace? 'error' -> mode(MESSAGE);
Warning : '#' Whitespace? 'warning' -> mode(MESSAGE);
Region : '#' Whitespace? 'region' -> mode(MESSAGE);
EndRegion : '#' Whitespace? 'endregion' -> mode(MESSAGE);
PragmaWarning : '#' Whitespace? 'pragma' Whitespace 'warning' Whitespace -> mode(PRAGMA_WARNING);
Pragma : '#' Whitespace? 'pragma' -> mode(MESSAGE);

BooleanLiteral
	: 'true'
	| 'false'
	;

// Defined after true and false so they are not included as conditional symbols
ConditionalSymbol
	: IdentifierStartChar IdentifierPartChar*
	;

// Operators
And : '&&';
Or : '||';
Not : '!';
Equal : '==';
NotEqual : '!=';
LeftParen : '(';
RightParen : ')';

mode LINE; // TODO eliminate this mode

LINE_Whitespace
	: Whitespace -> type(Whitespace), skip
	;

LINE_Comment
	: SingleLineComment -> type(SingleLineComment), skip
	;

LineMode
	: 'default'
	| 'hidden'
	;

LineNumber
	: DecimalDigit+
	;

FileName
	: '"' FileNameChar+ '"'
	;

// Any input_character Except "
fragment FileNameChar
	: ~["]
	;

mode PRAGMA_WARNING; // TODO eliminate this mode

PRAGMA_WARNING_Whitespace
	: Whitespace -> type(Whitespace), skip
	;

PRAGMA_WARNING_Comment
	: SingleLineComment -> type(SingleLineComment), skip
	;

WarningAction
	: 'disable'
	| 'restore'
	;

WarningNumber : DecimalDigit+;

Comma : ',';

mode MESSAGE;

Message
	: InputChar+
	;

