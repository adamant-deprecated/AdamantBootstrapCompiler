lexer grammar AdamantLexer;

import AdamantCommon; // defines Whitespace, Newline and InputChar

channels {DocComments, Preprocessor}

options
{
	language=CSharp;
}

//*************
// Comments
//*************
SingleLineDocComment
	: '///' InputChar* -> channel(DocComments)
	;

BlockDocComment
	: '/**' .*? '*/' -> channel(DocComments)
	;

SingleLineComment
	: '//' InputChar* -> skip
	;

BlockComment
	: '/*' .*? '*/' -> skip
	;

//*************
// Preprocessor
//*************
PreprocessorLine
	: Whitespace* '#' InputChar* { Preprocess(); } -> skip
	;

// Here we use a mode to handle preprocessor sections that are skipped
mode PREPROCESSOR_SKIP;

PreprocessorSkippedSection
	: InputChar+? Whitespace* '#' 
	;

PreprocessorLineInSkipped
	// the type here prevents it from creatig another token type
	: Whitespace* '#' InputChar* { Preprocess(); } -> type(PreprocessorLine), skip 
	;

// And switch back to default mode so rest of file is correct
//mode DEFAULT_MODE;

//*************
// Keywords
//*************
Using : 'using';
Namespace : 'namespace';

//*************
// Literals
//*************
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

// Operators
Semicolon : ';';
Dot : '.';
LeftBrace : '{';
RightBrace : '}';

fragment BooleanLiteral
	: True
	| False
	;
