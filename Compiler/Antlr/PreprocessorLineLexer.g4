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
Line : '#' Whitespace? 'line';
Error : '#' Whitespace? 'error' -> mode(MESSAGE);
Warning : '#' Whitespace? 'warning' -> mode(MESSAGE);
Region : '#' Whitespace? 'region' -> mode(MESSAGE);
EndRegion : '#' Whitespace? 'endregion' -> mode(MESSAGE);
PragmaWarning : '#' Whitespace? 'pragma' Whitespace 'warning' Whitespace;
Pragma : '#' Whitespace? 'pragma' -> mode(MESSAGE);

BooleanLiteral
	: 'true'
	| 'false'
	;

LineMode
	: 'default'
	| 'hidden'
	;

WarningAction
	: 'disable'
	| 'restore'
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

Number
	: DecimalDigit+
	;

FileName
	: '"' FileNameChar+ '"'
	;

// Any input_character Except "
fragment FileNameChar
	: ~["]
	;

Comma : ',';

mode MESSAGE;

Message
	: InputChar+
	;

