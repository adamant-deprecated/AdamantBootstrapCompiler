lexer grammar AdamantLexer;

import Unicode;

channels {Comments, DocComments}

SingleLineDocComment
	: '///' InputChar* -> channel(DocComments) // TODO contents
	;

BlockDocComment
	: '/**' .*? '*/' -> channel(DocComments) // TODO contents
	;

SingleLineComment
	: '//' InputChar* -> channel(Comments) // TODO contents
	;

BlockComment
	: '/*' .*? '*/' -> channel(Comments) // TODO contents
	;

fragment InputChar
	: ~[\u000D\u000A\u0085\u2028\u2029] // any char except a new line char
	;

NewLine
	: ('\u000D' // Carriage Return (U+000D)
	| '\u000A' // Line Feed (U+000A)
	| '\u000D' '\u000A' // Carriage Return (U+000D) Followed By Line Feed Character (U+000A)
	| '\u0085' // Next Line (U+0085)
	| '\u2028' // Line Separator (U+2028)
	| '\u2029') -> skip // Paragraph Separator (U+2029)
	;

Whitespace
	: WhitespaceChar+ -> skip
	;

fragment WhitespaceChar
	: Unicode_Zs // Any Character With Unicode Class Zs
	| '\u0009' // Horizontal Tab Character (U+0009)
	| '\u000B' // Vertical Tab Character (U+000B)'
	| '\u000C' // Form Feed Character (U+000C)
	;

// Keywords
Using : 'using';
Namespace : 'namespace';


// Boolean literals
True : 'true';
False : 'false';

// must be defined after all keywords so it will not match a keyword
Identifier
	: IdentifierStartChar IdentifierPartChar*
	;

EscapedIdentifier
	: '@' Identifier
	// | '@' Keyword // TODO is this needed?
	;

fragment IdentifierStartChar
	: LetterChar
	| '_'
	| UnicodeEscape
	;

// Operators
Semicolon : ';';
Dot : '.';
LeftBrace : '{';
RightBrace : '}';

fragment IdentifierPartChar
	: LetterChar
	| DigitChar
	| ConnectingChar
	| CombiningChar
	| FormattingChar
	| UnicodeEscape
	;

fragment LetterChar
	: Unicode_Lu
	| Unicode_Ll
	| Unicode_Lt
	| Unicode_Lm
	| Unicode_Lo
	| Unicode_Nl
	;

fragment DigitChar
	: Unicode_Nd
	;

fragment ConnectingChar
	: Unicode_Ll
	;

fragment CombiningChar
	: Unicode_Mn
	| Unicode_Mc
	;

fragment FormattingChar
	: Unicode_Cf
	;

fragment UnicodeEscape
	: '\\u' HexDigit HexDigit HexDigit HexDigit
	| '\\u(' HexDigit ')'
	| '\\u(' HexDigit HexDigit')'
	| '\\u(' HexDigit HexDigit HexDigit')'
	| '\\u(' HexDigit HexDigit HexDigit HexDigit')'
	| '\\u(' HexDigit HexDigit HexDigit HexDigit HexDigit')'
	| '\\u(' HexDigit HexDigit HexDigit HexDigit HexDigit HexDigit')'
	;

fragment HexDigit
	: [0-9a-fA-F]
	;

fragment DecimalDigit
	: [0-9]
	;

fragment BooleanLiteral
	: True
	| False
	;
